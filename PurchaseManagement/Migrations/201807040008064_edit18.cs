namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit18 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryList5497",
                c => new
                    {
                        CategoryListID = c.Int(nullable: false, identity: true),
                        FirstCategory = c.String(maxLength: 100),
                        FirstCategoryNum = c.String(maxLength: 100),
                        FirstCategoryName = c.String(maxLength: 100),
                        SecondCategory = c.String(maxLength: 100),
                        SecondCategoryNum = c.String(maxLength: 100),
                        SecondCategoryName = c.String(maxLength: 100),
                        ThirdCategory = c.String(maxLength: 100),
                        ThirdCategoryNum = c.String(maxLength: 100),
                        ThirdCategoryName = c.String(maxLength: 100),
                        Product = c.String(maxLength: 100),
                        ProductNum = c.String(maxLength: 100),
                        ProductName = c.String(maxLength: 100),
                        TypeSpecification = c.String(maxLength: 100),
                        MeasurementUnit = c.String(maxLength: 100),
                        Attribute01Name = c.String(maxLength: 100),
                        Attribute01Unit = c.String(maxLength: 100),
                        Attribute02Name = c.String(maxLength: 100),
                        Attribute02Unit = c.String(maxLength: 100),
                        Attribute03Name = c.String(maxLength: 100),
                        Attribute03Unit = c.String(maxLength: 100),
                        Attribute04Name = c.String(maxLength: 100),
                        Attribute04Unit = c.String(maxLength: 100),
                        Attribute05Name = c.String(maxLength: 100),
                        Attribute05Unit = c.String(maxLength: 100),
                        Attribute06Name = c.String(maxLength: 100),
                        Attribute06Unit = c.String(maxLength: 100),
                        Attribute07Name = c.String(maxLength: 100),
                        Attribute07Unit = c.String(maxLength: 100),
                        Attribute08Name = c.String(maxLength: 100),
                        Attribute08Unit = c.String(maxLength: 100),
                        Attribute09Name = c.String(maxLength: 100),
                        Attribute09Unit = c.String(maxLength: 100),
                        Attribute10Name = c.String(maxLength: 100),
                        Attribute10Unit = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.CategoryListID);
            
            CreateTable(
                "dbo.MaterialList",
                c => new
                    {
                        MaterialListID = c.Int(nullable: false, identity: true),
                        MaterialDesc = c.String(nullable: false, maxLength: 200),
                        MaterialNum = c.String(nullable: false, maxLength: 100),
                        MaterialCategoryNum = c.String(nullable: false, maxLength: 100),
                        ChangeDate = c.DateTime(nullable: false),
                        PGr = c.String(maxLength: 100),
                        ValCl = c.String(maxLength: 100),
                        MaterialUnit = c.String(maxLength: 500),
                        StateCode = c.Int(nullable: false),
                        BookDate = c.DateTime(),
                        BookPerson = c.Int(nullable: false),
                        AuditDate = c.DateTime(),
                        AuditPerson = c.Int(nullable: false),
                        Remark = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.MaterialListID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MaterialList");
            DropTable("dbo.CategoryList5497");
        }
    }
}
