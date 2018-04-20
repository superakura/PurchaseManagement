namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Log", "LogDataID", c => c.Int(nullable: false));
            AlterColumn("dbo.Log", "InputPersonName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Log", "InputPersonName", c => c.String());
            DropColumn("dbo.Log", "LogDataID");
        }
    }
}
