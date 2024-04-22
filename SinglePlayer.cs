using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _2DFightingGame
{
    static class SinglePlayer
    {
        static Stopwatch cooldownPlatformy = new Stopwatch();
        static Stopwatch cooldownStrela = new Stopwatch();
        static int pohybX = 0;
        public static void AI()
        {
            if (!cooldownPlatformy.IsRunning) cooldownPlatformy.Start();
            if (!cooldownStrela.IsRunning) cooldownStrela.Start();

            //Reset pohybů
            Globalni.hrac2.setSkrceni(false);
            Globalni.hrac2.setSkokTrigger(false);
            Globalni.hrac2.setUtok1(false);
            Globalni.hrac2.setUtok2(false);

            //Pozice hráčů
            Thickness poziceHrac = Globalni.hrac1.getImg().Margin;
            Thickness poziceBot = Globalni.hrac2.getImg().Margin;

            //Hledání volné platformy nad botem
            Image platformaNad = null;
            foreach (Image i in Globalni.platformy)
            {
                Thickness pozicePlatforma = i.Margin;
                if (poziceBot.Bottom + 150 > pozicePlatforma.Bottom && poziceBot.Bottom < pozicePlatforma.Bottom && poziceBot.Left > pozicePlatforma.Left && poziceBot.Left < pozicePlatforma.Left + i.Width)
                {
                    platformaNad = i;
                    break;
                }
            }

            //Hledání volné platformy pod botem
            Image platformaPod = null;
            foreach (Image i in Globalni.platformy)
            {
                Thickness pozicePlatforma = i.Margin;
                if (poziceBot.Bottom > pozicePlatforma.Bottom && poziceBot.Left > pozicePlatforma.Left && poziceBot.Left < pozicePlatforma.Left + i.Width)
                {
                    platformaPod = i;
                    break;
                }
            }

            //Hledání platformy, na které hráč stojí
            Image aktualniPlatforma = null;
            foreach (Image i in Globalni.platformy)
            {
                Thickness pozicePlatforma = i.Margin;
                if (poziceBot.Bottom > pozicePlatforma.Bottom && poziceBot.Bottom < pozicePlatforma.Bottom + i.Height + 5 && poziceBot.Left > pozicePlatforma.Left && poziceBot.Left < pozicePlatforma.Left + i.Width)
                {
                    aktualniPlatforma = i;
                    break;
                }
            }

            //Hledání bonusů
            Bonus hp = null;
            Bonus bonus = null;
            IEnumerable<Bonus> hp_IEnum = Globalni.bonusy.Where(x => x.id == 0);
            IEnumerable<Bonus> bonus_IEnum = Globalni.bonusy.Where(x => x.id != 0);
            if (hp_IEnum.Count() > 0) hp = hp_IEnum.First();
            if (bonus_IEnum.Count() > 0) bonus = bonus_IEnum.First();

            //Sbírání HP při nízkém počtu
            if (hp != null && aktualniPlatforma != null && Globalni.hrac2.getHP() <= 40)
            {
                //Platforma nad botem
                if (Globalni.platformy[hp.platforma].Margin.Bottom > aktualniPlatforma.Margin.Bottom && platformaNad != null)
                {
                    Globalni.hrac2.setSkokTrigger(true);
                }

                //Platforma pod botem
                else if (Globalni.platformy[hp.platforma].Margin.Bottom < aktualniPlatforma.Margin.Bottom && platformaPod != null)
                {
                    Globalni.hrac2.setSkrceni(true);
                }

                //Pohyb na stejné úrovni
                else
                {
                    pohybX = Convert.ToInt32(hp.getIkona().Margin.Left+(hp.getIkona().Width/2));
                }
            }

            //Vyhýbání se TNT
            TNT tnt = null;
            IEnumerable<Aktualizovatelne> tnt_IENum = Globalni.hrac1.aktivni_projektily.Where(x => x.GetType() == typeof(TNT) && x.getAktivni() && ((TNT)x).cisloFrame < 61);
            if (tnt_IENum.Count() > 0) tnt = (TNT)tnt_IENum.First();
            if (tnt == null)
            {
                tnt_IENum = Globalni.hrac2.aktivni_projektily.Where(x => x.GetType() == typeof(TNT) && x.getAktivni() && ((TNT)x).cisloFrame < 61);
                if (tnt_IENum.Count() > 0) tnt = (TNT)tnt_IENum.First();
            }
            if (tnt != null && Globalni.hrac2.id != 4)
            {
                if (Math.Abs(tnt.ReturnImage().Margin.Left - Globalni.hrac2.getImg().Margin.Left) < 600)
                {
                    if (tnt.ReturnImage().Margin.Left > Globalni.hrac2.getImg().Margin.Left)
                    {
                        if (tnt.ReturnImage().Margin.Left < 500) pohybX = 1720;
                        else pohybX = 200;
                    }
                    else
                    {
                        if (tnt.ReturnImage().Margin.Left > 1420) pohybX = 200;
                        else pohybX = 1720;
                    }
                }
                else pohybX = 0;
            }

            //Střelba po soupeři
            else if (Globalni.rnd.Next(1, 11) >= 3)
            {
                if (poziceBot.Bottom > poziceHrac.Bottom - 10 && poziceBot.Bottom < poziceHrac.Bottom + 30)
                {
                    if (poziceBot.Left < poziceHrac.Left)
                    {
                        Globalni.hrac2.setSmer(true);
                        if (Globalni.hrac2.id == 0 || Globalni.hrac2.id == 2)
                        {
                            Globalni.hrac2.setVlevo(false);
                            Globalni.hrac2.setVpravo(false);

                            if (Math.Abs(poziceBot.Left - poziceHrac.Left) < 300) Globalni.hrac2.setUtok2(true);
                            Globalni.hrac2.setUtok1(true);
                        }
                        if (Globalni.hrac2.id == 1)
                        {
                            if (Math.Abs(poziceBot.Left - poziceHrac.Left) < 300) Globalni.hrac2.setUtok1(true);
                            else Globalni.hrac2.setUtok2(true);
                            Globalni.hrac2.setVpravo(true);
                            Globalni.hrac2.setVlevo(false);

                            //Vyhnutí se mezery mezi platformami
                            if (aktualniPlatforma != null && poziceBot.Left > aktualniPlatforma.Margin.Left + aktualniPlatforma.Width - 150) Globalni.hrac2.setSkokTrigger(true);
                        }
                        if (Globalni.hrac2.id == 3)
                        {
                            if (Math.Abs(poziceBot.Left - poziceHrac.Left) < 300) Globalni.hrac2.setUtok1(true);
                            Globalni.hrac2.setUtok2(true);
                            Globalni.hrac2.setVpravo(true);
                            Globalni.hrac2.setVlevo(false);

                            //Vyhnutí se mezery mezi platformami
                            if (aktualniPlatforma != null && poziceBot.Left > aktualniPlatforma.Margin.Left + aktualniPlatforma.Width - 150) Globalni.hrac2.setSkokTrigger(true);
                        }
                        if (Globalni.hrac2.id == 4)
                        {
                            if (Math.Abs(poziceBot.Left - poziceHrac.Left) < 300) Globalni.hrac2.setUtok1(true);
                            else Globalni.hrac2.setUtok2(true);
                            Globalni.hrac2.setVpravo(true);
                            Globalni.hrac2.setVlevo(false);

                            //Vyhnutí se mezery mezi platformami
                            if (aktualniPlatforma != null && poziceBot.Left > aktualniPlatforma.Margin.Left + aktualniPlatforma.Width - 150) Globalni.hrac2.setSkokTrigger(true);
                        }
                    }
                    else
                    {
                        Globalni.hrac2.setSmer(false);
                        if (Globalni.hrac2.id == 0 || Globalni.hrac2.id == 2)
                        {
                            Globalni.hrac2.setVlevo(false);
                            Globalni.hrac2.setVpravo(false);

                            if (Math.Abs(poziceBot.Left - poziceHrac.Left) < 300) Globalni.hrac2.setUtok2(true);
                            Globalni.hrac2.setUtok1(true);
                        }
                        if (Globalni.hrac2.id == 1)
                        {
                            if (Math.Abs(poziceBot.Left - poziceHrac.Left) < 300) Globalni.hrac2.setUtok1(true);
                            else Globalni.hrac2.setUtok2(true);
                            Globalni.hrac2.setVpravo(false);
                            Globalni.hrac2.setVlevo(true);

                            //Vyhnutí se mezery mezi platformami
                            if (aktualniPlatforma != null && poziceBot.Left < aktualniPlatforma.Margin.Left + 150) Globalni.hrac2.setSkokTrigger(true);
                        }
                        if (Globalni.hrac2.id == 3)
                        {
                            if (Math.Abs(poziceBot.Left - poziceHrac.Left) < 300) Globalni.hrac2.setUtok1(true);
                            Globalni.hrac2.setUtok2(true);
                            Globalni.hrac2.setVpravo(false);
                            Globalni.hrac2.setVlevo(true);

                            //Vyhnutí se mezery mezi platformami
                            if (aktualniPlatforma != null && poziceBot.Left < aktualniPlatforma.Margin.Left + 150) Globalni.hrac2.setSkokTrigger(true);
                        }
                        if (Globalni.hrac2.id == 4)
                        {
                            if (Math.Abs(poziceBot.Left - poziceHrac.Left) < 300) Globalni.hrac2.setUtok1(true);
                            else Globalni.hrac2.setUtok2(true);
                            Globalni.hrac2.setVpravo(false);
                            Globalni.hrac2.setVlevo(true);

                            //Vyhnutí se mezery mezi platformami
                            if (aktualniPlatforma != null && poziceBot.Left < aktualniPlatforma.Margin.Left + 150) Globalni.hrac2.setSkokTrigger(true);
                        }
                    }
                    pohybX = 0;
                    cooldownStrela.Restart();
                }
            }

            //Sbírání bonusů
            else if (bonus != null && aktualniPlatforma != null)
            {
                //Platforma nad botem
                if (Globalni.platformy[bonus.platforma].Margin.Bottom > aktualniPlatforma.Margin.Bottom && platformaNad != null)
                {
                    Globalni.hrac2.setSkokTrigger(true);
                }

                //Platforma pod botem
                else if (Globalni.platformy[bonus.platforma].Margin.Bottom < aktualniPlatforma.Margin.Bottom && platformaPod != null)
                {
                    Globalni.hrac2.setSkrceni(true);
                }

                //Pohyb na stejné úrovni
                else
                {
                    pohybX = Convert.ToInt32(bonus.getIkona().Margin.Left + (bonus.getIkona().Width / 2));
                }
            }

            //Výskok na platformu
            else if (!Globalni.hrac2.zamknoutOvladani && (hp == null || Globalni.hrac2.getHP() > 40) && cooldownPlatformy.ElapsedMilliseconds > 1500 && platformaNad != null && Globalni.rnd.Next(1, 11) >= 5)
            {
                pohybX = 0;
                Globalni.hrac2.setSkokTrigger(true);
                cooldownPlatformy.Restart();
            }

            //Seskok z platformy
            else if (!Globalni.hrac2.zamknoutOvladani && (hp == null || Globalni.hrac2.getHP() > 40) && cooldownPlatformy.ElapsedMilliseconds > 1500 && platformaPod != null)
            {
                pohybX = 0;
                Globalni.hrac2.setSkrceni(true);
                cooldownPlatformy.Restart();
            }

            //Pohyb
            if (!Globalni.hrac2.zamknoutOvladani && (hp == null || Globalni.hrac2.getHP() > 40) && pohybX == 0 && cooldownStrela.ElapsedMilliseconds > 200)
            {
                Globalni.hrac2.setVlevo(false);
                Globalni.hrac2.setVpravo(false);
                if (aktualniPlatforma != null) pohybX = Globalni.rnd.Next(Convert.ToInt32(aktualniPlatforma.Margin.Left), Convert.ToInt32(aktualniPlatforma.Margin.Left + aktualniPlatforma.Width));
            }
            else if (!Globalni.hrac2.zamknoutOvladani && pohybX != 0)
            {
                if (Math.Abs(poziceBot.Left - pohybX) < 10) pohybX = 0;
                else if (poziceBot.Left < pohybX)
                {
                    Globalni.hrac2.setVpravo(true);
                    Globalni.hrac2.setVlevo(false);

                    //Vyhnutí se mezery mezi platformami
                    if (aktualniPlatforma != null && poziceBot.Left > aktualniPlatforma.Margin.Left + aktualniPlatforma.Width - 150) Globalni.hrac2.setSkokTrigger(true);
                }
                else if (poziceBot.Left > pohybX)
                {
                    Globalni.hrac2.setVlevo(true);
                    Globalni.hrac2.setVpravo(false);

                    //Vyhnutí se mezery mezi platformami
                    if (aktualniPlatforma != null && poziceBot.Left < aktualniPlatforma.Margin.Left + 150) Globalni.hrac2.setSkokTrigger(true);
                }
            }
        }
    }
}