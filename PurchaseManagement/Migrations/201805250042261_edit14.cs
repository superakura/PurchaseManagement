namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SupplierAppraise",
                c => new
                    {
                        SupplierAppraiseID = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(nullable: false, maxLength: 100),
                        ContractNumber = c.String(nullable: false, maxLength: 100),
                        AppraiseDepend = c.String(nullable: false, maxLength: 100),
                        AppraiseMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AppraiseScore = c.Single(nullable: false),
                        AppraiseContent = c.String(nullable: false, maxLength: 500),
                        AppraisePersonID = c.Int(nullable: false),
                        AppraiseInputTime = c.DateTime(nullable: false),
                        AppraiseTypeQuality = c.String(maxLength: 10),
                        AppraiseTypeContract = c.String(maxLength: 10),
                        AppraiseTypeService = c.String(maxLength: 10),
                        AppraiseTypePriceResponse = c.String(maxLength: 10),
                        AppraiseTypeSincerity = c.String(maxLength: 10),
                        AppraiseTypeOther = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.SupplierAppraiseID);
            
            AddColumn("dbo.MaterialQualityFeedback", "AppraiseBillState", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MaterialQualityFeedback", "AppraiseBillState");
            DropTable("dbo.SupplierAppraise");
        }
    }
}
