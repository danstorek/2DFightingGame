using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace _2DFightingGame
{
    abstract class Postava
    {
        protected List<Updatable> aktivni_projektily = new List<Updatable>();


        protected Image imgPostava;
        protected Grid gridPlocha;
        protected bool vlevo = false;
        protected bool vpravo = false;
        protected bool smer = true;
        protected bool skokTrigger = false;
        protected bool veVzduchu = false;
        protected bool skok = false;
        protected bool utok1 = false;
        protected bool utok2 = false;
        protected DateTime cooldown = DateTime.Now;

        protected int pohybX = 0;
        public abstract void Tick();
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
        //Aktualizace aktivních projektilů ve hře
        public void aktualizujProjektily()
        {
            foreach (Updatable i in aktivni_projektily)
            {
                if (i.getAktivni()) i.Tick();
                else gridPlocha.Children.Remove(i.ReturnImage());
            }
        }

    }

    class Postava_1 : Postava
    {
        public Postava_1(Grid plocha, Image postava, bool strana)
        {
            imgPostava = postava;
            gridPlocha = plocha;

        }
        public override void Tick()
        {
            Thickness pozice = imgPostava.Margin;
            //Útok 1 - hráč 1
            if (utok1)
            {
                if (DateTime.Now > cooldown)
                {
                    Fireball fireball = new Fireball(imgPostava, smer);
                    gridPlocha.Children.Add(fireball.ReturnImage());
                    aktivni_projektily.Add(fireball);
                    cooldown = DateTime.Now.AddMilliseconds(fireball.cooldown);
                }

            }

            //Útok 2 - hráč 1
            if (utok2)
            {
                if (DateTime.Now > cooldown)
                {
                    TNT sw = new TNT(imgPostava);
                    gridPlocha.Children.Add(sw.ReturnImage());
                    aktivni_projektily.Add(sw);
                    cooldown = DateTime.Now.AddMilliseconds(sw.cooldown);
                }
            }
            //Pohyb - hráč 1
            if (vpravo)
            {
                if (pohybX < 30) pohybX += 3;
                imgPostava.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/1.png"));
                smer = true;
            }
            else if (vlevo)
            {
                if (pohybX > -30) pohybX -= 3;
                imgPostava.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/1.png"));
                smer = false;
            }
            else
            {
                if (pohybX > 0) pohybX -= 3;
                if (pohybX < 0) pohybX += 3;
            }

            if (pohybX > 0)
            {
                if (pozice.Left + pohybX < 1800) pozice.Left += pohybX;
            }
            else
            {
                if (pozice.Left + pohybX > -70) pozice.Left += pohybX;
            }

            //Skok - hráč 1
            if (skokTrigger)
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
                if (pozice.Bottom >= 500) skok = false;
            }

            //Gravitace - hráč 1
            if (veVzduchu)
            {
                if (!skok)
                {
                    pozice.Bottom -= 25;
                    if (pozice.Bottom < 211)
                    {
                        pozice.Bottom = 211;
                        veVzduchu = false;
                    }
                }
            }

            imgPostava.Margin = pozice;
            aktualizujProjektily();
        }
    }
}
