using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestLogin;
using WebserviceColumbus.Authorization;

namespace WebserviceTest
{
    class Program
    {
        private const string API_KEY = "C0lumbu5";
        private const string LOCAL_BASE_URL = "http://localhost:2758/api/";
        private const string AZURE_BASE_URL = "http://columbus-webservice.azurewebsites.net/api/";
        private static Token token = JsonSerialization.Deserialize<Token>(Login(LOCAL_BASE_URL + "User/Login"));

        private static void Main(string[] args)
        {
            while(true) {
                string result = Console.ReadLine();

                if(result.Equals("") || result.ToLower().Equals("exit")) {
                    break;
                }
                else if(result.ToLower().Equals("post")) {
                    //PostJSON(LOCAL_BASE_URL + "Travelogue", token.TokenString, new Travelogue() { ID = 1, TravelID = 2, Paragraphs = new List<Paragraph>() { new Paragraph() { ID = 1, Text = "Test Paragraph UPDATED TWICE" } } });
                    //PostJSON(LOCAL_BASE_URL + "travel", token.TokenString, new Travel() { Title="A Trip to New York", UserID = 2, StartDate = DateTime.Now, EndDate = DateTime.Now, Locations = new List<Location>() { new Location() { Note = "Beautifull", Date = DateTime.Now, LocationDetails = new LocationDetails() { Name="Statue of Liberty", PhoneNumber="555-666", PlaceID="123ABC", Coordinates = new Coordinates() { Latitude = 1, Longitude = 1 } } } } });
                }
                else {
                    Request(LOCAL_BASE_URL + result, token);
                }
            }
        }

        private static string Login(string url)
        {
            string password = Hash.Encrypt("C0lumbus");
            string userInfo = string.Format("{0}:{1}", "C0lumbus", password);
            string encodedUserInfo = Convert.ToBase64String(Encoding.UTF8.GetBytes(userInfo));
            string credentials = string.Format("{0} {1}", "Basic", encodedUserInfo);

            WebRequest req = WebRequest.Create(url);
            req.Headers["Authorization"] = credentials;
            return GetResponse(req.GetResponse());
        }

        private static void Request(string url, Token token)
        {
            WebRequest req = WebRequest.Create(url);
            req.Headers["Token"] = token.TokenString;
            try {
                Console.WriteLine(GetResponse(req.GetResponse()) + "\n");
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
            }
        }
    }
}
