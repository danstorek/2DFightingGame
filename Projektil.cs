using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace _2DFightingGame
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    /// 

    abstract class Updatable
    {
        bool aktivni = true;
        public abstract void Tick();
        public abstract Image ReturnImage();
        protected void Neaktivni()
        {
            aktivni = false;
        }
        public bool getAktivni()
        {
            return aktivni;
        }
    }

    class Fireball : Updatable
    {
        int rychlost;
        Image img = new Image();
        Image postava;
        Postava souper;
        Image souperImg;

        public readonly int cooldown = 400;

        public Fireball(Image postava, bool smer)
        {
            this.postava = postava;
            if (postava == Hitboxy.hrac1.getImg())
            {
                souperImg = Hitboxy.hrac2.getImg();
                souper = Hitboxy.hrac2;
            }
            else
            {
                souperImg = Hitboxy.hrac1.getImg();
                souper = Hitboxy.hrac1;
            }

            Thickness pozice = postava.Margin;
            if (smer)
            {
                pozice.Left += 200;
                pozice.Bottom += 210;
                img.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/attacks/fireball.png"));
            }
            else
            {
                pozice.Left -= 80;
                pozice.Bottom += 210;
                img.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/attacks/fireballleft.png"));
            }
            img.Width = 91;
            img.Height = 91;
            img.VerticalAlignment = VerticalAlignment.Bottom;
            img.HorizontalAlignment = HorizontalAlignment.Left;
            img.Margin = pozice;

            if (smer) rychlost = 60;
            else rychlost = -60;
        }

        public override void Tick()
        {
            Thickness pozice = img.Margin;
            pozice.Left += rychlost;
            img.Margin = pozice;

            //Označení výstřelu mimo obraz jako neaktivní
            if (pozice.Left < -100 || pozice.Left > 2100) Neaktivni();

            //Kolize se soupeřem
            Thickness poziceSouper = souperImg.Margin;
            if (pozice.Left > poziceSouper.Left - 120 && pozice.Left < poziceSouper.Left + 120 && pozice.Bottom > poziceSouper.Bottom && pozice.Bottom < poziceSouper.Bottom + souperImg.Height-20)
            {
                souper.Poskozeni(10);
                Neaktivni();
            }
        }

        public override Image ReturnImage()
        {
            return img;
        }
    }

    class TNT : Updatable
    {
        Thickness pozice;

        public readonly int cooldown = 1200;

        Image img = new Image();
        List<BitmapImage> animace = new List<BitmapImage>();
        int cisloFrame = 0;

        public TNT(Image postava)
        {
            pozice = postava.Margin;
            img.Height = 371;
            img.Width = 398;
            img.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/attacks/tnt/1.png"));
            img.HorizontalAlignment = HorizontalAlignment.Left;
            img.VerticalAlignment = VerticalAlignment.Bottom;
            img.Margin = new Thickness(pozice.Left, pozice.Top, pozice.Right, 211);

            animace.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/attacks/tnt/1.png")));
            animace.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/attacks/tnt/10.png")));
            animace.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/attacks/tnt/20.png")));
            animace.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/attacks/tnt/30.png")));
            animace.Add(new BitmapImage(new Uri("pack://application:,,,/imgs/attacks/tnt/40.png")));
        }

        public override void Tick()
        {
            Thickness postava1Pozice = Hitboxy.hrac1.getImg().Margin;
            Thickness postava2Pozice = Hitboxy.hrac2.getImg().Margin;
            if (getAktivni())
            {
                switch (cisloFrame)
                {
                    case 0: img.Source = animace[0]; break;
                    case 15: img.Source = animace[1]; break;
                    case 30: img.Source = animace[2]; break;
                    case 45: img.Source = animace[3]; break;
                    case 60: img.Source = animace[4];
                        if (pozice.Left > postava1Pozice.Left - 350 && pozice.Left < postava1Pozice.Left + 350)
                        {
                            Hitboxy.hrac1.Poskozeni(20);
                        }
                        if (pozice.Left > postava2Pozice.Left - 350 && pozice.Left < postava2Pozice.Left + 350)
                        {
                            Hitboxy.hrac2.Poskozeni(20);
                        }
                        break;
                }

                if (cisloFrame > 120)
                {
                    Neaktivni();
                }
                cisloFrame++;
            }

        }

        public override Image ReturnImage()
        {
            return img;
        }
    }
}
