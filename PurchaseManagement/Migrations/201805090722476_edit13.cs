namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaterialQualityFeedback", "ReplyContent", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MaterialQualityFeedback", "ReplyContent");
        }
    }
}