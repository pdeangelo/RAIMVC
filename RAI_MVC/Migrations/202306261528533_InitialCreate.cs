namespace RAI_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientID = c.Int(nullable: false, identity: true),
                        ClientName = c.String(),
                        AdvanceRate = c.Double(nullable: false),
                        MinimumInterest = c.Double(nullable: false),
                        ClientPrimeRate = c.Double(nullable: false),
                        ClientPrimeRateSpread = c.Double(nullable: false),
                        OriginationDiscount = c.Double(nullable: false),
                        OriginationDiscount2 = c.Double(nullable: false),
                        OriginationDiscountNumDays = c.Int(nullable: false),
                        OriginationDiscountNumDays2 = c.Int(nullable: false),
                        InterestBasedOnAdvance = c.Boolean(nullable: false),
                        OriginationBasedOnAdvance = c.Boolean(nullable: false),
                        NoInterest = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ClientID);
            
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        LoanID = c.Int(nullable: false, identity: true),
                        LoanStatusID = c.Int(),
                        ClientID = c.Int(),
                        DwellingTypeID = c.Int(),
                        StateID = c.Int(),
                        EntityID = c.Int(),
                        InvestorID = c.Int(),
                        LoanTypeID = c.Int(),
                        LoanNumber = c.String(),
                        LoanFundingDate = c.DateTime(storeType: "date"),
                        LoanMortgagee = c.String(maxLength: 200),
                        LoanPropertyAddress = c.String(),
                        LoanInterestRate = c.Double(),
                        LoanMortgageAmount = c.Double(),
                        LoanAdvanceRate = c.Double(),
                        LoanEnteredDate = c.DateTime(storeType: "date"),
                        LoanUpdateDate = c.DateTime(),
                        LoanUpdateUserID = c.Int(),
                        LoanMortgageeBusiness = c.String(maxLength: 200),
                        LoanUW10031008LoanApplication = c.Boolean(),
                        LoanUWAllongePromissoryNote = c.Boolean(),
                        LoanUWAppraisal = c.Boolean(),
                        LoanUWAppraisalAmount = c.Double(),
                        LoanUWPostRepairAppraisalAmount = c.Double(),
                        LoanUWAssignmentOfMortgage = c.Boolean(),
                        LoanUWBackgroundCheck = c.Boolean(),
                        LoanUWCertofGoodStandingFormation = c.Boolean(),
                        LoanUWClosingProtectionLetter = c.Boolean(),
                        LoanUWCommitteeReview = c.Boolean(),
                        LoanUWCreditReport = c.Boolean(),
                        LoanUWFloodCertificate = c.Boolean(),
                        LoanUWHUD1SettlementStatement = c.Boolean(),
                        LoanUWHomeownersInsurance = c.Boolean(),
                        LoanUWLoanPackage = c.Boolean(),
                        LoanUWLoanSizerLoanSubmissionTape = c.Boolean(),
                        LoanUWTitleCommitment = c.Boolean(),
                        LoanUWClaytonReportApprovalEmail = c.Boolean(),
                        LoanUWZillowSqFt = c.Double(),
                        LoanUWIsComplete = c.Boolean(),
                        CompletedBy = c.Int(),
                        DateDepositedInEscrow = c.DateTime(storeType: "date"),
                        BaileeLetterDate = c.DateTime(storeType: "date"),
                        InvestorProceedsDate = c.DateTime(storeType: "date"),
                        InvestorProceeds = c.Double(),
                        ClosingDate = c.DateTime(storeType: "date"),
                        MinimumInterest = c.Double(),
                        OriginationDiscount = c.Double(),
                        OriginationDiscount2 = c.Double(),
                        OriginationDiscountNumDays = c.Int(),
                        OriginationDiscountNumDays2 = c.Int(),
                        InterestBasedOnAdvance = c.Boolean(),
                        OriginationBasedOnAdvance = c.Boolean(),
                        NoInterest = c.Boolean(),
                        InterestFee = c.Double(),
                        OriginationFee = c.Double(),
                        UnderwritingFee = c.Double(),
                        CustSvcUnderwritingDiscount = c.Double(),
                        CustSvcInterestDiscount = c.Double(),
                        CustSvcOriginationDiscount = c.Double(),
                        ClientPrimeRate = c.Double(),
                        ClientPrimeRateSpread = c.Double(),
                    })
                .PrimaryKey(t => t.LoanID)
                .ForeignKey("dbo.Clients", t => t.ClientID)
                .ForeignKey("dbo.DwellingTypes", t => t.DwellingTypeID)
                .ForeignKey("dbo.Entities", t => t.EntityID)
                .ForeignKey("dbo.Investors", t => t.InvestorID)
                .ForeignKey("dbo.LoanStatus", t => t.LoanStatusID)
                .ForeignKey("dbo.LoanTypes", t => t.LoanTypeID)
                .ForeignKey("dbo.States", t => t.StateID)
                .Index(t => t.LoanStatusID)
                .Index(t => t.ClientID)
                .Index(t => t.DwellingTypeID)
                .Index(t => t.StateID)
                .Index(t => t.EntityID)
                .Index(t => t.InvestorID)
                .Index(t => t.LoanTypeID);
            
            CreateTable(
                "dbo.DwellingTypes",
                c => new
                    {
                        DwellingTypeID = c.Int(nullable: false, identity: true),
                        OldKey = c.Int(),
                        DwellingType = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.DwellingTypeID);
            
            CreateTable(
                "dbo.Entities",
                c => new
                    {
                        EntityID = c.Int(nullable: false, identity: true),
                        EntityName = c.String(maxLength: 200),
                        EntityBank = c.String(maxLength: 100),
                        ABA = c.String(maxLength: 100),
                        Account = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.EntityID);
            
            CreateTable(
                "dbo.Investors",
                c => new
                    {
                        InvestorID = c.Int(nullable: false, identity: true),
                        InvestorName = c.String(maxLength: 200),
                        CustodianName = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.InvestorID);
            
            CreateTable(
                "dbo.LoanStatus",
                c => new
                    {
                        LoanStatusID = c.Int(nullable: false, identity: true),
                        LoanStatusName = c.String(),
                    })
                .PrimaryKey(t => t.LoanStatusID);
            
            CreateTable(
                "dbo.LoanTypes",
                c => new
                    {
                        LoanTypeID = c.Int(nullable: false, identity: true),
                        LoanTypeName = c.String(),
                    })
                .PrimaryKey(t => t.LoanTypeID);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateID = c.Int(nullable: false, identity: true),
                        OldKey = c.Int(),
                        State = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.StateID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(maxLength: 100),
                        InsertByUserID = c.Int(),
                        InsertDate = c.DateTime(),
                        UpdateByUserID = c.Int(),
                        UpdateDate = c.DateTime(),
                        Active = c.Boolean(),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        WinUserID = c.String(nullable: false, maxLength: 50),
                        RoleID = c.Int(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        Password = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.Loans", "StateID", "dbo.States");
            DropForeignKey("dbo.Loans", "LoanTypeID", "dbo.LoanTypes");
            DropForeignKey("dbo.Loans", "LoanStatusID", "dbo.LoanStatus");
            DropForeignKey("dbo.Loans", "InvestorID", "dbo.Investors");
            DropForeignKey("dbo.Loans", "EntityID", "dbo.Entities");
            DropForeignKey("dbo.Loans", "DwellingTypeID", "dbo.DwellingTypes");
            DropForeignKey("dbo.Loans", "ClientID", "dbo.Clients");
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropIndex("dbo.Loans", new[] { "LoanTypeID" });
            DropIndex("dbo.Loans", new[] { "InvestorID" });
            DropIndex("dbo.Loans", new[] { "EntityID" });
            DropIndex("dbo.Loans", new[] { "StateID" });
            DropIndex("dbo.Loans", new[] { "DwellingTypeID" });
            DropIndex("dbo.Loans", new[] { "ClientID" });
            DropIndex("dbo.Loans", new[] { "LoanStatusID" });
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.States");
            DropTable("dbo.LoanTypes");
            DropTable("dbo.LoanStatus");
            DropTable("dbo.Investors");
            DropTable("dbo.Entities");
            DropTable("dbo.DwellingTypes");
            DropTable("dbo.Loans");
            DropTable("dbo.Clients");
        }
    }
}
