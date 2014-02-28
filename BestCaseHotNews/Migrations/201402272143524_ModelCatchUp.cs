namespace BestCaseHotNews.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelCatchUp : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "email", c => c.String());
        }
    }
}
