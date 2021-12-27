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

            //Střelba po soupeři
            if (Hitboxy.rnd.Next(1, 11) >= 3)
            {
                if (poziceBot.Bottom > poziceHrac.Bottom - 10 && poziceBot.Bottom < poziceHrac.Bottom + 30)
                {
                    if (poziceBot.Left < poziceHrac.Left)
                    {
                        Hitboxy.hrac2.setSmer(true);
                        if (Hitboxy.hrac2.getId == 0)
                        {
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

                    }
                    else
                    {
                        Hitboxy.hrac2.setSmer(false);
                        if (Hitboxy.hrac2.getId == 0)
                        {
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
                    }
                    pohybX = 0;
                    cooldownStrela.Restart();
                }
            }

            //Výskok na platformu
            else if (cooldownPlatformy.ElapsedMilliseconds > 1500 && platformaNad != null && Hitboxy.rnd.Next(1, 11) >= 4)
            {
                pohybX = 0;
                Hitboxy.hrac2.setSkokTrigger(true);
                cooldownPlatformy.Restart();
            }

            //Seskok z platformy
            else if (cooldownPlatformy.ElapsedMilliseconds > 1500 && platformaPod != null && Hitboxy.rnd.Next(1, 11) >= 4)
            {
                pohybX = 0;
                Hitboxy.hrac2.setSkrceni(true);
                cooldownPlatformy.Restart();
            }

            //Pohyb
            if (pohybX == 0 && cooldownStrela.ElapsedMilliseconds > 200)
            {
                Hitboxy.hrac2.setVlevo(false);
                Hitboxy.hrac2.setVpravo(false);
                if (aktualniPlatforma != null) pohybX = Hitboxy.rnd.Next(Convert.ToInt32(aktualniPlatforma.Margin.Left), Convert.ToInt32(aktualniPlatforma.Margin.Left + aktualniPlatforma.Width));
            }
            else if (pohybX != 0)
            {
                if (Math.Abs(poziceBot.Left - pohybX) < 150) pohybX = 0;
                else if (poziceBot.Left < pohybX)
                {
                    Hitboxy.hrac2.setVpravo(true);
                    Hitboxy.hrac2.setVlevo(false);
                }
                else if (poziceBot.Left > pohybX)
                {
                    Hitboxy.hrac2.setVlevo(true);
                    Hitboxy.hrac2.setVpravo(false);
                }
            }
        }
    }
}