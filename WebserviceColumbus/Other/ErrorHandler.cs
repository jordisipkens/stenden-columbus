using System;
using System.Data.Entity.Validation;
using System.IO;
using WebserviceColumbus.IO;

namespace WebserviceColumbus.Other
{
    public class ErrorHandler
    {
        private Exception ThrownException;
        private string Message;
        private DateTime Date;

        public ErrorHandler(Exception ex, string message, bool logError)
        {
            this.ThrownException = ex;
            this.Message = message;
            this.Date = DateTime.Now;

            if(logError) {
                Log();
            }
        }

        /// <summary>
        /// Logs the error to the console of a log file.
        /// </summary>
        private void Log()
        {
#if DEBUG
            Console.WriteLine(Message + " | " + ThrownException.Message);
            Console.WriteLine(ThrownException.ToString());
#else
            WriteToLog();
#endif
        }

        /// <summary>
        /// Writes the error to the Log file
        /// </summary>
        private void WriteToLog()
        {
            using(StreamWriter sw = File.AppendText(GetProjectFilePath("ErrorLog.txt"))) {
                if(ThrownException.GetType() == typeof(DbEntityValidationException)) {
                }
                else {
                    sw.WriteLine(Date.ToString() + " | " + Message);
                    sw.WriteLine(ThrownException.ToString());
                    sw.WriteLine();
                }
            }
        }

        /// <summary>
        /// Gets the path to a file in the current project folder.
        /// </summary>
        /// <param name="projectPath"></param>
        /// <returns></returns>
        private string GetProjectFilePath(string projectPath)
        {
            return Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), projectPath);
        }
    }
}