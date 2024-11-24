namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationNotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Note = c.String(nullable: false, maxLength: 8000, unicode: false),
                        ApplicationId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobApplications", t => t.ApplicationId, cascadeDelete: true)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.JobApplications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Company = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Position = c.String(nullable: false, maxLength: 8000, unicode: false),
                        DateApplied = c.DateTime(nullable: false),
                        Status = c.String(nullable: false, maxLength: 8000, unicode: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ApplicationStatusHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(nullable: false, maxLength: 8000, unicode: false),
                        ApplicationId = c.Int(nullable: false),
                        ChangedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobApplications", t => t.ApplicationId, cascadeDelete: true)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Username = c.String(nullable: false, maxLength: 50, unicode: false),
                        Email = c.String(nullable: false, maxLength: 100, unicode: false),
                        Password = c.String(nullable: false, maxLength: 8000, unicode: false),
                        RoleId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.Username, unique: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reminders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReminderDate = c.DateTime(nullable: false),
                        IsSent = c.Boolean(nullable: false),
                        ApplicationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobApplications", t => t.ApplicationId, cascadeDelete: true)
                .Index(t => t.ApplicationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reminders", "ApplicationId", "dbo.JobApplications");
            DropForeignKey("dbo.ApplicationNotes", "ApplicationId", "dbo.JobApplications");
            DropForeignKey("dbo.JobApplications", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.ApplicationStatusHistories", "ApplicationId", "dbo.JobApplications");
            DropIndex("dbo.Reminders", new[] { "ApplicationId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Users", new[] { "Username" });
            DropIndex("dbo.ApplicationStatusHistories", new[] { "ApplicationId" });
            DropIndex("dbo.JobApplications", new[] { "UserId" });
            DropIndex("dbo.ApplicationNotes", new[] { "ApplicationId" });
            DropTable("dbo.Reminders");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.ApplicationStatusHistories");
            DropTable("dbo.JobApplications");
            DropTable("dbo.ApplicationNotes");
        }
    }
}
