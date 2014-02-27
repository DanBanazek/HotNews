using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BestCaseHotNews.DAL;

namespace BestCaseHotNews.Controllers
{
    public class HomeController : Controller
    {
        private HotNewsContext db = new HotNewsContext();

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to the new Best Case Hot News!";
            var posts = db.Posts.OrderByDescending(p=>p.lastUpdate);
            return View(posts.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
