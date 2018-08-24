using Shop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class AudiobooksController : Controller
    {
        private AudiobooksContext db = new AudiobooksContext();

        // GET: Audiobooks
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string categoryName, string searchQuery=null)
        {
            var categories = db.Categories.Include("Audiobooks").Where(k => k.CategoryName.ToUpper() == categoryName.ToUpper()).Single();



            var Audiobooks = categories.Audiobooks.Where(a => (searchQuery == null ||
                                             a.NameAudiobook.ToLower().Contains(searchQuery.ToLower()))  &&
                                             !a.LackAudiobook);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_AudiobooksList", Audiobooks);
            }
           //  var Audiobooks = categories.Audiobooks.ToList();  // bez filtrowania
            return View(Audiobooks);
        }
        public ActionResult Details(int id)
        {
            var audiobook = db.Audiobooks.Find(id);
            return View(audiobook);
        }

       [ChildActionOnly]
       [OutputCache(Duration = 60000)]
        public ActionResult CategoriesMenu()
        {
            var categories = db.Categories.ToList(); 
            return PartialView("_CategoriesMenu", categories);
        }

        //public ActionResult AudiobooksSuggestions(string temp)
        //{
        //    var Audiobooks = db.Audiobooks.Where(a => !a.LackPhone && a.NamePhone.ToLower().Contains(temp.ToLower()))
        //        .Take(5).Select(a=>new { label = a.NamePhone});
        //    return Json(Audiobooks, JsonRequestBehavior.AllowGet);
        //}

    }
}