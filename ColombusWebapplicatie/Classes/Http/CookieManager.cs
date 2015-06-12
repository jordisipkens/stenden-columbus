﻿using System.Web;

namespace ColombusWebapplicatie.Classes.Http
{
    public static class CookieManager
    {
        /// <summary>
        /// Creates a cookie with the given name and value.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void CreateCookie(HttpResponseBase context, string name, string value)
        {
            if(context != null) {
                HttpCookie myCookie = new HttpCookie(name, value);
                context.Cookies.Add(myCookie);
                context.SetCookie(myCookie);
            }
        }

        /// <summary>
        /// Returns the cookie with the given name.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetCookie(HttpRequestBase context, string name)
        {
            if(context != null && context.Cookies[name] != null) {
                return context.Cookies[name].Value;
            }
            return null;
        }
    }
}