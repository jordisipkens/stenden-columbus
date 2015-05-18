using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using WebserviceColumbus.Classes;

namespace WebserviceColumbus.HttpModules
{
    /// <summary>
    /// Copyright of Mircosoft
    /// </summary>
    public class BasicAuthHttpModule : IHttpModule
    {
        private const string Realm = "Columbus";

        /// <summary>
        /// Initiates the handlers.
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += OnApplicationAuthenticateRequest;
            context.EndRequest += OnApplicationEndRequest;
        }

        private static void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null) {
                HttpContext.Current.User = principal;
            }
        }

        /// <summary>
        /// Checks wether the given username or password are correct.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private static bool CheckPassword(string username, string password)
        {
            return username.Equals("C0lumbus") && password.Equals("C0lumbus");
            //TODO
        }

        private static void AuthenticateUser(string credentials)
        {
            try {
                var encoding = Encoding.GetEncoding("iso-8859-1");
                credentials = encoding.GetString(Convert.FromBase64String(credentials));

                int separator = credentials.IndexOf(':');
                string name = credentials.Substring(0, separator);
                string password = credentials.Substring(separator + 1);

                if (CheckPassword(name, password)) {
                    var identity = new GenericIdentity(name);
                    SetPrincipal(new GenericPrincipal(identity, null)); //Correct username or password. Principal is set.
                }
                else {
                    HttpContext.Current.Response.StatusCode = 401;  // Invalid username or password.
                }
            }
            catch (FormatException) {
                HttpContext.Current.Response.StatusCode = 401;  // Credentials were not formatted correctly.
            }
        }

        public static void OnApplicationAuthenticateRequest(object sender, EventArgs e)
        {
            var request = HttpContext.Current.Request;
            var authHeader = request.Headers["Authorization"];
            if (authHeader != null) {
                var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

                if (authHeaderVal.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase)    // RFC 2617 sec 1.2, "scheme" name is case-insensitive
                    && authHeaderVal.Parameter != null) {
                    AuthenticateUser(authHeaderVal.Parameter);
                }
            }
        }

        /// <summary>
        /// If the request was unauthorized a WWW-Authenticate Header is added to response.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnApplicationEndRequest(object sender, EventArgs e)
        {
            var response = HttpContext.Current.Response;
            if (response.StatusCode == 401) {
                response.Headers.Add("WWW-Authenticate",
                    string.Format("Basic realm=\"{0}\"", Realm));
            }
        }

        /// <summary>
        /// Disposes itself.
        /// </summary>
        public void Dispose()
        {
        }
    }
}