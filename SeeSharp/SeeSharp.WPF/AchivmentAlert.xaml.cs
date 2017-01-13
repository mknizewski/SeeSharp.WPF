using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SeeSharp.BO.Managers;

namespace SeeSharp.WPF
{
    /// <summary>
    /// Interaction logic for AchivmentAlert.xaml
    /// </summary>
    public partial class AchivmentAlert : UserControl
    {
        private Achivment achivment;

        public AchivmentAlert()
        {
            InitializeComponent();
        }

        public AchivmentAlert(Achivment achivment)
        {
            this.achivment = achivment;
        }
    }
}
