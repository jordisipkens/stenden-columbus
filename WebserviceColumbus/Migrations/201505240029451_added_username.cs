namespace WebserviceColumbus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_username : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Username", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Username");
        }
    }
}
