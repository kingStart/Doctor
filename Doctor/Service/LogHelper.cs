using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Service
{
    public class LogHelper
    {
        private static ILog logWriter = LogManager.GetLogger("AppLog");
        public static void Error(string str)
        {
            logWriter.Error(str);
        }

        public static void Info(string str)
        {
            logWriter.Info(str);
        }

    }
}
