using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BestCaseHotNews.Models;
using BestCaseHotNews.DAL;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
namespace BestCaseHotNews.Controllers
{
    public class NewsController : Controller
    {
        private HotNewsContext db = new HotNewsContext();

       
        //
        // GET: /News/
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.Product).Include(p => p.Category).Include(p => p.User).Include(p=>p.Tags);
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
            
            ViewBag.MultiSelectTags = new MultiSelectList(db.Tags, "tagID", "name");
            ViewBag.productID = new SelectList(db.Products, "productID", "productName");
            ViewBag.categoryID = new MultiSelectList(db.Categories, "categoryID", "categoryName");
            ViewBag.userID = new SelectList(db.Users, "userID", "userName");
            return View();
        }

        //
        // POST: /News/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(Post post, int[] tagsPicked, string newTags)
        {
            post.userID = (from u in db.Users where u.userName == post.userName select u.userID).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (post.Tags == null) { post.Tags = new List<Tag>(); }
                post.Tags = BuildTagListForPost(tagsPicked, newTags);
                
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productID = new SelectList(db.Products, "productID", "productName", post.productID);
            ViewBag.categoryID = new SelectList(db.Categories, "categoryID", "categoryName", post.categoryID);
            ViewBag.userID = new SelectList(db.Users, "userID", "userName", post.userID);
            return View(post);
        }

        private ICollection<Tag> BuildTagListForPost(int[] tagsPicked, string newTags, string originalTags="")
        {
            List<Tag> postTags = new List<Tag>();
            string[] existingTags = String.IsNullOrEmpty(originalTags) ? null : originalTags.Split(',');
            
            if (tagsPicked != null)
            {
                foreach (int t in tagsPicked)
                {
                    var tag = db.Tags.Find(t);
                    if(existingTags == null || existingTags.Contains(t.ToString())==false)
                        postTags.Add(tag);
                }
            }
            if(!string.IsNullOrEmpty(newTags))
            {
                string[] splitTags = newTags.Split(',');
                //post.userID = db.Users.Where(u => u.userName == post.userName).FirstOrDefault().userID;
                foreach (string s in splitTags)
                {
                    
                    Tag tag = new Tag();
                    //var existing = db.Tags.Where(t => t.name == s || t.name == s.ToLower() || t.name == s.ToUpper()).FirstOrDefault();
                    var existing = db.Tags.Where(t => t.name.ToLower() == s.ToLower().Trim()).FirstOrDefault();
                    if(existing !=null)
                        tag=existing;
                    else
                    {
                        tag=new Tag {name=s.Trim()};
                        db.Tags.Add(tag);
                        db.SaveChanges();
                    }
                    postTags.Add(tag);

                }
            }
            return postTags;
        }

        //
        // GET: /News/Edit/5
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            //Post post = db.Posts.Find(id);
            Post post =db.Posts.Include(p=>p.Tags).Where(p=>p.postID==id).Single();
            if (post == null)
            {
                return HttpNotFound();
            }
            var tags1 = db.Tags;
            var tags2 = new List<string>();
            string currentTags = "";
            //foreach (Tag t in post.Tags)
            for (int i = 0; i < post.Tags.Count; i++ )
            {
                Tag t = (Tag)post.Tags.ElementAt(i);
                tags2.Add(t.tagID.ToString());
                string sid = t.tagID.ToString();
                currentTags += i == post.Tags.Count - 1 ? sid : sid + ",";
            }
            var tags3 = new MultiSelectList(db.Tags, "tagID", "name", tags2);
            ViewBag.existingTags = currentTags;
            ViewBag.tagsForList = tags3;
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
        public ActionResult Edit(Post post, int[] tagsPicked, string originalTags, string newTags)
        {
            int id = post.postID;
            var updatePost = db.Posts.Include(p => p.Tags).Where(p => p.postID == id).Single();
            updatePost.userID = db.Users.Where(u => u.userName == post.userName).FirstOrDefault().userID;
            //Need to rebuild the postTags list
            UpdatePostTags(tagsPicked, newTags, updatePost);
            //need to set remaining fields from post to equal corresponding parts of updatePost
            updatePost.productID = post.productID;
            updatePost.categoryID = post.categoryID;
            updatePost.headline = post.headline;
            updatePost.body = post.body;
            updatePost.datePosted = post.datePosted;
            updatePost.lastUpdate= DateTime.Now;

            if (ModelState.IsValid)
            {

                db.Entry(updatePost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var tags2 = new List<string>();
            string currentTags = "";
            //foreach (Tag t in post.Tags)
            for (int i = 0; i < post.Tags.Count; i++)
            {
                Tag t = (Tag)updatePost.Tags.ElementAt(i);
                tags2.Add(t.tagID.ToString());
                string sid = t.tagID.ToString();
                currentTags += i == updatePost.Tags.Count - 1 ? sid : sid + ",";
            }
            var tags3 = new MultiSelectList(db.Tags, "tagID", "name", tags2);
            ViewBag.existingTags = currentTags;
            ViewBag.tagsForList = tags3;
            ViewBag.productID = new SelectList(db.Products, "productID", "productName", updatePost.productID);
            ViewBag.categoryID = new SelectList(db.Categories, "categoryID", "categoryName", updatePost.categoryID);
            ViewBag.userID = new SelectList(db.Users, "userID", "userName", updatePost.userID);
            return View(updatePost);
        }

        private void UpdatePostTags(int[] tagsPicked, string newTags, Post post)
        {

            if (tagsPicked == null && string.IsNullOrEmpty(newTags))
            {
                post.Tags = new List<Tag>();
                return;
            }
            else
            {
                if (post.Tags == null)
                    post.Tags = new List<Tag>();
                else
                    post.Tags.Clear();
                var updatedTagsList = BuildTagListForPost(tagsPicked, newTags);
                foreach (var tag in db.Tags)
                {
                    if (updatedTagsList.Contains(tag))
                        post.Tags.Add(tag);
                    else
                    {
                        if (post.Tags.Contains(tag))
                            post.Tags.Remove(tag);
                    }
                }
            }
            db.SaveChanges();
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