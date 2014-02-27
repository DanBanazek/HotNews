using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BestCaseHotNews.Models;
using BestCaseHotNews.DAL;

namespace BestCaseHotNews.Controllers
{
    public class NewsController : Controller
    {
        private HotNewsContext db = new HotNewsContext();

        //
        // GET: /News/

        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.Product).Include(p => p.Category).Include(p => p.User);
            return View(posts.ToList());
        }

        //
        // GET: /News/Details/5

        public ActionResult Details(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //
        // GET: /News/Create
        [Authorize]
        public ActionResult Create()
        {
             
            ViewBag.productID = new SelectList(db.Products, "productID", "productName");
            ViewBag.categoryID = new SelectList(db.Categories, "categoryID", "categoryName");
            ViewBag.userID = new SelectList(db.Users, "userID", "userName");
            return View();
        }

        //
        // POST: /News/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(Post post)
        {
            post.userID = (from u in db.Users where u.userName == post.userName select u.userID).FirstOrDefault();
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productID = new SelectList(db.Products, "productID", "productName", post.productID);
            ViewBag.categoryID = new SelectList(db.Categories, "categoryID", "categoryName", post.categoryID);
            ViewBag.userID = new SelectList(db.Users, "userID", "userName", post.userID);
            return View(post);
        }

        //
        // GET: /News/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.productID = new SelectList(db.Products, "productID", "productName", post.productID);
            ViewBag.categoryID = new SelectList(db.Categories, "categoryID", "categoryName", post.categoryID);
            ViewBag.userID = new SelectList(db.Users, "userID", "userName", post.userID);
            return View(post);
        }

        //
        // POST: /News/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            post.userID = db.Users.Where(u => u.userName == post.userName).FirstOrDefault().userID;
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productID = new SelectList(db.Products, "productID", "productName", post.productID);
            ViewBag.categoryID = new SelectList(db.Categories, "categoryID", "categoryName", post.categoryID);
            ViewBag.userID = new SelectList(db.Users, "userID", "userName", post.userID);
            return View(post);
        }

        //
        // GET: /News/Delete/5
        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //
        // POST: /News/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}