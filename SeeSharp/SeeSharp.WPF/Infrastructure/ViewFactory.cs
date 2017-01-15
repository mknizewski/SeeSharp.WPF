using SeeSharp.BO.Managers;
using SeeSharp.WPF;
using System;
using System.Windows.Controls;

namespace SeeSharp.Infrastructure
{
    public static class ViewFactory
    {
        public static TimeSpan CurrentPosition;

        public static UserControl GetView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.WelcomePage:
                    return new WelcomePage();

                case ViewType.AboutAuthors:
                    return new AboutAuthors();

                case ViewType.Register:
                    return new RegisterPage();

                case ViewType.Login:
                    return new LoginPage();

                case ViewType.AboutCourse:
                    return new AboutCourse();

                default:
                    return new WelcomePage();
            }
        }

        public static UserControl GetModule(string tag)
        {
            return new ModulePage(tag);
        }

        public static AchivmentAlert GetAchivmentAlert(Achivments achivments)
        {
            return new AchivmentAlert(AchivmentManager.GetAchivment(achivments));
        }
    }

    public enum ViewType
    {
        WelcomePage,
        AboutAuthors,
        Register,
        Login,
        AboutCourse
    }
}