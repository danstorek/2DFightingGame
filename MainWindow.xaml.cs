using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace _2DFightingGame
{
    public partial class MainWindow : Window
    {
        bool aktivni = true;
        bool napoveda = true;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Plocha_Loaded(object sender, RoutedEventArgs e)
        {
            Hitboxy.hrac1 = new Postava_1(Plocha, postava1, false, hrac1Details);
            Hitboxy.hrac2 = new Postava_1(Plocha, postava2, true, hrac2Details);

            hrac1Ukazatel.Width = postava1.Width;
            hrac2Ukazatel.Width = postava2.Width;
            hrac1Ukazatel1.Width = postava1.Width;
            hrac2Ukazatel1.Width = postava2.Width;

            DispatcherTimer gameTick = new DispatcherTimer();
            gameTick.Interval = TimeSpan.FromMilliseconds(1000 / 60);
            gameTick.Tick += GameTick_Tick;
            gameTick.Start();
        }

        private void GameTick_Tick(object sender, EventArgs e)
        {
            if (aktivni)
            {
                //Obnovení ukazovače hráčů
                Thickness hrac1Pozice = Hitboxy.hrac1.getImg().Margin;
                Thickness hrac2Pozice = Hitboxy.hrac2.getImg().Margin;
                hrac1Pozice.Bottom += Hitboxy.hrac1.getImg().Height + 20;
                hrac2Pozice.Bottom += Hitboxy.hrac2.getImg().Height + 20;
                hrac1Ukazatel.Margin = hrac1Pozice;
                hrac2Ukazatel.Margin = hrac2Pozice;
                hrac1Pozice.Bottom -= 50;
                hrac2Pozice.Bottom -= 50;
                hrac1Ukazatel1.Margin = hrac1Pozice;
                hrac2Ukazatel1.Margin = hrac2Pozice;

                Hitboxy.hrac1.Tick();
                Hitboxy.hrac2.Tick();

                //Obnovení healthbarů
                health1.Value = Hitboxy.hrac1.getHP();
                if (health1.Value > 80) health1.Foreground = new SolidColorBrush(Color.FromRgb(0,255,0));
                else if (health1.Value > 60) health1.Foreground = new SolidColorBrush(Color.FromRgb(155, 255, 0));
                else if (health1.Value > 40) health1.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                else if (health1.Value > 20) health1.Foreground = new SolidColorBrush(Color.FromRgb(255, 155, 0));
                else if (health1.Value >= 0) health1.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                health2.Value = Hitboxy.hrac2.getHP();
                if (health2.Value > 80) health2.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                else if (health2.Value > 60) health2.Foreground = new SolidColorBrush(Color.FromRgb(155, 255, 0));
                else if (health2.Value > 40) health2.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 0));
                else if (health2.Value > 20) health2.Foreground = new SolidColorBrush(Color.FromRgb(255, 155, 0));
                else if (health2.Value >= 0) health2.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));

                //Konec kola
                if (Hitboxy.hrac1.getHP() <= 0)
                {
                    MessageBox.Show("Hráč 2 vyhrál");
                    aktivni = false;
                }
                else if (Hitboxy.hrac2.getHP() <= 0)
                {
                    MessageBox.Show("Hráč 1 vyhrál");
                    aktivni = false;
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (napoveda)
            {
                napoveda = false;
                Plocha.Children.Remove(napoveda1);
                Plocha.Children.Remove(napoveda2);
            }
            switch (e.Key)
            {
                //Hráč 1
                case Key.Left:
                    Hitboxy.hrac1.setVlevo(true);
                    break;
                case Key.Right:
                    Hitboxy.hrac1.setVpravo(true);
                    break;
                case Key.Down:
                    Hitboxy.hrac1.setSkrceni(true);
                    break;
                case Key.Up:
                    Hitboxy.hrac1.setSkokTrigger(true);
                    break;
                case Key.M:
                    Hitboxy.hrac1.setUtok1(true);
                    break;
                case Key.N:
                    Hitboxy.hrac1.setUtok2(true);
                    break;


                //Hráč 2
                case Key.A:
                    Hitboxy.hrac2.setVlevo(true);
                    break;
                case Key.D:
                    Hitboxy.hrac2.setVpravo(true);
                    break;
                case Key.W:
                    Hitboxy.hrac2.setSkokTrigger(true);
                    break;
                case Key.S:
                    Hitboxy.hrac2.setSkrceni(true);
                    break;
                case Key.Q:
                    Hitboxy.hrac2.setUtok1(true);
                    break;
                case Key.E:
                    Hitboxy.hrac2.setUtok2(true);
                    break;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                //Hráč 1
                case Key.Left:
                    Hitboxy.hrac1.setVlevo(false);
                    break;
                case Key.Right:
                    Hitboxy.hrac1.setVpravo(false);
                    break;
                case Key.Up:
                    Hitboxy.hrac1.setSkokTrigger(false);
                    break;
                case Key.Down:
                    Hitboxy.hrac1.setSkrceni(false);
                    break;
                case Key.M:
                    Hitboxy.hrac1.setUtok1(false);
                    break;
                case Key.N:
                    Hitboxy.hrac1.setUtok2(false);
                    break;

                //Hráč 2
                case Key.A:
                    Hitboxy.hrac2.setVlevo(false);
                    break;
                case Key.D:
                    Hitboxy.hrac2.setVpravo(false);
                    break;
                case Key.W:
                    Hitboxy.hrac2.setSkokTrigger(false);
                    break;
                case Key.S:
                    Hitboxy.hrac2.setSkrceni(false);
                    break;
                case Key.Q:
                    Hitboxy.hrac2.setUtok1(false);
                    break;
                case Key.E:
                    Hitboxy.hrac2.setUtok2(false);
                    break;
            }
        }
    }
}
