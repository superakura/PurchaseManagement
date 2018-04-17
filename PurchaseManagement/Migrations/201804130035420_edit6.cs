namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        LogID = c.Int(nullable: false, identity: true),
                        LogType = c.String(nullable: false, maxLength: 100),
                        LogContent = c.String(nullable: false, maxLength: 500),
                        LogReason = c.String(maxLength: 500),
                        InputPersonID = c.Int(nullable: false),
                        InputPersonName = c.String(),
                        InputDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LogID);
            
            CreateTable(
                "dbo.MaterialQualityFeedback",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FeedbackTitle = c.String(nullable: false, maxLength: 200),
                        MaterialQualityTypeID = c.Int(nullable: false),
                        SupplierName = c.String(nullable: false, maxLength: 200),
                        MaterialNum = c.String(nullable: false, maxLength: 200),
                        ErpOrderNum = c.String(nullable: false, maxLength: 200),
                        FeedbackContent = c.String(nullable: false, maxLength: 1000),
                        InputPersonID = c.Int(nullable: false),
                        InputPersonName = c.String(maxLength: 100),
                        InputDateTime = c.DateTime(nullable: false),
                        InputDeptID = c.Int(nullable: false),
                        AcceptPersonID = c.Int(nullable: false),
                        AcceptPersonName = c.String(maxLength: 100),
                        AcceptDateTime = c.DateTime(),
                        ReplyPersonID = c.Int(nullable: false),
                        ReplyPersonName = c.String(maxLength: 100),
                        ReplyDateTime = c.DateTime(),
                        CheckPersonID = c.Int(nullable: false),
                        CheckPersonName = c.String(maxLength: 100),
                        CheckDateTime = c.DateTime(),
                        FeedBackState = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MaterialQualityType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MaterialQualityTypeName = c.String(nullable: false, maxLength: 100),
                        InputPerson = c.Int(nullable: false),
                        InputDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MaterialQualityType");
            DropTable("dbo.MaterialQualityFeedback");
            DropTable("dbo.Log");
        }
    }
}
