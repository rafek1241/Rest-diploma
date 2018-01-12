namespace Rest.Web.Engineer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2012018 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "Type", c => c.String());
            DropColumn("dbo.Files", "MimeType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "MimeType", c => c.String());
            DropColumn("dbo.Files", "Type");
        }
    }
}
