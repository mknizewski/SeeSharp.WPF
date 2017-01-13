using SeeSharp.BO.Infrastructure;
using System.Resources;

namespace SeeSharp.BO.Dictionaries
{
    public static class ExceptionDictionary
    {
        private readonly static ResourceManager ExceptionsMessage = ResourceManagerFactory.GetResource(typeof(BOL.Resources.Exception));

        public static string ModuleNotAllowed
        {
            get
            {
                return ExceptionsMessage.GetString("ModuleNotAllowed");
            }
        }

        public static string TutorialNotStarted
        {
            get
            {
                return ExceptionsMessage.GetString("TutorialNotStarted");
            }
        }

        public static string LoginNotFoundMessage
        {
            get
            {
                return ExceptionsMessage.GetString("LoginNameNotFound");
            }
        }

        public static string LoginIsUsed
        {
            get
            {
                return ExceptionsMessage.GetString("LoginIsUsed");
            }
        }

        public static string IncorrectLoginCreditentials
        {
            get
            {
                return ExceptionsMessage.GetString("IncorrectLoginCreditentials");
            }
        }

        public static string CodeIsNotNumber
        {
            get
            {
                return ExceptionsMessage.GetString("CodeIsNotNumber");
            }
        }
    }
}