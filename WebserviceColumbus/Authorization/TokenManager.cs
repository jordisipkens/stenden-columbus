using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using WebserviceColumbus.Classes.Encryption;

namespace WebserviceColumbus.Authorization
{
    public class TokenManager
    {
        public static bool IsAuthorized(HttpContext httpContext)
        {
            if (CheckToken(httpContext.Request.Headers["Token"])) {
                return true;
            }
            return false;
        }

        private static bool CheckToken(string token)
        {
            if (token != null) {
                string value = Hash.Decrypt(token.Replace("\"", ""), AuthorizationDictionary.REALM);
                if (value != null) {
                    string[] values = value.Split(':');
                    if (values.Length == 2) {
                        DateTime parsedDate = DateTime.Parse(values[0]);
                        if (parsedDate != null && true/*values[1] == IsUser()*/) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        internal static string CreateToken(HttpContext httpContext)
        {
            var request = HttpContext.Current.Request;
            var authHeader = request.Headers["Authorization"];
            if (authHeader != null) {
                var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

                if (authHeaderVal.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase)    // RFC 2617 sec 1.2, "scheme" name is case-insensitive
                    && authHeaderVal.Parameter != null) {
                    return AuthenticateUser(authHeaderVal.Parameter);
                }
            }
            return null;
        }

        private static string AuthenticateUser(string credentials)
        {
            credentials = Encoding.GetEncoding("iso-8859-1").GetString(Convert.FromBase64String(credentials));

            int separator = credentials.IndexOf(':');
            string username = credentials.Substring(0, separator);
            string password = Hash.Decrypt(credentials.Substring(separator + 1));
            if (CheckPassword(username, password)) {
                string token = string.Format("{0}:{1}", DateTime.Now.ToString("MM-dd-yy"), username);
                token = Hash.Encrypt(token, AuthorizationDictionary.REALM);
                return token;
            }
            return null;
        }

        private static bool CheckPassword(string username, string password)
        {
            return true;
            if (username != null && password != null) {
                return username.Equals("C0lumbus") && password.Equals("C0lumbus");
            }
            return false;
            //TODO
        }
    }
}