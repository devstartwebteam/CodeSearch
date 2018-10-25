namespace CodeSearch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdentityAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Snippets",
                c => new
                    {
                        SnippetId = c.Int(nullable: false, identity: true),
                        SnippetName = c.String(),
                        SnippetDescription = c.String(),
                        SnippetContent = c.String(),
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
                "dbo.Notes",
                c => new
                    {
                        NoteId = c.Int(nullable: false, identity: true),
                        NoteTitle = c.DateTime(),
                        NoteContent = c.String(),
                        NoteSnippetId = c.Int(nullable: false),
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
                        Snippet_SnippetId = c.Int(),
                    })
                .PrimaryKey(t => t.TagId)
                .ForeignKey("dbo.Snippets", t => t.Snippet_SnippetId)
                .Index(t => t.Snippet_SnippetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "Snippet_SnippetId", "dbo.Snippets");
            DropForeignKey("dbo.Notes", "Snippet_SnippetId", "dbo.Snippets");
            DropForeignKey("dbo.CategorySnippetAssociations", "Snippet_SnippetId", "dbo.Snippets");
            DropForeignKey("dbo.CategorySnippetAssociations", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.Tags", new[] { "Snippet_SnippetId" });
            DropIndex("dbo.Notes", new[] { "Snippet_SnippetId" });
            DropIndex("dbo.CategorySnippetAssociations", new[] { "Snippet_SnippetId" });
            DropIndex("dbo.CategorySnippetAssociations", new[] { "Category_CategoryId" });
            DropTable("dbo.Tags");
            DropTable("dbo.Notes");
            DropTable("dbo.Categories");
            DropTable("dbo.CategorySnippetAssociations");
            DropTable("dbo.Snippets");
        }
    }
}
