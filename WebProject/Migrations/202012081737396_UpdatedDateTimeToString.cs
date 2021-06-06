namespace WebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedDateTimeToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Experiences", "startDate", c => c.String(nullable: false));
            AlterColumn("dbo.Experiences", "endDate", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Experiences", "endDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Experiences", "startDate", c => c.DateTime(nullable: false));
        }
    }
}
