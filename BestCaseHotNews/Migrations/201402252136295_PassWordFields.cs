namespace BestCaseHotNews.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PassWordFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "password", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "userName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "email", c => c.String());
            AlterColumn("dbo.Users", "userName", c => c.String());
            DropColumn("dbo.Users", "password");
        }
    }
}
