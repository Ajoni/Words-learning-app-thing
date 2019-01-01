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
            return View(model);
        }

        // POST: Session
        [HttpPost]
        public ActionResult Index(UserChoicesViewModel model)
        {
            IBudowniczySesji budowniczySesji;
            switch (model.WybranyInput)
            {
                case RodzajInputu.Select:
                    budowniczySesji = new BudowniczySesjiZWyborem();
                    break;
                case RodzajInputu.TextInput:
                    budowniczySesji = new BudowniczySesjiZInputem();
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
            return View();
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

        // GET: Session/Next
        public ActionResult Next()
        {
            ISessionIterator iterator = (ISessionIterator)Session[iteratorKey];
            if (iterator.HasNext())
            {
                iterator.Next();
            }
            return RedirectToAction("Solve");
        }

        // GET: Session/Previous
        public ActionResult Previous()
        {
            ISessionIterator iterator = (ISessionIterator)Session[iteratorKey];
            if (iterator.HasPrev())
            {
                iterator.Prev();
            }
            return RedirectToAction("Solve");
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