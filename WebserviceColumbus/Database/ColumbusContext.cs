using System.Data.Entity;
using WebserviceColumbus.Models.Other;
using WebserviceColumbus.Models.Travel;
using WebserviceColumbus.Models.Travel.Travelogue;

namespace WebserviceColumbus.Database
{
    public class ColumbusDbContext : DbContext
    {
        /*public ColumbusDbContext()
            : base(connectionString)
        {
        }*/

        public DbSet<Travel> Travels { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<LocationDetails> LocationDetails { get; set; }

        public DbSet<Coordinates> Coordinates { get; set; }

        public DbSet<Travelogue> Travelogues { get; set; }

        public DbSet<Paragraph> Paragraphs { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<User> Users { get; set; }
    }
}