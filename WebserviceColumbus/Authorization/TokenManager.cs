using System;
using System.Globalization;
using System.Net.Http.Headers;
using System.Web;
using WebserviceColumbus.Database;
using WebserviceColumbus.Models.Other;

namespace WebserviceColumbus.Authorization
{
    public class TokenManager
    {
        private static TimeSpan TIMEOUT = new TimeSpan(3, 0, 0);

        #region Getters

        /// <summary>
        /// Gets the token and decrypts it for use.
        /// </summary>
        /// <returns></returns>
        private static string GetToken()
        {
            string token = HttpContext.Current.Request.Headers["Token"];
            token = token.Replace("\"", "");
            if(token != null) {
                string value = Encryption.Decrypt(token);

                if(value != null) {
                    return value;
                }
            }
            return null;
        }

        /// <summary>
        /// Get both values from the token header.
        /// </summary>
        /// <returns></returns>
        private static string[] GetTokenValues()
        {
            string token = GetToken();
            if(token != null) {
                return token.Split('/');
            }
            return null;
        }

        /// <summary>
        /// Returns only the username in the token.
        /// </summary>
        /// <returns></returns>
        public static string GetUsernameFromToken()
        {
            return GetTokenValues()[1];
        }

        #endregion Getters

        /// <summary>
        /// Checks if the token is valid and if the user is valid.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Checks if the Authorization header is set correctly, and if so returns a token.
        /// </summary>
        /// <returns></returns>
        public static LoginResponse CreateToken()
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

        /// <summary>
        /// Checks the Authorization header for it values and checks the user and password.
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        private static LoginResponse AuthenticateUser(string credentials)
        {
            credentials = Encryption.DecryptUTF8(credentials);

            int separator = credentials.IndexOf(':');
            string username = credentials.Substring(0, separator);
            string password = credentials.Substring(separator + 1);

            if(username.Equals("C0lumbus") && password.Equals("cxTt7qICqqZWQzG1uTTgbw==")) {    //TODO Change: UserDbManager.ValidateUser(username, password)
                User user = new UserDbManager().GetEntity(username);
                string token = string.Format("{0}/{1}", DateTime.Now.ToString("u"), username);
                token = Encryption.Encrypt(token);
                return new LoginResponse() { Token = token, User = user };
            }
            return null;
        }

        /// <summary>
        /// Checks if the token is still valid.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private static bool CheckDate(DateTime time)
        {
            TimeSpan diff = DateTime.Now - time;
            return diff < TIMEOUT;
        }
    }
}
