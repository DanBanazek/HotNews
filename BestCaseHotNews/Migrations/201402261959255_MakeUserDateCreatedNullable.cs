namespace BestCaseHotNews.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeUserDateCreatedNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "dateCreated", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "dateCreated", c => c.DateTime(nullable: false));
        }
    }
}
