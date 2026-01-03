namespace ANewReport.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50, unicode: false),
                        city = c.String(nullable: false, maxLength: 50, unicode: false),
                        HireDate = c.DateTime(nullable: false, precision: 0),
                        salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(nullable: false, maxLength: 50, unicode: false),
                        password_hash = c.String(nullable: false, maxLength: 255, unicode: false),
                        role_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Roles", t => t.role_id, cascadeDelete: true)
                .Index(t => t.role_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "role_id", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "role_id" });
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Employees");
        }
    }
}
