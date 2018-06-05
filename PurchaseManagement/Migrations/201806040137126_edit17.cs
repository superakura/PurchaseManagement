namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaterialQualityFeedback", "ErpPlanNum", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MaterialQualityFeedback", "ErpPlanNum");
        }
    }
}
