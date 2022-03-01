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
            Key tlacitko = Hitboxy.ukl.nastaveniKlaves[4];
            if (tlacitko == Key.Up) tlacitko1.Content = "↑";
            else if (tlacitko == Key.Down) tlacitko1.Content = "↓";
            else if (tlacitko == Key.Left) tlacitko1.Content = "←";
            else if (tlacitko == Key.Right) tlacitko1.Content = "→";
            else tlacitko1.Content = tlacitko.ToString();

            tlacitko = Hitboxy.ukl.nastaveniKlaves[5];
            if (tlacitko == Key.Up) tlacitko2.Content = "↑";
            else if (tlacitko == Key.Down) tlacitko2.Content = "↓";
            else if (tlacitko == Key.Left) tlacitko2.Content = "←";
            else if (tlacitko == Key.Right) tlacitko2.Content = "→";
            else tlacitko2.Content = tlacitko.ToString();

            tlacitko = Hitboxy.ukl.nastaveniKlaves[10];
            if (tlacitko == Key.Up) tlacitko3.Content = "↑";
            else if (tlacitko == Key.Down) tlacitko3.Content = "↓";
            else if (tlacitko == Key.Left) tlacitko3.Content = "←";
            else if (tlacitko == Key.Right) tlacitko3.Content = "→";
            else tlacitko3.Content = tlacitko.ToString();

            tlacitko = Hitboxy.ukl.nastaveniKlaves[11];
            if (tlacitko == Key.Up) tlacitko4.Content = "↑";
            else if (tlacitko == Key.Down) tlacitko4.Content = "↓";
            else if (tlacitko == Key.Left) tlacitko4.Content = "←";
            else if (tlacitko == Key.Right) tlacitko4.Content = "→";
            else tlacitko4.Content = tlacitko.ToString();

            for (int i = 0; i < ovladani1.Children.Count; i++)
            {
                if (ovladani1.Children[i] is Label lb && lb.Tag != null)
                {
                    tlacitko = Hitboxy.ukl.nastaveniKlaves[Convert.ToInt32((string)lb.Tag)];
                    if (tlacitko == Key.Up) lb.Content = "↑";
                    else if (tlacitko == Key.Down) lb.Content = "↓";
                    else if (tlacitko == Key.Left) lb.Content = "←";
                    else if (tlacitko == Key.Right) lb.Content = "→";
                    else lb.Content = tlacitko.ToString();
                }
                if (ovladani2.Children[i] is Label lb1 && lb1.Tag != null)
                {
                    tlacitko = Hitboxy.ukl.nastaveniKlaves[Convert.ToInt32((string)lb1.Tag)];
                    if (tlacitko == Key.Up) lb1.Content = "↑";
                    else if (tlacitko == Key.Down) lb1.Content = "↓";
                    else if (tlacitko == Key.Left) lb1.Content = "←";
                    else if (tlacitko == Key.Right) lb1.Content = "→";
                    else lb1.Content = tlacitko.ToString();
                }
            }

            //Přidat průběh achievementů
            if (Hitboxy.rezimHry)
            {
                Hitboxy.ukl.PridatPrubeh(0, 1);
                Hitboxy.ukl.PridatPrubeh(1, 1);
                Hitboxy.ukl.PridatPrubeh(2, 1);
            }
            if(Hitboxy.vez != -1)
            {
                Hitboxy.ukl.PridatPrubeh(5, 1);
            }
        }
        private void AnimacePrechod_Tick(object sender, EventArgs e)
        {
            if (prechod2.Width > 1) prechod2.Width -= 120;
            if (klik && prechod1.Width < 1920) prechod1.Width += 120;
            if (klik && prechod1.Width >= 1920)
            {
                if (Hitboxy.vez == 4) Hitboxy.vez++;
                animacePrechod.Stop();
                gameTick.Stop();
                if(Hitboxy.vez < 0)
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
            if (Hitboxy.rezimHry) napoveda2.Visibility = Visibility.Hidden;

            Hitboxy.bonusy = new List<Bonus>();

            pozadiMapa.Source = Hitboxy.pozadiMapa;

            foreach (Image i in Hitboxy.platformy)
            {
                i.Stretch = Stretch.Fill;
                i.VerticalAlignment = VerticalAlignment.Bottom;
                i.HorizontalAlignment = HorizontalAlignment.Left;
                Plocha.Children.Add(i);
            }

            //Věž
            if (Hitboxy.vez != -1 && Hitboxy.vez <= 3)
            {
                pozadiMapaVez.Source = Mapy.getMapaVez(Hitboxy.vezMapy[Hitboxy.vez + 1]);
                foreach (Image i in Hitboxy.platformyVez)
                {
                    i.Stretch = Stretch.Fill;
                    i.VerticalAlignment = VerticalAlignment.Bottom;
                    i.HorizontalAlignment = HorizontalAlignment.Left;
                    plochaVez.Children.Add(i);
                }
            }

            if (Hitboxy.vez == 4)
            {
                postava1round2.Visibility = Visibility.Hidden;
                postava2round2.Visibility = Visibility.Hidden;
                postava1round1.Visibility = Visibility.Hidden;
                postava2round1.Visibility = Visibility.Hidden;
            }

            switch (Hitboxy.hrac1Postava)
            {
                case 0: Hitboxy.hrac1 = new Postava_1(Plocha, postava1, false); break;
                case 1: Hitboxy.hrac1 = new Postava_2(Plocha, postava1, false); break;
                case 2: Hitboxy.hrac1 = new Postava_3(Plocha, postava1, false); break;
                case 3: Hitboxy.hrac1 = new Postava_4(Plocha, postava1, false); break;
                case 4: Hitboxy.hrac1 = new Postava_5(Plocha, postava1, false); break;
            }

            if (Hitboxy.vez >= 0)
            {
                if (Hitboxy.vez > 0) prechod2.Width = 0;
                switch (Hitboxy.vezMapy[Hitboxy.vez])
                {
                    case 0:
                        Hitboxy.hrac2Postava = 1; break;
                    case 1:
                        Hitboxy.hrac2Postava = 0; break;
                    case 2:
                        Hitboxy.hrac2Postava = 3; break;
                    case 3:
                        Hitboxy.hrac2Postava = 4; break;
                    case 4:
                        Hitboxy.hrac2Postava = 2; break;
                }
            }

            switch (Hitboxy.hrac2Postava)
            {
                case 0: Hitboxy.hrac2 = new Postava_1(Plocha, postava2, false); break;
                case 1: Hitboxy.hrac2 = new Postava_2(Plocha, postava2, false); break;
                case 2: Hitboxy.hrac2 = new Postava_3(Plocha, postava2, false); break;
                case 3: Hitboxy.hrac2 = new Postava_4(Plocha, postava2, false); break;
                case 4: Hitboxy.hrac2 = new Postava_5(Plocha, postava2, false); break;
            }

            Hitboxy.hrac1.getImg().Margin = new Thickness(0, 0, 0, Hitboxy.platformy[Hitboxy.platformy.Count - 2].Margin.Bottom + 100);
            Hitboxy.hrac2.getImg().Margin = new Thickness(1700, 0, 0, Hitboxy.platformy[Hitboxy.platformy.Count - 2].Margin.Bottom + 100);

            Hitboxy.hrac2.setSmer(false);

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

            dalsiBonus = Hitboxy.rnd.Next(7500, 15000);

            gameTick.Interval = TimeSpan.FromMilliseconds(1000 / 60);
            gameTick.Tick += GameTick_Tick;
            gameTick.Start();

            AktualizaceHracu();
        }

        private void GameTick_Tick(object sender, EventArgs e)
        {
            if (pruchodDoVeze)
            {
                Plocha.Opacity = 1;
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
                    Hitboxy.vez++;
                    animacePrechod.Stop();
                    gameTick.Stop();
                    Hitboxy.pozadiMapa = Mapy.getMapa(Hitboxy.vezMapy[Hitboxy.vez]);
                    Hitboxy.aktivniKolo = 0;
                    Hitboxy.kola = new int[3] { 0, 0, 0 };
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
                            Hitboxy.hrac2.redukcePoskozeni = -5000;
                            Hitboxy.hrac1.setHP(100);
                            Hitboxy.hrac1.pohybSchopnost = true;
                            Hitboxy.hrac1.maxRychlost = 50;
                            Hitboxy.hrac1.setEnergie(100);
                        }

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

                        //Pohyb bota ve hře pro jednoho hráče
                        else if (aktivni == 0 && !napoveda && Hitboxy.rezimHry && casKola.ElapsedMilliseconds > 3200)
                        {
                            SinglePlayer.AI();
                            if (Hitboxy.vez == 4)
                            {
                                if (casKola.ElapsedMilliseconds < 10000)
                                {
                                    if (vezBossTimer.ElapsedMilliseconds > 1000)
                                    {
                                        TNT t = new TNT(Hitboxy.rnd.Next(0, 1920));
                                        Plocha.Children.Add(t.ReturnImage());
                                        Hitboxy.hrac2.aktivni_projektily.Add(t);
                                        vezBossTimer.Restart();
                                    }
                                }
                                else if (casKola.ElapsedMilliseconds < 20000)
                                {
                                    if (vezBossTimer.ElapsedMilliseconds > 500)
                                    {
                                        MageStrela t = new MageStrela(false, Hitboxy.rnd.Next(100, 600));
                                        Plocha.Children.Add(t.ReturnImage());
                                        Hitboxy.hrac2.aktivni_projektily.Add(t);
                                        vezBossTimer.Restart();
                                    }
                                }
                                else
                                {
                                    if (vezBossTimer.ElapsedMilliseconds > 1000)
                                    {
                                        TNT t = new TNT(Hitboxy.rnd.Next(0, 1920));
                                        Plocha.Children.Add(t.ReturnImage());
                                        Hitboxy.hrac2.aktivni_projektily.Add(t);

                                        MageStrela n = new MageStrela(false, Hitboxy.rnd.Next(100, 600));
                                        Plocha.Children.Add(n.ReturnImage());
                                        Hitboxy.hrac2.aktivni_projektily.Add(n);
                                        vezBossTimer.Restart();
                                    }
                                }
                            }

                        }

                        //Spawn bonusů
                        if (bonusCasovac.ElapsedMilliseconds > dalsiBonus)
                        {
                            switch (Hitboxy.rnd.Next(1, 4))
                            {
                                case 1: Hitboxy.bonusy.Add(new HP()); Plocha.Children.Add(Hitboxy.bonusy[Hitboxy.bonusy.Count - 1].getIkona()); break;
                                case 2: Hitboxy.bonusy.Add(new Sila()); Plocha.Children.Add(Hitboxy.bonusy[Hitboxy.bonusy.Count - 1].getIkona()); break;
                                case 3: Hitboxy.bonusy.Add(new Rychlost()); Plocha.Children.Add(Hitboxy.bonusy[Hitboxy.bonusy.Count - 1].getIkona()); break;
                            }
                            bonusCasovac.Restart();
                            dalsiBonus = Hitboxy.rnd.Next(7500, 15000);
                        }

                        //Obnovení bonusů
                        foreach (Bonus i in Hitboxy.bonusy)
                        {
                            if (!i.sebrano) i.Sebrani();
                            else Plocha.Children.Remove(i.getIkona());
                        }

                        AktualizaceHracu();

                        //Obnovení cooldownů
                        postava1Utok1.Width = Hitboxy.hrac1.getCooldown()[0];
                        postava1Utok2.Width = Hitboxy.hrac1.getCooldown()[1];
                        postava2Utok1.Width = Hitboxy.hrac2.getCooldown()[0];
                        postava2Utok2.Width = Hitboxy.hrac2.getCooldown()[1];

                        //Obnovení healthbarů
                        double hp1 = Hitboxy.hrac1.getHP() * 6.59;
                        double hp2 = Hitboxy.hrac2.getHP() * 6.59;
                        if (hp1 < 0) hp1 = 0;
                        if (hp2 < 0) hp2 = 0;
                        health1.Width = hp1;
                        health2.Width = hp2;

                        //Automatické doplnění energie
                        Hitboxy.hrac1.setEnergie(Hitboxy.hrac1.getEnergie() + 0.5);
                        Hitboxy.hrac2.setEnergie(Hitboxy.hrac2.getEnergie() + 0.5);
                        if (Hitboxy.hrac1.getEnergie() > 100) Hitboxy.hrac1.setEnergie(100);
                        if (Hitboxy.hrac2.getEnergie() > 100) Hitboxy.hrac2.setEnergie(100);

                        //Obnovení barů na energii
                        double en1 = Hitboxy.hrac1.getEnergie() * 3.2;
                        double en2 = Hitboxy.hrac2.getEnergie() * 3.2;
                        if (en1 < 0) en1 = 0;
                        if (en2 < 0) en2 = 0;
                        energie1.Width = en1;
                        energie2.Width = en2;

                        //Konec kola
                        if (aktivni == 0)
                        {
                            //Remíza
                            if (Hitboxy.hrac1.getHP() <= 0 && Hitboxy.hrac2.getHP() <= 0)
                            {
                                textVyhra.Content = "Remíza";
                                aktivni += 1;
                                gridVyhra.Visibility = Visibility.Visible;
                            }
                            //Výhra 2. hráče
                            else if (Hitboxy.hrac1.getHP() <= 0)
                            {
                                Hitboxy.kola[Hitboxy.aktivniKolo] = 2;
                                Hitboxy.aktivniKolo++;
                                if (Vyhodnotit() == 0)
                                {
                                    postava2round1.Fill = postava2round1.Stroke;

                                    dalsiKolo = DateTime.Now + TimeSpan.FromMilliseconds(5000);
                                    textVyhra.Content = Hitboxy.hrac2.getJmeno() + " vyhrál kolo";
                                }
                                else
                                {
                                    postava2round2.Fill = postava2round2.Stroke;

                                    Statistika();
                                    textVyhra.Content = Hitboxy.hrac2.getJmeno() + " vyhrál zápas";

                                    KonecHry(true);
                                    if (Hitboxy.hrac1.getHP() <= 0 || Hitboxy.vez == -1 || Hitboxy.vez == 4)
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
                            else if (Hitboxy.hrac2.getHP() <= 0)
                            {
                                Hitboxy.kola[Hitboxy.aktivniKolo] = 1;
                                Hitboxy.aktivniKolo++;
                                if (Vyhodnotit() == 0)
                                {
                                    postava1round1.Fill = postava1round1.Stroke;

                                    dalsiKolo = DateTime.Now + TimeSpan.FromMilliseconds(5000);
                                    textVyhra.Content = Hitboxy.hrac1.getJmeno() + " vyhrál kolo";
                                }
                                else
                                {
                                    postava1round2.Fill = postava1round2.Stroke;

                                    Statistika();
                                    textVyhra.Content = Hitboxy.hrac1.getJmeno() + " vyhrál zápas";

                                    KonecHry(true);
                                    if (Hitboxy.hrac1.getHP() <= 0 || Hitboxy.vez == -1 || Hitboxy.vez == 4)
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
                        if (Hitboxy.hrac1.getHP() <= 0)
                        {
                            Hitboxy.hrac1.Umirani(aktivni);
                        }
                        if (Hitboxy.hrac2.getHP() <= 0)
                        {
                            Hitboxy.hrac2.Umirani(aktivni);
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
                            if (Hitboxy.rezimHry && Hitboxy.hrac1.getHP() > 0 && casKola.ElapsedMilliseconds <= 15000) Hitboxy.ukl.PridatPrubeh(3, 1);

                            if (dalsiKolo < DateTime.Now)
                            {
                                aktivni = 0;
                                //Další kolo
                                dalsiBonus = Hitboxy.rnd.Next(7500, 15000);
                                bonusCasovac.Restart();
                                Hitboxy.hrac1.clearBonusy();
                                Hitboxy.hrac2.clearBonusy();

                                foreach (Bonus i in Hitboxy.bonusy)
                                {
                                    if (!i.sebrano)
                                    {
                                        i.sebrano = true;
                                        Plocha.Children.Remove(i.getIkona());
                                    }

                                }

                                Hitboxy.hrac1.smazatProjektily();
                                Hitboxy.hrac2.smazatProjektily();

                                Hitboxy.hrac1.getImg().Margin = new Thickness(0, 0, 0, Hitboxy.platformy[Hitboxy.platformy.Count - 2].Margin.Bottom + 100);
                                Hitboxy.hrac2.getImg().Margin = new Thickness(1700, 0, 0, Hitboxy.platformy[Hitboxy.platformy.Count - 2].Margin.Bottom + 100);

                                Hitboxy.hrac1.setHP(100);
                                Hitboxy.hrac2.setHP(100);

                                Hitboxy.hrac1.setEnergie(100);
                                Hitboxy.hrac2.setEnergie(100);

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
                                textVyhra.Content = String.Format("{0}. kolo", Hitboxy.aktivniKolo + 1);
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
                textVyhra.Content = String.Format("{0}. kolo", Hitboxy.aktivniKolo + 1);
            }
        }

        private void AktualizaceHracu()
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
            if (Hitboxy.hrac1.getSila()) postava1Bonus1.Opacity = 1;
            else postava1Bonus1.Opacity = 0;
            if (Hitboxy.hrac1.getRychlost()) postava1Bonus2.Opacity = 1;
            else postava1Bonus2.Opacity = 0;
            if (Hitboxy.hrac2.getSila()) postava2Bonus1.Opacity = 1;
            else postava2Bonus1.Opacity = 0;
            if (Hitboxy.hrac2.getRychlost()) postava2Bonus2.Opacity = 1;
            else postava2Bonus2.Opacity = 0;

            Hitboxy.hrac1.Tick();
            Hitboxy.hrac2.Tick();
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
            if (Hitboxy.rezimHry && Hitboxy.hrac1.getHP() > 0 && casKola.ElapsedMilliseconds <= 15000) Hitboxy.ukl.PridatPrubeh(3, 1);
            if (Hitboxy.rezimHry && Hitboxy.hrac1.celkem > 0 && (Hitboxy.hrac1.uspesne * 100 / Hitboxy.hrac1.celkem) >= 75) Hitboxy.ukl.PridatPrubeh(4, 1);

            //Uložení do žebříčku
            Hitboxy.ukl.PridatSkore(Hitboxy.hrac1.getJmeno(), Hitboxy.hrac1.skore);
            if (!Hitboxy.rezimHry) Hitboxy.ukl.PridatSkore(Hitboxy.hrac2.getJmeno(), Hitboxy.hrac2.skore);

            //Věž
            if(Hitboxy.vez != -1)
            {
                Hitboxy.skoreCelkem = Hitboxy.hrac1.skore;
            }

            vyhraHrac1.Content = Hitboxy.hrac1Jmeno;
            vyhraHrac2.Content = Hitboxy.hrac2Jmeno;
            vyhraHrac1Neuspesne.Content = "Neúspěšné útoky: " + (Hitboxy.hrac1.celkem - Hitboxy.hrac1.uspesne);
            vyhraHrac1Uspesne.Content = "Úspěšné útoky: " + Hitboxy.hrac1.uspesne;
            vyhraHrac1Skore.Content = "Skóre: " + Hitboxy.hrac1.skore;
            if (Hitboxy.hrac1.celkem != 0) vyhraHrac1Uspesnost.Content = "Úspěšnost: " + (Hitboxy.hrac1.uspesne * 100 / Hitboxy.hrac1.celkem) + "%";
            else vyhraHrac1Uspesnost.Content = "Úspěšnost: 0%";
            vyhraHrac2Neuspesne.Content = "Neúspěšné útoky: " + (Hitboxy.hrac2.celkem - Hitboxy.hrac2.uspesne);
            vyhraHrac2Uspesne.Content = "Úspěšné útoky: " + Hitboxy.hrac2.uspesne;
            vyhraHrac2Skore.Content = "Skóre: " + Hitboxy.hrac2.skore;
            if (Hitboxy.hrac2.celkem != 0) vyhraHrac2Uspesnost.Content = "Úspěšnost: " + (Hitboxy.hrac2.uspesne * 100 / Hitboxy.hrac2.celkem) + "%";
            else vyhraHrac2Uspesnost.Content = "Úspěšnost: 0%";
        }

        //Vyhodnocení dosavadního výsledku zápasu
        int Vyhodnotit()
        {
            if(Hitboxy.vez == 4)
            {
                return Hitboxy.kola[0];
            }
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
            if (!cheatZapnut)
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
            if (!Hitboxy.hrac1.zamknoutOvladani)
            {
                if (e.Key == Hitboxy.ukl.nastaveniKlaves[2])
                    Hitboxy.hrac1.setVlevo(true);
                if (e.Key == Hitboxy.ukl.nastaveniKlaves[3])
                    Hitboxy.hrac1.setVpravo(true);
                if (e.Key == Hitboxy.ukl.nastaveniKlaves[4])
                    Hitboxy.hrac1.setUtok1(true);
                if (e.Key == Hitboxy.ukl.nastaveniKlaves[5])
                    Hitboxy.hrac1.setUtok2(true);
            }
            if (e.Key == Hitboxy.ukl.nastaveniKlaves[1])
                Hitboxy.hrac1.setSkrceni(true);
            if (e.Key == Hitboxy.ukl.nastaveniKlaves[0])
                Hitboxy.hrac1.setSkokTrigger(true);

            //Hráč 2
            if (!Hitboxy.hrac2.zamknoutOvladani)
            {
                if (!Hitboxy.rezimHry && e.Key == Hitboxy.ukl.nastaveniKlaves[8])
                    Hitboxy.hrac2.setVlevo(true);
                if (!Hitboxy.rezimHry && e.Key == Hitboxy.ukl.nastaveniKlaves[9])
                    Hitboxy.hrac2.setVpravo(true);
                if (!Hitboxy.rezimHry && e.Key == Hitboxy.ukl.nastaveniKlaves[10])
                    Hitboxy.hrac2.setUtok1(true);
                if (!Hitboxy.rezimHry && e.Key == Hitboxy.ukl.nastaveniKlaves[11])
                    Hitboxy.hrac2.setUtok2(true);
            }
            if (!Hitboxy.rezimHry && e.Key == Hitboxy.ukl.nastaveniKlaves[7])
                Hitboxy.hrac2.setSkrceni(true);
            if (!Hitboxy.rezimHry && e.Key == Hitboxy.ukl.nastaveniKlaves[6])
                Hitboxy.hrac2.setSkokTrigger(true);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            //Hráč 1
            if (e.Key == Hitboxy.ukl.nastaveniKlaves[2])
                Hitboxy.hrac1.setVlevo(false);
            if (e.Key == Hitboxy.ukl.nastaveniKlaves[3])
                Hitboxy.hrac1.setVpravo(false);
            if (e.Key == Hitboxy.ukl.nastaveniKlaves[1])
                Hitboxy.hrac1.setSkrceni(false);
            if (e.Key == Hitboxy.ukl.nastaveniKlaves[0])
                Hitboxy.hrac1.setSkokTrigger(false);
            if (e.Key == Hitboxy.ukl.nastaveniKlaves[4])
                Hitboxy.hrac1.setUtok1(false);
            if (e.Key == Hitboxy.ukl.nastaveniKlaves[5])
                Hitboxy.hrac1.setUtok2(false);

            //Hráč 2
            if (!Hitboxy.rezimHry && e.Key == Hitboxy.ukl.nastaveniKlaves[8])
                Hitboxy.hrac2.setVlevo(false);
            if (!Hitboxy.rezimHry && e.Key == Hitboxy.ukl.nastaveniKlaves[9])
                Hitboxy.hrac2.setVpravo(false);
            if (!Hitboxy.rezimHry && e.Key == Hitboxy.ukl.nastaveniKlaves[7])
                Hitboxy.hrac2.setSkrceni(false);
            if (!Hitboxy.rezimHry && e.Key == Hitboxy.ukl.nastaveniKlaves[6])
                Hitboxy.hrac2.setSkokTrigger(false);
            if (!Hitboxy.rezimHry && e.Key == Hitboxy.ukl.nastaveniKlaves[10])
                Hitboxy.hrac2.setUtok1(false);
            if (!Hitboxy.rezimHry && e.Key == Hitboxy.ukl.nastaveniKlaves[11])
                Hitboxy.hrac2.setUtok2(false);
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
            Hitboxy.zmenitScale((Grid)sender);
        }

        private void tlacVyhraVez_Click(object sender, RoutedEventArgs e)
        {
            pruchodDoVeze = true;
        }
    }
}
