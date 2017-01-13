using System.Configuration;

namespace SeeSharp.Web.Dictionaries
{
    public static class ServerDictionary
    {
        public static string DirectoryNotFoundMessage = "Nie znaleziono użytkownika!";
        public static string ErrorPattern = "Linia {0}: {1} {2}{3}";
        public static string ExeExtensionPattern = "{0}.exe";

        public static string XmlFileDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings["XmlFileDirectory"];
            }
        }

        public static string XmlAchivmentsFileName
        {
            get
            {
                return ConfigurationManager.AppSettings["XmlAchivmentsFileName"];
            }
        }

        public static string XmlProfileFileName
        {
            get
            {
                return ConfigurationManager.AppSettings["XmlProfileFileName"];
            }
        }

        public static string SourceFileDictionary
        {
            get
            {
                return ConfigurationManager.AppSettings["SourceFilesDirectory"];
            }
        }
    }
}