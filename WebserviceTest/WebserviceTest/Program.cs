using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using TestLogin;
using WebserviceColumbus.Authorization;
using WebserviceColumbus.Models.Other;
using WebserviceColumbus.Models.Travel;
using WebserviceColumbus.Models.Travel.Travelogue;

namespace WebserviceTest
{
    internal class Program
    {
        private const string API_KEY = "C0lumbu5";
        private const string LOCAL_BASE_URL = "http://localhost:2758/api/";
        private const string AZURE_BASE_URL = "http://columbus-webservice.azurewebsites.net/api/";
        private static LoginResponse token;

        private static void Main(string[] args)
        {
            Login(LOCAL_BASE_URL + "User/Login");
            while(true) {
                Console.Write("Type partial-URL: ");
                string result = Console.ReadLine().ToLower();
                Console.WriteLine("Processing...\n");

                if(result.Equals("") || result.ToLower().Equals("exit")) {
                    break;
                }
                else if(result.Equals("post")) {
                }
                else if(result.Equals("init")) {
                    PostJSON(LOCAL_BASE_URL + "User/Register", token.Token, new User() { FirstName = "Roy", LastName = "B", Email = "rb@email.com", Username = "Roy", Password = "cxTt7qICqqZWQzG1uTTgbw==" });
                    PostJSON(LOCAL_BASE_URL + "travel", token.Token, new Travel() { Title = "A Trip to New York", UserID = 1, StartDate = DateTime.Now, EndDate = DateTime.Now, Locations = new List<Location>() { new Location() { Note = "Beautifull", Date = DateTime.Now, LocationDetails = new LocationDetails() { Adres = "Libery Island", Name = "Statue of Liberty", PhoneNumber = "555-666", PlaceID = "123ABC", Coordinates = new Coordinates() { Latitude = 1, Longitude = 1 } } }, new Location() { Note = "Gorgeous", Date = DateTime.Now, LocationDetails = new LocationDetails() { Adres = "Upper Manhattan", Name = "Dominos Pizza", PhoneNumber = "555-123", PlaceID = "GHB321", Coordinates = new Coordinates() { Latitude = 1, Longitude = 1 } } } } });
                    PostJSON(LOCAL_BASE_URL + "Travelogue", token.Token, new Travelogue() { TravelID = 1, Published = false, Title = "Travelogue from my trip to New York", PublishedTime = DateTime.Now, Paragraphs = new List<Paragraph>() { new Paragraph() { Text = "Test Paragraph 1" }, new Paragraph() { Text = "Test Paragraph 2" }, new Paragraph() { Text = "Test Paragraph 3" } } });
                }
                else {
                    Request(LOCAL_BASE_URL + result, token);
                }
            }
        }

        private static void Request(string url, LoginResponse token)
        {
            WebRequest req = WebRequest.Create(url);
            req.Headers["Token"] = token.Token;
            try {
                Console.WriteLine(GetResponse(req.GetResponse()) + "\n");
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message + "\n");
            }
        }

        private static void PostJSON(string url, string token, object objectToPost)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers["Token"] = token;

            using(var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream())) {
                string json = JsonSerialization.Serialize(objectToPost);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using(var streamReader = new StreamReader(httpResponse.GetResponseStream())) {
                    var result = streamReader.ReadToEnd();
                }
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message + "\n");
            }
        }

        private static void Login(string url)
        {
            string password = Hash.Encrypt("C0lumbus");
            string userInfo = string.Format("{0}:{1}", "C0lumbus", password);
            string encodedUserInfo = Convert.ToBase64String(Encoding.UTF8.GetBytes(userInfo));
            string credentials = string.Format("{0} {1}", "Basic", encodedUserInfo);

            WebRequest req = WebRequest.Create(url);
            req.Headers["Authorization"] = credentials;
            try {
                token = JsonSerialization.Deserialize<LoginResponse>(GetResponse(req.GetResponse()));
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message + "\nStart the webservice first!\n");
            }
        }

        private static string GetResponse(WebResponse response)
        {
            Stream data = response.GetResponseStream();
            string html;
            using(StreamReader sr = new StreamReader(data)) {
                html = sr.ReadToEnd();
            }
            return html;
        }
    }
}