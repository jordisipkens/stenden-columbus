namespace WebserviceColumbus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rating_Add_UserID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "userID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ratings", "userID");
        }
    }
}
