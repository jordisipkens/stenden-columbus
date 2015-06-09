using System.Data.Entity.Migrations;
using WebserviceColumbus.Database;

namespace WebserviceColumbus.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ColumbusDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ColumbusDbContext context)
        {
        }
    }
}