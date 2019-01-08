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

                return RedirectToAction("Solve");
            }
            return View(model);
        }

        // Displays the question currently pointed to by the iterator
        // GET: Session/Solve
        public ActionResult Solve()
        {            
            ISessionIterator iterator = (ISessionIterator)Session[iteratorKey];
            Pytanie pytanie = iterator.GetCurrent();
            var model = pytanie.getViewModel();
            return View(model);
        }

        // This results in saving the answer
        [HttpPost]        
        public ActionResult Solve(string OdpowiedzUzytkownika)
        {
            ISessionIterator iterator = (ISessionIterator)Session[iteratorKey];
            Pytanie pytanie = iterator.GetCurrent();
            pytanie.OdpowiedzUzytkownika = OdpowiedzUzytkownika;
            return View(pytanie.getViewModel());
        }

        // POST: Session/Next
        [HttpPost]
        public ActionResult Next(string OdpowiedzUzytkownika)
        {
            ISessionIterator iterator = (ISessionIterator)Session[iteratorKey];
            Pytanie pytanie = iterator.GetCurrent();
            pytanie.OdpowiedzUzytkownika = OdpowiedzUzytkownika;
            if (iterator.HasNext())
            {
                iterator.Next();
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
            }
            return RedirectToAction("Solve");
        }

        public ActionResult Cancel()
        {
            Session[iteratorKey] = null;
            Session[sessionKey] = null;
            return RedirectToAction("Index");
        }

        // GET: Session/Finish
        public ActionResult Finish()
        {
            // TODO: Some sort of session summary
            Session[iteratorKey] = null;
            Session[sessionKey] = null;

            return View();
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