namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit16 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MaterialQualityFeedback", "AppraiseBillInputTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MaterialQualityFeedback", "AppraiseBillInputTime", c => c.DateTime(nullable: false));
        }
    }
}
