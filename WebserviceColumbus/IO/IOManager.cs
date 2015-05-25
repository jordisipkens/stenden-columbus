using System;
using System.IO;

namespace WebserviceColumbus.IO
{
    public static class IOManager
    {
        /// <summary>
        /// Reads a file and returns the text as a string.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadFile(string filePath)
        {
            if(File.Exists(filePath)) {
                string[] lines = File.ReadAllLines(filePath);
                string result = string.Empty;
                foreach(string line in lines) {
                    result = result + line;
                }
                return result;
            }
            return null;
        }

        /// <summary>
        /// Gets the path to a file in the current project folder.
        /// </summary>
        /// <param name="projectPath"></param>
        /// <returns></returns>
        public static string GetProjectFilePath(string projectPath)
        {
            return Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), projectPath);
        }
    }
}