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
            Hitboxy.hrac2.setSkrceni(false);
            Hitboxy.hrac2.setSkokTrigger(false);
            Hitboxy.hrac2.setUtok1(false);
            Hitboxy.hrac2.setUtok2(false);

            //Pozice hráčů
            Thickness poziceHrac = Hitboxy.hrac1.getImg().Margin;
            Thickness poziceBot = Hitboxy.hrac2.getImg().Margin;

            //Hledání volné platformy nad botem
            Image platformaNad = null;
            foreach (Image i in Hitboxy.platformy)
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
            foreach (Image i in Hitboxy.platformy)
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
            foreach (Image i in Hitboxy.platformy)
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
            IEnumerable<Bonus> hp_IEnum = Hitboxy.bonusy.Where(x => x.id == 0);
            IEnumerable<Bonus> bonus_IEnum = Hitboxy.bonusy.Where(x => x.id != 0);
            if (hp_IEnum.Count() > 0) hp = hp_IEnum.First();
            if (bonus_IEnum.Count() > 0) bonus = bonus_IEnum.First();

            //Sbírání HP při nízkém počtu
            if (hp != null && aktualniPlatforma != null && Hitboxy.hrac2.getHP() <= 40)
            {
                //Platforma nad botem
                if (Hitboxy.platformy[hp.platforma].Margin.Bottom > aktualniPlatforma.Margin.Bottom && platformaNad != null)
                {
                    Hitboxy.hrac2.setSkokTrigger(true);
                }

                //Platforma pod botem
                else if (Hitboxy.platformy[hp.platforma].Margin.Bottom < aktualniPlatforma.Margin.Bottom && platformaPod != null)
                {
                    Hitboxy.hrac2.setSkrceni(true);
                }

                //Pohyb na stejné úrovni
                else
                {
                    pohybX = Convert.ToInt32(hp.getIkona().Margin.Left+(hp.getIkona().Width/2));
                }
            }

            //Vyhýbání se TNT
            TNT tnt = null;
            IEnumerable<Updatable> tnt_IENum = Hitboxy.hrac1.aktivni_projektily.Where(x => x.GetType() == typeof(TNT) && x.getAktivni());
            if (tnt_IENum.Count() > 0) tnt = (TNT)tnt_IENum.First();
            if (tnt == null)
            {
                tnt_IENum = Hitboxy.hrac2.aktivni_projektily.Where(x => x.GetType() == typeof(TNT) && x.getAktivni());
                if (tnt_IENum.Count() > 0) tnt = (TNT)tnt_IENum.First();
            }
            if (tnt != null)
            {
                if (Math.Abs(tnt.ReturnImage().Margin.Left - Hitboxy.hrac2.getImg().Margin.Left) < 600)
                {
                    if (tnt.ReturnImage().Margin.Left > Hitboxy.hrac2.getImg().Margin.Left)
                    {
                        pohybX = 200;
                    }
                    else
                    {
                        pohybX = 1720;
                    }
                }
                else pohybX = 0;
            }

            //Sbírání bonusů
            else if(bonus != null && aktualniPlatforma != null)
            {
                //Platforma nad botem
                if (Hitboxy.platformy[bonus.platforma].Margin.Bottom > aktualniPlatforma.Margin.Bottom && platformaNad != null)
                {
                    Hitboxy.hrac2.setSkokTrigger(true);
                }

                //Platforma pod botem
                else if (Hitboxy.platformy[bonus.platforma].Margin.Bottom < aktualniPlatforma.Margin.Bottom && platformaPod != null)
                {
                    Hitboxy.hrac2.setSkrceni(true);
                }

                //Pohyb na stejné úrovni
                else
                {
                    pohybX = Convert.ToInt32(bonus.getIkona().Margin.Left + (bonus.getIkona().Width / 2));
                }
            }

            //Střelba po soupeři
            else if (Hitboxy.rnd.Next(1, 11) >= 3)
            {
                if (poziceBot.Bottom > poziceHrac.Bottom - 10 && poziceBot.Bottom < poziceHrac.Bottom + 30)
                {
                    if (poziceBot.Left < poziceHrac.Left)
                    {
                        Hitboxy.hrac2.setSmer(true);
                        if (Hitboxy.hrac2.getId == 0 || Hitboxy.hrac2.getId == 2)
                        {
                            Hitboxy.hrac2.setVlevo(false);
                            Hitboxy.hrac2.setVpravo(false);

                            if (Math.Abs(poziceBot.Left - poziceHrac.Left) < 300) Hitboxy.hrac2.setUtok2(true);
                            Hitboxy.hrac2.setUtok1(true);
                        }
                        if (Hitboxy.hrac2.getId == 1)
                        {
                            if (Math.Abs(poziceBot.Left - poziceHrac.Left) < 300) Hitboxy.hrac2.setUtok1(true);
                            else Hitboxy.hrac2.setUtok2(true);
                            Hitboxy.hrac2.setVpravo(true);
                            Hitboxy.hrac2.setVlevo(false);

                            //Vyhnutí se mezery mezi platformami
                            if (aktualniPlatforma != null && poziceBot.Left > aktualniPlatforma.Margin.Left + aktualniPlatforma.Width - 150) Hitboxy.hrac2.setSkokTrigger(true);
                        }
                        if (Hitboxy.hrac2.getId == 3)
                        {
                            if (Math.Abs(poziceBot.Left - poziceHrac.Left) < 300) Hitboxy.hrac2.setUtok1(true);
                            Hitboxy.hrac2.setUtok2(true);
                            Hitboxy.hrac2.setVpravo(true);
                            Hitboxy.hrac2.setVlevo(false);

                            //Vyhnutí se mezery mezi platformami
                            if (aktualniPlatforma != null && poziceBot.Left > aktualniPlatforma.Margin.Left + aktualniPlatforma.Width - 150) Hitboxy.hrac2.setSkokTrigger(true);
                        }
                        if (Hitboxy.hrac2.getId == 4)
                        {
                            if (Math.Abs(poziceBot.Left - poziceHrac.Left) < 300) Hitboxy.hrac2.setUtok1(true);
                            else Hitboxy.hrac2.setUtok2(true);
                            Hitboxy.hrac2.setVpravo(true);
                            Hitboxy.hrac2.setVlevo(false);

                            //Vyhnutí se mezery mezi platformami
                            if (aktualniPlatforma != null && poziceBot.Left > aktualniPlatforma.Margin.Left + aktualniPlatforma.Width - 150) Hitboxy.hrac2.setSkokTrigger(true);
                        }
                    }
                    else
                    {
                        Hitboxy.hrac2.setSmer(false);
                        if (Hitboxy.hrac2.getId == 0 || Hitboxy.hrac2.getId == 2)
                        {
                            Hitboxy.hrac2.setVlevo(false);
                            Hitboxy.hrac2.setVpravo(false);

                            if (Math.Abs(poziceBot.Left - poziceHrac.Left) < 300) Hitboxy.hrac2.setUtok2(true);
                            Hitboxy.hrac2.setUtok1(true);
                        }
                        if (Hitboxy.hrac2.getId == 1)
                        {
                            if (Math.Abs(poziceBot.Left - poziceHrac.Left) < 300) Hitboxy.hrac2.setUtok1(true);
                            else Hitboxy.hrac2.setUtok2(true);
                            Hitboxy.hrac2.setVpravo(false);
                            Hitboxy.hrac2.setVlevo(true);

                            //Vyhnutí se mezery mezi platformami
                            if (aktualniPlatforma != null && poziceBot.Left < aktualniPlatforma.Margin.Left + 150) Hitboxy.hrac2.setSkokTrigger(true);
                        }
                        if (Hitboxy.hrac2.getId == 3)
                        {
                            if (Math.Abs(poziceBot.Left - poziceHrac.Left) < 300) Hitboxy.hrac2.setUtok1(true);
                            Hitboxy.hrac2.setUtok2(true);
                            Hitboxy.hrac2.setVpravo(false);
                            Hitboxy.hrac2.setVlevo(true);

                            //Vyhnutí se mezery mezi platformami
                            if (aktualniPlatforma != null && poziceBot.Left < aktualniPlatforma.Margin.Left + 150) Hitboxy.hrac2.setSkokTrigger(true);
                        }
                        if (Hitboxy.hrac2.getId == 4)
                        {
                            if (Math.Abs(poziceBot.Left - poziceHrac.Left) < 300) Hitboxy.hrac2.setUtok1(true);
                            else Hitboxy.hrac2.setUtok2(true);
                            Hitboxy.hrac2.setVpravo(false);
                            Hitboxy.hrac2.setVlevo(true);

                            //Vyhnutí se mezery mezi platformami
                            if (aktualniPlatforma != null && poziceBot.Left < aktualniPlatforma.Margin.Left + 150) Hitboxy.hrac2.setSkokTrigger(true);
                        }
                    }
                    pohybX = 0;
                    cooldownStrela.Restart();
                }
            }

            //Výskok na platformu
            else if (!Hitboxy.hrac2.zamknoutOvladani && (hp == null || Hitboxy.hrac2.getHP() > 40) && cooldownPlatformy.ElapsedMilliseconds > 1500 && platformaNad != null && Hitboxy.rnd.Next(1, 11) >= 5)
            {
                pohybX = 0;
                Hitboxy.hrac2.setSkokTrigger(true);
                cooldownPlatformy.Restart();
            }

            //Seskok z platformy
            else if (!Hitboxy.hrac2.zamknoutOvladani && (hp == null || Hitboxy.hrac2.getHP() > 40) && cooldownPlatformy.ElapsedMilliseconds > 1500 && platformaPod != null)
            {
                pohybX = 0;
                Hitboxy.hrac2.setSkrceni(true);
                cooldownPlatformy.Restart();
            }

            //Pohyb
            if (!Hitboxy.hrac2.zamknoutOvladani && (hp == null || Hitboxy.hrac2.getHP() > 40) && pohybX == 0 && cooldownStrela.ElapsedMilliseconds > 200)
            {
                Hitboxy.hrac2.setVlevo(false);
                Hitboxy.hrac2.setVpravo(false);
                if (aktualniPlatforma != null) pohybX = Hitboxy.rnd.Next(Convert.ToInt32(aktualniPlatforma.Margin.Left), Convert.ToInt32(aktualniPlatforma.Margin.Left + aktualniPlatforma.Width));
            }
            else if (!Hitboxy.hrac2.zamknoutOvladani && pohybX != 0)
            {
                if (Math.Abs(poziceBot.Left - pohybX) < 10) pohybX = 0;
                else if (poziceBot.Left < pohybX)
                {
                    Hitboxy.hrac2.setVpravo(true);
                    Hitboxy.hrac2.setVlevo(false);

                    //Vyhnutí se mezery mezi platformami
                    if (aktualniPlatforma != null && poziceBot.Left > aktualniPlatforma.Margin.Left + aktualniPlatforma.Width - 150) Hitboxy.hrac2.setSkokTrigger(true);
                }
                else if (poziceBot.Left > pohybX)
                {
                    Hitboxy.hrac2.setVlevo(true);
                    Hitboxy.hrac2.setVpravo(false);

                    //Vyhnutí se mezery mezi platformami
                    if (aktualniPlatforma != null && poziceBot.Left < aktualniPlatforma.Margin.Left + 150) Hitboxy.hrac2.setSkokTrigger(true);
                }
            }
        }
    }
}