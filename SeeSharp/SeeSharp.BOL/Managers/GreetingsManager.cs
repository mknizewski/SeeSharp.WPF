using SeeSharp.BO.Dictionaries;
using System;

namespace SeeSharp.BO.Managers
{
    public class GreetingsManager
    {
        public static string GetGreetingsByDayOfWeek(DayOfWeek dayOfWeek, string userName)
        {
            string greetings = string.Empty;

            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    greetings = GreetingsDictionary.MondayPattern;
                    break;

                case DayOfWeek.Tuesday:
                    greetings = GreetingsDictionary.TuesdayPattern;
                    break;

                case DayOfWeek.Wednesday:
                    greetings = GreetingsDictionary.WenesdayPattern;
                    break;

                case DayOfWeek.Thursday:
                    greetings = GreetingsDictionary.ThursdayPattern;
                    break;

                case DayOfWeek.Friday:
                    greetings = GreetingsDictionary.FridayPattern;
                    break;

                case DayOfWeek.Saturday:
                    greetings = GreetingsDictionary.SaturdayPattern;
                    break;

                case DayOfWeek.Sunday:
                    greetings = GreetingsDictionary.SundayPattern;
                    break;

                default:
                    greetings = GreetingsDictionary.DefaultPattern;
                    break;
            }

            return string.Format(greetings, userName);
        }
    }
}