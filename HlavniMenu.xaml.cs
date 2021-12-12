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
using System.Windows.Shapes;

namespace _2DFightingGame
{
    /// <summary>
    /// Interakční logika pro HlavniMenu.xaml
    /// </summary>
    public partial class HlavniMenu : Window
    {
        public HlavniMenu()
        {
            InitializeComponent();
        }

        private void Ukoncit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void HraProDvaHrace(object sender, RoutedEventArgs e)
        {
            VyberPostav2Hraci vyberPostav = new VyberPostav2Hraci();
            vyberPostav.Show();
            System.Threading.Thread.Sleep(50);
            this.Close();
        }

        private void hlMenu_Loaded(object sender, RoutedEventArgs e)
        {
            Hitboxy.zmenitScale((Grid)sender);
        }
    }
}
