namespace BestCaseHotNews.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustUsers4SimpleLogin : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "email", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "email", c => c.String(nullable: false));
        }
    }
}
