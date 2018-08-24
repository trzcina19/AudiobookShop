using MvcSiteMapProvider.Caching;
using Shop.DAL;
using Shop.Infrastructure;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private AudiobooksContext db = new AudiobooksContext();
        public ActionResult Index()
        {


            ICacheProvider cache = new DefaultCacheProvider();

            //Categories
            List<Category> categories;
            if (cache.IsSet(Consts.CategoriesCacheKey))
            {
                categories = cache.Get(Consts.CategoriesCacheKey) as List<Category>;
            }
            else
            {
                categories = db.Categories.ToList();
                cache.Set(Consts.CategoriesCacheKey, categories, 60);
            }

            // News
            var news = db.Audiobooks.Where(a => !a.LackAudiobook).OrderByDescending(a => a.DateAdded).Take(3).ToList();


            // Bestseller
            var bestseller = db.Audiobooks.Where(a => !a.LackAudiobook && a.Bestseller).OrderBy(a => Guid.NewGuid()).Take(3).ToList();


            var vm = new HomeViewModel()
            {
                Categories = categories,
                News = news,
                Bestsellers = bestseller,
            };
            return View(vm);
        }

        public ActionResult StaticPages(string name)
        {
            return View(name);
        }
    }
}