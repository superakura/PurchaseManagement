namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit23 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CategoryList5497", "ProductName", c => c.String(maxLength: 200));
            AlterColumn("dbo.CategoryList5497", "TypeSpecification", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CategoryList5497", "TypeSpecification", c => c.String(maxLength: 100));
            AlterColumn("dbo.CategoryList5497", "ProductName", c => c.String(maxLength: 100));
        }
    }
}
