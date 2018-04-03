namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfo", "UserDuty", c => c.String(maxLength: 100));
            AddColumn("dbo.UserInfo", "UserEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserInfo", "UserEmail");
            DropColumn("dbo.UserInfo", "UserDuty");
        }
    }
}
