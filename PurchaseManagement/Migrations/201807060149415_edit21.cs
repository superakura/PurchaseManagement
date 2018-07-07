namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MaterialList", "StateCode", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MaterialList", "StateCode", c => c.Int(nullable: false));
        }
    }
}
