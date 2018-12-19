namespace CodeSearch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewUpdates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Snippets",
                c => new
                    {
                        SnippetId = c.Int(nullable: false, identity: true),
                        SnippetName = c.String(),
                        SnippetContent = c.String(),
                        SnippetDescription = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                        SnippetLanguage = c.String(),
                        ReferenceURL = c.String(),
                    })
                .PrimaryKey(t => t.SnippetId);
            
            CreateTable(
                "dbo.CategorySnippetAssociations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryAssociationId = c.Int(nullable: false),
                        SnippetAssociationId = c.Int(nullable: false),
                        Category_CategoryId = c.Int(),
                        Snippet_SnippetId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId)
                .ForeignKey("dbo.Snippets", t => t.Snippet_SnippetId)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.Snippet_SnippetId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        CategoryDescription = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                        ImageURL = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.ProjectCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectProjectId = c.Int(nullable: false),
                        CategoryCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectProjectId, cascadeDelete: true)
                .Index(t => t.ProjectProjectId)
                .Index(t => t.CategoryCategoryId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        ProjectTitle = c.String(),
                        ProjectDescription = c.String(),
                        Created = c.String(),
                        Modified = c.String(),
                    })
                .PrimaryKey(t => t.ProjectId);
            
            CreateTable(
                "dbo.ProjectNotes",
                c => new
                    {
                        ProjectNoteId = c.Int(nullable: false, identity: true),
                        ProjectNoteTitle = c.String(),
                        ProjectNoteContent = c.String(),
                        Created = c.String(),
                        Modified = c.String(),
                        ProjectProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectNoteId)
                .ForeignKey("dbo.Projects", t => t.ProjectProjectId, cascadeDelete: true)
                .Index(t => t.ProjectProjectId);
            
            CreateTable(
                "dbo.ProjectSnippets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectProjectId = c.Int(nullable: false),
                        Snippet_SnippetId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Snippets", t => t.Snippet_SnippetId)
                .Index(t => t.ProjectProjectId)
                .Index(t => t.Snippet_SnippetId);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        NoteId = c.Int(nullable: false, identity: true),
                        NoteTitle = c.String(),
                        NoteContent = c.String(),
                        NoteSnippetId = c.Int(nullable: false),
                        NoteCount = c.Int(),
                        Snippet_SnippetId = c.Int(),
                    })
                .PrimaryKey(t => t.NoteId)
                .ForeignKey("dbo.Snippets", t => t.Snippet_SnippetId)
                .Index(t => t.Snippet_SnippetId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        TagName = c.String(),
                        TagSnippetId = c.Int(nullable: false),
                        TagCategory = c.String(),
                        Snippet_SnippetId = c.Int(),
                    })
                .PrimaryKey(t => t.TagId)
                .ForeignKey("dbo.Snippets", t => t.Snippet_SnippetId)
                .Index(t => t.Snippet_SnippetId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tags", "Snippet_SnippetId", "dbo.Snippets");
            DropForeignKey("dbo.Notes", "Snippet_SnippetId", "dbo.Snippets");
            DropForeignKey("dbo.CategorySnippetAssociations", "Snippet_SnippetId", "dbo.Snippets");
            DropForeignKey("dbo.ProjectSnippets", "Snippet_SnippetId", "dbo.Snippets");
            DropForeignKey("dbo.ProjectSnippets", "ProjectProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectNotes", "ProjectProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectCategories", "ProjectProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectCategories", "CategoryCategoryId", "dbo.Categories");
            DropForeignKey("dbo.CategorySnippetAssociations", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Tags", new[] { "Snippet_SnippetId" });
            DropIndex("dbo.Notes", new[] { "Snippet_SnippetId" });
            DropIndex("dbo.ProjectSnippets", new[] { "Snippet_SnippetId" });
            DropIndex("dbo.ProjectSnippets", new[] { "ProjectProjectId" });
            DropIndex("dbo.ProjectNotes", new[] { "ProjectProjectId" });
            DropIndex("dbo.ProjectCategories", new[] { "CategoryCategoryId" });
            DropIndex("dbo.ProjectCategories", new[] { "ProjectProjectId" });
            DropIndex("dbo.CategorySnippetAssociations", new[] { "Snippet_SnippetId" });
            DropIndex("dbo.CategorySnippetAssociations", new[] { "Category_CategoryId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Tags");
            DropTable("dbo.Notes");
            DropTable("dbo.ProjectSnippets");
            DropTable("dbo.ProjectNotes");
            DropTable("dbo.Projects");
            DropTable("dbo.ProjectCategories");
            DropTable("dbo.Categories");
            DropTable("dbo.CategorySnippetAssociations");
            DropTable("dbo.Snippets");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
