namespace Hebony.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcustomerAccounts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AccNo = c.String(),
                        Balance = c.Double(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Branch_ID = c.Int(),
                        Customer_Id = c.Int(),
                        CustomerAccountType_Id = c.Int(),
                        LinkedAccount_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Branches", t => t.Branch_ID)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.CustomerAccountTypes", t => t.CustomerAccountType_Id)
                .ForeignKey("dbo.CustomerAccounts", t => t.LinkedAccount_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Branch_ID)
                .Index(t => t.Customer_Id)
                .Index(t => t.CustomerAccountType_Id)
                .Index(t => t.LinkedAccount_Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Gender = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.CustomerAccountTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreditInterestRate = c.Double(nullable: false),
                        DebitInterestRate = c.Double(nullable: false),
                        COT = c.Double(nullable: false),
                        MinBalance = c.Double(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        COTIncomeGLAccount_Id = c.Int(),
                        InterestIncomeGLAccount_Id = c.Int(),
                        IntestestExpenseGLAccount_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GLAccounts", t => t.COTIncomeGLAccount_Id)
                .ForeignKey("dbo.GLAccounts", t => t.InterestIncomeGLAccount_Id)
                .ForeignKey("dbo.GLAccounts", t => t.IntestestExpenseGLAccount_Id)
                .Index(t => t.COTIncomeGLAccount_Id)
                .Index(t => t.InterestIncomeGLAccount_Id)
                .Index(t => t.IntestestExpenseGLAccount_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerAccounts", "LinkedAccount_Id", "dbo.CustomerAccounts");
            DropForeignKey("dbo.CustomerAccountTypes", "IntestestExpenseGLAccount_Id", "dbo.GLAccounts");
            DropForeignKey("dbo.CustomerAccountTypes", "InterestIncomeGLAccount_Id", "dbo.GLAccounts");
            DropForeignKey("dbo.CustomerAccounts", "CustomerAccountType_Id", "dbo.CustomerAccountTypes");
            DropForeignKey("dbo.CustomerAccountTypes", "COTIncomeGLAccount_Id", "dbo.GLAccounts");
            DropForeignKey("dbo.CustomerAccounts", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Customers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CustomerAccounts", "Branch_ID", "dbo.Branches");
            DropForeignKey("dbo.CustomerAccounts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.CustomerAccountTypes", new[] { "IntestestExpenseGLAccount_Id" });
            DropIndex("dbo.CustomerAccountTypes", new[] { "InterestIncomeGLAccount_Id" });
            DropIndex("dbo.CustomerAccountTypes", new[] { "COTIncomeGLAccount_Id" });
            DropIndex("dbo.Customers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.CustomerAccounts", new[] { "LinkedAccount_Id" });
            DropIndex("dbo.CustomerAccounts", new[] { "CustomerAccountType_Id" });
            DropIndex("dbo.CustomerAccounts", new[] { "Customer_Id" });
            DropIndex("dbo.CustomerAccounts", new[] { "Branch_ID" });
            DropIndex("dbo.CustomerAccounts", new[] { "ApplicationUser_Id" });
            DropTable("dbo.CustomerAccountTypes");
            DropTable("dbo.Customers");
            DropTable("dbo.CustomerAccounts");
        }
    }
}
