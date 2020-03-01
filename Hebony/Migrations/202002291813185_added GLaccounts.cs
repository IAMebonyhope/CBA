namespace Hebony.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedGLaccounts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GLAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AccNo = c.String(),
                        Balance = c.Double(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Branch_ID = c.Int(),
                        GLCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Branches", t => t.Branch_ID)
                .ForeignKey("dbo.GLCategories", t => t.GLCategory_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Branch_ID)
                .Index(t => t.GLCategory_Id);
            
            CreateTable(
                "dbo.GLCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MainCategory = c.Int(nullable: false),
                        Description = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GLAccounts", "GLCategory_Id", "dbo.GLCategories");
            DropForeignKey("dbo.GLAccounts", "Branch_ID", "dbo.Branches");
            DropForeignKey("dbo.GLAccounts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.GLAccounts", new[] { "GLCategory_Id" });
            DropIndex("dbo.GLAccounts", new[] { "Branch_ID" });
            DropIndex("dbo.GLAccounts", new[] { "ApplicationUser_Id" });
            DropTable("dbo.GLCategories");
            DropTable("dbo.GLAccounts");
        }
    }
}
