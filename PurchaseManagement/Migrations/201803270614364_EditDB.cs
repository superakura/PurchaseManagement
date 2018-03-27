namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditDB : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserInfo", "UserRole");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInfo", "UserRole", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
