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
using System.Windows.Threading;

namespace _2DFightingGame
{
    /// <summary>
    /// Interakční logika pro HlavniMenu.xaml
    /// </summary>
    public partial class HlavniMenu : Window
    {
        bool klik = false;
        int vyber = -1;

        List<BitmapImage> animacePostava = new List<BitmapImage>();
        DispatcherTimer animaceTick = new DispatcherTimer();
        int tick = 0;

        DispatcherTimer animacePrechod = new DispatcherTimer();
        public HlavniMenu()
        {
            InitializeComponent();

            //Animace přechodu
            animacePrechod.Tick += AnimacePrechod_Tick;
            animacePrechod.Interval = TimeSpan.FromMilliseconds(1000 / 90);

            animaceTick.Tick += AnimaceTick_Tick;
            animaceTick.Interval = TimeSpan.FromMilliseconds(100);

            //Příprava obrázku pro animaci
            animacePostava.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/menu_idle/0.png")));
            animacePostava.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/menu_idle/1.png")));
            animacePostava.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/menu_idle/2.png")));
            animacePostava.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/menu_idle/3.png")));
            animacePostava.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/menu_idle/4.png")));
            animacePostava.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/menu_idle/5.png")));
            animacePostava.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/menu_idle/7.png")));
            animacePostava.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/menu_idle/8.png")));
            animacePostava.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/menu_idle/9.png")));

            animacePrechod.Start();
            animaceTick.Start();
        }

        private void AnimacePrechod_Tick(object sender, EventArgs e)
        {
            if (prechod2.Width > 1) prechod2.Width -= 120;
            if (klik && prechod1.Width < 1920) prechod1.Width += 120;
            else if (klik)
            {
                VyberPostav2Hraci vyberPostav;
                switch (vyber)
                {
                    case 1:
                        Hitboxy.rezimHry = true;
                        vyberPostav = new VyberPostav2Hraci();
                        vyberPostav.Show();
                        System.Threading.Thread.Sleep(50);
                        break;
                    case 2:
                        Hitboxy.rezimHry = false;
                        vyberPostav = new VyberPostav2Hraci();
                        vyberPostav.Show();
                        System.Threading.Thread.Sleep(50);
                        break;
                    case 3:
                        Uspechy uspechy = new Uspechy();
                        uspechy.Show();
                        System.Threading.Thread.Sleep(50);
                        break;
                    case 4:
                        Nastaveni nastaveni = new Nastaveni();
                        nastaveni.Show();
                        System.Threading.Thread.Sleep(50);
                        break;
                }
                animacePrechod.Stop();
                animaceTick.Stop();
                this.Close();
            }
        }

        private void AnimaceTick_Tick(object sender, EventArgs e)
        {
            menuCharacter.Source = animacePostava[tick];
            tick++;
            if (tick >= animacePostava.Count) tick = 0;
        }

        private void Ukoncit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void HraProJednohoHrace(object sender, RoutedEventArgs e)
        {
            klik = true;
            vyber = 1;
        }

        private void HraProDvaHrace(object sender, RoutedEventArgs e)
        {
            klik = true;
            vyber = 2;
        }

        private void hlMenu_Loaded(object sender, RoutedEventArgs e)
        {
            Hitboxy.zmenitScale((Grid)sender);
        }

        private void UspechyZebricek(object sender, RoutedEventArgs e)
        {
            klik = true;
            vyber = 3;
        }
        private void Nastaveni(object sender, RoutedEventArgs e)
        {
            klik = true;
            vyber = 4;
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
            else if(mysX > 955 && mysX < 1160)
            {
                if (mysY > 844 && mysY < 1065)
                {
                    lbNapoveda.Content = "Nastavení hry";
                    bdNapoveda.Visibility = Visibility.Visible;
                }
                else bdNapoveda.Visibility = Visibility.Hidden;
            }
            else bdNapoveda.Visibility = Visibility.Hidden;
        }
    }
}
