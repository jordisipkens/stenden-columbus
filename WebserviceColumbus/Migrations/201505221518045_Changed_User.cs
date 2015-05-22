namespace WebserviceColumbus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changed_User : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Travels", name: "User_ID", newName: "UserID");
            RenameIndex(table: "dbo.Travels", name: "IX_User_ID", newName: "IX_UserID");
            AddColumn("dbo.Users", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Password");
            RenameIndex(table: "dbo.Travels", name: "IX_UserID", newName: "IX_User_ID");
            RenameColumn(table: "dbo.Travels", name: "UserID", newName: "User_ID");
        }
    }
}
