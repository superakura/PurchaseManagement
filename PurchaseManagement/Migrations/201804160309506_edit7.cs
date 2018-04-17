namespace PurchaseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Crud",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Age = c.Int(nullable: false),
                        Sex = c.String(nullable: false, maxLength: 10),
                        Duty = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(maxLength: 100),
                        Email = c.String(maxLength: 100),
                        Remark = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Crud");
        }
    }
}
