namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaterialQualityFeedback", "AppraiseBillInputTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.MaterialQualityFeedback", "AppraiseBillNum", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MaterialQualityFeedback", "AppraiseBillNum");
            DropColumn("dbo.MaterialQualityFeedback", "AppraiseBillInputTime");
        }
    }
}
