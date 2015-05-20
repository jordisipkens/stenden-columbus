using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Http;
using WebserviceColumbus.Classes;

namespace WebserviceColumbus.Controllers
{
    public class TravelController : ApiController
    {
        // GET: api/Test
        [Authorize]
        public string Get(int index)
        {
            return null;
        }

        [Authorize]
        public string GetAll()
        {
            return null;
        }

        private byte[] GetFileByteArray(string filename)
        {
            FileStream oFileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);

            // Create a byte array of file size.
            byte[] FileByteArrayData = new byte[oFileStream.Length];

            //Read file in bytes from stream into the byte array
            oFileStream.Read(FileByteArrayData, 0, System.Convert.ToInt32(oFileStream.Length));

            //Close the File Stream
            oFileStream.Close();

            return FileByteArrayData; //return the byte data
        }

        //This above method return the file's Byte array then you need to call UploadVideoFile method.

        public string UploadVideoFile(string URL, byte[] VideoFileData)
        {
            string response = null;
            HttpWebRequest webReq = null;
            HttpWebResponse webRes = null;
            try {
                
                    webReq.Method = "POST";
                    webReq.Accept = "*/*";
                    webReq.Timeout = 50000;
                    webReq.KeepAlive = false;
                    webReq.AllowAutoRedirect = false;
                    webReq.AllowWriteStreamBuffering = true;
                    webReq.ContentType = "binary/octet-stream";
                    webReq.ContentLength = VideoFileData.Length;

                    using (Stream requestStream = webReq.GetRequestStream()) {
                        requestStream.Write(VideoFileData, 0, VideoFileData.Length);
                    }

                    webRes = (HttpWebResponse)webReq.GetResponse();
                    using (StreamReader streamResponseReader = new StreamReader(webRes.GetResponseStream(), Encoding.UTF8)) {
                        response = streamResponseReader.ReadToEnd();
                    }
            }
            catch (Exception ex) {
                new ErrorHandler(ex, "Error while reading response", true);
            }
            finally {
                if (webReq != null) {
                    webReq.Abort();
                    webReq = null;
                }
                if (webRes != null) {
                    webRes.Close();
                    webRes = null;
                }
            }
            return response;
        }
    }
}
