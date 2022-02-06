using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
        public void Neaktivni()
        {
            aktivni = false;
        }
        public bool getAktivni()
        {
            return aktivni;
        }
    }

    class Sip : Updatable
    {
        int rychlost;
        Image img = new Image();
        Image postava;
        Postava souper;
        Postava vyvolavaci;
        Image souperImg;
        bool smer;

        public Sip(Postava postava)
        {
            this.vyvolavaci = postava;
            this.smer = vyvolavaci.getSmer();
            this.postava = postava.getImg();
            this.souper = Hitboxy.getSouper(postava);
            this.souperImg = this.souper.getImg();

            vyvolavaci.celkem += 1;

            Thickness pozice = this.postava.Margin;
            if (smer)
            {
                pozice.Left += 100;
                pozice.Bottom += 45;
                img.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/attacks/fireball.png"));
            }
            else
            {
                pozice.Left -= 40;
                pozice.Bottom += 45;
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
            if (pozice.Left + (img.Width / 2) + 20 > poziceSouper.Left + (souperImg.Width / 2 - 40) && pozice.Left + (img.Width / 2) < poziceSouper.Left + (souperImg.Width / 2 + 40) && pozice.Bottom > poziceSouper.Bottom && pozice.Bottom < poziceSouper.Bottom + souperImg.Height - 20)
            {
                vyvolavaci.skore += 10;
                vyvolavaci.uspesne += 1;
                if(vyvolavaci.getSila()) souper.Poskozeni(24);
                else souper.Poskozeni(12);

                if (rychlost > 0)
                {
                    souper.Odrazeni(20);
                }
                else
                {
                    souper.Odrazeni(-20);
                }
                Neaktivni();
            }
        }

        public override Image ReturnImage()
        {
            return img;
        }
    }

    class Hook : Updatable
    {
        int rychlost;
        Image img = new Image();
        Image postava;
        Postava souper;
        Postava vyvolavaci;
        Image souperImg;
        bool smer;

        Line cara;

        public Hook(Postava postava)
        {
            this.vyvolavaci = postava;
            this.smer = vyvolavaci.getSmer();
            this.postava = postava.getImg();
            this.souper = Hitboxy.getSouper(postava);
            this.souperImg = this.souper.getImg();

            vyvolavaci.celkem += 1;

            cara = new Line();
            cara.StrokeThickness = 7;
            cara.Stroke = Brushes.Black;

            Thickness pozice = this.postava.Margin;
            if (smer)
            {
                pozice.Left += 100;
                pozice.Bottom += 45;
                img.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/hook.png"));
            }
            else
            {
                pozice.Left -= 40;
                pozice.Bottom += 45;
                img.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char5/hookleft.png"));
            }
            img.Width = 140;
            img.Height = 140;
            img.VerticalAlignment = VerticalAlignment.Bottom;
            img.HorizontalAlignment = HorizontalAlignment.Left;
            img.Margin = pozice;

            if (smer) rychlost = 60;
            else rychlost = -60;
        }

        public Line getCara()
        {
            return cara;
        }

        public override void Tick()
        {
            if (vyvolavaci.getSmer()) cara.X1 = postava.Margin.Left + 50;
            else cara.X1 = postava.Margin.Left + 100;


            cara.Y1 = 1080 - postava.Margin.Bottom - 100;

            if(rychlost > 0) cara.X2 = ReturnImage().Margin.Left + 90;
            else cara.X2 = ReturnImage().Margin.Left+40;

            cara.Y2 = 1080 - ReturnImage().Margin.Bottom - 40;

            Thickness pozice = img.Margin;
            pozice.Left += rychlost;
            img.Margin = pozice;

            //Označení výstřelu mimo obraz jako neaktivní
            if (pozice.Left < -100 || pozice.Left > 2100)
            {
                Neaktivni();
                cara.Visibility = Visibility.Hidden;
            }

            //Kolize se soupeřem
            Thickness poziceSouper = souperImg.Margin;
            if (pozice.Left + (img.Width / 2) + 20 > poziceSouper.Left + (souperImg.Width / 2 - 40) && pozice.Left + (img.Width / 2) < poziceSouper.Left + (souperImg.Width / 2 + 40) && pozice.Bottom > poziceSouper.Bottom && pozice.Bottom < poziceSouper.Bottom + souperImg.Height - 20)
            {
                vyvolavaci.skore += 10;
                vyvolavaci.uspesne += 1;
                if (vyvolavaci.getSila()) souper.Poskozeni(10);
                else souper.Poskozeni(5);

                if (rychlost > 0)
                {
                    souper.Odrazeni(-60);
                }
                else
                {
                    souper.Odrazeni(60);
                }
                Neaktivni();
                cara.Visibility = Visibility.Hidden;
            }
        }

        public override Image ReturnImage()
        {
            return img;
        }
    }

    class MageStrela : Updatable
    {
        int rychlost;
        Image img = new Image();
        Image postava;
        Postava souper;
        Postava vyvolavaci;
        Image souperImg;
        int tick = 0;
        bool smer;

        public MageStrela(Postava postava)
        {
            this.vyvolavaci = postava;
            this.postava = postava.getImg();
            this.souper = Hitboxy.getSouper(postava);
            this.souperImg = this.souper.getImg();

            vyvolavaci.celkem += 1;
        }

        public override void Tick()
        {
            if (tick == 20)
            {
                this.smer = vyvolavaci.getSmer();
                Thickness pozice = this.postava.Margin;
                if (smer)
                {
                    pozice.Left += 100;
                    pozice.Bottom += 75;
                }
                else
                {
                    pozice.Left -= 40;
                    pozice.Bottom += 75;
                }
                img.Width = 91;
                img.Height = 91;
                img.VerticalAlignment = VerticalAlignment.Bottom;
                img.HorizontalAlignment = HorizontalAlignment.Left;
                img.Margin = pozice;

                if (smer) rychlost = 40;
                else rychlost = -40;
                img.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/attacks/mageshot.png"));
            }
            if (tick > 20)
            {
                Thickness pozice = img.Margin;
                pozice.Left += rychlost;
                img.Margin = pozice;

                //Označení výstřelu mimo obraz jako neaktivní
                if (pozice.Left < -100 || pozice.Left > 2100) Neaktivni();

                //Kolize se soupeřem
                Thickness poziceSouper = souperImg.Margin;
                if (pozice.Left + (img.Width / 2) > poziceSouper.Left + (souperImg.Width / 2 - 40) && pozice.Left + (img.Width / 2) < poziceSouper.Left + (souperImg.Width / 2 + 40) && pozice.Bottom > poziceSouper.Bottom && pozice.Bottom < poziceSouper.Bottom + souperImg.Height - 20)
                {
                    vyvolavaci.skore += 20;
                    vyvolavaci.uspesne += 1;
                    if (vyvolavaci.getSila()) souper.Poskozeni(40);
                    else souper.Poskozeni(20);

                    if (rychlost > 0)
                    {
                        souper.Odrazeni(14);
                    }
                    else
                    {
                        souper.Odrazeni(-14);
                    }
                    Neaktivni();
                }
            }
            else tick++;

        }

        public override Image ReturnImage()
        {
            return img;
        }
    }

    class Tornado : Updatable
    {
        int rychlost;
        Image img = new Image();
        Image postava;
        Postava vyvolavaci;
        Postava souper;
        Image souperImg;
        bool zastaveno = false;

        public Tornado(Postava postava, bool smer)
        {
            this.vyvolavaci = postava;
            this.postava = postava.getImg();
            souper = Hitboxy.getSouper(postava);
            souperImg = souper.getImg();

            vyvolavaci.celkem += 1;

            Thickness pozice = this.postava.Margin;
            img.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/attacks/tornado.png"));
            Panel.SetZIndex(img, 3);
            if (smer)
            {
                pozice.Left += 0;
            }
            else
            {
                pozice.Left -= 0;
            }
            pozice.Bottom -= 50;
            img.Width = 200;
            img.Height = 300;
            img.VerticalAlignment = VerticalAlignment.Bottom;
            img.HorizontalAlignment = HorizontalAlignment.Left;
            img.Margin = pozice;

            if (smer) rychlost = 30;
            else rychlost = -30;
        }

        public override void Tick()
        {
            Thickness pozice = img.Margin;
            if (!zastaveno)
            {
                pozice.Left += rychlost;
                img.Margin = pozice;
            }

            //Označení výstřelu mimo obraz jako neaktivní
            if (pozice.Left < -100 || pozice.Left > 2100) Neaktivni();

            //Kolize se soupeřem
            Thickness poziceSouper = souperImg.Margin;
            if (!zastaveno && pozice.Left + (img.Width / 2) > poziceSouper.Left + (souperImg.Width / 2 - 40) && pozice.Left + (img.Width / 2) < poziceSouper.Left + (souperImg.Width / 2 + 40) && pozice.Bottom - 100 < poziceSouper.Bottom && pozice.Bottom + img.Height + 100 > poziceSouper.Bottom + souperImg.Height)
            {
                vyvolavaci.skore += 15;
                vyvolavaci.uspesne += 1;
                zastaveno = true;
                if(vyvolavaci.getSila()) souper.Poskozeni(20);
                else souper.Poskozeni(10);
                souper.setZmrazen(1500);
            }
            if (zastaveno && !souper.getZmrazen()) Neaktivni();
        }

        public override Image ReturnImage()
        {
            return img;
        }
    }

    class TNT : Updatable
    {
        Thickness pozice;

        Postava vyvolavaci;
        Image img = new Image();
        List<BitmapImage> animace = new List<BitmapImage>();
        int cisloFrame = 0;

        public Image getImg()
        {
            return img;
        }

        public TNT(Postava postava)
        {
            vyvolavaci = postava;

            vyvolavaci.celkem += 1;

            pozice = postava.getImg().Margin;
            img.Height = 200;
            img.Width = 200;
            img.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/attacks/tnt/1.png"));
            img.HorizontalAlignment = HorizontalAlignment.Left;
            img.VerticalAlignment = VerticalAlignment.Bottom;
            img.Margin = new Thickness(pozice.Left, pozice.Top, pozice.Right, pozice.Bottom);

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
                Thickness poziceTNT = img.Margin;
                //Gravitace
                if (Hitboxy.MuzePadat(this) >= 1)
                {
                    int pad = Hitboxy.MuzePadat(this);
                    if (pad == 1) poziceTNT.Bottom -= 25;
                    else
                    {
                        poziceTNT.Bottom = pad;
                    }
                }

                img.Margin = poziceTNT;
                switch (cisloFrame)
                {
                    case 0: img.Source = animace[0]; break;
                    case 15: img.Source = animace[1]; break;
                    case 30: img.Source = animace[2]; break;
                    case 45: img.Source = animace[3]; break;
                    case 60:
                        img.Source = animace[4];
                        if (pozice.Left > postava1Pozice.Left - 350 && pozice.Left < postava1Pozice.Left + 350 && poziceTNT.Bottom- 200 < postava1Pozice.Bottom && poziceTNT.Bottom + img.Height + 200 > postava1Pozice.Bottom + Hitboxy.hrac1.getImg().Height)
                        {
                            if(Hitboxy.hrac1 != vyvolavaci)
                            {
                                vyvolavaci.skore += 15;
                                vyvolavaci.uspesne += 1;
                            }

                            if(vyvolavaci.getSila()) Hitboxy.hrac1.Poskozeni(40);
                            else Hitboxy.hrac1.Poskozeni(20);
                            if (pozice.Left + 200 > postava1Pozice.Left + 120) Hitboxy.hrac1.Odrazeni(-45);
                            else Hitboxy.hrac1.Odrazeni(45);
                        }
                        if (pozice.Left > postava2Pozice.Left - 350 && pozice.Left < postava2Pozice.Left + 350 && poziceTNT.Bottom - 200 < postava2Pozice.Bottom && poziceTNT.Bottom + img.Height + 200 > postava2Pozice.Bottom + Hitboxy.hrac2.getImg().Height)
                        {
                            if (Hitboxy.hrac2 != vyvolavaci)
                            {
                                vyvolavaci.skore += 15;
                                vyvolavaci.uspesne += 1;
                            }

                            if (vyvolavaci.getSila()) Hitboxy.hrac2.Poskozeni(40);
                            else Hitboxy.hrac2.Poskozeni(20);
                            if (pozice.Left + 200 > postava2Pozice.Left + 120) Hitboxy.hrac2.Odrazeni(-45);
                            else Hitboxy.hrac2.Odrazeni(45);
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
    class Katana_Hit
    {
        double poziceX;
        double poziceY;

        Postava vyvolavaci;
        Postava souper;
        Image souperImg;

        public Katana_Hit(double souradniceX, double souradniceY, Postava postava, bool smer)
        {
            int rozdil;
            if (smer) rozdil = 150;
            else rozdil = -150;
            this.poziceX = souradniceX + rozdil;
            this.poziceY = souradniceY;

            this.vyvolavaci = postava;
            this.souper = Hitboxy.getSouper(postava);
            this.souperImg = souper.getImg();

            vyvolavaci.celkem += 1;

            Thickness poziceSouper = souperImg.Margin;
            if (poziceX > poziceSouper.Left - 50 && poziceX < poziceSouper.Left + 50 && poziceY < poziceSouper.Bottom + 100 && poziceY > poziceSouper.Bottom - 100)
            {
                vyvolavaci.uspesne += 1;
                vyvolavaci.skore += 20;
                if(vyvolavaci.getSila()) souper.Poskozeni(30);
                else souper.Poskozeni(15);
                if (vyvolavaci.getImg().Margin.Left < souper.getImg().Margin.Left) souper.Odrazeni(30);
                else souper.Odrazeni(-30);
            }
        }
    }
}
