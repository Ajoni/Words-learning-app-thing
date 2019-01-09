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

        // Lists all available Words from a given language
        // GET: Admin/Words?languageId={languageId}
        public ActionResult Words(int languageId)
        {
            var model = new WordsViewModel()
            {
                Words = UOW.SlowoRepo.GetAll(languageId),
                LanguageId = languageId
            };
            return View(model);
        }

        // GET:Admin/CreateWord?languageId={languageId}
        public ActionResult CreateWord(int languageId)
        {
            var model = new CreateSlowoViewModel()
            {
                JezykId = languageId,
                Jezyk = UOW.JezykRepo.Get(languageId)
            };
            return View(model);
        }

        // POST:Admin/CreateWord?languageId={languageId}
        [HttpPost]
        public ActionResult CreateWord(CreateSlowoViewModel model)
        {
            Slowo slowo = new Slowo()
            {
                Zawartosc = model.Slowo,
                Jezyk = UOW.JezykRepo.Get(model.JezykId)
            };
            UOW.SlowoRepo.Add(slowo);
            UOW.SaveChanges();
            return RedirectToAction("Words", new { languageId = model.JezykId });
        }

        // GET:Admin/EditWord/{id}
        public ActionResult EditWord(int id)
        {
            return View(UOW.SlowoRepo.Get(id));
        }

        // POST:Admin/EditWord/{id}
        [HttpPost]
        public ActionResult EditWord(Slowo model)
        {
            Slowo slowo = UOW.SlowoRepo.Get(model.Id);
            slowo.Zawartosc = model.Zawartosc;
            UOW.SaveChanges();
            return RedirectToAction("Words");
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

        [HttpPost]
        public ActionResult DeleteWord(Slowo slowo)
        {
            Slowo toRemove = UOW.SlowoRepo.Get(slowo.Id);
            UOW.SlowoRepo.Remove(toRemove);
            UOW.SaveChanges();
            return RedirectToAction("Words");
        }

        // GET:Admin/WordAddTranslation/{id}
        public ActionResult WordAddTranslation(int id)
        {
            Slowo slowo = UOW.SlowoRepo.Get(id);

            // Get all possible words
            var model = new WordAddTranslationViewModel()
            {
                slowo = slowo,
                PossibleWords = UOW.SlowoRepo.GetAll()
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Zawartosc
                                }),
                thisWordId = slowo.Id
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult WordAddTranslation(WordAddTranslationViewModel model)
        {
            Slowo slowo = UOW.SlowoRepo.Get(model.thisWordId);
            Slowo tlumaczenie = UOW.SlowoRepo.Get(model.translationId);

            slowo.Tlumaczenia.Add(tlumaczenie);
            tlumaczenie.Tlumaczenia.Add(slowo);
            UOW.SaveChanges();

            return RedirectToAction("EditWord", new { Id = model.thisWordId });
        }

        public ActionResult RemoveWordTranslation(int wordId, int translationId)
        {
            Slowo slowo = UOW.SlowoRepo.Get(wordId);
            Slowo tlumaczenie = UOW.SlowoRepo.Get(translationId);

            slowo.Tlumaczenia.Remove(tlumaczenie);
            tlumaczenie.Tlumaczenia.Remove(slowo);

            UOW.SaveChanges();
            return RedirectToAction("EditWord", new { Id = wordId });
        }

        // Lists all available Languages
        // GET: Admin/Languages 
        public ActionResult Languages()
        {
            return View(UOW.JezykRepo.GetAll());
        }

        // GET:Admin/CreateLanguage
        public ActionResult CreateLanguage()
        {
            return View();
        }

        // POST:Admin/CreateLanguage
        [HttpPost]
        public ActionResult CreateLanguage(Jezyk model)
        {
            UOW.JezykRepo.Add(model);
            UOW.SaveChanges();
            return RedirectToAction("Languages");
        }

        // GET:Admin/EditLanguage/{id}
        public ActionResult EditLanguage(int id)
        {
            return View(UOW.JezykRepo.Get(id));
        }

        // POST:Admin/EditLanguage/{id}
        [HttpPost]
        public ActionResult EditLanguage(Jezyk model)
        {
            Jezyk jezyk = UOW.JezykRepo.Get(model.Id);
            jezyk.Nazwa = model.Nazwa;
            UOW.SaveChanges();
            return RedirectToAction("Languages");
        }

        // GET:Admin/DetailsLanguage/{id}
        public ActionResult DetailsLanguage(int id)
        {
            return View(UOW.JezykRepo.Get(id));
        }

        // GET:Admin/DeleteLanguage/{id}
        public ActionResult DeleteLanguage(int id)
        {
            return View(UOW.JezykRepo.Get(id));
        }

        // POST:Admin/DeleteLanguage/{id}
        [HttpPost]
        public ActionResult DeleteLanguage(Jezyk jezyk)
        {
            Jezyk toRemove = UOW.JezykRepo.Get(jezyk.Id);
                List<Slowo> SlowaToRemove = UOW.SlowoRepo.GetAll(toRemove);
                foreach(var slowo in SlowaToRemove)
                {
                    UOW.SlowoRepo.Remove(slowo);
                }
            UOW.JezykRepo.Remove(toRemove);
            UOW.SaveChanges();
            return RedirectToAction("Languages");
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