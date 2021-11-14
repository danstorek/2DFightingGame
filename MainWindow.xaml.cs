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
        DateTime dalsiKolo;
        int aktivni = 0;
        bool napoveda = true;
        DispatcherTimer gameTick;
        public MainWindow()
        {
            InitializeComponent();
            gameTick = new DispatcherTimer();
        }

        private void Plocha_Loaded(object sender, RoutedEventArgs e)
        {
            pozadiMapa.Source = Hitboxy.pozadiMapa;

            foreach (Image i in Hitboxy.platformy)
            {
                i.Stretch = Stretch.Fill;
                i.VerticalAlignment = VerticalAlignment.Bottom;
                i.HorizontalAlignment = HorizontalAlignment.Left;
                Plocha.Children.Add(i);
            }

            switch (Hitboxy.hrac1Postava)
            {
                case 0: Hitboxy.hrac1 = new Postava_1(Plocha, postava1, false, hrac1Details); break;
                case 1: Hitboxy.hrac1 = new Postava_2(Plocha, postava1, false, hrac1Details); break;
            }

            switch (Hitboxy.hrac2Postava)
            {
                case 0: Hitboxy.hrac2 = new Postava_1(Plocha, postava2, false, hrac2Details); break;
                case 1: Hitboxy.hrac2 = new Postava_2(Plocha, postava2, false, hrac2Details); break;
            }

            Hitboxy.hrac1.getImg().Margin = new Thickness(0, 0, 0, Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin.Bottom);
            Hitboxy.hrac2.getImg().Margin = new Thickness(1700, 0, 0, Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin.Bottom);

            Hitboxy.hrac1.setJmeno(Hitboxy.hrac1Jmeno);
            Hitboxy.hrac2.setJmeno(Hitboxy.hrac2Jmeno);

            barJmeno1.Content = Hitboxy.hrac1.getJmeno();
            barJmeno2.Content = Hitboxy.hrac2.getJmeno();

            hrac1Ukazatel.Content = Hitboxy.hrac1.getJmeno();
            hrac1Ukazatel.Width = postava1.Width;
            hrac2Ukazatel.Content = Hitboxy.hrac2.getJmeno();
            hrac2Ukazatel.Width = postava2.Width;
            hrac1Ukazatel1.Width = postava1.Width;
            hrac2Ukazatel1.Width = postava2.Width;

            if (Hitboxy.kola[0] == 1) round1.Fill = Brushes.Blue;
            else if (Hitboxy.kola[0] == 2) round1.Fill = Brushes.Red;

            if (Hitboxy.kola[1] == 1) round2.Fill = Brushes.Blue;
            else if (Hitboxy.kola[1] == 2) round2.Fill = Brushes.Red;

            if (Hitboxy.kola[2] == 1) round3.Fill = Brushes.Blue;
            else if (Hitboxy.kola[2] == 2) round3.Fill = Brushes.Red;

            gameTick.Interval = TimeSpan.FromMilliseconds(1000 / 60);
            gameTick.Tick += GameTick_Tick;
            gameTick.Start();
        }

        private void GameTick_Tick(object sender, EventArgs e)
        {
            if (aktivni < 60)
            {
                //Vypnutí ovládání po skončení kola
                if (aktivni > 0)
                {
                    Hitboxy.hrac1.setVlevo(false);
                    Hitboxy.hrac1.setVpravo(false);
                    Hitboxy.hrac1.setSkrceni(false);
                    Hitboxy.hrac1.setSkokTrigger(false);
                    Hitboxy.hrac1.setUtok1(false);
                    Hitboxy.hrac1.setUtok2(false);

                    Hitboxy.hrac2.setVlevo(false);
                    Hitboxy.hrac2.setVpravo(false);
                    Hitboxy.hrac2.setSkrceni(false);
                    Hitboxy.hrac2.setSkokTrigger(false);
                    Hitboxy.hrac2.setUtok1(false);
                    Hitboxy.hrac2.setUtok2(false);
                }


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

                //Obnovení cooldownů
                postava1Utok1.Width = Hitboxy.hrac1.getCooldown()[0];
                postava1Utok2.Width = Hitboxy.hrac1.getCooldown()[1];
                postava2Utok1.Width = Hitboxy.hrac2.getCooldown()[0];
                postava2Utok2.Width = Hitboxy.hrac2.getCooldown()[1];

                //Obnovení healthbarů
                health1.Value = Hitboxy.hrac1.getHP();
                if (health1.Value > 80) health1.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
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
                if (aktivni == 0)
                {
                    //Remíza
                    if (Hitboxy.hrac1.getHP() <= 0 && Hitboxy.hrac2.getHP() <= 0)
                    {
                        textVyhra.Content = "Remíza";
                        textVyhra2.Content = "Remíza";
                        textVyhra2.Foreground = Brushes.White;
                        aktivni += 1;
                        gridVyhra.Visibility = Visibility.Visible;
                    }
                    //Výhra 1. hráče
                    else if (Hitboxy.hrac1.getHP() <= 0)
                    {
                        Hitboxy.kola[Hitboxy.aktivniKolo] = 2;
                        Hitboxy.aktivniKolo++;
                        if (Vyhodnotit() == 0)
                        {
                            dalsiKolo = DateTime.Now + TimeSpan.FromMilliseconds(1500);
                            textVyhra.Content = Hitboxy.hrac2.getJmeno() + " vyhrál kolo";
                            textVyhra2.Content = Hitboxy.hrac2.getJmeno() + " vyhrál kolo";
                            textVyhra2.Foreground = Brushes.LightPink;
                        }
                        else
                        {
                            textVyhra.Content = Hitboxy.hrac2.getJmeno() + " vyhrál zápas";
                            textVyhra2.Content = Hitboxy.hrac2.getJmeno() + " vyhrál zápas";
                            textVyhra2.Foreground = Brushes.LightPink;
                            tlacVyhra.IsEnabled = true;
                            tlacVyhra.Opacity = 1;
                        }
                        aktivni += 1;
                        gridVyhra.Visibility = Visibility.Visible;
                    }
                    //Výhra 2. hráče
                    else if (Hitboxy.hrac2.getHP() <= 0)
                    {
                        Hitboxy.kola[Hitboxy.aktivniKolo] = 1;
                        Hitboxy.aktivniKolo++;
                        if (Vyhodnotit() == 0)
                        {
                            dalsiKolo = DateTime.Now + TimeSpan.FromMilliseconds(1500);
                            textVyhra.Content = Hitboxy.hrac1.getJmeno() + " vyhrál kolo";
                            textVyhra2.Content = Hitboxy.hrac1.getJmeno() + " vyhrál kolo";
                            textVyhra2.Foreground = Brushes.LightBlue;
                        }
                        else
                        {
                            textVyhra.Content = Hitboxy.hrac1.getJmeno() + " vyhrál zápas";
                            textVyhra2.Content = Hitboxy.hrac1.getJmeno() + " vyhrál zápas";
                            textVyhra2.Foreground = Brushes.LightBlue;
                            tlacVyhra.IsEnabled = true;
                            tlacVyhra.Opacity = 1;
                        }

                        aktivni += 1;
                        gridVyhra.Visibility = Visibility.Visible;
                    }
                }
                else aktivni++;
            }

            if(aktivni > 1)
            {
                if (Plocha.Opacity > 0.5)
                {
                    Plocha.Opacity -= 0.05;
                }
                if (gridVyhra.Opacity < 1)
                {
                    gridVyhra.Opacity += 0.1;
                }
                if (Vyhodnotit() == 0)
                {
                    tlacVyhra.IsEnabled = false;
                    tlacVyhra.Opacity = 0;
                    if (dalsiKolo < DateTime.Now)
                    {
                        aktivni = 0;
                        //Další kolo
                        Hitboxy.hrac1.smazatProjektily();
                        Hitboxy.hrac2.smazatProjektily();

                        Hitboxy.hrac1.getImg().Margin = new Thickness(0, 0, 0, Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin.Bottom);
                        Hitboxy.hrac2.getImg().Margin = new Thickness(1700, 0, 0, Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin.Bottom);

                        Hitboxy.hrac1.setHP(100);
                        Hitboxy.hrac2.setHP(100);

                        gridVyhra.Opacity = 0;
                        gridVyhra.Visibility = Visibility.Hidden;
                        Plocha.Opacity = 1;
                    }

                }
            }
        }

        int Vyhodnotit()
        {
            if (Hitboxy.kola[0] == 1) round1.Fill = Brushes.Blue;
            else if (Hitboxy.kola[0] == 2) round1.Fill = Brushes.Red;

            if (Hitboxy.kola[1] == 1) round2.Fill = Brushes.Blue;
            else if (Hitboxy.kola[1] == 2) round2.Fill = Brushes.Red;

            if (Hitboxy.kola[2] == 1) round3.Fill = Brushes.Blue;
            else if (Hitboxy.kola[2] == 2) round3.Fill = Brushes.Red;

            if (Hitboxy.aktivniKolo == 2)
            {
                if (Hitboxy.kola[0] == 1 && Hitboxy.kola[1] == 1)
                {
                    return 1;
                }
                else if (Hitboxy.kola[0] == 2 && Hitboxy.kola[1] == 2)
                {
                    return 2;
                }
            }
            else if (Hitboxy.aktivniKolo == 3)
            {
                int pocetVyher1 = 0;
                int pocetVyher2 = 0;
                foreach (int i in Hitboxy.kola)
                {
                    if (i == 1) pocetVyher1++;
                    else pocetVyher2++;
                }
                if (pocetVyher1 > pocetVyher2)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            return 0;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gameTick.Stop();
            HlavniMenu okno = new HlavniMenu();
            okno.Show();
            System.Threading.Thread.Sleep(50);
            this.Close();
        }
    }
}
