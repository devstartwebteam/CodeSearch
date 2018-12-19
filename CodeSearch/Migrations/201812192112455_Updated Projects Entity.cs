namespace CodeSearch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedProjectsEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "ProjectDoc", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "ProjectDoc");
        }
    }
}
