namespace Hebony.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedidentitymodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Branch_ID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Branch_ID");
            AddForeignKey("dbo.AspNetUsers", "Branch_ID", "dbo.Branches", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Branch_ID", "dbo.Branches");
            DropIndex("dbo.AspNetUsers", new[] { "Branch_ID" });
            DropColumn("dbo.AspNetUsers", "Branch_ID");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropTable("dbo.Branches");
        }
    }
}
