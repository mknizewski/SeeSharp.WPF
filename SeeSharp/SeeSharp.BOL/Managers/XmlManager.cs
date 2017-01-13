using System.Collections.Generic;
using System.Xml;

namespace SeeSharp.Web.Managers
{
    public static class XmlManager
    {
        public static XmlDocument CreateNewXmlProfileFile(string loginName, int code)
        {
            XmlDocument xmlDocument = new XmlDocument();

            XmlNode rootNode = xmlDocument.CreateElement("appProfile");
            xmlDocument.AppendChild(rootNode);

            XmlNode userNode = xmlDocument.CreateElement("user");

            XmlNode userLoginSubNode = xmlDocument.CreateElement("login");
            userLoginSubNode.InnerText = loginName;
            userNode.AppendChild(userLoginSubNode);

            XmlNode codeSubNode = xmlDocument.CreateElement("code");
            codeSubNode.InnerText = code.ToString();
            userNode.AppendChild(codeSubNode);

            rootNode.AppendChild(userNode);

            XmlNode tutorialNode = xmlDocument.CreateElement("tutorial");

            XmlNode percentageSubNode = xmlDocument.CreateElement("percentage");
            percentageSubNode.InnerText = decimal.Zero.ToString();
            tutorialNode.AppendChild(percentageSubNode);

            XmlNode lastSubNode = xmlDocument.CreateElement("last");
            tutorialNode.AppendChild(lastSubNode);

            rootNode.AppendChild(tutorialNode);

            return xmlDocument;
        }

        public static XmlDocument CreateNewAchivmentFile()
        {
            XmlDocument xmlDocument = new XmlDocument();

            XmlNode root = xmlDocument.CreateElement("achivments");
            xmlDocument.AppendChild(root);

            return xmlDocument;
        }

        public static Dictionary<string, string> DeserializeXmlProfile(string xmlFilePath)
        {
            XmlDocument xmlProfile = new XmlDocument();
            xmlProfile.Load(xmlFilePath);

            var userDictionary = new Dictionary<string, string>();
            foreach (XmlNode node in xmlProfile.ChildNodes)
            {
                foreach (XmlNode subNode in node.ChildNodes)
                {
                    foreach (XmlNode properties in subNode.ChildNodes)
                    {
                        userDictionary.Add(properties.Name, properties.InnerText);
                    }
                }
            }

            return userDictionary;
        }

        public static int[] DeserializeXmlAchivments(string xmlFilePath)
        {
            XmlDocument xmlAchivments = new XmlDocument();
            xmlAchivments.Load(xmlFilePath);

            XmlNode root = xmlAchivments.FirstChild;
            int achivCount = root.ChildNodes.Count;

            if (achivCount == 0)
                return null;

            int[] achivArray = new int[achivCount];

            for (int i = 0; i < achivCount; i++)
            {
                XmlNode achiv = root.ChildNodes[i];
                achivArray[i] = int.Parse(achiv.InnerText);
            }

            return achivArray;
        }

        public static void UpdateXmlAchivments(int achivId, string xmlAchivFilePath)
        {
            XmlDocument xmlAchivments = new XmlDocument();
            xmlAchivments.Load(xmlAchivFilePath);

            XmlNode root = xmlAchivments.FirstChild;
            XmlNode achivment = xmlAchivments.CreateElement("achivment");

            achivment.InnerText = achivId.ToString();
            root.AppendChild(achivment);

            xmlAchivments.Save(xmlAchivFilePath);
        }

        public static void UpdateXmlProfile(Dictionary<string, string> userProfile, string xmlFilePath)
        {
            XmlDocument xmlProfile = new XmlDocument();
            xmlProfile.Load(xmlFilePath);

            XmlNode appProfileNode = xmlProfile.FirstChild;

            XmlNode userNode = appProfileNode.FirstChild;
            XmlNode loginProp = userNode.FirstChild;
            XmlNode codeProp = userNode.LastChild;

            XmlNode tutorialNode = appProfileNode.LastChild;
            XmlNode percetnageProp = tutorialNode.FirstChild;
            XmlNode lastProp = tutorialNode.LastChild;

            loginProp.InnerText = userProfile["login"];
            codeProp.InnerText = userProfile["code"];
            percetnageProp.InnerText = userProfile["percentage"];
            lastProp.InnerText = userProfile["last"];

            xmlProfile.Save(xmlFilePath);
        }
    }
}