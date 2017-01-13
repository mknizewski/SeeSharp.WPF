using SeeSharp.BO.Infrastructure;
using System.Resources;

namespace SeeSharp.BO.Dictionaries
{
    public static class PageDictionary
    {
        private readonly static ResourceManager ResourceManager = ResourceManagerFactory.GetResource(typeof(BOL.Resources.Page));

        public static string SuccesfulRegisterMessagePattern
        {
            get
            {
                return ResourceManager.GetString("SuccesfullRegisterMessagePattern");
            }
        }

        public static string TutorialNotStarted
        {
            get
            {
                return ResourceManager.GetString("TutorialNotStarted");
            }
        }
    }
}