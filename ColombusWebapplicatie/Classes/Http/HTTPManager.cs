using ColombusWebapplicatie.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace ColombusWebapplicatie.Classes.Http
{
    public static class HTTPManager
    {
        public const string LOCAL_BASE_URL = "http://localhost:2758/api/";
        public const string AZURE_BASE_URL = "http://columbus-webservice.azurewebsites.net/api/";
        public const string GOOGLE_PLACES_BASE_URL = "https://maps.googleapis.com/maps/api/place/textsearch/json?query=";

        public const string GOOGLE_PLACES_API_KEY = "AIzaSyDpXa5VtOKNRA8obETZwkV7vbHzjio-17k";

        /// <summary>
        /// Used for a default web request to any website/webservice.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static T GetRequest<T>(string url, Dictionary<string, string> headers = null)
        {
            HttpWebResponse response = (HttpWebResponse)CreateRequest(url, headers).GetResponse();
            return ReadResponse<T>(response);
        }

        /// <summary>
        /// Starts a GET request to the webservice.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="baseUrl"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static T WebserviceGetRequest<T>(string url, HttpRequestBase request, string baseUrl = AZURE_BASE_URL, Dictionary<string, string> headers = null)
        {
            HttpWebResponse reponse = (HttpWebResponse)CreateRequest(baseUrl + url, headers, request).GetResponse();
            return ReadResponse<T>(reponse);
        }

        /// <summary>
        /// Starts a POST request to the webservice.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="objectToPost"></param>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public static T WebservicePostRequest<T>(string url, HttpRequestBase request, object objectToPost, string baseUrl = AZURE_BASE_URL)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)CreateRequest(baseUrl + url, null, request);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using(var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
                streamWriter.Write(JsonConvert.SerializeObject(objectToPost));
                streamWriter.Flush();
                streamWriter.Close();
            }

            return ReadResponse<T>((HttpWebResponse)httpWebRequest.GetResponse());
        }

        /// <summary>
        /// Starts a login request.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="responseBase"></param>
        public static LoginResponse LoginRequest(User user, HttpResponseBase responseBase = null)
        {
            string userInfo = string.Format("{0}:{1}", user.Username, Encryption.Encrypt(user.Password));
            string encodedUserInfo = Convert.ToBase64String(Encoding.UTF8.GetBytes(userInfo));
            string credentials = string.Format("{0} {1}", "Basic", encodedUserInfo);

            LoginResponse loginRequest = WebserviceGetRequest<LoginResponse>("User/Login", null, AZURE_BASE_URL, new Dictionary<string, string>() { { "Authorization", credentials } });
            return loginRequest;
        }

        /// <summary>
        /// Creates the request and adds headers if necessary.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <param name="baseUrl"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private static WebRequest CreateRequest(string url, Dictionary<string, string> headers = null, HttpRequestBase request = null)
        {
            WebRequest req = WebRequest.Create(url);
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

            try {
                return JsonConvert.DeserializeObject<T>(html);
            }
            catch(Exception) {
                return default(T);
            }
        }
    }
}