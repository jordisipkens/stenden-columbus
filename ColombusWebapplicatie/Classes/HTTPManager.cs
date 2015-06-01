﻿using ColombusWebapplicatie.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace ColombusWebapplicatie.Classes
{
    public static class HTTPManager
    {
        public const string LOCAL_BASE_URL = "http://localhost:2758/api/";
        public const string AZURE_BASE_URL = "http://columbus-webservice.azurewebsites.net/api/";

        /// <summary>
        /// Starts a login request.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="responseBase"></param>
        public static void LoginRequest(User user, HttpResponseBase responseBase = null)
        {
            string userInfo = string.Format("{0}:{1}", user.Username, Encryption.Encrypt(user.Password));
            string encodedUserInfo = Convert.ToBase64String(Encoding.UTF8.GetBytes(userInfo));
            string credentials = string.Format("{0} {1}", "Basic", encodedUserInfo);

            Token token = GetRequest<Token>("User/Login", new Dictionary<string, string>() { { "Authorization", credentials } }, AZURE_BASE_URL);
            CookieManager.CreateCookie(responseBase, "Token", token.TokenString);
        }

        /// <summary>
        /// Starts a GET request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="baseUrl"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static T GetRequest<T>(string url, Dictionary<string, string> headers = null, string baseUrl = AZURE_BASE_URL, HttpRequestBase request = null)
        {
            HttpWebResponse q = (HttpWebResponse)CreateRequest(url, headers, baseUrl, request).GetResponse();
            return ReadResponse<T>(q);
        }

        /// <summary>
        /// Starts a POST request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="objectToPost"></param>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public static T PostRequest<T>(string url, object objectToPost, string baseUrl = AZURE_BASE_URL)
        {
            return default(T);
        }

        /// <summary>
        /// Creates the request and adds headers if necessary.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="baseUrl"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private static WebRequest CreateRequest(string url, Dictionary<string, string> headers = null, string baseUrl = AZURE_BASE_URL, HttpRequestBase request = null)
        {
            WebRequest req = WebRequest.Create(baseUrl + url);
            if(headers != null) {
                foreach(KeyValuePair<string, string> header in headers) {
                    req.Headers[header.Key] = header.Value;
                }
            }
            if(request != null) {
                req.Headers["Token"] = CookieManager.GetCookie(request, "Token");
            }
            return req;
        }

        /// <summary>
        /// Reads the response and converts it to an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        private static T ReadResponse<T>(HttpWebResponse response)
        {
            Stream data = response.GetResponseStream();
            string html;
            using(StreamReader sr = new StreamReader(data)) {
                html = sr.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<T>(html);
        }
    }
}