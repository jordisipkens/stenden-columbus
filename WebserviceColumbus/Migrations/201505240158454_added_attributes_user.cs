namespace WebserviceColumbus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_attributes_user : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE [dbo].[Users] SET Username = 'C0lumbus' WHERE Username IS NULL");
            Sql("UPDATE [dbo].[Users] SET Password = '3b2k77GHcx9aBCw2qKwB7b287/fQ+fxUohpti+2eLeA=' WHERE Password IS NULL");
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "FirstName", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Username", c => c.String());
        }
    }
}
