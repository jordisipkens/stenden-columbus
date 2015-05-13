using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebserviceColumbus.Classes.IO
{
    public static class IOManager
    {
        public static string ReadFile(string filePath)
        {
            if(File.Exists(filePath)) {
                string[] lines = File.ReadAllLines(filePath);
                string result = string.Empty;
                foreach (string line in lines) {
                    result = result + line;
                }
                return result;
            }
            return null;
        }
    }
}