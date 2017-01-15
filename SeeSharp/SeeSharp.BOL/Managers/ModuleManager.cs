using System;
using System.Collections.Generic;
using System.Linq;

namespace SeeSharp.BO.Managers
{
    public class ModuleManager : IDisposable
    {
        public Module CurrentModule { get { return _currentModule; } private set { _currentModule = value; } }
        public bool First { get { return _first; } private set { _first = value; } }
        public bool Last { get { return _last; } private set { _last = value; } }

        public static List<Module> ModuleList;
        public static List<Exam> ExamList;

        private const bool IsNotExam = false;
        private const bool IsExam = true;

        private Module _currentModule;
        private bool _first;
        private bool _last;

        static ModuleManager()
        {
            InitializeList();
        }

        private ModuleManager(string tag)
        {
            CurrentModule = ModuleList.Where(module => module.ModuleTag.Equals(tag)).FirstOrDefault();
            CheckModulePosition();
        }

        public static ModuleManager GetModuleManager(string tag)
        {
            return new ModuleManager(tag);
        }

        public void Dispose()
        {
            ExamList.Clear();
            ModuleList.Clear();
        }

        public void ChangeModule(ActionModule actionModule)
        {
            switch (actionModule)
            {
                case ActionModule.Next:
                    GetNextModule();
                    break;

                case ActionModule.Perv:
                    GetPervModule();
                    break;

                default:
                    break;
            }
        }

        public int GetIndexByTag(string tag)
        {
            Module module = ModuleList.Where(x => x.ModuleTag.Equals(tag)).FirstOrDefault();

            return ModuleList.IndexOf(module);
        }

        private void GetNextModule()
        {
            int nextIndex = ModuleList.IndexOf(CurrentModule) + 1;
            Module nextModule = ModuleList.ElementAt(nextIndex);

            CurrentModule = nextModule;
            CheckModulePosition();
        }

        private void GetPervModule()
        {
            int pervIndex = ModuleList.IndexOf(CurrentModule) - 1;
            Module pervModule = ModuleList.ElementAt(pervIndex);

            CurrentModule = pervModule;
            CheckModulePosition();
        }

        private void CheckModulePosition()
        {
            int indexModule = ModuleList.IndexOf(CurrentModule);

            First = indexModule == 0;
            Last = indexModule == ModuleList.Count - 1;
        }

        #region List Initializer

        private static void InitializeList()
        {
            // Ininicjalizacja
            ModuleList = new List<Module>();
            ExamList = new List<Exam>();

            // Rozdział I
            ModuleList.Add(Module.CreateModule("1.1 Czym jest .NET?", "1.1", IsNotExam));
            ModuleList.Add(Module.CreateModule("1.2 Dlaczego C#", "1.2", IsNotExam));

            // Rozdział II - sekcja I
            ModuleList.Add(Module.CreateModule("2.1.1 Pobranie Microsoft Visual Studio", "2.1.1", IsNotExam));
            ModuleList.Add(Module.CreateModule("2.1.2 Instalacja programu", "2.1.2", IsNotExam));
            ModuleList.Add(Module.CreateModule("2.1.3 Pierwszy program - Hello World", "2.1.3", IsExam));

            // Egzamin I
            ExamList.Add(new Exam
            {
                ModuleTag = "2.1.3",
                Task = string.Empty,
                Code = string.Empty,
                TestInput = string.Empty,
                TestOutput = string.Empty
            });

            // Rozdział II - sekcja II
            ModuleList.Add(Module.CreateModule("2.2.1 Typy zmiennych", "2.2.1", IsNotExam));
            ModuleList.Add(Module.CreateModule("2.2.2 Rzutowanie i konwersja", "2.2.2", IsNotExam));
            ModuleList.Add(Module.CreateModule("2.2.3 Stałe", "2.2.3", IsNotExam));
            ModuleList.Add(Module.CreateModule("2.2.4 Wyświetlenie wartości", "2.2.4", IsExam));

            // Egzamin II
            ExamList.Add(new Exam
            {
                ModuleTag = "2.2.4",
                Task = string.Empty,
                Code = string.Empty,
                TestInput = string.Empty,
                TestOutput = string.Empty
            });

            // Rozdział II - sekcja III
            ModuleList.Add(Module.CreateModule("2.3.1 Operatory arytmetyczne", "2.3.1", IsNotExam));
            ModuleList.Add(Module.CreateModule("2.3.2 Operatory logiczne", "2.3.2", IsNotExam));
            ModuleList.Add(Module.CreateModule("2.3.3 Operatory porównania i operator warunkowy", "2.3.3", IsExam));

            // Egzamin III
            ExamList.Add(new Exam
            {
                ModuleTag = "2.3.3",
                Task = string.Empty,
                Code = string.Empty,
                TestInput = string.Empty,
                TestOutput = string.Empty
            });

            // Rozdział II - sekcja IV
            ModuleList.Add(Module.CreateModule("2.4.1 Instrukcja if", "2.4.1", IsNotExam));
            ModuleList.Add(Module.CreateModule("2.4.2 Instrukcja switch", "2.4.2", IsExam));

            // Egzamin IV
            ExamList.Add(new Exam
            {
                ModuleTag = "2.4.2",
                Task = string.Empty,
                Code = string.Empty,
                TestInput = string.Empty,
                TestOutput = string.Empty
            });

            // Rozdział II - sekcja V
            ModuleList.Add(Module.CreateModule("2.5.1 Pętle while() i do-while()", "2.5.1", IsNotExam));
            ModuleList.Add(Module.CreateModule("2.5.2 Pętla for()", "2.5.2", IsNotExam));
            ModuleList.Add(Module.CreateModule("2.5.3 Pętla foreach()", "2.5.3", IsExam));

            // Egzamin V
            ExamList.Add(new Exam
            {
                ModuleTag = "2.5.3",
                Task = string.Empty,
                Code = string.Empty,
                TestInput = string.Empty,
                TestOutput = string.Empty
            });

            // Rozdział II - sekcja VI
            ModuleList.Add(Module.CreateModule("2.6.1 Tablice jednowymiarowe", "2.6.1", IsNotExam));
            ModuleList.Add(Module.CreateModule("2.6.2 Tablice wielowymiarowe", "2.6.2", IsExam));

            // Egzamin VI
            ExamList.Add(new Exam
            {
                ModuleTag = "2.6.2",
                Task = string.Empty,
                Code = string.Empty,
                TestInput = string.Empty,
                TestOutput = string.Empty
            });

            // Rozdział II - sekcja VII
            ModuleList.Add(Module.CreateModule("2.7.1 Wstęp", "2.7.1", IsNotExam));
            ModuleList.Add(Module.CreateModule("2.7.2 Pola i właściwości", "2.7.2", IsNotExam));
            ModuleList.Add(Module.CreateModule("2.7.3 Metody", "2.7.3", IsNotExam));
            ModuleList.Add(Module.CreateModule("2.7.4 Konstruktory", "2.7.4", IsNotExam));
            ModuleList.Add(Module.CreateModule("2.7.5 Dziedziczenie", "2.7.5", IsNotExam));
            ModuleList.Add(Module.CreateModule("2.7.6 Polimorfizm", "2.7.6", IsExam));

            // Egzamin VII
            ExamList.Add(new Exam
            {
                ModuleTag = "2.7.6",
                Task = string.Empty,
                Code = string.Empty,
                TestInput = string.Empty,
                TestOutput = string.Empty
            });

            // Rozdział II - sekcja VIII
            ModuleList.Add(Module.CreateModule("2.8 Interfejsy", "2.8", IsExam));

            // Egzamin VIII
            ExamList.Add(new Exam
            {
                ModuleTag = "2.8",
                Task = string.Empty,
                Code = string.Empty,
                TestInput = string.Empty,
                TestOutput = string.Empty
            });

            // Rozdział II - sekcja IX
            ModuleList.Add(Module.CreateModule("2.9 Klasy abstrakcyjne", "2.9", IsExam));

            // Egzamin IX
            ExamList.Add(new Exam
            {
                ModuleTag = "2.9",
                Task = string.Empty,
                Code = string.Empty,
                TestInput = string.Empty,
                TestOutput = string.Empty
            });

            // Rozdział III - sekcja I
            ModuleList.Add(Module.CreateModule("3.1.1 Płytkie kopie", "3.1.1", IsNotExam));
            ModuleList.Add(Module.CreateModule("3.1.2 Głębokie kopie", "3.1.2", IsNotExam));

            // Rozdział III - sekcja II
            ModuleList.Add(Module.CreateModule("3.2.1 Kolekcje", "3.2.1", IsNotExam));
            ModuleList.Add(Module.CreateModule("3.2.2. Typy generyczne", "3.2.2", IsNotExam));

            // Rozdział III - sekcja III
            ModuleList.Add(Module.CreateModule("3.3.1 Budowa", "3.3.1", IsNotExam));
            ModuleList.Add(Module.CreateModule("3.3.2 Delegaty złożone i anonimowe", "3.3.2", IsNotExam));

            // Rozdział III - sekcja IV
            ModuleList.Add(Module.CreateModule("3.4 Wyrażenia lambda", "3.4", IsNotExam));

            // Rozdział III - sekcja V
            ModuleList.Add(Module.CreateModule("3.5 Parę słów o Garbage Collector", "3.5", IsNotExam));
        }

        #endregion List Initializer
    }

    public enum ActionModule { Next, Perv }

    public struct Module
    {
        public string ModuleName { get; set; }
        public string ModuleTag { get; set; }
        public bool IsExamNext { get; set; }

        public Module(string moduleName, string moduleTag, bool isExamNext) : this()
        {
            this.ModuleName = moduleName;
            this.ModuleTag = moduleTag;
            this.IsExamNext = isExamNext;
        }

        public static Module CreateModule(string moduleName, string moduleTag, bool isExamNext)
        {
            return new Module(moduleName, moduleTag, isExamNext);
        }
    }

    public struct Exam
    {
        public string ModuleTag { get; set; }
        public string Task { get; set; }
        public string Code { get; set; }
        public string TestOutput { get; set; }
        public string TestInput { get; set; }
    }
}