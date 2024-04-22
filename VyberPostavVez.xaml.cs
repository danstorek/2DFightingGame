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
    /// Interakční logika pro VyberPostavVez.xaml
    /// </summary>
    public partial class VyberPostavVez : Window
    {
        bool hrac1Ready = false;
        bool klik = false;
        DispatcherTimer animacePrechod = new DispatcherTimer();

        BitmapImage[] postavy = new BitmapImage[] {
            new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/nahled.png")),
            new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/nahled.png")),
            new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/nahled.png")),
            new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/nahled.png")),
        };
        public VyberPostavVez()
        {
            InitializeComponent();

            //Animace přechodu
            animacePrechod.Tick += AnimacePrechod_Tick;
            animacePrechod.Interval = TimeSpan.FromMilliseconds(1000 / 90);
            animacePrechod.Start();

            selectedHrac1.Tag = 0;
            selectedHrac1.Source = postavy[(int)selectedHrac1.Tag];
        }
        private void Navrat(object sender, RoutedEventArgs e)
        {
            klik = true;
        }
        private void AnimacePrechod_Tick(object sender, EventArgs e)
        {
            if (prechod2.Width > 1) prechod2.Width -= 120;
            if (klik && prechod1.Width < 1920 || hrac1Ready) prechod1.Width += 120;

            if (klik && prechod1.Width >= 1920)
            {
                //tmr.Stop();
                animacePrechod.Stop();
                HlavniMenu okno = new HlavniMenu();
                okno.Show();
                System.Threading.Thread.Sleep(50);
                this.Close();
            }
            else if (hrac1Ready && prechod1.Width >= 1920)
            {
                //Výběr obtížnosti
                if ((bool)obtLehka.IsChecked) Globalni.obtiznost = 0;
                if ((bool)obtStredni.IsChecked) Globalni.obtiznost = 1;
                if ((bool)obtTezka.IsChecked) Globalni.obtiznost = 2;

                Globalni.vezMapy[0] = 1;
                Globalni.vezMapy[1] = 4;
                Globalni.vezMapy[2] = 0;
                Globalni.vezMapy[3] = 2;
                Globalni.vezMapy[4] = 3;

                Globalni.obdrzeneCelkem = 0;
                Globalni.skoreCelkem = 0;
                Globalni.hrac1Jmeno = "Hráč";
                Globalni.hrac2Jmeno = "Bot";
                Globalni.hrac1Postava = (int)selectedHrac1.Tag;
                Globalni.vez = 0;
                animacePrechod.Stop();
                Globalni.pozadiMapa = Mapy.getMapa(Globalni.vezMapy[Globalni.vez]);
                Globalni.aktivniKolo = 0;
                Globalni.kola = new int[3] { 0, 0, 0 };
                MainWindow hra = new MainWindow();
                hra.Show();
                System.Threading.Thread.Sleep(50);
                this.Close();
            }

            if (Globalni.rezimHry && (int)selectedHrac1.Tag == 1 && Globalni.ukl.ZiskatPrubeh(0) < Achievementy.getSplneni(0)) lblUzamceno1.Visibility = Visibility.Visible;
            else if (Globalni.rezimHry && (int)selectedHrac1.Tag == 2 && Globalni.ukl.ZiskatPrubeh(5) < Achievementy.getSplneni(5)) lblUzamceno1.Visibility = Visibility.Visible;
            else if (Globalni.rezimHry && (int)selectedHrac1.Tag == 3 && Globalni.ukl.ZiskatPrubeh(6) < Achievementy.getSplneni(6)) lblUzamceno1.Visibility = Visibility.Visible;
            else lblUzamceno1.Visibility = Visibility.Hidden;
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Label lbl)
            {
                switch ((string)lbl.Tag)
                {
                    case "hrac1vlevo":
                        if (!hrac1Ready)
                        {
                            if ((int)selectedHrac1.Tag == 0)
                            {
                                selectedHrac1.Tag = postavy.Length - 1;
                            }
                            else selectedHrac1.Tag = (int)selectedHrac1.Tag - 1;
                            selectedHrac1.Source = postavy[(int)selectedHrac1.Tag];
                        }
                        break;
                    case "hrac1vpravo":
                        if (!hrac1Ready)
                        {
                            if ((int)selectedHrac1.Tag == postavy.Length - 1)
                            {
                                selectedHrac1.Tag = 0;
                            }
                            else selectedHrac1.Tag = (int)selectedHrac1.Tag + 1;
                            selectedHrac1.Source = postavy[(int)selectedHrac1.Tag];
                        }
                        break;
                }
            }
        }
        private void selectedHrac1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((int)selectedHrac1.Tag == 1 && Globalni.ukl.ZiskatPrubeh(0) < Achievementy.getSplneni(0)) return;
            if ((int)selectedHrac1.Tag == 2 && Globalni.ukl.ZiskatPrubeh(5) < Achievementy.getSplneni(5)) return;
            if ((int)selectedHrac1.Tag == 3 && Globalni.ukl.ZiskatPrubeh(6) < Achievementy.getSplneni(6)) return;
            hrac1Ready = true;
        }

        private void vyberVez_Loaded(object sender, RoutedEventArgs e)
        {
            Globalni.zmenitScale((Grid)sender);
        }
    }
}
