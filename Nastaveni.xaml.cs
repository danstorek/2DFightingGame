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
    /// Interakční logika pro Nastaveni.xaml
    /// </summary>
    public partial class Nastaveni : Window
    {
        int zvoleneTlacitko = -1;

        bool klik = false;
        DispatcherTimer animacePrechod = new DispatcherTimer();
        public Nastaveni()
        {
            InitializeComponent();

            for (int i = 0; i < ovladani1.Children.Count; i++)
            {
                if (ovladani1.Children[i] is Label lb && lb.Tag != null)
                {
                    Key tlacitko = Globalni.ukl.nastaveniKlaves[Convert.ToInt32((string)lb.Tag)];
                    if (tlacitko == Key.Up) lb.Content = "↑";
                    else if (tlacitko == Key.Down) lb.Content = "↓";
                    else if (tlacitko == Key.Left) lb.Content = "←";
                    else if (tlacitko == Key.Right) lb.Content = "→";
                    else lb.Content = tlacitko.ToString();
                }
                if (ovladani2.Children[i] is Label lb1 && lb1.Tag != null)
                {
                    Key tlacitko = Globalni.ukl.nastaveniKlaves[Convert.ToInt32((string)lb1.Tag)];
                    if (tlacitko == Key.Up) lb1.Content = "↑";
                    else if (tlacitko == Key.Down) lb1.Content = "↓";
                    else if (tlacitko == Key.Left) lb1.Content = "←";
                    else if (tlacitko == Key.Right) lb1.Content = "→";
                    else lb1.Content = tlacitko.ToString();
                }
            }

            //Animace přechodu
            animacePrechod.Tick += AnimacePrechod_Tick;
            animacePrechod.Interval = TimeSpan.FromMilliseconds(1000 / 90);
            animacePrechod.Start();
        }
        private void AnimacePrechod_Tick(object sender, EventArgs e)
        {
            if (prechod2.Width > 1) prechod2.Width -= 120;
            if (klik && prechod1.Width < 1920) prechod1.Width += 120;
            if (klik && prechod1.Width >= 1920)
            {
                animacePrechod.Stop();
                HlavniMenu okno = new HlavniMenu();
                okno.Show();
                System.Threading.Thread.Sleep(50);
                this.Close();
            }
        }
        private void Navrat(object sender, RoutedEventArgs e)
        {
            klik = true;
        }

        private void klikTlacitko(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < ovladani1.Children.Count; i++)
            {
                if (ovladani1.Children[i] is Label lb)
                {
                    lb.Background = Brushes.Transparent;
                }
                if (ovladani2.Children[i] is Label lb1)
                {
                    lb1.Background = Brushes.Transparent;
                }
            }
            if (sender is Label tlacitko)
            {
                zvoleneTlacitko = Convert.ToInt32(tlacitko.Tag.ToString());
                tlacitko.Background = Brushes.LightBlue;
            }
        }

        private void zmenitTlacitko(object sender, KeyEventArgs e)
        {
            if (zvoleneTlacitko != -1 && !Globalni.ukl.nastaveniKlaves.Contains(e.Key))
            {
                Globalni.ukl.nastaveniKlaves[zvoleneTlacitko] = e.Key;
                Globalni.ukl.Ulozit();
                for (int i = 0; i < ovladani1.Children.Count; i++)
                {
                    if (ovladani1.Children[i] is Label lb)
                    {
                        if ((string)lb.Tag == zvoleneTlacitko.ToString())
                        {
                            if (e.Key == Key.Up) lb.Content = "↑";
                            else if (e.Key == Key.Down) lb.Content = "↓";
                            else if (e.Key == Key.Left) lb.Content = "←";
                            else if (e.Key == Key.Right) lb.Content = "→";
                            else lb.Content = e.Key.ToString();
                        }
                    }
                    if (ovladani2.Children[i] is Label lb1)
                    {
                        if ((string)lb1.Tag == zvoleneTlacitko.ToString())
                        {
                            if (e.Key == Key.Up) lb1.Content = "↑";
                            else if (e.Key == Key.Down) lb1.Content = "↓";
                            else if (e.Key == Key.Left) lb1.Content = "←";
                            else if (e.Key == Key.Right) lb1.Content = "→";
                            else lb1.Content = e.Key.ToString();
                        }
                    }
                }
            }
        }

        private void Obnovit(object sender, RoutedEventArgs e)
        {
            Globalni.ukl.nastaveniKlaves = new List<Key>()
            {
                Key.Up,Key.Down,Key.Left,Key.Right,
                Key.N,Key.M,
                Key.W,Key.S,Key.A,Key.D,
                Key.Q,Key.E
            };
            Globalni.ukl.Ulozit();

            for (int i = 0; i < ovladani1.Children.Count; i++)
            {
                if (ovladani1.Children[i] is Label lb && lb.Tag != null)
                {
                    Key tlacitko = Globalni.ukl.nastaveniKlaves[Convert.ToInt32((string)lb.Tag)];
                    if (tlacitko == Key.Up) lb.Content = "↑";
                    else if (tlacitko == Key.Down) lb.Content = "↓";
                    else if (tlacitko == Key.Left) lb.Content = "←";
                    else if (tlacitko == Key.Right) lb.Content = "→";
                    else lb.Content = tlacitko.ToString();
                }
                if (ovladani2.Children[i] is Label lb1 && lb1.Tag != null)
                {
                    Key tlacitko = Globalni.ukl.nastaveniKlaves[Convert.ToInt32((string)lb1.Tag)];
                    if (tlacitko == Key.Up) lb1.Content = "↑";
                    else if (tlacitko == Key.Down) lb1.Content = "↓";
                    else if (tlacitko == Key.Left) lb1.Content = "←";
                    else if (tlacitko == Key.Right) lb1.Content = "→";
                    else lb1.Content = tlacitko.ToString();
                }
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Globalni.zmenitScale((Grid)sender);
        }
    }
}
