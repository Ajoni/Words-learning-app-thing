using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Words_learning_app_thing.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Words_learning_app_thing.Logic.Interfaces;
using Words_learning_app_thing.Helpers;
using Words_learning_app_thing.Logic;
using Words_learning_app_thing.Data;
using Words_learning_app_thing.Models;

namespace Words_learning_app_thing.Controllers
{
    [Authorize]
    public class SessionController : Controller
    {
        IUOW _uow;
        IDirector _director;
        private ApplicationUserManager _userManager;

        private const string sessionKey = "session";
        private const string iteratorKey = "iterator";
        private const string currentQuestionKey = "question_number";
        private const string questionCountKey = "question_count";

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public SessionController(IUOW iUOW, IDirector director)
        {
            _uow = iUOW;
            _director = director;
        }

        // GET: Session
        public ActionResult Index()
        {
            // If session exists, redirect to Solve
            if (Session[sessionKey] != null)
            {
                return RedirectToAction("Solve");
            }
            // Otherwise, offer a choice to create one
            UserChoicesViewModel model = GetSessionViewModel();
            return View(model);
        }

        private UserChoicesViewModel GetSessionViewModel()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var model = new UserChoicesViewModel()
            {
                Jezyki = GetJezyki()
            };
            if (user.PrefferedMode == TrybWybieraniaSesji.Automatyczny)
            {
                model.CanEditLevel = false;
                model.SetLevel(user.Progress);
            }
            else
            {
                model.CanEditLevel = true;
            }

            return model;
        }

        // POST: Session
        [HttpPost]
        public ActionResult Index(UserChoicesViewModel model)
        {
            if (model.UczonyJezykId == model.ZnanyJezykId)
            {
                var newModel = GetSessionViewModel();
                newModel.ZnanyJezykId = model.ZnanyJezykId;
                newModel.UczonyJezykId = model.UczonyJezykId;

                ModelState.AddModelError("JezykUczony", "Język który znasz i którego się uczysz muszą być różne");
                return View(newModel);
            }

            if (ModelState.IsValid)
            {
                model.UczonyJezyk = _uow.JezykRepo.Get(model.UczonyJezykId);
                model.ZnanyJezyk = _uow.JezykRepo.Get(model.ZnanyJezykId);

                IBudowniczySesji budowniczySesji;
                switch (model.WybranyInput)
                {
                    case RodzajInputu.Select:
                        budowniczySesji = new BudowniczySesjiZWyborem(_uow.SlowoRepo);
                        break;
                    case RodzajInputu.TextInput:
                        budowniczySesji = new BudowniczySesjiZInputem(_uow.SlowoRepo);
                        break;
                    default:
                        throw new ArgumentException($"Brak ustalonego budowniczego dla {model.WybranyInput}");
                }
                budowniczySesji.Reset();
                _director.Akceptuj(budowniczySesji);
                _director.Stworz(model);
                Sesja sesja = budowniczySesji.BudowanaSesja;
                sesja.Uzytkownik = _uow.UserRepo.Get(User.Identity.GetUserId());

                Session[sessionKey] = sesja;
                Session[iteratorKey] = sesja.GetIterator();
                Session[currentQuestionKey] = 1;
                Session[questionCountKey] = sesja.Pytania.Count;

                return RedirectToAction("Solve");
            }
            return View(model);
        }

        // Displays the question currently pointed to by the iterator
        // GET: Session/Solve
        public ActionResult Solve()
        {            
            ISessionIterator iterator = (ISessionIterator)Session[iteratorKey];
            Sesja sesja = (Sesja)Session[sessionKey];
            Pytanie pytanie = iterator.GetCurrent();
            var model = pytanie.getViewModel();
            model.NumerPytania = (int)Session[currentQuestionKey];
            model.LiczbaPytań = (int)Session[questionCountKey];
            model.JestPierwszymPytaniem = !iterator.HasPrev();
            model.JestOstatnimPytaniem = !iterator.HasNext();
            model.MozeZakonczyc = (sesja.TypSesji == TypSesji.Nauka && model.JestOstatnimPytaniem && pytanie.CzyOdpowiedzianoPoprawnie())
                                || (sesja.TypSesji == TypSesji.Test);
            return View(model);
        }

        // This results in saving the answer.
        [HttpPost]        
        public ActionResult Solve(string OdpowiedzUzytkownika)
        {
            ISessionIterator iterator = (ISessionIterator)Session[iteratorKey];
            Pytanie pytanie = iterator.GetCurrent();
            pytanie.OdpowiedzUzytkownika = OdpowiedzUzytkownika;
            return RedirectToAction("Solve");
        }

        // POST: Session/Next
        [HttpPost]
        public ActionResult Next(string OdpowiedzUzytkownika)
        {
            ISessionIterator iterator = (ISessionIterator)Session[iteratorKey];
            Sesja sesja = (Sesja)Session[sessionKey];
            Pytanie pytanie = iterator.GetCurrent();
            pytanie.OdpowiedzUzytkownika = OdpowiedzUzytkownika;
            if (iterator.HasNext())
            {
                iterator.Next();
                if ((pytanie.CzyOdpowiedzianoPoprawnie() && sesja.TypSesji == TypSesji.Nauka) || sesja.TypSesji == TypSesji.Test)
                    Session[currentQuestionKey] = (int)Session[currentQuestionKey] + 1;
            }
            return RedirectToAction("Solve");
        }

        // POST: Session/Previous
        [HttpPost]
        public ActionResult Previous(string OdpowiedzUzytkownika)
        {
            ISessionIterator iterator = (ISessionIterator)Session[iteratorKey];
            Pytanie pytanie = iterator.GetCurrent();
            pytanie.OdpowiedzUzytkownika = OdpowiedzUzytkownika;
            if (iterator.HasPrev())
            {
                iterator.Prev();
                Session[currentQuestionKey] = (int)Session[currentQuestionKey] - 1;
            }
            return RedirectToAction("Solve");
        }

        public ActionResult Cancel()
        {
            Session[iteratorKey] = null;
            Session[sessionKey] = null;
            Session[currentQuestionKey] = null;
            Session[questionCountKey] = null;
            return RedirectToAction("Index");
        }

        // GET: Session/Finish
        public ActionResult Finish()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            Sesja sesja = (Sesja)Session[sessionKey];
            if (sesja.TypSesji == TypSesji.Nauka)
            {
                user.Progress++;
                UserManager.Update(user);
            }
            var model = new FinishViewModel()
            {
                Pytania = sesja.Pytania,
                TypSesji = sesja.TypSesji
            };

            // Cleanup
            Session[iteratorKey] = null;
            Session[sessionKey] = null;
            Session[currentQuestionKey] = null;
            Session[questionCountKey] = null;
            return View(model);
        }

        private IEnumerable<SelectListItem> GetJezyki()
        {
            var jezyki = _uow.JezykRepo.GetAll()
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Nazwa
                                });

            return new SelectList(jezyki, "Value", "Text");
        }
    }
}