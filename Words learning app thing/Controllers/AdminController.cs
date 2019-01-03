using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Words_learning_app_thing.Data;
using Words_learning_app_thing.Logic.Interfaces;
using Words_learning_app_thing.Models;
using Words_learning_app_thing.ViewModels;

namespace Words_learning_app_thing.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IUOW UOW;
        
        public AdminController(IUOW _uow, IDirector director)
        {
            UOW = _uow;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // Lists all available Words
        // GET: Admin/Words
        public ActionResult Words()
        {
            return View(UOW.SlowoRepo.GetAll());
        }

        // GET:Admin/CreateWord
        public ActionResult CreateWord()
        {
            var model = new CreateSlowoViewModel()
            {
                Jezyki = GetJezyki()
            };
            return View(model);
        }

        // POST:Admin/CreateWord
        [HttpPost]
        public ActionResult CreateWord(CreateSlowoViewModel model)
        {
            Slowo slowo = new Slowo()
            {
                Zawartosc = model.Slowo,
                Jezyk = UOW.JezykRepo.Get(model.Jezyk)
            };
            UOW.SlowoRepo.Add(slowo);
            UOW.SlowoRepo.Save();
            return RedirectToAction("Words");
        }

        // GET:Admin/EditWord/{id}
        public ActionResult EditWord(int id)
        {
            return View(UOW.SlowoRepo.Get(id));
        }

        // GET:Admin/DetailsWord/{id}
        public ActionResult DetailsWord(int id)
        {
            return View(UOW.SlowoRepo.Get(id));
        }

        // GET:Admin/DeleteWord/{id}
        public ActionResult DeleteWord(int id)
        {
            return View(UOW.SlowoRepo.Get(id));
        }

        // Lists all available Languages
        // GET: Admin/Languages 
        public ActionResult Languages()
        {
            return View(UOW.JezykRepo.GetAll());
        }

        private IEnumerable<SelectListItem> GetJezyki()
        {
            var jezyki = UOW.JezykRepo.GetAll()
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