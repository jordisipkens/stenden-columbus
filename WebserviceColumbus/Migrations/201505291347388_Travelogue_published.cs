namespace WebserviceColumbus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Travelogue_published : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Travelogues", "Published", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Travelogues", "Published");
        }
    }
}
