namespace WebProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstPush : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Experiences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        rollNum = c.String(nullable: false, maxLength: 128),
                        organization = c.String(nullable: false),
                        location = c.String(nullable: false),
                        position = c.String(nullable: false),
                        details = c.String(),
                        startDate = c.DateTime(nullable: false),
                        endDate = c.DateTime(nullable: false),
                        stipend = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentRecords", t => t.rollNum, cascadeDelete: true)
                .Index(t => t.rollNum);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        rollNum = c.String(nullable: false, maxLength: 128),
                        text = c.String(nullable: false),
                        status = c.String(nullable: false),
                        time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentRecords", t => t.rollNum, cascadeDelete: true)
                .Index(t => t.rollNum);
            
            CreateTable(
                "dbo.StudentRecords",
                c => new
                    {
                        rollNum = c.String(nullable: false, maxLength: 128),
                        name = c.String(nullable: false),
                        discipline = c.String(nullable: false),
                        cgpa = c.String(nullable: false),
                        batch = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.rollNum);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "rollNum", "dbo.StudentRecords");
            DropForeignKey("dbo.Experiences", "rollNum", "dbo.StudentRecords");
            DropIndex("dbo.Messages", new[] { "rollNum" });
            DropIndex("dbo.Experiences", new[] { "rollNum" });
            DropTable("dbo.StudentRecords");
            DropTable("dbo.Messages");
            DropTable("dbo.Experiences");
        }
    }
}
