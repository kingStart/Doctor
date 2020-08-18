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
        public static void AddErrorLog(string str)
        {
            logWriter.Error(str);
        }

        public static void AddEventLog(string str)
        {
            logWriter.Info(str);
        }

    }
}
