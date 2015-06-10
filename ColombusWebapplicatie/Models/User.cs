using System.ComponentModel.DataAnnotations;

namespace ColombusWebapplicatie.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Voornaam is vereist")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Achternaam is vereist")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "E-mail is vereist")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gebruikersnaam is vereist")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Wachtwoord is vereist")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage="Wachtwoorden komen niet overeen")]
        public string PasswordRepeat { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }
    }
}