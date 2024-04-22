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
        bool klikSingle = false;
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
                VyberPostavVez vyberPostavVez;
                switch (vyber)
                {
                    case 1:
                        Globalni.rezimHry = true;
                        vyberPostav = new VyberPostav2Hraci();
                        vyberPostav.Show();
                        System.Threading.Thread.Sleep(50);
                        break;
                    case 5:
                        Globalni.rezimHry = true;
                        vyberPostavVez = new VyberPostavVez();
                        vyberPostavVez.Show();
                        System.Threading.Thread.Sleep(50);
                        break;
                    case 2:
                        Globalni.rezimHry = false;
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

            if (klikSingle)
            {
                btnSingle.Visibility = Visibility.Hidden;
                btnLocal.Visibility = Visibility.Hidden;
                btnExit.Visibility = Visibility.Hidden;
                btnUspechy.Visibility = Visibility.Hidden;

                btnArcade.Visibility = Visibility.Visible;
                btnBack.Visibility = Visibility.Visible;
                btnQuick.Visibility = Visibility.Visible;
            }
            else
            {
                btnSingle.Visibility = Visibility.Visible;
                btnLocal.Visibility = Visibility.Visible;
                btnExit.Visibility = Visibility.Visible;
                btnUspechy.Visibility = Visibility.Visible;

                btnArcade.Visibility = Visibility.Hidden;
                btnBack.Visibility= Visibility.Hidden;
                btnQuick.Visibility= Visibility.Hidden;
            }

            if (klikSingle && btnSingle.Opacity > 0)
            {
                btnSingle.Opacity -= 0.1;
                btnLocal.Opacity -= 0.1;
                btnExit.Opacity -= 0.1;
                btnUspechy.Opacity -= 0.1;

                btnArcade.Opacity += 0.1;
                btnBack.Opacity += 0.1;
                btnQuick.Opacity += 0.1;

                btnSingle.Margin = new Thickness(btnSingle.Margin.Left, btnSingle.Margin.Top, btnSingle.Margin.Right, btnSingle.Margin.Bottom - 5);
                btnLocal.Margin = new Thickness(btnLocal.Margin.Left, btnLocal.Margin.Top, btnLocal.Margin.Right, btnLocal.Margin.Bottom - 5);
                btnExit.Margin = new Thickness(btnExit.Margin.Left, btnExit.Margin.Top, btnExit.Margin.Right, btnExit.Margin.Bottom - 5);
                btnUspechy.Margin = new Thickness(btnUspechy.Margin.Left, btnUspechy.Margin.Top, btnUspechy.Margin.Right, btnUspechy.Margin.Bottom - 5);

                btnArcade.Margin = new Thickness(btnArcade.Margin.Left, btnArcade.Margin.Top, btnArcade.Margin.Right, btnArcade.Margin.Bottom + 5);
                btnBack.Margin = new Thickness(btnBack.Margin.Left, btnBack.Margin.Top, btnBack.Margin.Right, btnBack.Margin.Bottom + 5);
                btnQuick.Margin = new Thickness(btnQuick.Margin.Left, btnQuick.Margin.Top, btnQuick.Margin.Right, btnQuick.Margin.Bottom + 5);
            }
            else if(!klikSingle && btnSingle.Opacity < 1)
            {
                btnSingle.Opacity += 0.1;
                btnLocal.Opacity += 0.1;
                btnExit.Opacity += 0.1;
                btnUspechy.Opacity += 0.1;

                btnArcade.Opacity -= 0.1;
                btnBack.Opacity -= 0.1;
                btnQuick.Opacity -= 0.1;

                btnSingle.Margin = new Thickness(btnSingle.Margin.Left, btnSingle.Margin.Top, btnSingle.Margin.Right, btnSingle.Margin.Bottom + 5);
                btnLocal.Margin = new Thickness(btnLocal.Margin.Left, btnLocal.Margin.Top, btnLocal.Margin.Right, btnLocal.Margin.Bottom + 5);
                btnExit.Margin = new Thickness(btnExit.Margin.Left, btnExit.Margin.Top, btnExit.Margin.Right, btnExit.Margin.Bottom + 5);
                btnUspechy.Margin = new Thickness(btnUspechy.Margin.Left, btnUspechy.Margin.Top, btnUspechy.Margin.Right, btnUspechy.Margin.Bottom + 5);

                btnArcade.Margin = new Thickness(btnArcade.Margin.Left, btnArcade.Margin.Top, btnArcade.Margin.Right, btnArcade.Margin.Bottom - 5);
                btnBack.Margin = new Thickness(btnBack.Margin.Left, btnBack.Margin.Top, btnBack.Margin.Right, btnBack.Margin.Bottom - 5);
                btnQuick.Margin = new Thickness(btnQuick.Margin.Left, btnQuick.Margin.Top, btnQuick.Margin.Right, btnQuick.Margin.Bottom - 5);
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
            klikSingle = true;
        }

        private void HraProDvaHrace(object sender, RoutedEventArgs e)
        {
            klik = true;
            vyber = 2;
        }

        private void hlMenu_Loaded(object sender, RoutedEventArgs e)
        {
            Globalni.zmenitScale((Grid)sender);
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            klikSingle = false;
        }

        private void HraPruchodVezi(object sender, RoutedEventArgs e)
        {
            klik = true;
            vyber = 5;
        }

        private void HraRychla(object sender, RoutedEventArgs e)
        {
            klik = true;
            vyber = 1;
        }
    }
}
