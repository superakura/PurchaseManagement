namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthorityInfo",
                c => new
                    {
                        AuthorityID = c.Int(nullable: false, identity: true),
                        AuthorityName = c.String(nullable: false, maxLength: 50),
                        AuthorityDescribe = c.String(maxLength: 100),
                        AuthorityType = c.String(nullable: false, maxLength: 50),
                        ConflictCode = c.Int(nullable: false),
                        MenuUrl = c.String(maxLength: 100),
                        MenuOrder = c.Int(nullable: false),
                        MenuFatherID = c.Int(nullable: false),
                        MenuIcon = c.String(maxLength: 50),
                        MenuName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.AuthorityID);
            
            CreateTable(
                "dbo.DeptInfo",
                c => new
                    {
                        DeptID = c.Int(nullable: false, identity: true),
                        DeptName = c.String(nullable: false, maxLength: 50),
                        DeptFatherID = c.Int(nullable: false),
                        DeptState = c.Int(nullable: false),
                        DeptRemark = c.String(maxLength: 200),
                        DeptOrder = c.Int(nullable: false),
                        DeptCreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DeptID);
            
            CreateTable(
                "dbo.RoleAuthority",
                c => new
                    {
                        RoleAuthorityID = c.Int(nullable: false, identity: true),
                        RoleID = c.Int(nullable: false),
                        AuthorityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoleAuthorityID)
                .ForeignKey("dbo.AuthorityInfo", t => t.AuthorityID, cascadeDelete: true)
                .ForeignKey("dbo.RoleInfo", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID)
                .Index(t => t.AuthorityID);
            
            CreateTable(
                "dbo.RoleInfo",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 50),
                        RoleDescribe = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.UserInfo",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserNum = c.String(nullable: false, maxLength: 20),
                        UserName = c.String(nullable: false, maxLength: 20),
                        UserPassword = c.String(maxLength: 100),
                        UserPhone = c.String(maxLength: 50),
                        UserMobile = c.String(maxLength: 50),
                        UserRole = c.String(nullable: false, maxLength: 30),
                        UserRemark = c.String(maxLength: 200),
                        UserState = c.Int(nullable: false),
                        UserDeptID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.DeptInfo", t => t.UserDeptID, cascadeDelete: true)
                .Index(t => t.UserDeptID);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserRoleID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserRoleID)
                .ForeignKey("dbo.RoleInfo", t => t.RoleID, cascadeDelete: true)
                .ForeignKey("dbo.UserInfo", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "UserID", "dbo.UserInfo");
            DropForeignKey("dbo.UserRole", "RoleID", "dbo.RoleInfo");
            DropForeignKey("dbo.UserInfo", "UserDeptID", "dbo.DeptInfo");
            DropForeignKey("dbo.RoleAuthority", "RoleID", "dbo.RoleInfo");
            DropForeignKey("dbo.RoleAuthority", "AuthorityID", "dbo.AuthorityInfo");
            DropIndex("dbo.UserRole", new[] { "RoleID" });
            DropIndex("dbo.UserRole", new[] { "UserID" });
            DropIndex("dbo.UserInfo", new[] { "UserDeptID" });
            DropIndex("dbo.RoleAuthority", new[] { "AuthorityID" });
            DropIndex("dbo.RoleAuthority", new[] { "RoleID" });
            DropTable("dbo.UserRole");
            DropTable("dbo.UserInfo");
            DropTable("dbo.RoleInfo");
            DropTable("dbo.RoleAuthority");
            DropTable("dbo.DeptInfo");
            DropTable("dbo.AuthorityInfo");
        }
    }
}
