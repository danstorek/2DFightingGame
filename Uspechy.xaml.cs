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
    /// Interaction logic for Uspechy.xaml
    /// </summary>
    public partial class Uspechy : Window
    {
        bool klik = false;
        DispatcherTimer animacePrechod = new DispatcherTimer();
        public Uspechy()
        {
            InitializeComponent();

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
        private void gridHlavni_Loaded(object sender, RoutedEventArgs e)
        {
            Globalni.zmenitScale((Grid)sender);

            //Načtení žebříčku
            List<KeyValuePair<string, int>> zebricek = Globalni.ukl.SerazeneSkore();
            int aktualniVyska = 0;
            int aktualniPoradi = 1;
            foreach(KeyValuePair<string, int> i in zebricek)
            {
                //Umístění
                Label tmp = new Label();
                tmp.Content = aktualniPoradi.ToString() + ".";
                tmp.Width = 183;
                tmp.Height = 90;
                tmp.FontSize = 40;
                tmp.Foreground = Brushes.White;
                tmp.HorizontalContentAlignment = HorizontalAlignment.Center;
                tmp.VerticalAlignment = VerticalAlignment.Top;
                tmp.HorizontalAlignment = HorizontalAlignment.Left;
                tmp.Margin = new Thickness(0, aktualniVyska, 0, 0);

                //Jméno hráče
                Label tmp1 = new Label();
                tmp1.Content = i.Key;
                tmp1.Width = 492;
                tmp1.Height = 90;
                tmp1.FontSize = 40;
                tmp1.Foreground = Brushes.White;
                tmp1.VerticalAlignment = VerticalAlignment.Top;
                tmp1.HorizontalAlignment = HorizontalAlignment.Left;
                tmp1.Margin = new Thickness(200, aktualniVyska, 0, 0);

                //Skóre
                Label tmp2 = new Label();
                tmp2.Content = i.Value;
                tmp2.Width = 233;
                tmp2.Height = 90;
                tmp2.FontSize = 40;
                tmp2.Foreground = Brushes.White;
                tmp2.HorizontalContentAlignment = HorizontalAlignment.Center;
                tmp2.VerticalAlignment = VerticalAlignment.Top;
                tmp2.HorizontalAlignment = HorizontalAlignment.Left;
                tmp2.Margin = new Thickness(692, aktualniVyska, 0, 0);

                gridZebricek.Children.Add(tmp);
                gridZebricek.Children.Add(tmp1);
                gridZebricek.Children.Add(tmp2);

                aktualniPoradi++;
                aktualniVyska += 80;
            }

            aktualniVyska = 0;
            //Načtení achievementů
            for(int i = 0; i<=6; i++)
            {
                Grid tmp = Achievementy.achievementUkazatel(i);
                tmp.Margin = new Thickness(50, aktualniVyska, 0, 0);
                gridUspechy.Children.Add(tmp);
                aktualniVyska += 180;
            }
        }
        private void Navrat(object sender, RoutedEventArgs e)
        {
            klik = true;
        }
        private void VymazatZebricek(object sender, RoutedEventArgs e)
        {
            Globalni.ukl.VymazatZebricek();
            gridZebricek.Children.Clear();
        }
    }
}
