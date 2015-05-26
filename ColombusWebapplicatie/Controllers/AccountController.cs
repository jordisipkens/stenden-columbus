using ColombusWebapplicatie.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ColombusWebapplicatie.Controllers
{
    public class AccountController : BaseController
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Login(User user)
        {
            WebRequest request = WebRequest.Create("http://columbusstenden-001-site1.myasp.net/api/Dummy/Login");
            string userInfo = string.Format("{0}:{1}", user.Username, Encrypt(user.Password));
            string encodedUserInfo = Convert.ToBase64String(Encoding.GetEncoding("iso-8859-1").GetBytes(userInfo));
            string credentials = string.Format("{0} {1}", "Basic", encodedUserInfo);
            request.Headers["Authorization"] = credentials;
            WebResponse response = request.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            string token = streamReader.ReadToEnd();
            return token;
        }

        private static string Encrypt(string value)
        {
            try
            {
                DeriveBytes rgb = new Rfc2898DeriveBytes("C0lumbu5", Encoding.Unicode.GetBytes("C0lumbu5"));
                SymmetricAlgorithm algorithm = new AesManaged();
                byte[] rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
                byte[] rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);
                ICryptoTransform transform = algorithm.CreateEncryptor(rgbKey, rgbIV);
                using (MemoryStream buffer = new MemoryStream())
                {
                    using (CryptoStream stream = new CryptoStream(buffer, transform, CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(stream, Encoding.Unicode))
                        {
                            writer.Write(value);
                        }
                    }
                    return Convert.ToBase64String(buffer.ToArray());
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
