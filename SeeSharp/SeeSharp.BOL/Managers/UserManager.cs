using SeeSharp.BO.Dictionaries;
using System;
using System.Collections.Generic;

namespace SeeSharp.BO.Managers
{
    public class UserManager
    {
        private const int MinRandomValue = 1000;
        private const int MaxRandomValue = 9999;

        public UserInfo UserInfo;

        public static int GenerateCodeForNewUser()
        {
            Random randomNumber = new Random(DateTime.Now.Millisecond);

            return randomNumber.Next(MinRandomValue, MaxRandomValue);
        }

        public void SignIn(Dictionary<string, string> userProfile, string loginCode)
        {
            if (userProfile.Count == 0)
                throw new Exception(ExceptionDictionary.IncorrectLoginCreditentials);
            else if (!string.Equals(userProfile["code"], loginCode))
                throw new Exception(ExceptionDictionary.IncorrectLoginCreditentials);

            UserInfo = new UserInfo
            {
                Login = userProfile["login"],
                Code = userProfile["code"],
                Percentage = int.Parse(userProfile["percentage"]),
                LastTutorial = userProfile["last"]
            };
        }

        public Dictionary<string, string> UserProfileToDictionary()
        {
            Dictionary<string, string> userInfoDictionary = new Dictionary<string, string>();

            userInfoDictionary.Add("login", UserInfo.Login);
            userInfoDictionary.Add("code", UserInfo.Code);
            userInfoDictionary.Add("percentage", UserInfo.Percentage.ToString());
            userInfoDictionary.Add("last", UserInfo.LastTutorial);

            return userInfoDictionary;
        }

        public void SignOut()
        {
            UserInfo = null;
        }
    }

    public class UserInfo
    {
        public string Login { get; set; }
        public string Code { get; set; }
        public int Percentage { get; set; }
        public string LastTutorial { get; set; }
    }

    public enum User
    {
        Logged, Unlogged
    }
}