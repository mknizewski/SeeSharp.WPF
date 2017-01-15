using SeeSharp.BO.Dictionaries;
using SeeSharp.BO.Managers;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SeeSharp.WPF
{
    /// <summary>
    /// Interaction logic for AchivmentItem.xaml
    /// </summary>
    public partial class AchivmentItem : UserControl
    {
        private Achivment _achivment;

        public AchivmentItem(Achivment achivment)
        {
            _achivment = achivment;

            InitializeComponent();
            InitializeAchivment();
        }

        private void InitializeAchivment()
        {
            string achivImageFileName = string.Format(AppSettingsDictionary.AchivmentImageDirectory, _achivment.File);
            Uri uri = new Uri(achivImageFileName, UriKind.Relative);

            this.ImageAchiv.Source = new BitmapImage(uri);
            this.TitleText.Text = _achivment.Title;
            this.DetialText.Text = _achivment.Details;
        }
    }
}