using System.Collections.Generic;
using System.Data.Entity.Migrations;
using WebserviceColumbus.IO;
using WebserviceColumbus.Database;
using WebserviceColumbus.Models.Travel;

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
            //List<Travel> travels = JsonSerialization.Deserialize<List<Travel>>(IOManager.ReadFile(IOManager.GetProjectFilePath("Resources/DummyData/Travel.json")));
            //context.Travels.AddOrUpdate(travels[0], travels[1]);

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
