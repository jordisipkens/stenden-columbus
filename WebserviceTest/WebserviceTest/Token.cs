using WebserviceColumbus.Models.Other;

namespace TestLogin
{
    internal class LoginResponse
    {
        public User User { get; set; }

        public string Token { get; set; }
    }
}