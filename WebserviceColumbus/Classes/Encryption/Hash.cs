using System;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;

namespace WebserviceColumbus.Classes.Encryption
{
    public static class Hash
    {
        /// <summary>
        /// Hashes the text with SHA1.
        /// </summary>
        /// <param name="password">Password to has</param>
        /// <param name="salt">Salt for the hash (Username)</param>
        /// <returns></returns>
        public static string HashText(string password, string salt)
        {
            SHA1CryptoServiceProvider hasher = new SHA1CryptoServiceProvider();
            byte[] textWithSaltBytes = Encoding.UTF8.GetBytes(string.Concat(password, salt));
            byte[] hashedBytes = hasher.ComputeHash(textWithSaltBytes);
            hasher.Clear();
            return Convert.ToBase64String(hashedBytes);
        }
    }
}