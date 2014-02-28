using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BestCaseHotNews.Models;
using BestCaseHotNews.DAL;
using System.Web.Security;
using WebMatrix.WebData;

namespace BestCaseHotNews.Controllers
{
    [Authorize(Roles="Admin")]
    public class UsersController : Controller
    {
        private HotNewsContext db = new HotNewsContext();
        
        //
        // GET: /Users/

        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        //
        // GET: /Users/Details/5

        public ActionResult Details(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // GET: /Users/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Users/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        //
        // GET: /Users/Edit/5

        public ActionResult Edit(int id = 0)
        {
            User user = db.Users.Find(id);
            user.isSiteAdmin = CheckIsAdmin(user.userName);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        private bool CheckIsAdmin(string userName)
        {

            if (!WebSecurity.Initialized) { WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "userID", "userName", true); }
            if (!Roles.RoleExists("Admin"))
                return false;
            if (Roles.IsUserInRole(userName, "Admin"))
                return true;
            else
                return false;
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                if (user.isSiteAdmin)
                {
                    if (!WebSecurity.Initialized) { WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "userID", "userName", true); }
                    var roles = Roles.GetAllRoles();
                    //Note that the Admin Role should be added in the seed method
                    //Also if the user is alreadyin the role an exception will be thrown
                    if (!Roles.RoleExists("Admin")) 
                    {
                        Roles.CreateRole("Admin");
                      
                    }
                    if(!Roles.IsUserInRole(user.userName, "Admin"))
                        Roles.AddUserToRole(user.userName, "Admin");
                }
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /Users/Delete/5

        public ActionResult Delete(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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