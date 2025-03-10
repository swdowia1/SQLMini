using System.Configuration;

namespace SQLMini.Klasy
{
    public static class classConfig
    {

        //context
        public static string context
        {
            get { return ConfigurationManager.AppSettings["context"]; }
        }
        public static string edytor
        {
            get { return ConfigurationManager.AppSettings["edytor"]; }
        }
        public static string queryCatalog
        {
            get { return ConfigurationManager.AppSettings["queryCatalog"]; }
        }
        public static string zapCatalog
        {
            get { return ConfigurationManager.AppSettings["zapCatalog"]; }
        }

        public static string ekselCatalog
        {
            get { return ConfigurationManager.AppSettings["ekselCatalog"]; }
        }
    }
}
