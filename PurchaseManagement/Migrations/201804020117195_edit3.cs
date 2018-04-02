namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserDept",
                c => new
                    {
                        UserDeptID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        DeptID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserDeptID)
                .ForeignKey("dbo.DeptInfo", t => t.DeptID, cascadeDelete: true)
                .Index(t => t.DeptID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDept", "DeptID", "dbo.DeptInfo");
            DropIndex("dbo.UserDept", new[] { "DeptID" });
            DropTable("dbo.UserDept");
        }
    }
}
