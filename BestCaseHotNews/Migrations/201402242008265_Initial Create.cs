namespace BestCaseHotNews.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        productID = c.Int(nullable: false, identity: true),
                        productName = c.String(),
                        dateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.productID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        postID = c.Int(nullable: false, identity: true),
                        productID = c.Int(nullable: false),
                        categoryID = c.Int(nullable: false),
                        userID = c.Int(nullable: false),
                        headline = c.String(),
                        body = c.String(),
                        datePosted = c.DateTime(nullable: false),
                        lastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.postID)
                .ForeignKey("dbo.Products", t => t.productID, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.categoryID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.userID, cascadeDelete: true)
                .Index(t => t.productID)
                .Index(t => t.categoryID)
                .Index(t => t.userID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        categoryID = c.Int(nullable: false, identity: true),
                        categoryName = c.String(),
                        dateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.categoryID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        userID = c.Int(nullable: false, identity: true),
                        userName = c.String(),
                        fullName = c.String(),
                        email = c.String(),
                        dateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.userID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Posts", new[] { "userID" });
            DropIndex("dbo.Posts", new[] { "categoryID" });
            DropIndex("dbo.Posts", new[] { "productID" });
            DropForeignKey("dbo.Posts", "userID", "dbo.Users");
            DropForeignKey("dbo.Posts", "categoryID", "dbo.Categories");
            DropForeignKey("dbo.Posts", "productID", "dbo.Products");
            DropTable("dbo.Users");
            DropTable("dbo.Categories");
            DropTable("dbo.Posts");
            DropTable("dbo.Products");
        }
    }
}
