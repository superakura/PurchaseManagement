namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit22 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MaterialList", "ChangeDate", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MaterialList", "ChangeDate", c => c.DateTime(nullable: false));
        }
    }
}
