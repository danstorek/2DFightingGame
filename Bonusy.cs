using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace _2DFightingGame
{
    abstract class Bonus
    {
        public int id;
        public int platforma;
        protected Image ikona;
        public abstract void Sebrani();
        public bool sebrano = false;
        public Image getIkona()
        {
            return ikona;
        }
    }
    class HP : Bonus
    {
        public HP()
        {
            id = 0;
            platforma = Globalni.rnd.Next(0, Globalni.platformy.Count - 1);
            double cast = Globalni.rnd.Next(Convert.ToInt32(Globalni.platformy[platforma].Margin.Left), Convert.ToInt32(Globalni.platformy[platforma].Margin.Left + Globalni.platformy[platforma].Width)-100);

            ikona = new Image();
            ikona.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            ikona.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            ikona.Margin = new System.Windows.Thickness(cast, 0, 0, Globalni.platformy[platforma].Margin.Bottom+50);
            ikona.Width = 100;
            ikona.Height = 100;
            ikona.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/bonuses/hp.png"));
        }
        public override void Sebrani()
        {
            if(Globalni.hrac1.getImg().Margin.Left + 100 > ikona.Margin.Left && Globalni.hrac1.getImg().Margin.Left < ikona.Margin.Left + 100 && Globalni.hrac1.getImg().Margin.Bottom > ikona.Margin.Bottom-25 && Globalni.hrac1.getImg().Margin.Bottom < ikona.Margin.Bottom + ikona.Height)
            {
                Globalni.hrac1.Regen(50);
                sebrano = true;
            }
            else if (Globalni.hrac2.getImg().Margin.Left + 100 > ikona.Margin.Left && Globalni.hrac2.getImg().Margin.Left < ikona.Margin.Left + 100 && Globalni.hrac2.getImg().Margin.Bottom > ikona.Margin.Bottom - 25 && Globalni.hrac2.getImg().Margin.Bottom < ikona.Margin.Bottom + ikona.Height)
            {
                Globalni.hrac2.Regen(50);
                sebrano = true;
            }
        }
    }
    class Sila : Bonus
    {
        public Sila()
        {
            id = 1;
            platforma = Globalni.rnd.Next(0, Globalni.platformy.Count - 1);
            double cast = Globalni.rnd.Next(Convert.ToInt32(Globalni.platformy[platforma].Margin.Left), Convert.ToInt32(Globalni.platformy[platforma].Margin.Left + Globalni.platformy[platforma].Width) - 100);

            ikona = new Image();
            ikona.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            ikona.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            ikona.Margin = new System.Windows.Thickness(cast, 0, 0, Globalni.platformy[platforma].Margin.Bottom + 50);
            ikona.Width = 100;
            ikona.Height = 100;
            ikona.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/bonuses/sila.png"));
        }
        public override void Sebrani()
        {
            if (Globalni.hrac1.getImg().Margin.Left + 100 > ikona.Margin.Left && Globalni.hrac1.getImg().Margin.Left < ikona.Margin.Left + 100 && Globalni.hrac1.getImg().Margin.Bottom > ikona.Margin.Bottom - 25 && Globalni.hrac1.getImg().Margin.Bottom < ikona.Margin.Bottom + ikona.Height)
            {
                Globalni.hrac1.sebratSila();
                sebrano = true;
            }
            else if (Globalni.hrac2.getImg().Margin.Left + 100 > ikona.Margin.Left && Globalni.hrac2.getImg().Margin.Left < ikona.Margin.Left + 100 && Globalni.hrac2.getImg().Margin.Bottom > ikona.Margin.Bottom - 25 && Globalni.hrac2.getImg().Margin.Bottom < ikona.Margin.Bottom + ikona.Height)
            {
                Globalni.hrac2.sebratSila();
                sebrano = true;
            }
        }
    }
    class Rychlost : Bonus
    {
        public Rychlost()
        {
            id = 2;
            platforma = Globalni.rnd.Next(0, Globalni.platformy.Count - 1);
            double cast = Globalni.rnd.Next(Convert.ToInt32(Globalni.platformy[platforma].Margin.Left), Convert.ToInt32(Globalni.platformy[platforma].Margin.Left + Globalni.platformy[platforma].Width) - 100);

            ikona = new Image();
            ikona.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            ikona.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            ikona.Margin = new System.Windows.Thickness(cast, 0, 0, Globalni.platformy[platforma].Margin.Bottom + 50);
            ikona.Width = 100;
            ikona.Height = 100;
            ikona.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/bonuses/rychlost.png"));
        }
        public override void Sebrani()
        {
            if (Globalni.hrac1.getImg().Margin.Left + 100 > ikona.Margin.Left && Globalni.hrac1.getImg().Margin.Left < ikona.Margin.Left + 100 && Globalni.hrac1.getImg().Margin.Bottom > ikona.Margin.Bottom - 25 && Globalni.hrac1.getImg().Margin.Bottom < ikona.Margin.Bottom + ikona.Height)
            {
                Globalni.hrac1.skore += 5;
                Globalni.hrac1.sebratRychlost();
                sebrano = true;
            }
            else if (Globalni.hrac2.getImg().Margin.Left + 100 > ikona.Margin.Left && Globalni.hrac2.getImg().Margin.Left < ikona.Margin.Left + 100 && Globalni.hrac2.getImg().Margin.Bottom > ikona.Margin.Bottom - 25 && Globalni.hrac2.getImg().Margin.Bottom < ikona.Margin.Bottom + ikona.Height)
            {
                Globalni.hrac2.skore += 5;
                Globalni.hrac2.sebratRychlost();
                sebrano = true;
            }
        }
    }
}