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
    /// Interakční logika pro VyberPostav.xaml
    /// </summary>
    public partial class VyberMapy : Window
    {
        bool isMapaReady = false;
        DateTime zahajeni = DateTime.Now;
        DispatcherTimer tmr = new DispatcherTimer();
        BitmapImage[] mapy = new BitmapImage[] {
            new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/background.png")),
            new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/background.png")),
            new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/background.png"))
        };
        public VyberMapy()
        {
            InitializeComponent();
            tmr.Interval = TimeSpan.FromMilliseconds(1000 / 60);
            tmr.Tick += Tmr_Tick;
            tmr.Start();

            selectedMap.Tag = 0;
            selectedMap.Source = mapy[(int)selectedMap.Tag];
        }

        private void Tmr_Tick(object sender, EventArgs e)
        {
            if (isMapaReady && mapReady.Opacity < 1) mapReady.Opacity += 0.05;
            if (isMapaReady && zahajeni < DateTime.Now)
            {
                Hitboxy.pozadiMapa = Mapy.getMapa((int)selectedMap.Tag);
                tmr.Stop();
                Hitboxy.aktivniKolo = 0;
                Hitboxy.kola = new int[3] { 0, 0, 0 };
                MainWindow hra = new MainWindow();
                hra.Show();
                System.Threading.Thread.Sleep(50);
                this.Close();
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Label lbl)
            {
                switch ((string)lbl.Tag)
                {
                    case "mapavlevo":
                        if (!isMapaReady)
                        {
                            if ((int)selectedMap.Tag == 0)
                            {
                                selectedMap.Tag = mapy.Length - 1;
                            }
                            else selectedMap.Tag = (int)selectedMap.Tag - 1;
                            selectedMap.Source = mapy[(int)selectedMap.Tag];
                        }
                        break;
                    case "mapavpravo":
                        if (!isMapaReady)
                        {
                            if ((int)selectedMap.Tag == mapy.Length - 1)
                            {
                                selectedMap.Tag = 0;
                            }
                            else selectedMap.Tag = (int)selectedMap.Tag + 1;
                            selectedMap.Source = mapy[(int)selectedMap.Tag];
                        }
                        break;
                }
            }
        }

        private void selectedHrac1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!isMapaReady)
            {
                mapReady.Visibility = Visibility.Visible;
                isMapaReady = true;
                zahajeni = DateTime.Now + TimeSpan.FromSeconds(2);
            }
        }

        private void Navrat(object sender, RoutedEventArgs e)
        {
            HlavniMenu okno = new HlavniMenu();
            okno.Show();
            System.Threading.Thread.Sleep(50);
            this.Close();
        }

        private void gridVyber_Loaded(object sender, RoutedEventArgs e)
        {
            Hitboxy.zmenitScale((Grid)sender);
        }
    }
}
