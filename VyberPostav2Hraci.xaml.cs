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
    public partial class VyberPostav2Hraci : Window
    {
        bool klik = false;

        bool hrac1Ready = false;
        bool hrac2Ready = false;
        DateTime zahajeni = DateTime.Now;
        DispatcherTimer animacePrechod = new DispatcherTimer();
        BitmapImage[] postavy = new BitmapImage[] {
            new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/nahled.png")),
            new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/nahled.png")),
            new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/nahled.png")),
            new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/nahled.png")),
        };
        public VyberPostav2Hraci()
        {
            InitializeComponent();

            //Animace přechodu
            animacePrechod.Tick += AnimacePrechod_Tick;
            animacePrechod.Interval = TimeSpan.FromMilliseconds(1000 / 90);
            animacePrechod.Start();

            //Základní názvy postav ve hře pro jednoho hráče
            if (Globalni.rezimHry)
            {
                jmenoHrac1.Text = "Hráč";
                jmenoHrac2.Text = "Bot";
                botVarovani.Visibility = Visibility.Visible;
            }

            selectedHrac1.Tag = 0;
            selectedHrac2.Tag = 0;
            selectedHrac1.Source = postavy[(int)selectedHrac1.Tag];
            selectedHrac2.Source = postavy[(int)selectedHrac2.Tag];
        }

        private void AnimacePrechod_Tick(object sender, EventArgs e)
        {
            if (prechod2.Width > 1) prechod2.Width -= 120;
            if ((lblReady1.Opacity >= 1 && lblReady2.Opacity >= 1) || klik && prechod1.Width < 1920) prechod1.Width += 120;
            if (hrac1Ready && hrac2Ready && prechod1.Width >= 1920)
            {
                Globalni.hrac1Jmeno = jmenoHrac1.Text;
                Globalni.hrac2Jmeno = jmenoHrac2.Text;

                Globalni.hrac1Postava = (int)selectedHrac1.Tag;
                Globalni.hrac2Postava = (int)selectedHrac2.Tag;
                Globalni.vez = -1;
                animacePrechod.Stop();
                VyberMapy vybermapy = new VyberMapy();
                vybermapy.Show();
                System.Threading.Thread.Sleep(50);
                this.Close();
            }

            if (klik && prechod1.Width >= 1920)
            {
                animacePrechod.Stop();
                HlavniMenu okno = new HlavniMenu();
                okno.Show();
                System.Threading.Thread.Sleep(50);
                this.Close();
            }

            if (Globalni.rezimHry && (int)selectedHrac1.Tag == 1 && Globalni.ukl.ZiskatPrubeh(0) < Achievementy.getSplneni(0)) lblUzamceno1.Visibility = Visibility.Visible;
            else if (Globalni.rezimHry && (int)selectedHrac1.Tag == 2 && Globalni.ukl.ZiskatPrubeh(5) < Achievementy.getSplneni(5)) lblUzamceno1.Visibility = Visibility.Visible;
            else if (Globalni.rezimHry && (int)selectedHrac1.Tag == 3 && Globalni.ukl.ZiskatPrubeh(6) < Achievementy.getSplneni(6)) lblUzamceno1.Visibility = Visibility.Visible;
            else lblUzamceno1.Visibility = Visibility.Hidden;

            if (hrac1Ready && lblReady1.Opacity < 1) lblReady1.Opacity += 0.05;
            if (hrac2Ready && lblReady2.Opacity < 1) lblReady2.Opacity += 0.05;
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
                    case "hrac2vlevo":
                        if (!hrac2Ready)
                        {
                            if ((int)selectedHrac2.Tag == 0)
                            {
                                selectedHrac2.Tag = postavy.Length - 1;
                            }
                            else selectedHrac2.Tag = (int)selectedHrac2.Tag - 1;
                            selectedHrac2.Source = postavy[(int)selectedHrac2.Tag];
                        }
                        break;
                    case "hrac2vpravo":
                        if (!hrac2Ready)
                        {
                            if ((int)selectedHrac2.Tag == postavy.Length - 1)
                            {
                                selectedHrac2.Tag = 0;
                            }
                            else selectedHrac2.Tag = (int)selectedHrac2.Tag + 1;
                            selectedHrac2.Source = postavy[(int)selectedHrac2.Tag];
                        }
                        break;
                }
            }
        }

        private void selectedHrac1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!hrac1Ready)
            {
                if (Globalni.rezimHry)
                {
                    if ((int)selectedHrac1.Tag == 1 && Globalni.ukl.ZiskatPrubeh(0) < Achievementy.getSplneni(0)) return;
                    if ((int)selectedHrac1.Tag == 2 && Globalni.ukl.ZiskatPrubeh(5) < Achievementy.getSplneni(5)) return;
                    if ((int)selectedHrac1.Tag == 3 && Globalni.ukl.ZiskatPrubeh(6) < Achievementy.getSplneni(6)) return;
                }

                jmenoHrac1.IsEnabled = false;
                lblReady1.Visibility = Visibility.Visible;
                hrac1Ready = true;
                if (Globalni.rezimHry && !hrac2Ready)
                {
                    lblReady2.Visibility = Visibility.Visible;
                    selectedHrac2.Tag = Globalni.rnd.Next(0, postavy.Length);
                    selectedHrac2.Source = postavy[(int)selectedHrac2.Tag];
                    hrac2Ready = true;
                }
                //if (hrac1Ready && hrac2Ready) zahajeni = DateTime.Now + TimeSpan.FromSeconds(2);
            }
        }

        private void selectedHrac2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!hrac2Ready)
            {
                jmenoHrac2.IsEnabled = false;
                lblReady2.Visibility = Visibility.Visible;
                hrac2Ready = true;
                if (hrac1Ready && hrac2Ready) zahajeni = DateTime.Now + TimeSpan.FromSeconds(2);
            }

        }

        private void Navrat(object sender, RoutedEventArgs e)
        {
            klik = true;
        }

        private void gridVyber_Loaded(object sender, RoutedEventArgs e)
        {
            Globalni.zmenitScale((Grid)sender);
        }
    }
}
