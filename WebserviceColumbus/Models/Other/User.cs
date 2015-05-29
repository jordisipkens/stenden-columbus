using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using WebserviceColumbus.Model;

namespace WebserviceColumbus.Models.Other
{
    public class User : iDbEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [JsonIgnore, Required]
        public string Password { get; set; }

        //public ICollection<Travel.Travel> Travels { get; set; }
    }
}