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

        private void HraProJednohoHrace(object sender, RoutedEventArgs e)
        {
            Hitboxy.rezimHry = true;
            VyberPostav2Hraci vyberPostav = new VyberPostav2Hraci();
            vyberPostav.Show();
            System.Threading.Thread.Sleep(50);
            this.Close();
        }

        private void HraProDvaHrace(object sender, RoutedEventArgs e)
        {
            Hitboxy.rezimHry = false;
            VyberPostav2Hraci vyberPostav = new VyberPostav2Hraci();
            vyberPostav.Show();
            System.Threading.Thread.Sleep(50);
            this.Close();
        }

        private void hlMenu_Loaded(object sender, RoutedEventArgs e)
        {
            Hitboxy.zmenitScale((Grid)sender);
        }

        private void UspechyZebricek(object sender, RoutedEventArgs e)
        {
            Uspechy uspechy = new Uspechy();
            uspechy.Show();
            System.Threading.Thread.Sleep(50);
            this.Close();
        }

        private void hlMenu_MouseMove(object sender, MouseEventArgs e)
        {
            double mysX = e.GetPosition(this).X;
            double mysY = e.GetPosition(this).Y;

            bdNapoveda.Margin = new Thickness(mysX + 10, mysY - 160, 0, 0);

            if (mysX > 350 && mysX < 840)
            {
                if (mysY > 418 && mysY < 578)
                {
                    lbNapoveda.Content = "Spustí hru pro jednoho hráče, můžeš\nhrát pouze s odemčenými postavami";
                    bdNapoveda.Visibility = Visibility.Visible;
                }
                else if (mysY > 583 && mysY < 743)
                {
                    lbNapoveda.Content = "Spustí hru pro dva hráče, můžeš\nhrát s jakoukoliv postavou";
                    bdNapoveda.Visibility = Visibility.Visible;
                }
                else if (mysY > 748 && mysY < 908)
                {
                    lbNapoveda.Content = "Zobrazí ti odemknuté úspěchy a\nžebříček tvých nejlepší výsledků";
                    bdNapoveda.Visibility = Visibility.Visible;
                }
                else if (mysY > 920 && mysY < 1080)
                {
                    lbNapoveda.Content = "Ukončí hru";
                    bdNapoveda.Visibility = Visibility.Visible;
                }
                else bdNapoveda.Visibility = Visibility.Hidden;
            }
            else bdNapoveda.Visibility = Visibility.Hidden;
        }
    }
}
