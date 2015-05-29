namespace WebserviceColumbus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Travelogue_Publish_Rating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RatingValue = c.Double(nullable: false),
                        Travelogue_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Travelogues", t => t.Travelogue_ID)
                .Index(t => t.Travelogue_ID);
            
            AddColumn("dbo.Travelogues", "PublishedTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "Travelogue_ID", "dbo.Travelogues");
            DropIndex("dbo.Ratings", new[] { "Travelogue_ID" });
            DropColumn("dbo.Travelogues", "PublishedTime");
            DropTable("dbo.Ratings");
        }
    }
}
