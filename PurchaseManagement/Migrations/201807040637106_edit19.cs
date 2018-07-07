namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit19 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MaterialList", "Typ", c => c.String(maxLength: 100));
            AddColumn("dbo.MaterialList", "Factory", c => c.String(maxLength: 100));
            AddColumn("dbo.MaterialList", "MaterialType", c => c.String(maxLength: 100));
            AddColumn("dbo.MaterialList", "Pr", c => c.String(maxLength: 100));
            AddColumn("dbo.MaterialList", "Money", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.MaterialList", "Currency", c => c.String(maxLength: 20));
            AlterColumn("dbo.MaterialList", "MaterialUnit", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MaterialList", "MaterialUnit", c => c.String(maxLength: 500));
            DropColumn("dbo.MaterialList", "Currency");
            DropColumn("dbo.MaterialList", "Money");
            DropColumn("dbo.MaterialList", "Pr");
            DropColumn("dbo.MaterialList", "MaterialType");
            DropColumn("dbo.MaterialList", "Factory");
            DropColumn("dbo.MaterialList", "Typ");
        }
    }
}
