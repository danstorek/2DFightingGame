using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace _2DFightingGame
{
    abstract class Postava
    {
        protected string jmeno;

        protected List<Updatable> aktivni_projektily = new List<Updatable>();

        protected Label detaily;
        protected Image imgPostava;
        protected Image imgDamage = new Image();
        protected Grid gridPlocha;
        protected int hp = 100;
        protected bool vlevo = false;
        protected bool vpravo = false;
        protected bool skrceni = false;
        protected bool smer = true;
        protected bool skokTrigger = false;
        protected bool veVzduchu = false;
        protected bool skok = false;
        protected int skokPocet = 0;
        protected bool utok1 = false;
        protected bool utok2 = false;
        protected int maxRychlost = 30;
        protected DateTime cooldownUtok1 = DateTime.Now;
        protected DateTime cooldownUtok2 = DateTime.Now;
        protected int cooldownUtok1Max;
        protected int cooldownUtok2Max;
        protected int poskozeniTimer = 0;
        protected DateTime zmrazen = DateTime.Now;

        protected int pohybX = 0;
        public abstract void Tick();
        protected List<BitmapImage> animace_left = new List<BitmapImage>();
        protected List<BitmapImage> animace_right = new List<BitmapImage>();
        protected int animace_index = 0;
        protected int tick_animace = 0;
        protected void Pohyb(Thickness pozice)
        {
            //Pohyb - hráč 1
            if (vpravo && !skrceni)
            {
                if (pohybX < maxRychlost && !skrceni) pohybX += maxRychlost / 10;
                imgPostava.Source = animace_right[animace_index];
                smer = true;
            }
            else if (vlevo && !skrceni)
            {
                if (pohybX > (0 - maxRychlost) && !skrceni) pohybX -= maxRychlost / 10;
                imgPostava.Source = animace_left[animace_index];
                smer = false;
            }
            else
            {
                if (pohybX < 3 && pohybX > -3) pohybX = 0;
                if (pohybX > 0) pohybX -= 3;
                if (pohybX < 0) pohybX += 3;
            }

            if (pohybX != 0)
            {
                tick_animace++;
                if (tick_animace > 30) tick_animace = 0;
                animace_index = tick_animace / 7;
            }
            else
            {
                animace_index = 0;
                if (!smer) imgPostava.Source = animace_left[animace_index];
                else imgPostava.Source = animace_right[animace_index];
            }

            if (!getZmrazen())
            {
                if (pohybX > 0)
                {
                    if (pozice.Left + pohybX < 1800 && (pozice.Left + pohybX < Hitboxy.getSouper(this).getImg().Margin.Left-100 || pozice.Left > Hitboxy.getSouper(this).getImg().Margin.Left || pozice.Bottom < Hitboxy.getSouper(this).getImg().Margin.Bottom -50 || pozice.Bottom > Hitboxy.getSouper(this).getImg().Margin.Bottom+50)) pozice.Left += pohybX;
                }
                else
                {
                    if (pozice.Left + pohybX > -70 && (pozice.Left + pohybX + getImg().Width > Hitboxy.getSouper(this).getImg().Margin.Left + Hitboxy.getSouper(this).getImg().Width+100 || pozice.Left + getImg().Width < Hitboxy.getSouper(this).getImg().Margin.Left + Hitboxy.getSouper(this).getImg().Width || pozice.Bottom < Hitboxy.getSouper(this).getImg().Margin.Bottom - 50 || pozice.Bottom > Hitboxy.getSouper(this).getImg().Margin.Bottom + 50)) pozice.Left += pohybX;
                }
            }

            //Skrčení
            if (!veVzduchu && Hitboxy.MuzePadat(this) == 210 && !getZmrazen())
            {
                int pad = Hitboxy.MuzePadat(this);
                if (skrceni)
                {
                    pozice.Bottom = Hitboxy.MuzePadat(this) - 50;
                    if (smer) imgPostava.Source = animace_right[animace_right.Count - 1];
                    else imgPostava.Source = animace_left[animace_right.Count - 1];
                }
                else if (pad != 1)
                {
                    pozice.Bottom = pad;
                }
            }
            else if(!veVzduchu && Hitboxy.MuzePadat(this) != 210 && Hitboxy.MuzePadat(this) != 1 && skrceni && !getZmrazen())
            {
                pozice.Bottom -= 25;
            }


            //Skok - hráč 1
            if (skokTrigger && !skrceni && !getZmrazen())
            {
                if (!veVzduchu)
                {
                    veVzduchu = true;
                    skok = true;
                }
            }
            if (skok)
            {
                pozice.Bottom += 20;
                skokPocet++;
                if (skokPocet >= 12)
                {
                    skok = false;
                    skokPocet = 0;
                }
            }

            //Gravitace - hráč 1
            if (veVzduchu || Hitboxy.MuzePadat(this) >= 1)
            {
                if (!skok)
                {
                    int pad = Hitboxy.MuzePadat(this);
                    if (pad == 1) pozice.Bottom -= 25;
                    else if(!skrceni)
                    {
                        pozice.Bottom = pad;
                        veVzduchu = false;
                    }
                }
            }
            imgPostava.Margin = pozice;
        }
        public void setJmeno(string jmeno)
        {
            this.jmeno = jmeno;
        }
        public string getJmeno()
        {
            return this.jmeno;
        }
        public abstract void setSkrceni(bool hodnota);
        public int[] getCooldown()
        {
            int[] temp = new int[2];
            double zbyvajiciCas1 = (cooldownUtok1 - DateTime.Now).TotalMilliseconds;
            double zbyvajiciCas2 = (cooldownUtok2 - DateTime.Now).TotalMilliseconds;
            if (zbyvajiciCas1 <= 0) temp[0] = 0;
            else
            {
                temp[0] = Convert.ToInt32(zbyvajiciCas1 / cooldownUtok1Max * 64);
            }
            if (zbyvajiciCas2 <= 0) temp[1] = 0;
            else
            {
                temp[1] = Convert.ToInt32(zbyvajiciCas2 / cooldownUtok2Max * 64);
            }
            return temp;
        }
        public Image getImg()
        {
            return imgPostava;
        }
        public int getHP()
        {
            return hp;
        }
        public void setVlevo(bool hodnota)
        {
            vlevo = hodnota;
        }
        public void setVpravo(bool hodnota)
        {
            vpravo = hodnota;
        }
        public void setSkokTrigger(bool hodnota)
        {
            skokTrigger = hodnota;
        }
        public void setUtok1(bool hodnota)
        {
            utok1 = hodnota;
        }
        public void setUtok2(bool hodnota)
        {
            utok2 = hodnota;
        }
        public void setZmrazen(int ms)
        {
            zmrazen = DateTime.Now + TimeSpan.FromMilliseconds(ms);
        }
        public bool getZmrazen()
        {
            return zmrazen > DateTime.Now;
        }
        //Odražení protivníkem
        public void Odrazeni(int sila)
        {
            pohybX += sila;
        }

        //Poškození protivníkem
        public void Poskozeni(int rozdil)
        {
            hp -= rozdil;
            poskozeniTimer = 10;
        }
        //Aktualizace aktivních projektilů ve hře
        public void aktualizujProjektily()
        {
            foreach (Updatable i in aktivni_projektily)
            {
                if (i.getAktivni()) i.Tick();
                else gridPlocha.Children.Remove(i.ReturnImage());
            }
        }
        protected void VytvorDmgIndikator()
        {
            imgDamage.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/chars/damage.png"));
            imgDamage.Opacity = 0;
            imgDamage.Width = 120;
            imgDamage.Height = 185;
            imgDamage.HorizontalAlignment = HorizontalAlignment.Left;
            imgDamage.VerticalAlignment = VerticalAlignment.Bottom;
            Panel.SetZIndex(imgDamage, 2);
            gridPlocha.Children.Add(imgDamage);
        }
        protected void AktualizujDmgIndikator()
        {
            imgDamage.Margin = imgPostava.Margin;
            if (poskozeniTimer > 0 && imgDamage.Opacity < 1)
            {
                imgDamage.Opacity += 0.1;
                poskozeniTimer--;
            }
            else if (imgDamage.Opacity > 0) imgDamage.Opacity -= 0.1;
        }
    }

    class Postava_1 : Postava
    {
        Image muzzleflash = new Image();

        int naboje = 7;
        DateTime cooldownPrebiti = DateTime.Now;
        DateTime muzzleTimer = DateTime.Now;
        public Postava_1(Grid plocha, Image postava, bool strana, Label detaily)
        {
            cooldownUtok1Max = 400;
            cooldownUtok2Max = 1200;

            this.maxRychlost = 20;

            imgPostava = postava;
            gridPlocha = plocha;
            this.detaily = detaily;

            muzzleflash.Width = 50;
            muzzleflash.HorizontalAlignment = HorizontalAlignment.Left;
            muzzleflash.VerticalAlignment = VerticalAlignment.Bottom;
            //muzzleflash.Opacity = 0;
            gridPlocha.Children.Add(muzzleflash);

            VytvorDmgIndikator();

            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/0.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/1.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/2.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/3.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/4.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/crouch.png")));

            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/0.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/1.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/2.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/3.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/4.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/crouch.png")));

            this.detaily.Content = naboje;

            if (strana) imgPostava.Source = animace_left[animace_index];
            else imgPostava.Source = animace_right[animace_index];
        }

        public override void setSkrceni(bool hodnota)
        {
            skrceni = hodnota;
            if (!hodnota)
            {
                animace_index = 0;
                if (!smer) imgPostava.Source = animace_left[animace_index];
                else imgPostava.Source = animace_right[animace_index];
            }
        }
        public override void Tick()
        {
            AktualizujDmgIndikator();
            //Přebíjení
            if (naboje == 0 && DateTime.Now > cooldownPrebiti)
            {
                naboje = 7;
                this.detaily.Content = naboje;
            }
            Thickness pozice = imgPostava.Margin;
            //Útok 1 - hráč 1
            if (utok1 && DateTime.Now > cooldownUtok1)
            {
                if (naboje > 0)
                {
                    naboje--;
                    muzzleTimer = DateTime.Now + TimeSpan.FromMilliseconds(150);
                    Fireball fireball = new Fireball(imgPostava, smer);
                    gridPlocha.Children.Add(fireball.ReturnImage());
                    aktivni_projektily.Add(fireball);
                    cooldownUtok1 = DateTime.Now.AddMilliseconds(cooldownUtok1Max);
                    if (naboje == 0)
                    {
                        detaily.Content = "...";
                        cooldownPrebiti = DateTime.Now + TimeSpan.FromMilliseconds(1500);
                    }
                    else
                    {
                        this.detaily.Content = naboje;
                    }
                }

            }

            //Útok 2 - hráč 1
            if (utok2 && DateTime.Now > cooldownUtok2)
            {
                TNT sw = new TNT(this);
                gridPlocha.Children.Add(sw.ReturnImage());
                aktivni_projektily.Add(sw);
                cooldownUtok2 = DateTime.Now.AddMilliseconds(cooldownUtok2Max);
            }

            Pohyb(pozice);

            //Výstřel zbraně
            if (!smer) muzzleflash.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/attacks/muzzle_left.png"));
            else muzzleflash.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/attacks/muzzle_right.png"));

            if (muzzleTimer > DateTime.Now && muzzleflash.Opacity <= 1)
            {
                muzzleflash.Opacity += 0.2;
            }
            else if (muzzleTimer < DateTime.Now && muzzleflash.Opacity >= 0)
            {
                muzzleflash.Opacity -= 0.2;
            }
            pozice.Bottom += 117;
            if (smer) pozice.Left += 120;
            else pozice.Left -= 50;
            muzzleflash.Margin = pozice;

            aktualizujProjektily();
        }
    }
    class Postava_2 : Postava
    {
        //Příprava pro animace katany
        Image imgKatana = new Image();
        int katana_tick_animace = 0;
        int katana_animace_index = 0;
        List<BitmapImage> katana_animace_left = new List<BitmapImage>();
        List<BitmapImage> katana_animace_right = new List<BitmapImage>();
        public Postava_2(Grid plocha, Image postava, bool strana, Label detaily)
        {
            cooldownUtok1Max = 500;
            cooldownUtok2Max = 3000;

            this.maxRychlost = 30;

            imgPostava = postava;
            gridPlocha = plocha;
            this.detaily = detaily;

            VytvorDmgIndikator();

            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/0.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/1.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/2.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/3.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/4.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/crouch.png")));

            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/0.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/1.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/2.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/3.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/4.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/crouch.png")));

            katana_animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/katana_left/idle.png")));
            katana_animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/katana_left/0.png")));
            katana_animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/katana_left/1.png")));
            katana_animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/katana_left/2.png")));
            katana_animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/katana_left/3.png")));

            katana_animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/katana_right/idle.png")));
            katana_animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/katana_right/0.png")));
            katana_animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/katana_right/1.png")));
            katana_animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/katana_right/2.png")));
            katana_animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/katana_right/3.png")));

            if (smer) imgKatana.Source = katana_animace_left[0];
            else imgKatana.Source = katana_animace_right[0];
            imgKatana.Width = 320;
            imgKatana.Height = 440;
            gridPlocha.Children.Add(imgKatana);
        }

        public override void setSkrceni(bool hodnota)
        {
            skrceni = hodnota;
            if (!hodnota)
            {
                animace_index = 0;
                if (!smer) imgPostava.Source = animace_left[animace_index];
                else imgPostava.Source = animace_right[animace_index];
            }
        }

        public override void Tick()
        {
            AktualizujDmgIndikator();
            Thickness pozice = imgPostava.Margin;

            //Útok
            if (utok1 && DateTime.Now > cooldownUtok1)
            {
                katana_tick_animace = 1;
                cooldownUtok1 = DateTime.Now + TimeSpan.FromMilliseconds(cooldownUtok1Max);
            }
            if (katana_tick_animace > 0)
            {
                switch (katana_tick_animace)
                {
                    case 1: katana_animace_index = 1; break;
                    case 3: katana_animace_index = 2; break;
                    case 5: katana_animace_index = 3; break;
                    case 7: katana_animace_index = 4; new Katana_Hit(getImg().Margin.Left, getImg().Margin.Bottom, getImg(), smer); break;
                    case 9: katana_animace_index = 3; break;
                    case 11: katana_animace_index = 2; break;
                    case 13: katana_animace_index = 1; break;
                }
                katana_tick_animace++;
                if (katana_tick_animace >= 15)
                {
                    katana_tick_animace = 0;
                    katana_animace_index = 0;
                }
            }
            if (utok2 && DateTime.Now > cooldownUtok2)
            {
                Tornado tornado = new Tornado(this, smer);
                gridPlocha.Children.Add(tornado.ReturnImage());
                aktivni_projektily.Add(tornado);
                cooldownUtok2 = DateTime.Now.AddMilliseconds(cooldownUtok2Max);
            }

            Pohyb(pozice);

            pozice = imgPostava.Margin;
            //Pohyb katany s tělem
            pozice.Bottom -= 100;
            imgKatana.HorizontalAlignment = HorizontalAlignment.Left;
            imgKatana.VerticalAlignment = VerticalAlignment.Bottom;
            if (!smer)
            {
                imgKatana.Source = katana_animace_left[katana_animace_index];
                Panel.SetZIndex(imgKatana, -1);
                pozice.Left -= 150;
            }
            else
            {
                imgKatana.Source = katana_animace_right[katana_animace_index];
                Panel.SetZIndex(imgKatana, 1);
                pozice.Left -= 50;
            }
            imgKatana.Margin = pozice;
            aktualizujProjektily();
        }
    }
}
