namespace Hebony.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madechangestomodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerAccountTypes", "COTIncomeGLAccount_Id", "dbo.GLAccounts");
            DropForeignKey("dbo.CustomerAccounts", "CustomerAccountType_Id", "dbo.CustomerAccountTypes");
            DropForeignKey("dbo.CustomerAccountTypes", "InterestIncomeGLAccount_Id", "dbo.GLAccounts");
            DropForeignKey("dbo.CustomerAccountTypes", "IntestestExpenseGLAccount_Id", "dbo.GLAccounts");
            DropForeignKey("dbo.CustomerAccounts", "LinkedAccount_Id", "dbo.CustomerAccounts");
            DropIndex("dbo.CustomerAccounts", new[] { "CustomerAccountType_Id" });
            DropIndex("dbo.CustomerAccounts", new[] { "LinkedAccount_Id" });
            DropIndex("dbo.CustomerAccountTypes", new[] { "COTIncomeGLAccount_Id" });
            DropIndex("dbo.CustomerAccountTypes", new[] { "InterestIncomeGLAccount_Id" });
            DropIndex("dbo.CustomerAccountTypes", new[] { "IntestestExpenseGLAccount_Id" });
            CreateTable(
                "dbo.Configurations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsBusinessOpen = c.Boolean(nullable: false),
                        FinancialDate = c.DateTime(nullable: false),
                        SavingsCreditInterestRate = c.Double(nullable: false),
                        SavingsMinimumBalance = c.Double(nullable: false),
                        CurrentCreditInterestRate = c.Double(nullable: false),
                        CurrentMinimumBalance = c.Double(nullable: false),
                        CurrentCOT = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoanDebitInterestRate = c.Double(nullable: false),
                        CurrentCOTIncomeGl_Id = c.Int(),
                        CurrentInterestExpenseGLAccount_Id = c.Int(),
                        CurrentInterestPayableGLAccount_Id = c.Int(),
                        LoanInterestExpenseGL_Id = c.Int(),
                        LoanInterestIncomeGL_Id = c.Int(),
                        LoanInterestReceivableGL_Id = c.Int(),
                        SavingsInterestExpenseGLAccount_Id = c.Int(),
                        SavingsInterestPayableGLAccount_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GLAccounts", t => t.CurrentCOTIncomeGl_Id)
                .ForeignKey("dbo.GLAccounts", t => t.CurrentInterestExpenseGLAccount_Id)
                .ForeignKey("dbo.GLAccounts", t => t.CurrentInterestPayableGLAccount_Id)
                .ForeignKey("dbo.GLAccounts", t => t.LoanInterestExpenseGL_Id)
                .ForeignKey("dbo.GLAccounts", t => t.LoanInterestIncomeGL_Id)
                .ForeignKey("dbo.GLAccounts", t => t.LoanInterestReceivableGL_Id)
                .ForeignKey("dbo.GLAccounts", t => t.SavingsInterestExpenseGLAccount_Id)
                .ForeignKey("dbo.GLAccounts", t => t.SavingsInterestPayableGLAccount_Id)
                .Index(t => t.CurrentCOTIncomeGl_Id)
                .Index(t => t.CurrentInterestExpenseGLAccount_Id)
                .Index(t => t.CurrentInterestPayableGLAccount_Id)
                .Index(t => t.LoanInterestExpenseGL_Id)
                .Index(t => t.LoanInterestIncomeGL_Id)
                .Index(t => t.LoanInterestReceivableGL_Id)
                .Index(t => t.SavingsInterestExpenseGLAccount_Id)
                .Index(t => t.SavingsInterestPayableGLAccount_Id);
            
            AddColumn("dbo.Branches", "SortCode", c => c.Long(nullable: false));
            AddColumn("dbo.CustomerAccounts", "AccountType", c => c.Int(nullable: false));
            AddColumn("dbo.CustomerAccounts", "DailyInterestAccrued", c => c.Double(nullable: false));
            AddColumn("dbo.CustomerAccounts", "DailyCOTAccrued", c => c.Double(nullable: false));
            DropColumn("dbo.CustomerAccounts", "RowVersion");
            DropColumn("dbo.CustomerAccounts", "CustomerAccountType_Id");
            DropColumn("dbo.CustomerAccounts", "LinkedAccount_Id");
            DropTable("dbo.CustomerAccountTypes");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CustomerAccounts", "LinkedAccount_Id", c => c.Int());
            AddColumn("dbo.CustomerAccounts", "CustomerAccountType_Id", c => c.Int());
            AddColumn("dbo.CustomerAccounts", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            DropForeignKey("dbo.Configurations", "SavingsInterestPayableGLAccount_Id", "dbo.GLAccounts");
            DropForeignKey("dbo.Configurations", "SavingsInterestExpenseGLAccount_Id", "dbo.GLAccounts");
            DropForeignKey("dbo.Configurations", "LoanInterestReceivableGL_Id", "dbo.GLAccounts");
            DropForeignKey("dbo.Configurations", "LoanInterestIncomeGL_Id", "dbo.GLAccounts");
            DropForeignKey("dbo.Configurations", "LoanInterestExpenseGL_Id", "dbo.GLAccounts");
            DropForeignKey("dbo.Configurations", "CurrentInterestPayableGLAccount_Id", "dbo.GLAccounts");
            DropForeignKey("dbo.Configurations", "CurrentInterestExpenseGLAccount_Id", "dbo.GLAccounts");
            DropForeignKey("dbo.Configurations", "CurrentCOTIncomeGl_Id", "dbo.GLAccounts");
            DropIndex("dbo.Configurations", new[] { "SavingsInterestPayableGLAccount_Id" });
            DropIndex("dbo.Configurations", new[] { "SavingsInterestExpenseGLAccount_Id" });
            DropIndex("dbo.Configurations", new[] { "LoanInterestReceivableGL_Id" });
            DropIndex("dbo.Configurations", new[] { "LoanInterestIncomeGL_Id" });
            DropIndex("dbo.Configurations", new[] { "LoanInterestExpenseGL_Id" });
            DropIndex("dbo.Configurations", new[] { "CurrentInterestPayableGLAccount_Id" });
            DropIndex("dbo.Configurations", new[] { "CurrentInterestExpenseGLAccount_Id" });
            DropIndex("dbo.Configurations", new[] { "CurrentCOTIncomeGl_Id" });
            DropColumn("dbo.CustomerAccounts", "DailyCOTAccrued");
            DropColumn("dbo.CustomerAccounts", "DailyInterestAccrued");
            DropColumn("dbo.CustomerAccounts", "AccountType");
            DropColumn("dbo.Branches", "SortCode");
            DropTable("dbo.Configurations");
            CreateIndex("dbo.CustomerAccountTypes", "IntestestExpenseGLAccount_Id");
            CreateIndex("dbo.CustomerAccountTypes", "InterestIncomeGLAccount_Id");
            CreateIndex("dbo.CustomerAccountTypes", "COTIncomeGLAccount_Id");
            CreateIndex("dbo.CustomerAccounts", "LinkedAccount_Id");
            CreateIndex("dbo.CustomerAccounts", "CustomerAccountType_Id");
            AddForeignKey("dbo.CustomerAccounts", "LinkedAccount_Id", "dbo.CustomerAccounts", "Id");
            AddForeignKey("dbo.CustomerAccountTypes", "IntestestExpenseGLAccount_Id", "dbo.GLAccounts", "Id");
            AddForeignKey("dbo.CustomerAccountTypes", "InterestIncomeGLAccount_Id", "dbo.GLAccounts", "Id");
            AddForeignKey("dbo.CustomerAccounts", "CustomerAccountType_Id", "dbo.CustomerAccountTypes", "Id");
            AddForeignKey("dbo.CustomerAccountTypes", "COTIncomeGLAccount_Id", "dbo.GLAccounts", "Id");
        }
    }
}
