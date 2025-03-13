using System;

namespace SQLMini.Klasy
{
    internal class classConst
    {
        /// <summary>
        /// Katalog główny ze znakiem \
        /// </summary>
        static public string Katalog
        {
            get
            {
                return Environment.CurrentDirectory + "\\";

            }


        }
        /// <summary>
        /// Katalog CSV ze znakiem \
        /// </summary>
        static public string KatalogCSV
        {
            get
            {
                return Environment.CurrentDirectory + "\\CSV\\";

            }


        }

        public static string EOL
        {
            get
            {
                return Environment.NewLine;

            }


        }
    }
}
