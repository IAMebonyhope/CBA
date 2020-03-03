namespace Hebony.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeaddedtransactionmodel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Configurations", name: "CurrentCOTIncomeGl_Id", newName: "CurrentCOTIncomeGLAccount_Id");
            RenameColumn(table: "dbo.Configurations", name: "LoanInterestExpenseGL_Id", newName: "LoanInterestExpenseGLAccount_Id");
            RenameColumn(table: "dbo.Configurations", name: "LoanInterestIncomeGL_Id", newName: "LoanInterestIncomeGLAccount_Id");
            RenameColumn(table: "dbo.Configurations", name: "LoanInterestReceivableGL_Id", newName: "LoanInterestReceivableGLAccount_Id");
            RenameIndex(table: "dbo.Configurations", name: "IX_CurrentCOTIncomeGl_Id", newName: "IX_CurrentCOTIncomeGLAccount_Id");
            RenameIndex(table: "dbo.Configurations", name: "IX_LoanInterestExpenseGL_Id", newName: "IX_LoanInterestExpenseGLAccount_Id");
            RenameIndex(table: "dbo.Configurations", name: "IX_LoanInterestIncomeGL_Id", newName: "IX_LoanInterestIncomeGLAccount_Id");
            RenameIndex(table: "dbo.Configurations", name: "IX_LoanInterestReceivableGL_Id", newName: "IX_LoanInterestReceivableGLAccount_Id");
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        AccountName = c.String(),
                        SubCategory = c.String(),
                        MainCategory = c.Int(nullable: false),
                        TransactionType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.CustomerAccounts", "SavingsWithdrawalCount", c => c.Int(nullable: false));
            AlterColumn("dbo.Configurations", "CurrentCOT", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Configurations", "CurrentCOT", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.CustomerAccounts", "SavingsWithdrawalCount");
            DropTable("dbo.Transactions");
            RenameIndex(table: "dbo.Configurations", name: "IX_LoanInterestReceivableGLAccount_Id", newName: "IX_LoanInterestReceivableGL_Id");
            RenameIndex(table: "dbo.Configurations", name: "IX_LoanInterestIncomeGLAccount_Id", newName: "IX_LoanInterestIncomeGL_Id");
            RenameIndex(table: "dbo.Configurations", name: "IX_LoanInterestExpenseGLAccount_Id", newName: "IX_LoanInterestExpenseGL_Id");
            RenameIndex(table: "dbo.Configurations", name: "IX_CurrentCOTIncomeGLAccount_Id", newName: "IX_CurrentCOTIncomeGl_Id");
            RenameColumn(table: "dbo.Configurations", name: "LoanInterestReceivableGLAccount_Id", newName: "LoanInterestReceivableGL_Id");
            RenameColumn(table: "dbo.Configurations", name: "LoanInterestIncomeGLAccount_Id", newName: "LoanInterestIncomeGL_Id");
            RenameColumn(table: "dbo.Configurations", name: "LoanInterestExpenseGLAccount_Id", newName: "LoanInterestExpenseGL_Id");
            RenameColumn(table: "dbo.Configurations", name: "CurrentCOTIncomeGLAccount_Id", newName: "CurrentCOTIncomeGl_Id");
        }
    }
}
