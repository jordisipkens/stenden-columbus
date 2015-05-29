using System.Data.Entity;
using WebserviceColumbus.Models.Other;
using WebserviceColumbus.Models.Travel;
using WebserviceColumbus.Models.Travel.Travelogue;

namespace WebserviceColumbus.Database
{
    public class ColumbusDbContext : DbContext
    {
        /*public ColumbusDbContext() : 
            base("workstation id=Stenden-Columbus.mssql.somee.com;packet size=4096;user id=RoyB_SQLLogin_1;pwd=2hqlc93kkr;data source=Stenden-Columbus.mssql.somee.com;persist security info=False;initial catalog=Stenden-Columbus")
        { }*/

        public DbSet<Travel> Travels { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<LocationDetails> LocationDetails { get; set; }

        public DbSet<Coordinates> Coordinates { get; set; }

        public DbSet<Travelogue> Travelogues { get; set; }

        public DbSet<Paragraph> Paragraphs { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<User> Users { get; set; }
    }
}