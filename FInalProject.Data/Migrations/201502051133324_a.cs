namespace FInalProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        AnswerId = c.Int(nullable: false, identity: true),
                        Answertitle = c.String(),
                        IsCorrectAnswer = c.Boolean(nullable: false),
                        QuestinId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnswerId)
                .ForeignKey("dbo.Questions", t => t.QuestinId, cascadeDelete: true)
                .Index(t => t.QuestinId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        QuestionTitle = c.String(nullable: false),
                        QuestionDescription = c.String(maxLength: 2000),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
                        QuizOrSurveyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.QuizSurveys", t => t.QuizOrSurveyId, cascadeDelete: true)
                .Index(t => t.QuizOrSurveyId);
            
            CreateTable(
                "dbo.QuizSurveys",
                c => new
                    {
                        QuizSurveyId = c.Int(nullable: false, identity: true),
                        QuestionSurveyTitle = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedAt = c.DateTime(precision: 7, storeType: "datetime2"),
                        Type = c.Int(nullable: false),
                        Description = c.String(maxLength: 2000),
                        Reference = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.QuizSurveyId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.QuestionAnswers",
                c => new
                    {
                        QuestionAnserId = c.Int(nullable: false, identity: true),
                        QuizId = c.Int(nullable: false),
                        AnswerId = c.Int(nullable: false),
                        QuizTakerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionAnserId)
                .ForeignKey("dbo.Answers", t => t.AnswerId, cascadeDelete: true)
                .ForeignKey("dbo.QuizSurveys", t => t.QuizId, cascadeDelete: false)
                .ForeignKey("dbo.QuizTakers", t => t.QuizTakerId, cascadeDelete: true)
                .Index(t => t.QuizId)
                .Index(t => t.AnswerId)
                .Index(t => t.QuizTakerId);
            
            CreateTable(
                "dbo.QuizTakers",
                c => new
                    {
                        QuizTakerId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        FullName = c.String(),
                        QuizSurveyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuizTakerId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserQuizs",
                c => new
                    {
                        UserQuizId = c.Int(nullable: false, identity: true),
                        QuziId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserQuizId)
                .ForeignKey("dbo.QuizSurveys", t => t.QuziId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.QuziId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserQuizs", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserQuizs", "QuziId", "dbo.QuizSurveys");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.QuestionAnswers", "QuizTakerId", "dbo.QuizTakers");
            DropForeignKey("dbo.QuestionAnswers", "QuizId", "dbo.QuizSurveys");
            DropForeignKey("dbo.QuestionAnswers", "AnswerId", "dbo.Answers");
            DropForeignKey("dbo.Answers", "QuestinId", "dbo.Questions");
            DropForeignKey("dbo.QuizSurveys", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Questions", "QuizOrSurveyId", "dbo.QuizSurveys");
            DropIndex("dbo.UserQuizs", new[] { "UserId" });
            DropIndex("dbo.UserQuizs", new[] { "QuziId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.QuestionAnswers", new[] { "QuizTakerId" });
            DropIndex("dbo.QuestionAnswers", new[] { "AnswerId" });
            DropIndex("dbo.QuestionAnswers", new[] { "QuizId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.QuizSurveys", new[] { "UserId" });
            DropIndex("dbo.Questions", new[] { "QuizOrSurveyId" });
            DropIndex("dbo.Answers", new[] { "QuestinId" });
            DropTable("dbo.UserQuizs");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.QuizTakers");
            DropTable("dbo.QuestionAnswers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.QuizSurveys");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
