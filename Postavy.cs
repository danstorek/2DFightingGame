using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public int id;

        protected string jmeno;

        public List<Aktualizovatelne> aktivni_projektily = new List<Aktualizovatelne>();
        public void smazatProjektily()
        {
            foreach (Aktualizovatelne i in aktivni_projektily)
            {
                gridPlocha.Children.Remove(i.ReturnImage());
                i.Neaktivni();
            }
            aktivni_projektily.Clear();
        }

        protected Image imgPostava;
        protected Image imgDamage = new Image();
        protected Image imgHPRegen = new Image();
        protected Image imgDamageBoost = new Image();
        protected Image imgSpeedBoost = new Image();
        protected Grid gridPlocha;
        protected double hp = 100;
        protected double energie = 100;
        protected bool vlevo = false;
        protected bool vpravo = false;
        protected bool skrceni = false;

        //True - Doprava
        //False - Doleva
        protected bool smer = true;
        protected bool skokTrigger = false;
        protected bool veVzduchu = false;
        protected bool skok = false;
        protected int skokPocet = 0;
        protected bool utok1 = false;
        protected bool utok2 = false;
        protected int zakladniRychlost = 30;
        public int maxRychlost = 30;
        protected DateTime cooldownUtok1 = DateTime.Now;
        protected DateTime cooldownUtok2 = DateTime.Now;
        protected int cooldownUtok1Max;
        protected int cooldownUtok2Max;
        protected int poskozeniTimer = 0;
        protected int regenTimer = 0;
        protected DateTime zmrazen = DateTime.Now;
        public int redukcePoskozeni = 0;

        protected Stopwatch silaTimer = new Stopwatch();
        protected Stopwatch rychlostTimer = new Stopwatch();

        protected int pohybX = 0;
        public abstract void Tick();
        protected List<BitmapImage> animace_left = new List<BitmapImage>();
        protected List<BitmapImage> animace_right = new List<BitmapImage>();
        protected int animace_index = 0;
        protected double tick_animace = 0;
        protected bool utoceni1 = false;
        public bool pohybSchopnost = false;
        public bool zamknoutOvladani = false;
        protected List<BitmapImage> animaceUtoceni1_left = new List<BitmapImage>();
        protected List<BitmapImage> animaceUtoceni1_right = new List<BitmapImage>();

        protected List<BitmapImage> animaceUmirani_left = new List<BitmapImage>();
        protected List<BitmapImage> animaceUmirani_right = new List<BitmapImage>();

        public void vymazCooldown()
        {
            cooldownUtok2 = DateTime.Now;
        }

        //Animace umření po konci kola
        public void Umirani(double tick)
        {
            int index = Convert.ToInt32(Convert.ToDouble(animaceUmirani_left.Count) / 40 * tick);
            if(index < animaceUmirani_left.Count-1)
            {
                if (!smer) imgPostava.Source = animaceUmirani_left[index];
                else imgPostava.Source = animaceUmirani_right[index];
            }
            else
            {
                if (!smer) imgPostava.Source = animaceUmirani_left[animaceUmirani_left.Count-1];
                else imgPostava.Source = animaceUmirani_right[animaceUmirani_left.Count-1];
            }
        }

        //Statistiky
        public int uspesne = 0;
        public int celkem = 0;
        public int skore = 0;

        //Aktivace bonusů
        public void clearBonusy()
        {
            silaTimer.Reset();
            rychlostTimer.Reset();
        }
        protected void checkBonusy()
        {
            if (silaTimer.ElapsedMilliseconds > 5000)
            {
                silaTimer.Reset();
            }
            if (rychlostTimer.ElapsedMilliseconds > 5000)
            {
                rychlostTimer.Reset();
            }
        }
        public void sebratSila()
        {
            silaTimer.Restart();
        }
        public void sebratRychlost()
        {
            rychlostTimer.Restart();
        }
        public bool getSila()
        {
            if (silaTimer.IsRunning) return true;
            return false;
        }
        public bool getRychlost()
        {
            if (rychlostTimer.IsRunning) return true;
            return false;
        }
        public void setSmer(bool smer)
        {
            this.smer = smer;
        }
        public bool getSmer()
        {
            return smer;
        }

        protected void Pohyb(Thickness pozice)
        {
            if (!pohybSchopnost)
            {
                if (getRychlost()) this.maxRychlost = Convert.ToInt32(this.zakladniRychlost * 1.5);
                else this.maxRychlost = this.zakladniRychlost;
            }

            //Pohyb - hráč 1
            if (vpravo)
            {
                if (pohybX < maxRychlost && !skrceni) pohybX += maxRychlost / 10;
                smer = true;
            }
            else if (vlevo)
            {
                if (pohybX > (0 - maxRychlost) && !skrceni) pohybX -= maxRychlost / 10;
                smer = false;
            }
            else
            {
                if (pohybX < 3 && pohybX > -3) pohybX = 0;
                if (pohybX > 0) pohybX -= 3;
                if (pohybX < 0) pohybX += 3;
            }

            //Animace
            if (utoceni1)
            {
                tick_animace += Convert.ToDouble(animaceUtoceni1_left.Count) / 30;
                if (tick_animace >= animaceUtoceni1_left.Count - 0.49)
                {
                    tick_animace = 0;
                    utoceni1 = false;
                }
                animace_index = Convert.ToInt32(tick_animace);
            }
            else if (pohybX != 0)
            {
                tick_animace += Convert.ToDouble(animace_left.Count) / 30;
                if (tick_animace >= animace_left.Count - 0.49) tick_animace = 1;
                animace_index = Convert.ToInt32(tick_animace);
            }
            else
            {
                animace_index = 0;
                if (!smer) imgPostava.Source = animace_left[animace_index];
                else imgPostava.Source = animace_right[animace_index];
            }

            if(smer)
            {
                if(!utoceni1) imgPostava.Source = animace_right[animace_index];
                else imgPostava.Source = animaceUtoceni1_right[animace_index];
            }
            else
            {
                if(!utoceni1) imgPostava.Source = animace_left[animace_index];
                else imgPostava.Source = animaceUtoceni1_left[animace_index];
            }

            if (!getZmrazen())
            {
                //Kontrola kolize
                if (pohybX > 0)
                {
                    if (pozice.Left + pohybX < 1800 && (pozice.Left + pohybX < Globalni.getSouper(this).getImg().Margin.Left - 100 || pozice.Left > Globalni.getSouper(this).getImg().Margin.Left || pozice.Bottom < Globalni.getSouper(this).getImg().Margin.Bottom - 50 || pozice.Bottom > Globalni.getSouper(this).getImg().Margin.Bottom + 50)) pozice.Left += pohybX;
                }
                else
                {
                    if (pozice.Left + pohybX > -70 && (pozice.Left + pohybX + getImg().Width > Globalni.getSouper(this).getImg().Margin.Left + Globalni.getSouper(this).getImg().Width + 100 || pozice.Left + getImg().Width < Globalni.getSouper(this).getImg().Margin.Left + Globalni.getSouper(this).getImg().Width || pozice.Bottom < Globalni.getSouper(this).getImg().Margin.Bottom - 50 || pozice.Bottom > Globalni.getSouper(this).getImg().Margin.Bottom + 50)) pozice.Left += pohybX;
                }
            }

            //Při kolizi
            if(pozice.Left > Globalni.getSouper(this).getImg().Margin.Left - 100 && pozice.Left < Globalni.getSouper(this).getImg().Margin.Left && pozice.Bottom == Globalni.getSouper(this).getImg().Margin.Bottom)
            {
                pozice.Left -= 10;
            }
            if (pozice.Left + getImg().Width < Globalni.getSouper(this).getImg().Margin.Left + Globalni.getSouper(this).getImg().Width + 100 && pozice.Left + getImg().Width > Globalni.getSouper(this).getImg().Margin.Left + Globalni.getSouper(this).getImg().Width && pozice.Bottom == Globalni.getSouper(this).getImg().Margin.Bottom)
            {
                pozice.Left += 10;
            }

            //Skrčení
            if (!veVzduchu && Globalni.MuzePadat(this) != Globalni.platformy[Globalni.platformy.Count - 2].Margin.Bottom + 54 && Globalni.MuzePadat(this) != 1 && skrceni && !getZmrazen())
            {
                pozice.Bottom -= 25;
            }
            else if (skrceni)
            {
                skrceni = false;
            }


            //Skok - hráč 1
            if (skokTrigger && !skrceni && !getZmrazen())
            {
                if (!veVzduchu && Globalni.MuzePadat(this) != 1)
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
            if (veVzduchu || Globalni.MuzePadat(this) >= 1)
            {
                if (!skok)
                {
                    int pad = Globalni.MuzePadat(this);
                    if (pad == 1) pozice.Bottom -= 25;
                    else if (!skrceni)
                    {
                        pozice.Bottom = pad;
                        veVzduchu = false;
                    }
                }
            }
            if (pozice.Bottom < -40 && pozice.Bottom > -70) this.Poskozeni(999);
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
        public double getHP()
        {
            return hp;
        }
        public void setHP(double hp)
        {
            this.hp = hp;
        }
        public double getEnergie()
        {
            return energie;
        }
        public void setEnergie(double energie)
        {
            this.energie = energie;
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

        public bool getVeVzduchu()
        {
            return veVzduchu;
        }
        //Odražení protivníkem
        public void Odrazeni(int sila)
        {
            pohybX += sila;
        }

        //Poškození protivníkem
        public void Poskozeni(int rozdil)
        {
            rozdil *= 100-redukcePoskozeni;
            rozdil /= 100;

            //Vez
            if (this == Globalni.hrac1 && Globalni.vez != -1)
            {
                switch (Globalni.obtiznost)
                {
                    case 0: rozdil *= 50; rozdil /= 100; break;
                    case 1: rozdil *= 90; rozdil /= 100; break;
                    case 2: rozdil *= 120; rozdil /= 100; break; 
                }
                Globalni.obdrzeneCelkem += rozdil;
            }

            hp -= rozdil;
            poskozeniTimer = 10;
        }

        //Regenerace HP
        public void Regen(int rozdil)
        {
            hp += rozdil;
            if (hp > 100) hp = 100;
            regenTimer = 10;
        }
        //Aktualizace aktivních projektilů ve hře
        public void aktualizujProjektily()
        {
            foreach (Aktualizovatelne i in aktivni_projektily)
            {
                if (i.getAktivni()) i.Tick();
                else gridPlocha.Children.Remove(i.ReturnImage());
            }
        }
        protected void VytvorIndikatory()
        {
            imgDamage.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/chars/damage.png"));
            imgDamage.Opacity = 0;
            imgDamage.Width = 120;
            imgDamage.Height = 185;
            imgDamage.HorizontalAlignment = HorizontalAlignment.Left;
            imgDamage.VerticalAlignment = VerticalAlignment.Bottom;
            Panel.SetZIndex(imgDamage, 2);
            gridPlocha.Children.Add(imgDamage);

            imgHPRegen.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/chars/hpregen.png"));
            imgHPRegen.Opacity = 0;
            imgHPRegen.Width = 120;
            imgHPRegen.Height = 185;
            imgHPRegen.HorizontalAlignment = HorizontalAlignment.Left;
            imgHPRegen.VerticalAlignment = VerticalAlignment.Bottom;
            Panel.SetZIndex(imgHPRegen, 2);
            gridPlocha.Children.Add(imgHPRegen);

            imgSpeedBoost.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/chars/speedboost.png"));
            imgSpeedBoost.Opacity = 0;
            imgSpeedBoost.Width = 120;
            imgSpeedBoost.Height = 185;
            imgSpeedBoost.HorizontalAlignment = HorizontalAlignment.Left;
            imgSpeedBoost.VerticalAlignment = VerticalAlignment.Bottom;
            Panel.SetZIndex(imgSpeedBoost, 2);
            gridPlocha.Children.Add(imgSpeedBoost);

            imgDamageBoost.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/chars/damageboost.png"));
            imgDamageBoost.Opacity = 0;
            imgDamageBoost.Width = 120;
            imgDamageBoost.Height = 185;
            imgDamageBoost.HorizontalAlignment = HorizontalAlignment.Left;
            imgDamageBoost.VerticalAlignment = VerticalAlignment.Bottom;
            Panel.SetZIndex(imgDamageBoost, 2);
            gridPlocha.Children.Add(imgDamageBoost);
        }
        protected void AktualizujIndikatory()
        {
            //Vyladění pozic
            Thickness tmp = imgPostava.Margin;
            if (id == 0 || id == 1 || id == 2)
            {
                tmp = imgPostava.Margin;
                if (!smer) tmp.Left += 30;
            }
            else if (id == 3 || id == 4)
            {
                tmp = imgPostava.Margin;
                if (!smer) tmp.Left += 60;
            }


            imgDamage.Margin = tmp;
            if (poskozeniTimer > 0 && imgDamage.Opacity < 1)
            {
                imgDamage.Opacity += 0.1;
                poskozeniTimer--;
            }
            else if (imgDamage.Opacity > 0) imgDamage.Opacity -= 0.1;

            imgHPRegen.Margin = tmp;
            if (regenTimer > 0 && imgHPRegen.Opacity < 1)
            {
                imgHPRegen.Opacity += 0.1;
                regenTimer--;
            }
            else if (imgHPRegen.Opacity > 0) imgHPRegen.Opacity -= 0.1;

            //Bonusy
            imgDamageBoost.Margin = tmp;
            if (getSila() && imgDamageBoost.Opacity < 1) imgDamageBoost.Opacity += 0.1;
            else if (!getSila() && imgDamageBoost.Opacity > 0) imgDamageBoost.Opacity -= 0.1;

            imgSpeedBoost.Margin = tmp;
            if (getRychlost() && imgSpeedBoost.Opacity < 1) imgSpeedBoost.Opacity += 0.1;
            else if (!getRychlost() && imgSpeedBoost.Opacity > 0) imgSpeedBoost.Opacity -= 0.1;
        }
    }

    class Postava_1 : Postava
    {
        bool zautoceno = false;
        public Postava_1(Grid plocha, Image postava, bool strana)
        {
            cooldownUtok1Max = 500;
            cooldownUtok2Max = 1200;

            this.maxRychlost = 20;
            this.zakladniRychlost = 20;

            id = 0;
            imgPostava = postava;
            gridPlocha = plocha;

            VytvorIndikatory();

            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/00.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/0.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/1.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/2.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/3.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/4.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/5.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/6.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/7.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/8.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/9.png")));

            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/00.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/0.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/1.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/2.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/3.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/4.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/5.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/6.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/7.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/8.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/9.png")));

            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/attack/0.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/attack/1.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/attack/2.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/attack/3.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/attack/4.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/attack/5.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/attack/7.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/attack/8.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/attack/9.png")));

            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/attack/0.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/attack/1.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/attack/2.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/attack/3.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/attack/4.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/attack/5.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/attack/7.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/attack/8.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/attack/9.png")));

            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/die/0.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/die/1.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/die/2.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/die/3.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/die/4.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/die/5.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/die/7.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/die/8.png")));

            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/die/0.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/die/1.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/die/2.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/die/3.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/die/4.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/die/5.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/die/7.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/die/8.png")));

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
            checkBonusy();
            AktualizujIndikatory();
            Thickness pozice = imgPostava.Margin;
            //Útok 1 - hráč 1
            if (utok1 && DateTime.Now > cooldownUtok1 && energie >= 15)
            {
                tick_animace = 0;
                utoceni1 = true;
                zautoceno = false;
                energie -= 20;
                cooldownUtok1 = DateTime.Now.AddMilliseconds(cooldownUtok1Max);
            }
            if (utoceni1 && tick_animace > 4 && !zautoceno)
            {
                Sip sip = new Sip(this);
                gridPlocha.Children.Add(sip.ReturnImage());
                aktivni_projektily.Add(sip);
                zautoceno = true;
            }

            //Útok 2 - hráč 1
            if (utok2 && DateTime.Now > cooldownUtok2 && energie >= 25)
            {
                energie -= 25;
                TNT sw = new TNT(this);
                gridPlocha.Children.Add(sw.ReturnImage());
                aktivni_projektily.Add(sw);
                cooldownUtok2 = DateTime.Now.AddMilliseconds(cooldownUtok2Max);
            }

            Pohyb(pozice);

            aktualizujProjektily();
        }
    }
    class Postava_2 : Postava
    {
        bool zautoceno = false;
        public Postava_2(Grid plocha, Image postava, bool strana)
        {
            cooldownUtok1Max = 500;
            cooldownUtok2Max = 3000;

            this.maxRychlost = 30;
            this.zakladniRychlost = 30;

            id = 1;
            imgPostava = postava;
            gridPlocha = plocha;

            VytvorIndikatory();

            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/00.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/0.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/1.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/2.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/3.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/4.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/5.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/6.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/7.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/8.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/9.png")));

            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/00.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/0.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/1.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/2.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/3.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/4.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/5.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/6.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/7.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/8.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/9.png")));

            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/attack/0.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/attack/1.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/attack/2.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/attack/3.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/attack/4.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/attack/5.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/attack/7.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/attack/8.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/attack/9.png")));

            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/attack/0.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/attack/1.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/attack/2.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/attack/3.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/attack/4.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/attack/5.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/attack/7.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/attack/8.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/attack/9.png")));

            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/die/0.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/die/1.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/die/2.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/die/3.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/die/4.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/die/5.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/die/7.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/die/8.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/left/die/9.png")));

            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/die/0.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/die/1.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/die/2.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/die/3.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/die/4.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/die/5.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/die/7.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/die/8.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char2/right/die/9.png")));
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
            checkBonusy();
            AktualizujIndikatory();
            Thickness pozice = imgPostava.Margin;

            //Útok
            if (utok1 && DateTime.Now > cooldownUtok1 && energie >= 20)
            {
                tick_animace = 0;
                utoceni1 = true;
                zautoceno = false;
                energie -= 20;
                cooldownUtok1 = DateTime.Now + TimeSpan.FromMilliseconds(cooldownUtok1Max);
            }
            if (utoceni1 && tick_animace > 4 && !zautoceno)
            {
                new Katana_Hit(getImg().Margin.Left, getImg().Margin.Bottom, this, smer);
                zautoceno = true;
            }
            if (utok2 && DateTime.Now > cooldownUtok2 && energie >= 30)
            {
                energie -= 30;
                Tornado tornado = new Tornado(this, smer);
                gridPlocha.Children.Add(tornado.ReturnImage());
                aktivni_projektily.Add(tornado);
                cooldownUtok2 = DateTime.Now.AddMilliseconds(cooldownUtok2Max);
            }

            Pohyb(pozice);

            pozice = imgPostava.Margin;
            aktualizujProjektily();
        }
    }

    class Postava_3 : Postava
    {
        protected Image imgProtection = new Image();
        Stopwatch tmrObrana = new Stopwatch();
        public Postava_3(Grid plocha, Image postava, bool strana)
        {
            cooldownUtok1Max = 600;
            cooldownUtok2Max = 5000;

            this.maxRychlost = 25;
            this.zakladniRychlost = 25;

            id = 2;
            imgPostava = postava;
            gridPlocha = plocha;

            VytvorIndikatory();

            //Indikátor obrany
            imgProtection.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/chars/protection.png"));
            imgProtection.Opacity = 0;
            imgProtection.Width = 120;
            imgProtection.Height = 185;
            imgProtection.HorizontalAlignment = HorizontalAlignment.Left;
            imgProtection.VerticalAlignment = VerticalAlignment.Bottom;
            Panel.SetZIndex(imgProtection, 2);
            gridPlocha.Children.Add(imgProtection);

            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/00.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/0.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/1.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/2.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/3.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/4.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/5.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/6.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/7.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/8.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/9.png")));

            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/00.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/0.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/1.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/2.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/3.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/4.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/5.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/6.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/7.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/8.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/9.png")));

            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/attack/0.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/attack/1.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/attack/2.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/attack/3.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/attack/4.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/attack/5.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/attack/7.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/attack/8.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/attack/9.png")));

            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/attack/0.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/attack/1.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/attack/2.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/attack/3.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/attack/4.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/attack/5.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/attack/7.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/attack/8.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/attack/9.png")));

            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/die/0.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/die/1.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/die/2.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/die/3.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/die/4.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/die/5.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/die/7.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/left/die/8.png")));

            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/die/0.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/die/1.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/die/2.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/die/3.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/die/4.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/die/5.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/die/7.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char3/right/die/8.png")));

            if (strana) imgPostava.Source = animace_left[animace_index];
            else imgPostava.Source = animace_right[animace_index];
        }

        public override void setSkrceni(bool hodnota)
        {
            skrceni = hodnota;
        }
        public override void Tick()
        {
            checkBonusy();

            //Aktualizace indikátorů
            AktualizujIndikatory();
            Thickness tmp = imgPostava.Margin;
            tmp.Left += 30;
            imgProtection.Margin = tmp;
            if (tmrObrana.ElapsedMilliseconds > 2500) tmrObrana.Reset();
            if(tmrObrana.IsRunning)
            {
                if (imgProtection.Opacity < 1) imgProtection.Opacity += 0.1;
                redukcePoskozeni = 80;
            }
            else
            {
                if (imgProtection.Opacity > 0) imgProtection.Opacity -= 0.1;
                redukcePoskozeni = 0;
            }

            Thickness pozice = imgPostava.Margin;
            //Útok 1 - hráč 1
            if (utok1 && DateTime.Now > cooldownUtok1 && energie >= 20)
            {
                tick_animace = 0;
                utoceni1 = true;
                energie -= 20;
                MageStrela strela = new MageStrela(this);
                gridPlocha.Children.Add(strela.ReturnImage());
                aktivni_projektily.Add(strela);
                cooldownUtok1 = DateTime.Now.AddMilliseconds(cooldownUtok1Max);
            }

            //Útok 2 - hráč 1
            if (utok2 && DateTime.Now > cooldownUtok2 && energie >= 50)
            {
                tmrObrana.Start();
                energie -= 50;
                cooldownUtok2 = DateTime.Now.AddMilliseconds(cooldownUtok2Max);
            }

            Pohyb(pozice);

            aktualizujProjektily();
        }
    }
    class Postava_4 : Postava
    {
        bool zautoceno = false;
        Stopwatch utok2Timer = new Stopwatch();
        Image imgRage = new Image();
        public Postava_4(Grid plocha, Image postava, bool strana)
        {
            cooldownUtok1Max = 700;
            cooldownUtok2Max = 3000;

            this.maxRychlost = 25;
            this.zakladniRychlost = 25;

            id = 3;
            imgPostava = postava;
            gridPlocha = plocha;

            VytvorIndikatory();

            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/00.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/0.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/1.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/2.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/3.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/4.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/5.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/6.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/7.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/8.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/9.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/10.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/11.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/12.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/13.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/14.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/15.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/16.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/17.png")));

            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/00.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/0.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/1.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/2.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/3.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/4.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/5.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/6.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/7.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/8.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/9.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/10.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/11.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/12.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/13.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/14.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/15.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/16.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/17.png")));

            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/attack/0.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/attack/1.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/attack/2.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/attack/3.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/attack/4.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/attack/5.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/attack/7.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/attack/8.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/attack/9.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/attack/10.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/attack/11.png")));

            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/attack/0.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/attack/1.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/attack/2.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/attack/3.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/attack/4.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/attack/5.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/attack/7.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/attack/8.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/attack/9.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/attack/10.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/attack/11.png")));

            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/die/0.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/die/1.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/die/2.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/die/3.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/die/4.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/die/5.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/die/7.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/die/8.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/die/9.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/die/10.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/die/11.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/die/12.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/die/13.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/die/14.png")));

            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/die/0.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/die/1.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/die/2.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/die/3.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/die/4.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/die/5.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/die/7.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/die/8.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/die/9.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/die/10.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/die/11.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/die/12.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/die/13.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/die/14.png")));

            imgRage.Opacity = 0;
            imgRage.Width = 200;
            imgRage.Height = 185;
            imgRage.HorizontalAlignment = HorizontalAlignment.Left;
            imgRage.VerticalAlignment = VerticalAlignment.Bottom;
            Panel.SetZIndex(imgRage, 2);
            gridPlocha.Children.Add(imgRage);
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
            checkBonusy();
            AktualizujIndikatory();
            Thickness pozice = imgPostava.Margin;

            if (utok2Timer.IsRunning && imgRage.Opacity < 1) imgRage.Opacity += 0.2;
            else if (!utok2Timer.IsRunning && imgRage.Opacity > 0) imgRage.Opacity -= 0.2;

            if (smer)
            {
                imgRage.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/right/rage.png"));
            }
            else
            {
                imgRage.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char4/left/rage.png"));
            }
            imgRage.Margin = pozice;

            //Útok
            if (utok1 && DateTime.Now > cooldownUtok1 && energie >= 20)
            {
                tick_animace = 0;
                utoceni1 = true;
                zautoceno = false;
                energie -= 20;
                cooldownUtok1 = DateTime.Now + TimeSpan.FromMilliseconds(cooldownUtok1Max);
            }
            if (utoceni1 && tick_animace > 8 && !zautoceno)
            {
                new Katana_Hit(getImg().Margin.Left, getImg().Margin.Bottom, this, smer);
                zautoceno = true;
            }
            if (utok2 && DateTime.Now > cooldownUtok2 && energie >= 30)
            {
                pohybSchopnost = true;
                zamknoutOvladani = true;
                utok2Timer.Restart();
                energie -= 30;
                cooldownUtok2 = DateTime.Now.AddMilliseconds(cooldownUtok2Max);
            }
            if (utok2Timer.IsRunning)
            {
                maxRychlost = 65;
                bool kolize = false;
                if (smer && getImg().Margin.Left < Globalni.getSouper(this).getImg().Margin.Left)
                {
                    pohybX = 65;
                    if(Globalni.getSouper(this).getImg().Margin.Left - this.getImg().Margin.Left < 200 && Math.Abs(Globalni.getSouper(this).getImg().Margin.Bottom - this.getImg().Margin.Bottom) < 70)
                    {
                        kolize = true;
                    }
                }
                else if (!smer && getImg().Margin.Left > Globalni.getSouper(this).getImg().Margin.Left)
                {
                    pohybX = -65;
                    if (Globalni.getSouper(this).getImg().Margin.Left - this.getImg().Margin.Left > -200 && Math.Abs(Globalni.getSouper(this).getImg().Margin.Bottom - this.getImg().Margin.Bottom) < 70)
                    {
                        kolize = true;
                    }
                }
                if (utok2Timer.ElapsedMilliseconds > 400 || kolize)
                {
                    if (kolize)
                    {
                        pohybX = 0;
                        Globalni.getSouper(this).Poskozeni(30);
                        if(smer)Globalni.getSouper(this).Odrazeni(50);
                        else Globalni.getSouper(this).Odrazeni(-50);
                    }
                    pohybSchopnost = false;
                    zamknoutOvladani = false;
                    utok2Timer.Reset();
                }
            }

            Pohyb(pozice);

            pozice = imgPostava.Margin;
            aktualizujProjektily();
        }
    }
    class Postava_5 : Postava
    {
        bool zautoceno = false;
        Stopwatch utok2Timer = new Stopwatch();
        public Postava_5(Grid plocha, Image postava, bool strana)
        {
            cooldownUtok1Max = 700;
            cooldownUtok2Max = 3000;

            this.maxRychlost = 25;
            this.zakladniRychlost = 25;

            id = 4;
            imgPostava = postava;
            gridPlocha = plocha;

            VytvorIndikatory();

            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/00.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/0.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/1.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/2.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/3.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/4.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/5.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/6.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/7.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/8.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/9.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/10.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/11.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/12.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/13.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/14.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/15.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/16.png")));
            animace_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/17.png")));

            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/00.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/0.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/1.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/2.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/3.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/4.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/5.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/6.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/7.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/8.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/9.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/10.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/11.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/12.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/13.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/14.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/15.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/16.png")));
            animace_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/17.png")));

            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/attack/0.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/attack/1.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/attack/2.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/attack/3.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/attack/4.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/attack/5.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/attack/7.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/attack/8.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/attack/9.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/attack/10.png")));
            animaceUtoceni1_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/attack/11.png")));

            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/attack/0.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/attack/1.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/attack/2.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/attack/3.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/attack/4.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/attack/5.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/attack/7.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/attack/8.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/attack/9.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/attack/10.png")));
            animaceUtoceni1_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/attack/11.png")));

            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/die/0.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/die/1.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/die/2.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/die/3.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/die/4.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/die/5.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/die/7.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/die/8.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/die/9.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/die/10.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/die/11.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/die/12.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/die/13.png")));
            animaceUmirani_left.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/left/die/14.png")));

            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/die/0.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/die/1.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/die/2.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/die/3.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/die/4.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/die/5.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/die/7.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/die/8.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/die/9.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/die/10.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/die/11.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/die/12.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/die/13.png")));
            animaceUmirani_right.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/right/die/14.png")));
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
            checkBonusy();
            AktualizujIndikatory();
            Thickness pozice = imgPostava.Margin;

            //Útok
            if (utok1 && DateTime.Now > cooldownUtok1 && energie >= 20)
            {
                tick_animace = 0;
                utoceni1 = true;
                zautoceno = false;
                energie -= 20;
                cooldownUtok1 = DateTime.Now + TimeSpan.FromMilliseconds(cooldownUtok1Max);
            }
            if (utoceni1 && tick_animace > 8 && !zautoceno)
            {
                new Katana_Hit(getImg().Margin.Left, getImg().Margin.Bottom, this, smer);
                zautoceno = true;
            }
            if (utok2 && DateTime.Now > cooldownUtok2 && energie >= 30)
            {
                Hook hook = new Hook(this);
                gridPlocha.Children.Add(hook.ReturnImage());
                gridPlocha.Children.Add(hook.getCara());
                aktivni_projektily.Add(hook);
                energie -= 30;
                cooldownUtok2 = DateTime.Now.AddMilliseconds(cooldownUtok2Max);
            }

            Pohyb(pozice);

            pozice = imgPostava.Margin;
            aktualizujProjektily();
        }
    }
}