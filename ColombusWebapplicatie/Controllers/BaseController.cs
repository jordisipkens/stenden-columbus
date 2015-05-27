using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace ColombusWebapplicatie.Controllers
{
    public class BaseController : Controller
    {
        private string apiUrl = "http://columbusstenden-001-site1.myasp.net/";
        private string apiKey = "";

        public ActionResult Error(string errorMessage, ActionResult result)
        {
            TempData["error"] = errorMessage;
            return result;
        }

        public ActionResult ErrorToIndex(string errorMessage)
        {
            return Error(errorMessage, RedirectToAction("Index"));
        }

        public static string Encrypt(string value)
        {
            try
            {
                DeriveBytes rgb = new Rfc2898DeriveBytes(API_KEY, Encoding.Unicode.GetBytes(API_KEY));
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
