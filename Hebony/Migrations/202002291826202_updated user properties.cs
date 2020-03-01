namespace Hebony.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateduserproperties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerAccounts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Customers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.GLAccounts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.CustomerAccounts", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Customers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.GLAccounts", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.AspNetUsers", "GLAccount_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "GLAccount_Id");
            AddForeignKey("dbo.AspNetUsers", "GLAccount_Id", "dbo.GLAccounts", "Id");
            DropColumn("dbo.CustomerAccounts", "ApplicationUser_Id");
            DropColumn("dbo.Customers", "ApplicationUser_Id");
            DropColumn("dbo.GLAccounts", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GLAccounts", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Customers", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.CustomerAccounts", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.AspNetUsers", "GLAccount_Id", "dbo.GLAccounts");
            DropIndex("dbo.AspNetUsers", new[] { "GLAccount_Id" });
            DropColumn("dbo.AspNetUsers", "GLAccount_Id");
            CreateIndex("dbo.GLAccounts", "ApplicationUser_Id");
            CreateIndex("dbo.Customers", "ApplicationUser_Id");
            CreateIndex("dbo.CustomerAccounts", "ApplicationUser_Id");
            AddForeignKey("dbo.GLAccounts", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Customers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.CustomerAccounts", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
