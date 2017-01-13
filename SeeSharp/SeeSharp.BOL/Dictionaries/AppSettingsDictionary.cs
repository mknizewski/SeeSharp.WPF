using SeeSharp.BO.Infrastructure;
using System.Resources;

namespace SeeSharp.BO.Dictionaries
{
    public static class AppSettingsDictionary
    {
        private readonly static ResourceManager AppSettings = ResourceManagerFactory.GetResource(typeof(BOL.Resources.AppSettings));

        public static string AchivmentImageDirectory
        {
            get
            {
                return AppSettings.GetString("AchivmentImageDirectory");
            }
        }

        public static string ShowPercentage
        {
            get
            {
                return AppSettings.GetString("ShowPercentage");
            }
        }

        public static string JavaScriptAlert
        {
            get
            {
                return AppSettings.GetString("JavaScriptAlert");
            }
        }

        public static string VideoDirectory
        {
            get
            {
                return AppSettings.GetString("VideoDirectory");
            }
        }

        public static string ProgramFilesDirectory
        {
            get
            {
                return AppSettings.GetString("ProgramFilesDirectory");
            }
        }

        public static string TextDirectory
        {
            get
            {
                return AppSettings.GetString("TextDirectory");
            }
        }

        public static string XmlFilesDirectiory
        {
            get
            {
                return AppSettings.GetString("XmlFileDirectory");
            }
        }

        public static string AppVersion
        {
            get
            {
                return AppSettings.GetString("AppVersion");
            }
        }

        public static string AppVersionMessagePattern
        {
            get
            {
                return AppSettings.GetString("VersionMessagePattern");
            }
        }

        public static string HelloWorldProgram
        {
            get
            {
                return AppSettings.GetString("HelloWorld");
            }
        }

        public static string UnllogedAlert
        {
            get
            {
                return AppSettings.GetString("UnloggedAlert");
            }
        }

        public static string SectionPrefixPattern
        {
            get
            {
                return AppSettings.GetString("SectionPrefixPattern");
            }
        }

        public static string PercentagePattern
        {
            get
            {
                return AppSettings.GetString("PercentagePattern");
            }
        }

        public static string CheckMark
        {
            get
            {
                return AppSettings.GetString("CheckMark");
            }
        }

        public static string RandomText
        {
            get
            {
                return AppSettings.GetString("RandomText");
            }
        }
    }
}