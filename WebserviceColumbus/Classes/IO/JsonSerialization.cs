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
        public static void Serialization(object objectToSerialize, string filePath)
        {
            if (File.Exists(filePath)) {
                using (FileStream fs = File.Open(filePath, FileMode.Append)) {
                    using (StreamWriter sw = new StreamWriter(fs)) {
                        using (JsonWriter jw = new JsonTextWriter(sw)) {
                            jw.Formatting = Formatting.Indented;
                            JsonSerializer serializer = new JsonSerializer();
                            serializer.Serialize(jw, objectToSerialize);
                        }
                    }
                }
            }
        }

        public static string Serialization(object objectToSerialize)
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