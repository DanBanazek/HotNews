namespace BestCaseHotNews.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foobar : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "password", c => c.String(nullable: false));
        }
    }
}
