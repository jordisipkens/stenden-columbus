using System;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;

namespace WebserviceColumbus.Classes.Encryption
{
    public static class Hash
    {
        public static string HashText(string password, string salt)
        {
            SHA1CryptoServiceProvider hasher = new SHA1CryptoServiceProvider();
            byte[] textWithSaltBytes = Encoding.UTF8.GetBytes(string.Concat(password, salt));
            byte[] hashedBytes = hasher.ComputeHash(textWithSaltBytes);
            hasher.Clear();
            return Convert.ToBase64String(hashedBytes);
        }

        private static string GenerateSalt(int maxSize = 64)
        {
            var alphaSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890#!".ToCharArray();
            var crypto = new RNGCryptoServiceProvider();
            var bytes = new byte[maxSize];
            crypto.GetBytes(bytes);
            var tempSB = new StringBuilder(maxSize);
            foreach (var b in bytes) {
                tempSB.Append(alphaSet[b % (alphaSet.Length)]);
            }
            return tempSB.ToString();
        }
    }
}