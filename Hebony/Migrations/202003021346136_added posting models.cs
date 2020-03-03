namespace Hebony.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedpostingmodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GLPostings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DebitAmount = c.Double(nullable: false),
                        CreditAmount = c.Double(nullable: false),
                        Narration = c.String(),
                        TransactionDate = c.DateTime(nullable: false),
                        CreditAccount_Id = c.Int(),
                        DebitAccount_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GLAccounts", t => t.CreditAccount_Id)
                .ForeignKey("dbo.GLAccounts", t => t.DebitAccount_Id)
                .Index(t => t.CreditAccount_Id)
                .Index(t => t.DebitAccount_Id);
            
            CreateTable(
                "dbo.TellerPostings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DebitAmount = c.Double(nullable: false),
                        CreditAmount = c.Double(nullable: false),
                        Narration = c.String(),
                        TransactionDate = c.DateTime(nullable: false),
                        PostingType = c.Int(nullable: false),
                        CustomerAccount_Id = c.Int(),
                        TillAccount_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerAccounts", t => t.CustomerAccount_Id)
                .ForeignKey("dbo.GLAccounts", t => t.TillAccount_Id)
                .Index(t => t.CustomerAccount_Id)
                .Index(t => t.TillAccount_Id);
            
            AddColumn("dbo.Branches", "IsOpen", c => c.Boolean(nullable: false));
            AddColumn("dbo.CustomerAccounts", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TellerPostings", "TillAccount_Id", "dbo.GLAccounts");
            DropForeignKey("dbo.TellerPostings", "CustomerAccount_Id", "dbo.CustomerAccounts");
            DropForeignKey("dbo.GLPostings", "DebitAccount_Id", "dbo.GLAccounts");
            DropForeignKey("dbo.GLPostings", "CreditAccount_Id", "dbo.GLAccounts");
            DropIndex("dbo.TellerPostings", new[] { "TillAccount_Id" });
            DropIndex("dbo.TellerPostings", new[] { "CustomerAccount_Id" });
            DropIndex("dbo.GLPostings", new[] { "DebitAccount_Id" });
            DropIndex("dbo.GLPostings", new[] { "CreditAccount_Id" });
            DropColumn("dbo.CustomerAccounts", "IsActive");
            DropColumn("dbo.Branches", "IsOpen");
            DropTable("dbo.TellerPostings");
            DropTable("dbo.GLPostings");
        }
    }
}
