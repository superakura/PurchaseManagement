namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MaterialQualityFeedback", "MaterialNum", c => c.String(maxLength: 200));
            AlterColumn("dbo.MaterialQualityFeedback", "ErpOrderNum", c => c.String(maxLength: 200));
            AlterColumn("dbo.MaterialQualityFeedback", "InputPersonName", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MaterialQualityFeedback", "InputPersonName", c => c.String(maxLength: 100));
            AlterColumn("dbo.MaterialQualityFeedback", "ErpOrderNum", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.MaterialQualityFeedback", "MaterialNum", c => c.String(nullable: false, maxLength: 200));
        }
    }
}
