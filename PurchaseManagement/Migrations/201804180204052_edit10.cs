namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaterialQualityFeedback", "InputPersonPhone", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MaterialQualityFeedback", "InputPersonPhone");
        }
    }
}
