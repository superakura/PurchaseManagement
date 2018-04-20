namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaterialQualityFeedback", "InputPersonMobile", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MaterialQualityFeedback", "InputPersonMobile");
        }
    }
}
