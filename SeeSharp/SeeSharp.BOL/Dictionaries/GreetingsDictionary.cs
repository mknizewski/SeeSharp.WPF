using SeeSharp.BO.Infrastructure;
using System.Resources;

namespace SeeSharp.BO.Dictionaries
{
    public static class GreetingsDictionary
    {
        private static ResourceManager Greetings = ResourceManagerFactory.GetResource(typeof(BOL.Resources.Greetings));

        public static string MondayPattern
        {
            get
            {
                return Greetings.GetString("MondayPattern");
            }
        }

        public static string TuesdayPattern
        {
            get
            {
                return Greetings.GetString("TuesdayPattern");
            }
        }

        public static string WenesdayPattern
        {
            get
            {
                return Greetings.GetString("WenesdayPattern");
            }
        }

        public static string ThursdayPattern
        {
            get
            {
                return Greetings.GetString("ThursdayPattern");
            }
        }

        public static string FridayPattern
        {
            get
            {
                return Greetings.GetString("FridayPattern");
            }
        }

        public static string SaturdayPattern
        {
            get
            {
                return Greetings.GetString("SaturdayPattern");
            }
        }

        public static string SundayPattern
        {
            get
            {
                return Greetings.GetString("SundayPattern");
            }
        }

        public static string DefaultPattern
        {
            get
            {
                return Greetings.GetString("DefaultPattern");
            }
        }
    }
}