namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit20 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MaterialList", "BookPerson", c => c.Int());
            AlterColumn("dbo.MaterialList", "AuditPerson", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MaterialList", "AuditPerson", c => c.Int(nullable: false));
            AlterColumn("dbo.MaterialList", "BookPerson", c => c.Int(nullable: false));
        }
    }
}
