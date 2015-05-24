using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using WebserviceColumbus.Other;

namespace WebserviceColumbus.Authorization
{
    public static class Hash
    {
        private const string API_KEY = "C0lumbu5";

        /// <summary>
        /// Hashes the text with SHA1. Used for password encryption
        /// </summary>
        /// <param name="text">Password to has</param>
        /// <param name="salt">Salt for the hash (Username)</param>
        /// <returns></returns>
        public static string OneWayHash(string text, string salt)
        {
            SHA1CryptoServiceProvider hasher = new SHA1CryptoServiceProvider();
            byte[] textWithSaltBytes = Encoding.UTF8.GetBytes(string.Concat(text, salt));
            byte[] hashedBytes = hasher.ComputeHash(textWithSaltBytes);
            hasher.Clear();
            return Convert.ToBase64String(hashedBytes);
        }

        /// <summary>
        /// Encrpyts the string with Aes encrpytion. API key is used for salt.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Encrypt(string value, string salt = API_KEY)
        {
            try {
                DeriveBytes rgb = new Rfc2898DeriveBytes(salt, Encoding.Unicode.GetBytes(salt));
                SymmetricAlgorithm algorithm = new AesManaged();

                byte[] rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
                byte[] rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);

                ICryptoTransform transform = algorithm.CreateEncryptor(rgbKey, rgbIV);

                using (MemoryStream buffer = new MemoryStream()) {
                    using (CryptoStream stream = new CryptoStream(buffer, transform, CryptoStreamMode.Write)) {
                        using (StreamWriter writer = new StreamWriter(stream, Encoding.Unicode)) {
                            writer.Write(value);
                        }
                    }
                    return Convert.ToBase64String(buffer.ToArray());
                }
            }
            catch (Exception ex) {
                new ErrorHandler(ex, "Error while encrypting", true);
                return null;
            }
        }

        /// <summary>
        /// Encrpyts the string with Aes encrpytion. API key is used for salt.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Decrypt(string text, string salt = API_KEY)
        {
            try {
                DeriveBytes rgb = new Rfc2898DeriveBytes(salt, Encoding.Unicode.GetBytes(salt));
                SymmetricAlgorithm algorithm = new AesManaged();

                byte[] rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
                byte[] rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);

                ICryptoTransform transform = algorithm.CreateDecryptor(rgbKey, rgbIV);

                using (MemoryStream buffer = new MemoryStream(Convert.FromBase64String(text))) {
                    using (CryptoStream stream = new CryptoStream(buffer, transform, CryptoStreamMode.Read)) {
                        using (StreamReader reader = new StreamReader(stream, Encoding.Unicode)) {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex) {
                new ErrorHandler(ex, "Error while decrypting", true);
                return null;
            }
        }
    }
}