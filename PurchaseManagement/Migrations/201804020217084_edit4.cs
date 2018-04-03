namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeptInfo", "Open", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeptInfo", "Open");
        }
    }
}
