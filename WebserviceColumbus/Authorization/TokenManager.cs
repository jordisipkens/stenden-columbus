using System;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using WebserviceColumbus.Database;

namespace WebserviceColumbus.Authorization
{
    public class TokenManager
    {
        #region Getters

        private static string GetToken()
        {
            string token = HttpContext.Current.Request.Headers["Token"];
            token = token.Replace("\"", "");
            if(token != null) {
                string value = Hash.Decrypt(token);

                if(value != null) {
                    return value;
                }
            }
            return null;
        }

        private static string[] GetTokenValues()
        {
            string token = GetToken();
            if(token != null) {
                return token.Split('/');
            }
            return null;
        }

        public static string GetUsernameFromToken()
        {
            return GetTokenValues()[1];
        }

        #endregion Getters

        public static bool IsAuthorized()
        {
            string[] values = GetTokenValues();
            if(values != null && values.Length == 2) {
                DateTime parsedDate = DateTime.Parse(values[0], null, DateTimeStyles.RoundtripKind);

                if(CheckDate(parsedDate) && values[1].Equals("C0lumbus")) {    //TODO Change: UserDbManager.ValidateUser(values[1])
                    return true;
                }
            }
            return false;
        }

        public static string CreateToken()
        {
            string authHeader = HttpContext.Current.Request.Headers["Authorization"];
            if(authHeader != null) {
                AuthenticationHeaderValue authHeaderValue = AuthenticationHeaderValue.Parse(authHeader);

                if(authHeaderValue.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase)
                    && authHeaderValue.Parameter != null) {
                    return AuthenticateUser(authHeaderValue.Parameter);
                }
            }
            return null;
        }

        private static string AuthenticateUser(string credentials)
        {
            credentials = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(credentials));

            int separator = credentials.IndexOf(':');
            string username = credentials.Substring(0, separator);
            string password = credentials.Substring(separator + 1);

            if (username.Equals("C0lumbus") && password.Equals("3b2k77GHcx9aBCw2qKwB7b287/fQ+fxUohpti+2eLeA=")) {    //TODO Change: UserDbManager.ValidateUser(username, password)
                string token = string.Format("{0}/{1}", DateTime.Now.ToString("u"), username);
                token = Hash.Encrypt(token);
                return token;
            }
            return null;
        }

        private static bool CheckDate(DateTime time)
        {
            TimeSpan diff = DateTime.Now - time;
            return diff.TotalHours <= 3;
        }
    }
}