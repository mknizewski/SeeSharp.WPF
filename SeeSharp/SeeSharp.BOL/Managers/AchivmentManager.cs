using System.Collections.Generic;
using System.Linq;

namespace SeeSharp.BO.Managers
{
    public class AchivmentManager
    {
        public static List<Achivment> AchivmentList;

        static AchivmentManager()
        {
            InitializeAchivmentList();
        }

        private static void InitializeAchivmentList()
        {
            AchivmentList = new List<Achivment>();

            AchivmentList.Add(Achivment.CreateAchivment(0, "TechnologyPionier.png", "Pionier techonologii", "Zacząłeś kurs o .NET"));
            AchivmentList.Add(Achivment.CreateAchivment(1, "MakeVSGreatAgain.png", "Make Visual Studio greater!", "Dokonałeś instalacji programu Microsoft Visual Studio."));
            AchivmentList.Add(Achivment.CreateAchivment(2, "DeckareVarNotWar.png", "Declare var, not war", "Zakończono podrozdział o zmiennych."));
            AchivmentList.Add(Achivment.CreateAchivment(3, "ObiektowyJanusz.png", "Obiektowy Janusz", "Zakończono podrozdział o klasach."));
            AchivmentList.Add(Achivment.CreateAchivment(4, "CopyAndPasteDev.png", "Copy & Paste Developer", ""));
            AchivmentList.Add(Achivment.CreateAchivment(5, "CesarzNET.png", "Cesarz .NET-a", "Brawo! Ukończyłeś w 100% kurs!"));
            AchivmentList.Add(Achivment.CreateAchivment(6, "ItsPower.png", "#ToJestPotęga", "Zaczynasz kurs dla zaawansowanych użytkowników."));
        }

        public static Achivment GetAchivment(Achivments achivments)
        {
            int achivId = (int)achivments;

            return AchivmentList.Where(x => x.Id == achivId).First();
        }
    }

    public struct Achivment
    {
        public int Id { get; set; }
        public string File { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }

        public static Achivment CreateAchivment(int id, string file, string title, string details)
        {
            return new Achivment(id, file, title, details);
        }

        public Achivment(int id, string file, string title, string details) : this()
        {
            this.Id = id;
            this.File = file;
            this.Title = title;
            this.Details = details;
        }
    }

    public enum Achivments
    {
        TechnologyPionier,
        MakeVsGreatAgain,
        DeclareVarNotWar,
        ObjectiveJanusz,
        CopyAndPasteDev,
        KingOfNET,
        ItsAPower,
        None
    }
}