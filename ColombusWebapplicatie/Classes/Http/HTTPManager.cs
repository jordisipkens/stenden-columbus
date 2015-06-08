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
    public static class HttpManager
    {
        public const string LOCAL_BASE_URL = "http://localhost:2758/api/";
        public const string AZURE_BASE_URL = "http://columbus-webservice.azurewebsites.net/api/";

        public const string GOOGLE_PLACES_BASE_URL = "https://maps.googleapis.com/maps/api/place/";
        public const string GOOGLE_PLACES_API_KEY = "AIzaSyDpXa5VtOKNRA8obETZwkV7vbHzjio-17k";

        /// <summary>
        /// Used for a default web request to any website/webservice.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static T GoogleGetRequest<T>(string method, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null)
        {
            parameters.Add("key", GOOGLE_PLACES_API_KEY);
            string url = string.Format("{0}{1}/json", GOOGLE_PLACES_BASE_URL, method);
            return ReadResponse<T>(CreateRequest(url, parameters, headers));
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
        public static T WebserviceGetRequest<T>(string url, HttpRequestBase request, Dictionary<string, string> headers = null, Dictionary<string, string> parameters = null, string baseUrl = AZURE_BASE_URL)
        {
            return ReadResponse<T>(CreateRequest(baseUrl + url, parameters, headers, request));
        }

        /// <summary>
        /// Starts a POST request to the webservice.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="objectToPost"></param>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public static T WebservicePostRequest<T>(string url, HttpRequestBase request, T objectToPost, string baseUrl = AZURE_BASE_URL)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)CreateRequest(baseUrl + url, null, request);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using(var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
                streamWriter.Write(JsonConvert.SerializeObject(objectToPost));
                streamWriter.Flush();
                streamWriter.Close();
            }

            return ReadResponse<T>((WebRequest)httpWebRequest);
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

            LoginResponse loginRequest = WebserviceGetRequest<LoginResponse>("User/Login", null, new Dictionary<string, string>() { { "Authorization", credentials } });
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

        private static WebRequest CreateRequest(string url, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null, HttpRequestBase request = null)
        {
            return CreateRequest(BuildUrl(url, parameters), headers, request);
        }

        /// <summary>
        /// Reads the response and converts it to an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        private static T ReadResponse<T>(WebRequest request)
        {
            try {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream data = response.GetResponseStream();
                using(StreamReader sr = new StreamReader(data)) {
                    return JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
                }
            }
            catch(WebException) {
                return default(T);
            }
        }

        /// <summary>
        /// Builds an url by adding all parameters to the base
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static string BuildUrl(string url, Dictionary<string, string> parameters = null)
        {
            if(parameters != null && parameters.Count > 0) {
                url += "?";
                foreach(KeyValuePair<string, string> parameter in parameters) {
                    url += string.Format("{0}={1}&", parameter.Key, parameter.Value);
                }
                url = url.Remove(url.Length - 1);
            }
            return url;
        }
    }
}