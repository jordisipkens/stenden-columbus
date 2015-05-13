using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebserviceColumbus.Classes.IO;

namespace WebserviceColumbus.Classes
{
    public class ErrorHandler
    {
        Exception ex;
        string message;
        DateTime date;

        public ErrorHandler(Exception ex, string message, bool logError)
        {
            this.ex = ex;
            this.message = message;
            this.date = DateTime.Now;

            if (logError) {
                Log();
            }
        }

        /// <summary>
        /// Logs the error to the console of a log file.
        /// </summary>
        private void Log()
        {
#if DEBUG
            Console.WriteLine(message + " | " + ex.Message);
            Console.WriteLine(ex.ToString());
#else
            WriteToLog();
#endif
        }

        /// <summary>
        /// Writes the error to the Log file
        /// </summary>
        private void WriteToLog()
        {
            using (StreamWriter sw = File.AppendText(IOManager.GetProjectFilePath("Resources/ErrorLog.txt"))) {
                sw.WriteLine(date.ToString() + " | " + message);
                sw.WriteLine(ex.ToString());
                sw.WriteLine();
            }
        }
    }
}