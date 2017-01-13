using SeeSharp.Web.Dictionaries;
using SeeSharp.Web.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace SeeSharp.Web
{
    public class ServerServiceClient
    {
        private const string Separator = @"\";

        public static ServerServiceClient GetInstance()
        {
            return new ServerServiceClient();
        }

        public bool CreateDirectoryForUser(string loginName, int code)
        {
            string xmlDirectoryPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, ServerDictionary.XmlFileDirectory);
            string userDirectory = string.Concat(xmlDirectoryPath, Separator, loginName);

            if (!Directory.Exists(userDirectory))
            {
                Directory.CreateDirectory(userDirectory);
                XmlDocument xmlProfileFile = XmlManager.CreateNewXmlProfileFile(loginName, code);
                XmlDocument xmlAchivmentFile = XmlManager.CreateNewAchivmentFile();

                string fullXmlProfileFilePath = string.Concat(userDirectory, Separator, ServerDictionary.XmlProfileFileName);
                string fullXmlAchivmentFilePath = string.Concat(userDirectory, Separator, ServerDictionary.XmlAchivmentsFileName);

                xmlProfileFile.Save(fullXmlProfileFilePath);
                xmlAchivmentFile.Save(fullXmlAchivmentFilePath);

                return true;
            }

            return false;
        }

        public void CreateDirectoriesIfDosentExists()
        {
            string xmlDirectoryPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, ServerDictionary.XmlFileDirectory);
            string sourceFileDirectoryPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, ServerDictionary.SourceFileDictionary);

            if (!Directory.Exists(xmlDirectoryPath))
                Directory.CreateDirectory(xmlDirectoryPath);

            if (!Directory.Exists(sourceFileDirectoryPath))
                Directory.CreateDirectory(sourceFileDirectoryPath);
        }

        public Dictionary<string, string> GetUserProfile(string loginName)
        {
            string xmlDirectoryPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, ServerDictionary.XmlFileDirectory);
            string userProfilePath = string.Concat(
                xmlDirectoryPath,
                Separator,
                loginName,
                Separator,
                ServerDictionary.XmlProfileFileName);

            if (!File.Exists(userProfilePath))
                return new Dictionary<string, string>();

            return XmlManager.DeserializeXmlProfile(userProfilePath);
        }

        public void UpdateUserProfile(Dictionary<string, string> userProfile)
        {
            string loginName = userProfile["login"];
            string xmlDirectoryPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, ServerDictionary.XmlFileDirectory);
            string userProfilePath = string.Concat(
                xmlDirectoryPath,
                Separator,
                loginName,
                Separator,
                ServerDictionary.XmlProfileFileName);

            XmlManager.UpdateXmlProfile(userProfile, userProfilePath);
        }

        public int[] GetAchivmentFile(string loginName)
        {
            string xmlDirectoryPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, ServerDictionary.XmlFileDirectory);
            string userAchivmentPath = string.Concat(
                xmlDirectoryPath,
                Separator,
                loginName,
                Separator,
                ServerDictionary.XmlAchivmentsFileName);

            return XmlManager.DeserializeXmlAchivments(userAchivmentPath);
        }

        public void UpdateAchivmentFile(int achivId, string loginName)
        {
            string xmlDirectoryPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, ServerDictionary.XmlFileDirectory);
            string userAchivmentPath = string.Concat(
                xmlDirectoryPath,
                Separator,
                loginName,
                Separator,
                ServerDictionary.XmlAchivmentsFileName);

            XmlManager.UpdateXmlAchivments(achivId, userAchivmentPath);
        }

        public string GetModuleText(string path)
        {
            return File.ReadAllText(string.Concat(AppDomain.CurrentDomain.BaseDirectory, path));
        }
    }
}