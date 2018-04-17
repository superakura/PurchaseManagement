namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit8 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.MaterialQualityType");
            DropColumn("dbo.MaterialQualityType", "ID");
            AddColumn("dbo.MaterialQualityType", "MaterialQualityTypeID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.MaterialQualityType", "MaterialQualityTypeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MaterialQualityType", "ID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.MaterialQualityType");
            DropColumn("dbo.MaterialQualityType", "MaterialQualityTypeID");
            AddPrimaryKey("dbo.MaterialQualityType", "ID");
        }
    }
}
