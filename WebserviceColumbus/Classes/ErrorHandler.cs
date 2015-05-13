using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebserviceColumbus.Classes
{
    public class ErrorHandler
    {
        Exception ex;
        string message;

        public ErrorHandler(Exception ex, string message, bool logError)
        {
            this.ex = ex;
            this.message = message;

            if (logError) {
                Log();
            }
        }

        private void Log()
        {
#if DEBUG
            Console.WriteLine(message + " | " + ex.Message);
            Console.WriteLine(ex.ToString());
#endif
        }
    }
}