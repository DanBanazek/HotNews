namespace BestCaseHotNews.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using BestCaseHotNews.Models;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<BestCaseHotNews.DAL.HotNewsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BestCaseHotNews.DAL.HotNewsContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Users.AddOrUpdate(
              u => u.fullName,
                  new User { userName = "testUser", fullName = "Tech Worker", email = "bestcasestaff@bestcase.com",dateCreated = DateTime.Now },
                  new User { userName = "Dan B", fullName = "Dan Banazek", email = "dbanazek@bestcase.com", dateCreated = DateTime.Now }
            //    new User { userName = "John G.", fullName = "John Gamalinda", email = "jgamalinda@bestcase.com", dateCreated = DateTime.Now }
               );
            context.SaveChanges();
            context.Products.AddOrUpdate(
                p => p.productName,
                new Product { productName = "Best Case Bankruptcy", dateCreated = DateTime.Now },
                new Product { productName = "MyECFMail", dateCreated = DateTime.Now },
                new Product { productName="Time and Billing", dateCreated=DateTime.Now}
                );
            context.SaveChanges();
            context.Categories.AddOrUpdate(
                c => c.categoryName,
                new Category { categoryName = "New Feature", dateAdded = DateTime.Now },
                new Category { categoryName = "Silent Update", dateAdded = DateTime.Now },
                new Category { categoryName = "Bug Fix", dateAdded = DateTime.Now }
                );
            context.SaveChanges();

            context.Tags.AddOrUpdate(
                t => t.name,
                    new Tag { name = "MIE" },
                    new Tag { name = "ECFExpress 2.0" },
                    new Tag {name="OneTouch 2.0"}
            );
            //context.Posts.AddOrUpdate(
            //    p => p.headline,
            //    new Post
            //    {
            //        headline = "Fixed the Inbox Update Issues",
            //        body = "The failed inbox update issues have been fixed in the latest releases of MyECFMail",
            //        datePosted = DateTime.Now,
            //        lastUpdate = DateTime.Now,
            //        categoryID = context.Categories.Single(c => c.categoryName == "Bug Fix").categoryID,
            //        userID = context.Users.Single(u => u.userName == "Dan B.").userID,
            //        productID = context.Products.Single(p => p.productName == "MyECFMail").productID
            //    }
            //    );
            //context.SaveChanges();
            SeedMembership();
        }

        private void SeedMembership()
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "userID", "userName", true);
            var roles = Roles.GetAllRoles();
            if (!Roles.RoleExists("Admin"))
            {
                Roles.CreateRole("Admin");

            }

            if (!Roles.IsUserInRole("Dan B", "Admin"))
                Roles.AddUserToRole("Dan B", "Admin");
            
            //MembershipUserCollection users = Membership.GetAllUsers(;
            //throw new Exception("User count" + users.Count);
            //if (Membership.GetUser("Dan B", false) != null)
            //{
            //    Roles.AddUserToRole("Dan B", "Admin");
            //    throw new Exception("found user. should be adding role");
                
            //}
            //else
            //{
            //    throw new Exception("user not found");
            //}
        }
    }
}
