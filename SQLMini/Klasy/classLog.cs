using NLog;
using System;

namespace SQLMini.Klasy
{
    internal class classLog
    {
        internal static void LogInfo(string info)
        {


            LogManager.GetCurrentClassLogger().Info(info);
        }
        internal static void LogError(Exception ee, string gdzie = "")
        {


            LogManager.GetCurrentClassLogger().Error(ee.Message + ee.Source + " funkcja :" + gdzie);
        }

    }
}
