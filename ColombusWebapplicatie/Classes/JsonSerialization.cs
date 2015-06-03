using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace ColombusWebapplicatie.Controllers
{
    public class JsonSerialization
    {
        /// <summary>
        /// Serializes the object in JSON format to the given file. The file will be overwritten.
        /// </summary>
        /// <param name="objectToSerialize"></param>
        /// <param name="filePath"></param>
        public static void SerializeToFile(object objectToSerialize, string filePath)
        {
            if(File.Exists(filePath)) {
                string result = Serialize(objectToSerialize);
                if(result != null && result.Length > 0) {
                    System.IO.File.WriteAllText(filePath, result);
                }
            }
        }

        /// <summary>
        /// Reads the file and deserializes it to the given Type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T DeserializeFromFile<T>(string fileName)
        {

            return Deserialize<T>(System.IO.File.ReadAllText(fileName));
        }

        /// <summary>
        /// Serializes the object to a JSON string.
        /// </summary>
        /// <param name="objectToSerialize"></param>
        /// <returns></returns>
        public static string Serialize(object objectToSerialize)
        {
            try
            {
                return JsonConvert.SerializeObject(objectToSerialize);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

        }

        /// <summary>
        /// Deserializes the given string to an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string value)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return default(T);
            }
        }
    }
}