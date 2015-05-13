using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebserviceColumbus.Classes.IO
{
    public static class JsonSerialization
    {
        /// <summary>
        /// Serializes the object in JSON format to the given file. The file will be overwritten.
        /// </summary>
        /// <param name="objectToSerialize"></param>
        /// <param name="filePath"></param>
        public static void Serialize(object objectToSerialize, string filePath)
        {
            if (File.Exists(filePath)) {
                string result = Serialize(objectToSerialize);
                if (result != null && result.Length > 0) {
                    System.IO.File.WriteAllText(filePath, result);
                }
            }
        }

        /// <summary>
        /// Serializes the object to a JSON string.
        /// </summary>
        /// <param name="objectToSerialize"></param>
        /// <returns></returns>
        public static string Serialize(object objectToSerialize)
        {
            return JsonConvert.SerializeObject(objectToSerialize);
        }

        public static T Deserialize<T>(string value)
        {
            try {
                return JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception ex) {
                new ErrorHandler(ex, "Error during Deserialization", true);
                return default(T);
            }
        }
    }
}