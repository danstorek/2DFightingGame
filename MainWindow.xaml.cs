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
        bool klik = false;
        bool pruchodDoVeze = false;
        bool pozastaveno = false;
        DateTime dalsiKolo;
        int aktivni = -1;
        bool napoveda = true;
        DispatcherTimer gameTick;
        DispatcherTimer animacePrechod = new DispatcherTimer();

        int dalsiBonus;
        Stopwatch bonusCasovac;

        Stopwatch casKola;
        Stopwatch vezBossTimer;

        Queue<Key> cheaty = new Queue<Key>();
        bool cheatZapnut = false;
        public MainWindow()
        {
            InitializeComponent();

            //Animace přechodu
            animacePrechod.Tick += AnimacePrechod_Tick;
            animacePrechod.Interval = TimeSpan.FromMilliseconds(1000 / 90);
            animacePrechod.Start();

            gameTick = new DispatcherTimer();
            bonusCasovac = new Stopwatch();
            casKola = new Stopwatch();
            vezBossTimer = new Stopwatch();
            vezBossTimer.Start();

            //Popis kláves podle nastavení
            Key tlacitko = Globalni.ukl.nastaveniKlaves[4];
            if (tlacitko == Key.Up) tlacitko1.Content = "↑";
            else if (tlacitko == Key.Down) tlacitko1.Content = "↓";
            else if (tlacitko == Key.Left) tlacitko1.Content = "←";
            else if (tlacitko == Key.Right) tlacitko1.Content = "→";
            else tlacitko1.Content = tlacitko.ToString();

            tlacitko = Globalni.ukl.nastaveniKlaves[5];
            if (tlacitko == Key.Up) tlacitko2.Content = "↑";
            else if (tlacitko == Key.Down) tlacitko2.Content = "↓";
            else if (tlacitko == Key.Left) tlacitko2.Content = "←";
            else if (tlacitko == Key.Right) tlacitko2.Content = "→";
            else tlacitko2.Content = tlacitko.ToString();

            tlacitko = Globalni.ukl.nastaveniKlaves[10];
            if (tlacitko == Key.Up) tlacitko3.Content = "↑";
            else if (tlacitko == Key.Down) tlacitko3.Content = "↓";
            else if (tlacitko == Key.Left) tlacitko3.Content = "←";
            else if (tlacitko == Key.Right) tlacitko3.Content = "→";
            else tlacitko3.Content = tlacitko.ToString();

            tlacitko = Globalni.ukl.nastaveniKlaves[11];
            if (tlacitko == Key.Up) tlacitko4.Content = "↑";
            else if (tlacitko == Key.Down) tlacitko4.Content = "↓";
            else if (tlacitko == Key.Left) tlacitko4.Content = "←";
            else if (tlacitko == Key.Right) tlacitko4.Content = "→";
            else tlacitko4.Content = tlacitko.ToString();

            for (int i = 0; i < ovladani1.Children.Count; i++)
            {
                if (ovladani1.Children[i] is Label lb && lb.Tag != null)
                {
                    tlacitko = Globalni.ukl.nastaveniKlaves[Convert.ToInt32((string)lb.Tag)];
                    if (tlacitko == Key.Up) lb.Content = "↑";
                    else if (tlacitko == Key.Down) lb.Content = "↓";
                    else if (tlacitko == Key.Left) lb.Content = "←";
                    else if (tlacitko == Key.Right) lb.Content = "→";
                    else lb.Content = tlacitko.ToString();
                }
                if (ovladani2.Children[i] is Label lb1 && lb1.Tag != null)
                {
                    tlacitko = Globalni.ukl.nastaveniKlaves[Convert.ToInt32((string)lb1.Tag)];
                    if (tlacitko == Key.Up) lb1.Content = "↑";
                    else if (tlacitko == Key.Down) lb1.Content = "↓";
                    else if (tlacitko == Key.Left) lb1.Content = "←";
                    else if (tlacitko == Key.Right) lb1.Content = "→";
                    else lb1.Content = tlacitko.ToString();
                }
            }

            //Přidat průběh achievementů
            if (Globalni.rezimHry)
            {
                Globalni.ukl.PridatPrubeh(0, 1);
                Globalni.ukl.PridatPrubeh(1, 1);
                Globalni.ukl.PridatPrubeh(2, 1);
            }
            if(Globalni.vez != -1)
            {
                Globalni.ukl.PridatPrubeh(5, 1);
            }
        }
        private void AnimacePrechod_Tick(object sender, EventArgs e)
        {
            if (prechod2.Width > 1) prechod2.Width -= 120;
            if (klik && prechod1.Width < 1920) prechod1.Width += 120;
            if (klik && prechod1.Width >= 1920)
            {
                if (Globalni.vez == 4 && Globalni.kola[0] == 1) Globalni.vez++;
                animacePrechod.Stop();
                gameTick.Stop();
                if(Globalni.vez < 0)
                {
                    HlavniMenu okno = new HlavniMenu();
                    okno.Show();
                }
                else
                {
                    VezStatistika okno = new VezStatistika();
                    okno.Show();
                }
                System.Threading.Thread.Sleep(50);
                this.Close();
            }
        }

        private void Plocha_Loaded(object sender, RoutedEventArgs e)
        {
            if (Globalni.rezimHry) napoveda2.Visibility = Visibility.Hidden;

            Globalni.bonusy = new List<Bonus>();

            pozadiMapa.Source = Globalni.pozadiMapa;

            foreach (Image i in Globalni.platformy)
            {
                i.Stretch = Stretch.Fill;
                i.VerticalAlignment = VerticalAlignment.Bottom;
                i.HorizontalAlignment = HorizontalAlignment.Left;
                Plocha.Children.Add(i);
            }

            //Věž
            if (Globalni.vez != -1 && Globalni.vez <= 3)
            {
                pozadiMapaVez.Source = Mapy.getMapaVez(Globalni.vezMapy[Globalni.vez + 1]);
                foreach (Image i in Globalni.platformyVez)
                {
                    i.Stretch = Stretch.Fill;
                    i.VerticalAlignment = VerticalAlignment.Bottom;
                    i.HorizontalAlignment = HorizontalAlignment.Left;
                    plochaVez.Children.Add(i);
                }
            }

            if (Globalni.vez == 4)
            {
                postava1round2.Visibility = Visibility.Hidden;
                postava2round2.Visibility = Visibility.Hidden;
                postava1round1.Visibility = Visibility.Hidden;
                postava2round1.Visibility = Visibility.Hidden;
            }

            switch (Globalni.hrac1Postava)
            {
                case 0: Globalni.hrac1 = new Postava_1(Plocha, postava1, false); break;
                case 1: Globalni.hrac1 = new Postava_2(Plocha, postava1, false); break;
                case 2: Globalni.hrac1 = new Postava_3(Plocha, postava1, false); break;
                case 3: Globalni.hrac1 = new Postava_4(Plocha, postava1, false); break;
                case 4: Globalni.hrac1 = new Postava_5(Plocha, postava1, false); break;
            }

            if (Globalni.vez >= 0)
            {
                if (Globalni.vez > 0) prechod2.Width = 0;
                switch (Globalni.vezMapy[Globalni.vez])
                {
                    case 0:
                        Globalni.hrac2Postava = 1; break;
                    case 1:
                        Globalni.hrac2Postava = 0; break;
                    case 2:
                        Globalni.hrac2Postava = 3; break;
                    case 3:
                        Globalni.hrac2Postava = 4; break;
                    case 4:
                        Globalni.hrac2Postava = 2; break;
                }
            }

            switch (Globalni.hrac2Postava)
            {
                case 0: Globalni.hrac2 = new Postava_1(Plocha, postava2, false); break;
                case 1: Globalni.hrac2 = new Postava_2(Plocha, postava2, false); break;
                case 2: Globalni.hrac2 = new Postava_3(Plocha, postava2, false); break;
                case 3: Globalni.hrac2 = new Postava_4(Plocha, postava2, false); break;
                case 4: Globalni.hrac2 = new Postava_5(Plocha, postava2, false); break;
            }

            Globalni.hrac1.getImg().Margin = new Thickness(0, 0, 0, Globalni.platformy[Globalni.platformy.Count - 2].Margin.Bottom + 100);
            Globalni.hrac2.getImg().Margin = new Thickness(1700, 0, 0, Globalni.platformy[Globalni.platformy.Count - 2].Margin.Bottom + 100);

            Globalni.hrac2.setSmer(false);

            Globalni.hrac1.setJmeno(Globalni.hrac1Jmeno);
            Globalni.hrac2.setJmeno(Globalni.hrac2Jmeno);

            barJmeno1.Content = Globalni.hrac1.getJmeno();
            barJmeno2.Content = Globalni.hrac2.getJmeno();

            hrac1Ukazatel.Content = Globalni.hrac1.getJmeno();
            hrac1Ukazatel.Width = postava1.Width;
            hrac2Ukazatel.Content = Globalni.hrac2.getJmeno();
            hrac2Ukazatel.Width = postava2.Width;
            hrac1Ukazatel1.Width = postava1.Width;
            hrac2Ukazatel1.Width = postava2.Width;

            dalsiBonus = Globalni.rnd.Next(7500, 15000);

            gameTick.Interval = TimeSpan.FromMilliseconds(1000 / 60);
            gameTick.Tick += GameTick_Tick;
            gameTick.Start();

            AktualizaceHracu();
        }

        private void GameTick_Tick(object sender, EventArgs e)
        {
            if (pruchodDoVeze)
            {
                switch (Globalni.vezMapy[Globalni.vez+1])
                {
                    case 0:
                        postava2Vez.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/00.png")); break;
                    case 1:
                        postava2Vez.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/00.png")); break;
                    case 2:
                        postava2Vez.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/00.png")); break;
                    case 3:
                        postava2Vez.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/00.png")); break;
                    case 4:
                        postava2Vez.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/00.png")); break;
                }

                postava1Vez.Source = new BitmapImage(new Uri(String.Format("pack://application:,,,/imgs/chars/char{0}/right/00.png", Globalni.hrac1Postava+1)));

                if(Plocha.Opacity < 1) Plocha.Opacity += 0.05;
                gridVyhra.Opacity = 0;
                if (plochaVez.Margin.Top < 0)
                {
                    Thickness tmp;
                    tmp = plochaVez.Margin;
                    tmp.Top += 30;
                    plochaVez.Margin = tmp;

                    tmp = Plocha.Margin;
                    tmp.Top += 30;
                    Plocha.Margin = tmp;
                }
                else
                {
                    Globalni.vez++;
                    animacePrechod.Stop();
                    gameTick.Stop();
                    Globalni.pozadiMapa = Mapy.getMapa(Globalni.vezMapy[Globalni.vez]);
                    Globalni.aktivniKolo = 0;
                    Globalni.kola = new int[3] { 0, 0, 0 };
                    MainWindow hra = new MainWindow();
                    hra.Show();
                    System.Threading.Thread.Sleep(50);
                    this.Close();
                }
            }
            else if (casKola.ElapsedMilliseconds > 2500)
            {
                if (casKola.ElapsedMilliseconds < 3000)
                {
                    gridVyhra.Opacity = 0;
                    gridVyhra.Visibility = Visibility.Hidden;
                }
                if (!pozastaveno)
                {
                    if (aktivni < 60)
                    {
                        //Cheat na poškození
                        if (cheatZapnut)
                        {
                            Globalni.hrac2.redukcePoskozeni = -5000;
                            Globalni.hrac1.setHP(100);
                            Globalni.hrac1.pohybSchopnost = true;
                            Globalni.hrac1.maxRychlost = 50;
                            Globalni.hrac1.setEnergie(100);
                        }

                        //Vypnutí ovládání po skončení kola
                        if (aktivni > 0)
                        {
                            Globalni.hrac1.setVlevo(false);
                            Globalni.hrac1.setVpravo(false);
                            Globalni.hrac1.setSkrceni(false);
                            Globalni.hrac1.setSkokTrigger(false);
                            Globalni.hrac1.setUtok1(false);
                            Globalni.hrac1.setUtok2(false);

                            Globalni.hrac2.setVlevo(false);
                            Globalni.hrac2.setVpravo(false);
                            Globalni.hrac2.setSkrceni(false);
                            Globalni.hrac2.setSkokTrigger(false);
                            Globalni.hrac2.setUtok1(false);
                            Globalni.hrac2.setUtok2(false);
                        }

                        //Pohyb bota ve hře pro jednoho hráče
                        else if (aktivni == 0 && !napoveda && Globalni.rezimHry && casKola.ElapsedMilliseconds > 3200)
                        {
                            SinglePlayer.AI();
                            if (Globalni.vez == 4)
                            {
                                if (casKola.ElapsedMilliseconds < 10000)
                                {
                                    if (vezBossTimer.ElapsedMilliseconds > 1000)
                                    {
                                        TNT t = new TNT(Globalni.rnd.Next(0, 1920));
                                        Plocha.Children.Add(t.ReturnImage());
                                        Globalni.hrac2.aktivni_projektily.Add(t);
                                        vezBossTimer.Restart();
                                    }
                                }
                                else if (casKola.ElapsedMilliseconds < 20000)
                                {
                                    if (vezBossTimer.ElapsedMilliseconds > 500)
                                    {
                                        MageStrela t = new MageStrela(false, Globalni.rnd.Next(100, 600));
                                        Plocha.Children.Add(t.ReturnImage());
                                        Globalni.hrac2.aktivni_projektily.Add(t);
                                        vezBossTimer.Restart();
                                    }
                                }
                                else
                                {
                                    if (vezBossTimer.ElapsedMilliseconds > 1000)
                                    {
                                        TNT t = new TNT(Globalni.rnd.Next(0, 1920));
                                        Plocha.Children.Add(t.ReturnImage());
                                        Globalni.hrac2.aktivni_projektily.Add(t);

                                        MageStrela n = new MageStrela(false, Globalni.rnd.Next(100, 600));
                                        Plocha.Children.Add(n.ReturnImage());
                                        Globalni.hrac2.aktivni_projektily.Add(n);
                                        vezBossTimer.Restart();
                                    }
                                }
                            }

                        }

                        //Spawn bonusů
                        if (bonusCasovac.ElapsedMilliseconds > dalsiBonus)
                        {
                            //Hitboxy.rnd.Next(1, 4)
                            switch (Globalni.rnd.Next(1, 4))
                            {
                                case 1: Globalni.bonusy.Add(new HP()); Plocha.Children.Add(Globalni.bonusy[Globalni.bonusy.Count - 1].getIkona()); break;
                                case 2: Globalni.bonusy.Add(new Sila()); Plocha.Children.Add(Globalni.bonusy[Globalni.bonusy.Count - 1].getIkona()); break;
                                case 3: Globalni.bonusy.Add(new Rychlost()); Plocha.Children.Add(Globalni.bonusy[Globalni.bonusy.Count - 1].getIkona()); break;
                            }
                            bonusCasovac.Restart();
                            dalsiBonus = Globalni.rnd.Next(7500, 15000);
                        }

                        //Obnovení bonusů
                        List<Bonus> tmp = new List<Bonus>();
                        foreach (Bonus i in Globalni.bonusy)
                        {
                            if (!i.sebrano)
                            {
                                i.Sebrani();
                                tmp.Add(i);
                            }
                            else
                            {
                                Plocha.Children.Remove(i.getIkona());
                            }
                        }
                        Globalni.bonusy = tmp;

                        AktualizaceHracu();

                        //Obnovení cooldownů
                        postava1Utok1.Width = Globalni.hrac1.getCooldown()[0];
                        postava1Utok2.Width = Globalni.hrac1.getCooldown()[1];
                        postava2Utok1.Width = Globalni.hrac2.getCooldown()[0];
                        postava2Utok2.Width = Globalni.hrac2.getCooldown()[1];

                        //Obnovení healthbarů
                        double hp1 = Globalni.hrac1.getHP() * 6.59;
                        double hp2 = Globalni.hrac2.getHP() * 6.59;
                        if (hp1 < 0) hp1 = 0;
                        if (hp2 < 0) hp2 = 0;
                        health1.Width = hp1;
                        health2.Width = hp2;

                        //Automatické doplnění energie
                        Globalni.hrac1.setEnergie(Globalni.hrac1.getEnergie() + 0.5);
                        Globalni.hrac2.setEnergie(Globalni.hrac2.getEnergie() + 0.5);
                        if (Globalni.hrac1.getEnergie() > 100) Globalni.hrac1.setEnergie(100);
                        if (Globalni.hrac2.getEnergie() > 100) Globalni.hrac2.setEnergie(100);

                        //Obnovení barů na energii
                        double en1 = Globalni.hrac1.getEnergie() * 3.2;
                        double en2 = Globalni.hrac2.getEnergie() * 3.2;
                        if (en1 < 0) en1 = 0;
                        if (en2 < 0) en2 = 0;
                        energie1.Width = en1;
                        energie2.Width = en2;

                        //Konec kola
                        if (aktivni == 0)
                        {
                            //Remíza
                            if (Globalni.hrac1.getHP() <= 0 && Globalni.hrac2.getHP() <= 0)
                            {
                                textVyhra.Content = "Remíza";
                                aktivni += 1;
                                gridVyhra.Visibility = Visibility.Visible;
                            }
                            //Výhra 2. hráče
                            else if (Globalni.hrac1.getHP() <= 0)
                            {
                                Globalni.kola[Globalni.aktivniKolo] = 2;
                                Globalni.aktivniKolo++;
                                if (Vyhodnotit() == 0)
                                {
                                    postava2round1.Fill = postava2round1.Stroke;

                                    dalsiKolo = DateTime.Now + TimeSpan.FromMilliseconds(5000);
                                    textVyhra.Content = Globalni.hrac2.getJmeno() + " vyhrál kolo";
                                }
                                else
                                {
                                    postava2round2.Fill = postava2round2.Stroke;

                                    Statistika();
                                    textVyhra.Content = Globalni.hrac2.getJmeno() + " vyhrál zápas";

                                    KonecHry(true);
                                    if (Globalni.hrac1.getHP() <= 0 || Globalni.vez == -1 || Globalni.vez == 4)
                                    {
                                        tlacVyhraVez.IsEnabled = false;
                                        tlacVyhraVez.Opacity = 0;
                                        tlacVyhraVez.Visibility = Visibility.Hidden;

                                        tlacVyhra.IsEnabled = true;
                                        tlacVyhra.Opacity = 1;
                                        tlacVyhra.Visibility = Visibility.Visible;
                                    }
                                    else
                                    {
                                        tlacVyhra.IsEnabled = false;
                                        tlacVyhra.Opacity = 0;
                                        tlacVyhra.Visibility = Visibility.Hidden;

                                        tlacVyhraVez.IsEnabled = true;
                                        tlacVyhraVez.Opacity = 1;
                                        tlacVyhraVez.Visibility = Visibility.Visible;
                                    }
                                }
                                aktivni += 1;
                                gridVyhra.Visibility = Visibility.Visible;
                            }
                            //Výhra 1. hráče
                            else if (Globalni.hrac2.getHP() <= 0)
                            {
                                Globalni.kola[Globalni.aktivniKolo] = 1;
                                Globalni.aktivniKolo++;
                                if (Vyhodnotit() == 0)
                                {
                                    postava1round1.Fill = postava1round1.Stroke;

                                    dalsiKolo = DateTime.Now + TimeSpan.FromMilliseconds(5000);
                                    textVyhra.Content = Globalni.hrac1.getJmeno() + " vyhrál kolo";
                                }
                                else
                                {
                                    postava1round2.Fill = postava1round2.Stroke;

                                    Statistika();
                                    textVyhra.Content = Globalni.hrac1.getJmeno() + " vyhrál zápas";

                                    KonecHry(true);
                                    if (Globalni.hrac1.getHP() <= 0 || Globalni.vez == -1 || Globalni.vez == 4)
                                    {
                                        tlacVyhraVez.IsEnabled = false;
                                        tlacVyhraVez.Opacity = 0;
                                        tlacVyhraVez.Visibility = Visibility.Hidden;

                                        tlacVyhra.IsEnabled = true;
                                        tlacVyhra.Opacity = 1;
                                        tlacVyhra.Visibility = Visibility.Visible;
                                    }
                                    else
                                    {
                                        tlacVyhra.IsEnabled = false;
                                        tlacVyhra.Opacity = 0;
                                        tlacVyhra.Visibility = Visibility.Hidden;

                                        tlacVyhraVez.IsEnabled = true;
                                        tlacVyhraVez.Opacity = 1;
                                        tlacVyhraVez.Visibility = Visibility.Visible;
                                    }
                                }

                                aktivni += 1;
                                gridVyhra.Visibility = Visibility.Visible;
                            }
                        }
                        else aktivni++;
                    }

                    if (aktivni > 1)
                    {
                        if (Globalni.hrac1.getHP() <= 0)
                        {
                            Globalni.hrac1.Umirani(aktivni);
                        }
                        if (Globalni.hrac2.getHP() <= 0)
                        {
                            Globalni.hrac2.Umirani(aktivni);
                        }

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
                            KonecHry(false);
                            //Achievement
                            if (Globalni.rezimHry && Globalni.hrac1.getHP() > 0 && casKola.ElapsedMilliseconds <= 15000) Globalni.ukl.PridatPrubeh(3, 1);

                            if (dalsiKolo < DateTime.Now)
                            {
                                aktivni = 0;
                                //Další kolo
                                dalsiBonus = Globalni.rnd.Next(7500, 15000);
                                bonusCasovac.Restart();
                                Globalni.hrac1.clearBonusy();
                                Globalni.hrac2.clearBonusy();

                                foreach (Bonus i in Globalni.bonusy)
                                {
                                    if (!i.sebrano)
                                    {
                                        i.sebrano = true;
                                        Plocha.Children.Remove(i.getIkona());

                                    }
                                }

                                Globalni.hrac1.smazatProjektily();
                                Globalni.hrac2.smazatProjektily();

                                Globalni.hrac1.getImg().Margin = new Thickness(0, 0, 0, Globalni.platformy[Globalni.platformy.Count - 2].Margin.Bottom + 100);
                                Globalni.hrac2.getImg().Margin = new Thickness(1700, 0, 0, Globalni.platformy[Globalni.platformy.Count - 2].Margin.Bottom + 100);

                                Globalni.hrac1.setHP(100);
                                Globalni.hrac2.setHP(100);

                                Globalni.hrac1.setEnergie(100);
                                Globalni.hrac2.setEnergie(100);

                                gridVyhra.Opacity = 0;
                                gridVyhra.Visibility = Visibility.Hidden;
                                Plocha.Opacity = 1;
                            }
                            else if (dalsiKolo - TimeSpan.FromMilliseconds(500) < DateTime.Now)
                            {
                                textVyhra.Content = "1";
                            }
                            else if (dalsiKolo - TimeSpan.FromMilliseconds(1000) < DateTime.Now)
                            {
                                textVyhra.Content = "2";
                            }
                            else if (dalsiKolo - TimeSpan.FromMilliseconds(1500) < DateTime.Now)
                            {
                                textVyhra.Content = "3";
                            }
                            else if (dalsiKolo - TimeSpan.FromMilliseconds(2500) < DateTime.Now)
                            {
                                if (Globalni.vez == 4) textVyhra.Content = "Souboj s bossem!";
                                else textVyhra.Content = String.Format("{0}. kolo", Globalni.aktivniKolo + 1);
                            }

                        }
                    }
                }

                else if (pozastaveno && bonusCasovac.IsRunning) bonusCasovac.Stop();
            }
            else if (casKola.ElapsedMilliseconds > 2000)
            {
                textVyhra.Content = "1";
            }
            else if (casKola.ElapsedMilliseconds > 1500)
            {
                textVyhra.Content = "2";
            }
            else if (casKola.ElapsedMilliseconds > 1000)
            {
                textVyhra.Content = "3";
            }
            else if (casKola.ElapsedMilliseconds > 1)
            {
                KonecHry(false);

                gridVyhra.Opacity = 1;
                gridVyhra.Visibility = Visibility.Visible;
                if (Globalni.vez == 4) textVyhra.Content = "Souboj s bossem!";
                else textVyhra.Content = String.Format("{0}. kolo", Globalni.aktivniKolo + 1);
            }
        }

        private void AktualizaceHracu()
        {
            //Obnovení ukazovače hráčů
            Thickness hrac1Pozice = Globalni.hrac1.getImg().Margin;
            Thickness hrac2Pozice = Globalni.hrac2.getImg().Margin;
            hrac1Pozice.Bottom += Globalni.hrac1.getImg().Height + 20;
            hrac2Pozice.Bottom += Globalni.hrac2.getImg().Height + 20;
            hrac1Ukazatel.Margin = hrac1Pozice;
            hrac2Ukazatel.Margin = hrac2Pozice;
            hrac1Pozice.Bottom -= 50;
            hrac2Pozice.Bottom -= 50;
            hrac1Ukazatel1.Margin = hrac1Pozice;
            hrac2Ukazatel1.Margin = hrac2Pozice;
            if (Globalni.hrac1.getSila()) postava1Bonus1.Opacity = 1;
            else postava1Bonus1.Opacity = 0;
            if (Globalni.hrac1.getRychlost()) postava1Bonus2.Opacity = 1;
            else postava1Bonus2.Opacity = 0;
            if (Globalni.hrac2.getSila()) postava2Bonus1.Opacity = 1;
            else postava2Bonus1.Opacity = 0;
            if (Globalni.hrac2.getRychlost()) postava2Bonus2.Opacity = 1;
            else postava2Bonus2.Opacity = 0;

            Globalni.hrac1.Tick();
            Globalni.hrac2.Tick();
        }

        private void KonecHry(bool hodnota)
        {
            if (!hodnota)
            {
                vyhraHrac1.Opacity = 0;
                vyhraHrac1Neuspesne.Opacity = 0;
                vyhraHrac1Skore.Opacity = 0;
                vyhraHrac1Uspesne.Opacity = 0;
                vyhraHrac1Uspesnost.Opacity = 0;
                vyhraHrac2.Opacity = 0;
                vyhraHrac2Neuspesne.Opacity = 0;
                vyhraHrac2Skore.Opacity = 0;
                vyhraHrac2Uspesne.Opacity = 0;
                vyhraHrac2Uspesnost.Opacity = 0;
                tlacVyhra.IsEnabled = false;
                tlacVyhraVez.IsEnabled = false;
                tlacVyhra.Opacity = 0;
                tlacVyhraVez.Opacity = 0;
                tlacVyhra.Visibility = Visibility.Hidden;
                tlacVyhraVez.Visibility = Visibility.Hidden;
            }
            else
            {
                vyhraHrac1.Opacity = 1;
                vyhraHrac1Neuspesne.Opacity = 1;
                vyhraHrac1Skore.Opacity = 1;
                vyhraHrac1Uspesne.Opacity = 1;
                vyhraHrac1Uspesnost.Opacity = 1;
                vyhraHrac2.Opacity = 1;
                vyhraHrac2Neuspesne.Opacity = 1;
                vyhraHrac2Skore.Opacity = 1;
                vyhraHrac2Uspesne.Opacity = 1;
                vyhraHrac2Uspesnost.Opacity = 1;
            }
        }

        void Statistika()
        {
            //Achievementy
            if (Globalni.rezimHry && Globalni.hrac1.getHP() > 0 && casKola.ElapsedMilliseconds <= 15000) Globalni.ukl.PridatPrubeh(3, 1);
            if (Globalni.rezimHry && Globalni.hrac1.celkem > 0 && (Globalni.hrac1.uspesne * 100 / Globalni.hrac1.celkem) >= 75) Globalni.ukl.PridatPrubeh(4, 1);

            //Uložení do žebříčku
            Globalni.ukl.PridatSkore(Globalni.hrac1.getJmeno(), Globalni.hrac1.skore);
            if (!Globalni.rezimHry) Globalni.ukl.PridatSkore(Globalni.hrac2.getJmeno(), Globalni.hrac2.skore);

            //Věž
            if(Globalni.vez != -1)
            {
                Globalni.skoreCelkem += Globalni.hrac1.skore;
            }

            vyhraHrac1.Content = Globalni.hrac1Jmeno;
            vyhraHrac2.Content = Globalni.hrac2Jmeno;
            vyhraHrac1Neuspesne.Content = "Neúspěšné útoky: " + (Globalni.hrac1.celkem - Globalni.hrac1.uspesne);
            vyhraHrac1Uspesne.Content = "Úspěšné útoky: " + Globalni.hrac1.uspesne;
            vyhraHrac1Skore.Content = "Skóre: " + Globalni.hrac1.skore;
            if (Globalni.hrac1.celkem != 0) vyhraHrac1Uspesnost.Content = "Úspěšnost: " + (Globalni.hrac1.uspesne * 100 / Globalni.hrac1.celkem) + "%";
            else vyhraHrac1Uspesnost.Content = "Úspěšnost: 0%";
            vyhraHrac2Neuspesne.Content = "Neúspěšné útoky: " + (Globalni.hrac2.celkem - Globalni.hrac2.uspesne);
            vyhraHrac2Uspesne.Content = "Úspěšné útoky: " + Globalni.hrac2.uspesne;
            vyhraHrac2Skore.Content = "Skóre: " + Globalni.hrac2.skore;
            if (Globalni.hrac2.celkem != 0) vyhraHrac2Uspesnost.Content = "Úspěšnost: " + (Globalni.hrac2.uspesne * 100 / Globalni.hrac2.celkem) + "%";
            else vyhraHrac2Uspesnost.Content = "Úspěšnost: 0%";
        }

        //Vyhodnocení dosavadního výsledku zápasu
        int Vyhodnotit()
        {
            if(Globalni.vez == 4)
            {
                return Globalni.kola[0];
            }
            if (Globalni.aktivniKolo == 2)
            {
                if (Globalni.kola[0] == 1 && Globalni.kola[1] == 1)
                {
                    return 1;
                }
                else if (Globalni.kola[0] == 2 && Globalni.kola[1] == 2)
                {
                    return 2;
                }
            }
            else if (Globalni.aktivniKolo == 3)
            {
                int pocetVyher1 = 0;
                int pocetVyher2 = 0;
                foreach (int i in Globalni.kola)
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
            string tmp = "";
            cheaty.Enqueue(e.Key);
            if (cheaty.Count > 5) cheaty.Dequeue();
            if (cheaty.Count == 5)
                foreach (Key k in cheaty)
                {
                    tmp += k.ToString();
                }
            if (tmp == "SVALY")
            {
                cheatZapnut = true;
            }
            else if (tmp == "NAHIT")
            {
                Globalni.hrac2.setHP(5);
            }

            if (napoveda)
            {
                casKola.Start();
                bonusCasovac.Start();
                napoveda = false;
                Plocha.Children.Remove(napoveda1);
                Plocha.Children.Remove(napoveda2);
            }

            //Pozastavení hry
            if (e.Key == Key.Escape)
            {
                if (!pozastaveno && aktivni == 0)
                {
                    casKola.Stop();
                    pauseMenu.Visibility = Visibility.Visible;
                    pozastaveno = true;
                }
                else if (pozastaveno && aktivni == 0)
                {
                    casKola.Start();
                    pauseMenu.Visibility = Visibility.Hidden;
                    pozastaveno = false;
                }
            }

            //Hráč 1
            if (!Globalni.hrac1.zamknoutOvladani)
            {
                if (e.Key == Globalni.ukl.nastaveniKlaves[2])
                    Globalni.hrac1.setVlevo(true);
                if (e.Key == Globalni.ukl.nastaveniKlaves[3])
                    Globalni.hrac1.setVpravo(true);
                if (e.Key == Globalni.ukl.nastaveniKlaves[4])
                    Globalni.hrac1.setUtok1(true);
                if (e.Key == Globalni.ukl.nastaveniKlaves[5])
                    Globalni.hrac1.setUtok2(true);
            }
            if (e.Key == Globalni.ukl.nastaveniKlaves[1])
                Globalni.hrac1.setSkrceni(true);
            if (e.Key == Globalni.ukl.nastaveniKlaves[0])
                Globalni.hrac1.setSkokTrigger(true);

            //Hráč 2
            if (!Globalni.hrac2.zamknoutOvladani)
            {
                if (!Globalni.rezimHry && e.Key == Globalni.ukl.nastaveniKlaves[8])
                    Globalni.hrac2.setVlevo(true);
                if (!Globalni.rezimHry && e.Key == Globalni.ukl.nastaveniKlaves[9])
                    Globalni.hrac2.setVpravo(true);
                if (!Globalni.rezimHry && e.Key == Globalni.ukl.nastaveniKlaves[10])
                    Globalni.hrac2.setUtok1(true);
                if (!Globalni.rezimHry && e.Key == Globalni.ukl.nastaveniKlaves[11])
                    Globalni.hrac2.setUtok2(true);
            }
            if (!Globalni.rezimHry && e.Key == Globalni.ukl.nastaveniKlaves[7])
                Globalni.hrac2.setSkrceni(true);
            if (!Globalni.rezimHry && e.Key == Globalni.ukl.nastaveniKlaves[6])
                Globalni.hrac2.setSkokTrigger(true);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            //Hráč 1
            if (e.Key == Globalni.ukl.nastaveniKlaves[2])
                Globalni.hrac1.setVlevo(false);
            if (e.Key == Globalni.ukl.nastaveniKlaves[3])
                Globalni.hrac1.setVpravo(false);
            if (e.Key == Globalni.ukl.nastaveniKlaves[1])
                Globalni.hrac1.setSkrceni(false);
            if (e.Key == Globalni.ukl.nastaveniKlaves[0])
                Globalni.hrac1.setSkokTrigger(false);
            if (e.Key == Globalni.ukl.nastaveniKlaves[4])
                Globalni.hrac1.setUtok1(false);
            if (e.Key == Globalni.ukl.nastaveniKlaves[5])
                Globalni.hrac1.setUtok2(false);

            //Hráč 2
            if (!Globalni.rezimHry && e.Key == Globalni.ukl.nastaveniKlaves[8])
                Globalni.hrac2.setVlevo(false);
            if (!Globalni.rezimHry && e.Key == Globalni.ukl.nastaveniKlaves[9])
                Globalni.hrac2.setVpravo(false);
            if (!Globalni.rezimHry && e.Key == Globalni.ukl.nastaveniKlaves[7])
                Globalni.hrac2.setSkrceni(false);
            if (!Globalni.rezimHry && e.Key == Globalni.ukl.nastaveniKlaves[6])
                Globalni.hrac2.setSkokTrigger(false);
            if (!Globalni.rezimHry && e.Key == Globalni.ukl.nastaveniKlaves[10])
                Globalni.hrac2.setUtok1(false);
            if (!Globalni.rezimHry && e.Key == Globalni.ukl.nastaveniKlaves[11])
                Globalni.hrac2.setUtok2(false);
        }

        private void tlacMenu_Click(object sender, RoutedEventArgs e)
        {
            klik = true;
        }

        private void tlacPokracovat_Click(object sender, RoutedEventArgs e)
        {
            pauseMenu.Visibility = Visibility.Hidden;
            pozastaveno = false;
        }

        private void tlacUkoncit_Click(object sender, RoutedEventArgs e)
        {
            klik = true;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Globalni.zmenitScale((Grid)sender);
        }

        private void tlacVyhraVez_Click(object sender, RoutedEventArgs e)
        {
            pruchodDoVeze = true;
        }
    }
}
