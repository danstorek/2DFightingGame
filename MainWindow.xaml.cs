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
        bool pozastaveno = false;
        DateTime dalsiKolo;
        int aktivni = 0;
        bool napoveda = true;
        DispatcherTimer gameTick;

        int dalsiBonus;
        Stopwatch bonusCasovac;

        Stopwatch casKola;
        public MainWindow()
        {
            InitializeComponent();
            gameTick = new DispatcherTimer();
            bonusCasovac = new Stopwatch();
            casKola = new Stopwatch();

            //Přidat průběh achievementů
            if (Hitboxy.rezimHry)
            {
                Hitboxy.ukl.PridatPrubeh(0, 1);
                Hitboxy.ukl.PridatPrubeh(1, 1);
                Hitboxy.ukl.PridatPrubeh(2, 1);
            }
        }

        private void Plocha_Loaded(object sender, RoutedEventArgs e)
        {
            Hitboxy.bonusy = new List<Bonus>();

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
                case 0: Hitboxy.hrac1 = new Postava_1(Plocha, postava1, false); break;
                case 1: Hitboxy.hrac1 = new Postava_2(Plocha, postava1, false); break;
                case 2: Hitboxy.hrac1 = new Postava_3(Plocha, postava1, false); break;
            }

            switch (Hitboxy.hrac2Postava)
            {
                case 0: Hitboxy.hrac2 = new Postava_1(Plocha, postava2, false); break;
                case 1: Hitboxy.hrac2 = new Postava_2(Plocha, postava2, false); break;
                case 2: Hitboxy.hrac2 = new Postava_3(Plocha, postava2, false); break;
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
        }

        private void GameTick_Tick(object sender, EventArgs e)
        {
            if (!pozastaveno)
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
                    //Pohyb bota ve hře pro jednoho hráče
                    else if (!napoveda && Hitboxy.rezimHry) SinglePlayer.AI();

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
                            textVyhra2.Content = "Remíza";
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

                                dalsiKolo = DateTime.Now + TimeSpan.FromMilliseconds(1500);
                                textVyhra.Content = Hitboxy.hrac2.getJmeno() + " vyhrál kolo";
                                textVyhra2.Content = Hitboxy.hrac2.getJmeno() + " vyhrál kolo";
                            }
                            else
                            {
                                postava2round2.Fill = postava2round2.Stroke;

                                Statistika();
                                textVyhra.Content = Hitboxy.hrac2.getJmeno() + " vyhrál zápas";
                                textVyhra2.Content = Hitboxy.hrac2.getJmeno() + " vyhrál zápas";
                                tlacVyhra.IsEnabled = true;
                                tlacVyhra.Opacity = 1;
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

                                dalsiKolo = DateTime.Now + TimeSpan.FromMilliseconds(1500);
                                textVyhra.Content = Hitboxy.hrac1.getJmeno() + " vyhrál kolo";
                                textVyhra2.Content = Hitboxy.hrac1.getJmeno() + " vyhrál kolo";
                            }
                            else
                            {
                                postava1round2.Fill = postava1round2.Stroke;

                                Statistika();
                                textVyhra.Content = Hitboxy.hrac1.getJmeno() + " vyhrál zápas";
                                textVyhra2.Content = Hitboxy.hrac1.getJmeno() + " vyhrál zápas";
                                tlacVyhra.IsEnabled = true;
                                tlacVyhra.Opacity = 1;
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

                            aktivni += 1;
                            gridVyhra.Visibility = Visibility.Visible;
                        }
                    }
                    else aktivni++;
                }

                if (aktivni > 1)
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
                        tlacVyhra.Opacity = 0;
                        //Achievement
                        if (Hitboxy.rezimHry && casKola.ElapsedMilliseconds <= 12000) Hitboxy.ukl.PridatPrubeh(3,1);

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

                    }
                }
            }
            else if (pozastaveno && bonusCasovac.IsRunning) bonusCasovac.Stop();
        }

        void Statistika()
        {
            //Achievementy
            if (Hitboxy.rezimHry && casKola.ElapsedMilliseconds <= 12000) Hitboxy.ukl.PridatPrubeh(3, 1);
            if (Hitboxy.rezimHry && (Hitboxy.hrac1.uspesne * 100 / Hitboxy.hrac1.celkem) >= 75) Hitboxy.ukl.PridatPrubeh(4, 1);

            //Uložení do žebříčku
            Hitboxy.ukl.PridatSkore(Hitboxy.hrac1.getJmeno(), Hitboxy.hrac1.skore);
            if(!Hitboxy.rezimHry)Hitboxy.ukl.PridatSkore(Hitboxy.hrac2.getJmeno(), Hitboxy.hrac2.skore);

            vyhraHrac1.Content = Hitboxy.hrac1Jmeno;
            vyhraHrac2.Content = Hitboxy.hrac2Jmeno;
            vyhraHrac1Neuspesne.Content = "Neúspěšné útoky: "+(Hitboxy.hrac1.celkem - Hitboxy.hrac1.uspesne);
            vyhraHrac1Uspesne.Content = "Úspěšné útoky: "+Hitboxy.hrac1.uspesne;
            vyhraHrac1Skore.Content = "Skóre: "+Hitboxy.hrac1.skore;
            if(Hitboxy.hrac1.celkem != 0) vyhraHrac1Uspesnost.Content = "Úspěšnost: "+(Hitboxy.hrac1.uspesne * 100 / Hitboxy.hrac1.celkem)+"%";
            else vyhraHrac1Uspesnost.Content = "Úspěšnost: 0%";
            vyhraHrac2Neuspesne.Content = "Neúspěšné útoky: "+(Hitboxy.hrac2.celkem - Hitboxy.hrac2.uspesne);
            vyhraHrac2Uspesne.Content = "Úspěšné útoky: "+Hitboxy.hrac2.uspesne;
            vyhraHrac2Skore.Content = "Skóre: "+Hitboxy.hrac2.skore;
            if (Hitboxy.hrac2.celkem != 0) vyhraHrac2Uspesnost.Content = "Úspěšnost: " + (Hitboxy.hrac2.uspesne * 100 / Hitboxy.hrac2.celkem) + "%";
            else vyhraHrac2Uspesnost.Content = "Úspěšnost: 0%";
        }

        //Vyhodnocení dosavadního výsledku zápasu
        int Vyhodnotit()
        {
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
                casKola.Start();
                bonusCasovac.Start();
                napoveda = false;
                Plocha.Children.Remove(napoveda1);
                Plocha.Children.Remove(napoveda2);
            }
            switch (e.Key)
            {
                //Pozastavení hry
                case Key.Escape:
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
                    break;

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
                case Key.N:
                    Hitboxy.hrac1.setUtok1(true);
                    break;
                case Key.M:
                    Hitboxy.hrac1.setUtok2(true);
                    break;


                //Hráč 2
                case Key.A:
                    if(!Hitboxy.rezimHry)Hitboxy.hrac2.setVlevo(true);
                    break;
                case Key.D:
                    if (!Hitboxy.rezimHry) Hitboxy.hrac2.setVpravo(true);
                    break;
                case Key.W:
                    if (!Hitboxy.rezimHry) Hitboxy.hrac2.setSkokTrigger(true);
                    break;
                case Key.S:
                    if (!Hitboxy.rezimHry) Hitboxy.hrac2.setSkrceni(true);
                    break;
                case Key.Q:
                    if (!Hitboxy.rezimHry) Hitboxy.hrac2.setUtok1(true);
                    break;
                case Key.E:
                    if (!Hitboxy.rezimHry) Hitboxy.hrac2.setUtok2(true);
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
                case Key.N:
                    Hitboxy.hrac1.setUtok1(false);
                    break;
                case Key.M:
                    Hitboxy.hrac1.setUtok2(false);
                    break;

                //Hráč 2
                case Key.A:
                    if (!Hitboxy.rezimHry) Hitboxy.hrac2.setVlevo(false);
                    break;
                case Key.D:
                    if (!Hitboxy.rezimHry) Hitboxy.hrac2.setVpravo(false);
                    break;
                case Key.W:
                    if (!Hitboxy.rezimHry) Hitboxy.hrac2.setSkokTrigger(false);
                    break;
                case Key.S:
                    if (!Hitboxy.rezimHry) Hitboxy.hrac2.setSkrceni(false);
                    break;
                case Key.Q:
                    if (!Hitboxy.rezimHry) Hitboxy.hrac2.setUtok1(false);
                    break;
                case Key.E:
                    if (!Hitboxy.rezimHry) Hitboxy.hrac2.setUtok2(false);
                    break;
            }
        }

        private void tlacMenu_Click(object sender, RoutedEventArgs e)
        {
            gameTick.Stop();
            HlavniMenu okno = new HlavniMenu();
            okno.Show();
            System.Threading.Thread.Sleep(50);
            this.Close();
        }

        private void tlacPokracovat_Click(object sender, RoutedEventArgs e)
        {
            pauseMenu.Visibility = Visibility.Hidden;
            pozastaveno = false;
        }

        private void tlacUkoncit_Click(object sender, RoutedEventArgs e)
        {
            gameTick.Stop();
            HlavniMenu okno = new HlavniMenu();
            okno.Show();
            System.Threading.Thread.Sleep(50);
            this.Close();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Hitboxy.zmenitScale((Grid)sender);
        }
    }
}
