namespace WebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSubjectInMessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "subject", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "subject");
        }
    }
}
