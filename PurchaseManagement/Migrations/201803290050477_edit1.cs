namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NoticeInfo",
                c => new
                    {
                        NoticeID = c.Int(nullable: false, identity: true),
                        NoticeTitle = c.String(nullable: false, maxLength: 200),
                        Content = c.String(nullable: false, maxLength: 1000),
                        ContentType = c.String(maxLength: 50),
                        ContentCount = c.Int(nullable: false),
                        InsertPersonID = c.Int(nullable: false),
                        InsertDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NoticeID)
                .ForeignKey("dbo.UserInfo", t => t.InsertPersonID, cascadeDelete: true)
                .Index(t => t.InsertPersonID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NoticeInfo", "InsertPersonID", "dbo.UserInfo");
            DropIndex("dbo.NoticeInfo", new[] { "InsertPersonID" });
            DropTable("dbo.NoticeInfo");
        }
    }
}
