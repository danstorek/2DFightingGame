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
            platforma = Hitboxy.rnd.Next(0, Hitboxy.platformy.Count - 1);
            double cast = Hitboxy.rnd.Next(Convert.ToInt32(Hitboxy.platformy[platforma].Margin.Left), Convert.ToInt32(Hitboxy.platformy[platforma].Margin.Left + Hitboxy.platformy[platforma].Width)-100);

            ikona = new Image();
            ikona.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            ikona.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            ikona.Margin = new System.Windows.Thickness(cast, 0, 0, Hitboxy.platformy[platforma].Margin.Bottom+50);
            ikona.Width = 100;
            ikona.Height = 100;
            ikona.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/bonuses/hp.png"));
        }
        public override void Sebrani()
        {
            if(Hitboxy.hrac1.getImg().Margin.Left + 100 > ikona.Margin.Left && Hitboxy.hrac1.getImg().Margin.Left < ikona.Margin.Left + 100 && Hitboxy.hrac1.getImg().Margin.Bottom > ikona.Margin.Bottom-25 && Hitboxy.hrac1.getImg().Margin.Bottom < ikona.Margin.Bottom + ikona.Height)
            {
                Hitboxy.hrac1.Regen(50);
                sebrano = true;
            }
            else if (Hitboxy.hrac2.getImg().Margin.Left + 100 > ikona.Margin.Left && Hitboxy.hrac2.getImg().Margin.Left < ikona.Margin.Left + 100 && Hitboxy.hrac2.getImg().Margin.Bottom > ikona.Margin.Bottom - 25 && Hitboxy.hrac2.getImg().Margin.Bottom < ikona.Margin.Bottom + ikona.Height)
            {
                Hitboxy.hrac2.Regen(50);
                sebrano = true;
            }
        }
    }
    class Sila : Bonus
    {
        public Sila()
        {
            id = 1;
            platforma = Hitboxy.rnd.Next(0, Hitboxy.platformy.Count - 1);
            double cast = Hitboxy.rnd.Next(Convert.ToInt32(Hitboxy.platformy[platforma].Margin.Left), Convert.ToInt32(Hitboxy.platformy[platforma].Margin.Left + Hitboxy.platformy[platforma].Width) - 100);

            ikona = new Image();
            ikona.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            ikona.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            ikona.Margin = new System.Windows.Thickness(cast, 0, 0, Hitboxy.platformy[platforma].Margin.Bottom + 50);
            ikona.Width = 100;
            ikona.Height = 100;
            ikona.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/bonuses/sila.png"));
        }
        public override void Sebrani()
        {
            if (Hitboxy.hrac1.getImg().Margin.Left + 100 > ikona.Margin.Left && Hitboxy.hrac1.getImg().Margin.Left < ikona.Margin.Left + 100 && Hitboxy.hrac1.getImg().Margin.Bottom > ikona.Margin.Bottom - 25 && Hitboxy.hrac1.getImg().Margin.Bottom < ikona.Margin.Bottom + ikona.Height)
            {
                Hitboxy.hrac1.sebratSila();
                sebrano = true;
            }
            else if (Hitboxy.hrac2.getImg().Margin.Left + 100 > ikona.Margin.Left && Hitboxy.hrac2.getImg().Margin.Left < ikona.Margin.Left + 100 && Hitboxy.hrac2.getImg().Margin.Bottom > ikona.Margin.Bottom - 25 && Hitboxy.hrac2.getImg().Margin.Bottom < ikona.Margin.Bottom + ikona.Height)
            {
                Hitboxy.hrac2.sebratSila();
                sebrano = true;
            }
        }
    }
    class Rychlost : Bonus
    {
        public Rychlost()
        {
            id = 2;
            platforma = Hitboxy.rnd.Next(0, Hitboxy.platformy.Count - 1);
            double cast = Hitboxy.rnd.Next(Convert.ToInt32(Hitboxy.platformy[platforma].Margin.Left), Convert.ToInt32(Hitboxy.platformy[platforma].Margin.Left + Hitboxy.platformy[platforma].Width) - 100);

            ikona = new Image();
            ikona.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            ikona.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            ikona.Margin = new System.Windows.Thickness(cast, 0, 0, Hitboxy.platformy[platforma].Margin.Bottom + 50);
            ikona.Width = 100;
            ikona.Height = 100;
            ikona.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/bonuses/rychlost.png"));
        }
        public override void Sebrani()
        {
            if (Hitboxy.hrac1.getImg().Margin.Left + 100 > ikona.Margin.Left && Hitboxy.hrac1.getImg().Margin.Left < ikona.Margin.Left + 100 && Hitboxy.hrac1.getImg().Margin.Bottom > ikona.Margin.Bottom - 25 && Hitboxy.hrac1.getImg().Margin.Bottom < ikona.Margin.Bottom + ikona.Height)
            {
                Hitboxy.hrac1.skore += 5;
                Hitboxy.hrac1.sebratRychlost();
                sebrano = true;
            }
            else if (Hitboxy.hrac2.getImg().Margin.Left + 100 > ikona.Margin.Left && Hitboxy.hrac2.getImg().Margin.Left < ikona.Margin.Left + 100 && Hitboxy.hrac2.getImg().Margin.Bottom > ikona.Margin.Bottom - 25 && Hitboxy.hrac2.getImg().Margin.Bottom < ikona.Margin.Bottom + ikona.Height)
            {
                Hitboxy.hrac2.skore += 5;
                Hitboxy.hrac2.sebratRychlost();
                sebrano = true;
            }
        }
    }
}