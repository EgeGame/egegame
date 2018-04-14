namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Badge",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageSrc = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(),
                        Name = c.String(),
                        Avatar = c.String(),
                        Points = c.Int(nullable: false),
                        UsePoints = c.Int(nullable: false),
                        TeacherId = c.Int(),
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
                        CurrentExam_Id = c.Int(),
                        School_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exams", t => t.CurrentExam_Id)
                .ForeignKey("dbo.Schools", t => t.School_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.TeacherId)
                .Index(t => t.TeacherId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.CurrentExam_Id)
                .Index(t => t.School_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(),
                        FinishTime = c.DateTime(),
                        Points = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Exam_Id = c.Int(),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exams", t => t.Exam_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Exam_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserTaskAttempts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserAnswer = c.String(),
                        Points = c.Int(nullable: false),
                        CheckTime = c.DateTime(),
                        IsChecked = c.Boolean(),
                        Comment = c.String(),
                        LinksAsString = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ExamTask_Id = c.Int(nullable: false),
                        Train_Id = c.Int(nullable: false),
                        Reviewer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExamTasks", t => t.ExamTask_Id, cascadeDelete: true)
                .ForeignKey("dbo.Trains", t => t.Train_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Reviewer_Id)
                .Index(t => t.ExamTask_Id)
                .Index(t => t.Train_Id)
                .Index(t => t.Reviewer_Id);
            
            CreateTable(
                "dbo.ExamTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Text = c.String(nullable: false),
                        Answer = c.String(nullable: false),
                        TaskTopic_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaskTopics", t => t.TaskTopic_Id, cascadeDelete: true)
                .Index(t => t.TaskTopic_Id);
            
            CreateTable(
                "dbo.TaskTopics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        Number = c.Int(nullable: false),
                        PointsPerTask = c.Int(nullable: false),
                        IsShort = c.Boolean(nullable: false),
                        Code = c.String(),
                        Exam_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exams", t => t.Exam_Id, cascadeDelete: true)
                .Index(t => t.Number, unique: true)
                .Index(t => t.Exam_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserBadges",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Badge_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Badge_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Badge", t => t.Badge_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Badge_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Trains", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserTaskAttempts", "Reviewer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserTaskAttempts", "Train_Id", "dbo.Trains");
            DropForeignKey("dbo.UserTaskAttempts", "ExamTask_Id", "dbo.ExamTasks");
            DropForeignKey("dbo.ExamTasks", "TaskTopic_Id", "dbo.TaskTopics");
            DropForeignKey("dbo.TaskTopics", "Exam_Id", "dbo.Exams");
            DropForeignKey("dbo.Trains", "Exam_Id", "dbo.Exams");
            DropForeignKey("dbo.AspNetUsers", "TeacherId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "School_Id", "dbo.Schools");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "CurrentExam_Id", "dbo.Exams");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserBadges", "Badge_Id", "dbo.Badge");
            DropForeignKey("dbo.UserBadges", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserBadges", new[] { "Badge_Id" });
            DropIndex("dbo.UserBadges", new[] { "User_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TaskTopics", new[] { "Exam_Id" });
            DropIndex("dbo.TaskTopics", new[] { "Number" });
            DropIndex("dbo.ExamTasks", new[] { "TaskTopic_Id" });
            DropIndex("dbo.UserTaskAttempts", new[] { "Reviewer_Id" });
            DropIndex("dbo.UserTaskAttempts", new[] { "Train_Id" });
            DropIndex("dbo.UserTaskAttempts", new[] { "ExamTask_Id" });
            DropIndex("dbo.Trains", new[] { "User_Id" });
            DropIndex("dbo.Trains", new[] { "Exam_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "School_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "CurrentExam_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "TeacherId" });
            DropTable("dbo.UserBadges");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TaskTopics");
            DropTable("dbo.ExamTasks");
            DropTable("dbo.UserTaskAttempts");
            DropTable("dbo.Trains");
            DropTable("dbo.Schools");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Exams");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Badge");
        }
    }
}
