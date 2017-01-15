using System;
using System.Windows.Controls;

namespace SeeSharp.WPF
{
    /// <summary>
    /// Interaction logic for AboutAuthors.xaml
    /// </summary>
    public partial class AboutAuthors : UserControl
    {
        private const string CopyRightInfoPattern = "Uniwersytet w Białymstoku \n Białystok, {0}";

        public AboutAuthors()
        {
            InitializeComponent();
            this.CopyrightInformation.Text = string.Format(CopyRightInfoPattern, DateTime.Now.Year.ToString());
            this.Focus();
        }
    }
}