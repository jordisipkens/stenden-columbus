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
        private static string REALM = "apiColumbus";

        #region Getters

        private static string GetToken()
        {
            string token = HttpContext.Current.Request.Headers["Token"];
            token = token.Replace("\"", "");
            if(token != null) {
                string value = Hash.Decrypt(token, REALM);

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

                if(CheckDate(parsedDate) && UserDbManager.ValidateUser(values[1])) {
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

                if(authHeaderValue.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase)    // RFC 2617 sec 1.2, "scheme" name is case-insensitive
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

            if(UserDbManager.ValidateUser(username, password)) {
                string token = string.Format("{0}/{1}", DateTime.Now.ToString("u"), username);
                token = Hash.Encrypt(token, REALM);
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