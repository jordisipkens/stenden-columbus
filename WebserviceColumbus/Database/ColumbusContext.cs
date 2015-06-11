using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using WebserviceColumbus.Authorization;
using WebserviceColumbus.Models.Other;
using WebserviceColumbus.Models.Travel;
using WebserviceColumbus.Models.Travel.Travelogue;
//ALWAYS HAVE System.Data.SqlClient!! OTHERWISE IT THROWS DATAEXCEPTION

namespace WebserviceColumbus.Database
{
    public class ColumbusDbContext : DbContext
    {
        public ColumbusDbContext() :
            base(GetConnectionString())
        {
        }

        /// <summary>
        /// Small security for connectionstring password
        /// </summary>
        /// <returns></returns>
        private static string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ColumbusDbContext"].ConnectionString;
            return Encryption.Decrypt(connectionString);
        }

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
