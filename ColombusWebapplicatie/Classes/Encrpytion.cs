﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ColombusWebapplicatie.Classes
{
    public class Encryption
    {
        private const string API_KEY = "C0lumbu5";

        /// <summary>
        /// Encrpyts the string with RijndaelManaged encrpytion. API key is used for salt.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Encrypt(string value, string salt = API_KEY)
        {
            return EncryptDecrypt(value, GetHashSha256(salt, 32));
        }

        private static string EncryptDecrypt(string inputText, string encryptionKey)
        {
            RijndaelManaged cipher = GetCipher();
            byte[] rgbIV = new byte[cipher.BlockSize / 8];
            byte[] rgbKey = new byte[32];
            byte[] rgbIVBytes = new byte[16];

            byte[] encrpytedPassword = Encoding.UTF8.GetBytes(encryptionKey);
            rgbIVBytes = Encoding.UTF8.GetBytes("");

            int length = encrpytedPassword.Length;
            if(length > rgbKey.Length) {
                length = rgbKey.Length;
            }
            int ivLenth = rgbIVBytes.Length;
            if(ivLenth > rgbIV.Length) {
                ivLenth = rgbIV.Length;
            }

            Array.Copy(encrpytedPassword, rgbKey, length);
            Array.Copy(rgbIVBytes, rgbIV, ivLenth);
            cipher.Key = rgbKey;
            cipher.IV = rgbIV;

            UTF8Encoding encoding = new UTF8Encoding();

            byte[] plainText = cipher.CreateEncryptor().TransformFinalBlock(encoding.GetBytes(inputText), 0, inputText.Length);
            string result = Convert.ToBase64String(plainText);
            cipher.Dispose();
            return result;
        }

        public static string GetHashSha256(string text, int length)
        {
            byte[] hash = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(text));
            string hashString = string.Empty;
            foreach(byte x in hash) {
                hashString += string.Format("{0:x2}", x);
            }

            if(length > hashString.Length) {
                return hashString;
            }
            else {
                return hashString.Substring(0, length);
            }
        }

        private static RijndaelManaged GetCipher()
        {
            RijndaelManaged cipher = new RijndaelManaged();
            cipher.Mode = CipherMode.CBC;
            cipher.Padding = PaddingMode.PKCS7;
            cipher.KeySize = 256;
            cipher.BlockSize = 128;
            return cipher;
        }
    }
}