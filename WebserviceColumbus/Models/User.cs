using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using WebserviceColumbus.Database;

namespace WebserviceColumbus.Models
{
    public class User : iDbEntity
    {
        public int ID { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        //public ICollection<Travel.Travel> Travels { get; set; }
    }
}