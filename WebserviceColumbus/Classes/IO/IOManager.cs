using System;
using System.IO;

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

        public static string GetProjectFilePath(string projectPath)
        {
            return Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), projectPath);
        }
    }
}