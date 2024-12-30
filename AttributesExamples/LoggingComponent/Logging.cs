using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingComponent
{
    public class Logging
    {
        //[Conditional("LOG_INFO")]
        [Obsolete("The LogToScreen method has now been deprecated. Please use LogToFile instead.",false)]
        public static void LogToScreen(string msg)
        {
            Console.WriteLine(msg);
        }
        public static void LogToFile(string msg)
        {
            Console.WriteLine("Writing text to file:" + msg);
        }
    }
}
