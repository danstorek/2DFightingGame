using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace _2DFightingGame
{
    static class Globalni
    {
        public static void zmenitScale(Grid Okno)
        {
            ScaleTransform scale = new ScaleTransform();
            scale.ScaleX = Okno.ActualWidth / 1920;
            scale.ScaleY = Okno.ActualHeight / 1080;
            scale.CenterX = Okno.ActualWidth / 2;
            scale.CenterY = Okno.ActualHeight / 2;
            Okno.LayoutTransform = scale;
        }

        public static Random rnd = new Random();

        //true - Hra pro jednoho hráče
        //false - Hra pro dva hráče
        public static bool rezimHry = false;

        public static int aktivniKolo = 0;
        public static int[] kola = new int[3] { 0, 0, 0 };

        //Pouze pro věž
        public static int vez = -1;
        public static int[] vezMapy = new int[] {0,0,0,0,0};
        public static int obtiznost;

        public static int obdrzeneCelkem = 0;
        public static int skoreCelkem = 0;

        public static string hrac1Jmeno;
        public static string hrac2Jmeno;

        public static int hrac1Postava;
        public static int hrac2Postava;

        public static Postava hrac1;
        public static Postava hrac2;

        public static Postava getSouper(Postava postava)
        {
            if (postava == hrac1)
            {
                return hrac2;
            }
            else
            {
                return hrac1;
            }
        }

        public static List<Image> platformy;
        public static List<Image> platformyVez;
        public static BitmapImage pozadiMapa;
        public static List<Bonus> bonusy;

        //Ukládání
        public static Ukladani ukl = new Ukladani();
        public static int MuzePadat(Postava hrac)
        {
            int tmp = 0;
            Thickness pozice = hrac.getImg().Margin;
            if (pozice.Bottom < Convert.ToInt32(Globalni.platformy[Globalni.platformy.Count - 1].Margin.Bottom - 10)) return Convert.ToInt32(Globalni.platformy[Globalni.platformy.Count - 1].Margin.Bottom + 10);
            foreach (Image platforma in platformy)
            {
                if (pozice.Left + (hrac.getImg().Height / 2) > platforma.Margin.Left && pozice.Left < platforma.Margin.Left + platforma.Width)
                {
                    if (pozice.Bottom > platforma.Margin.Bottom + platforma.Height + 25)
                    {
                        tmp = 1;
                    }
                    else if (pozice.Bottom >= platforma.Margin.Bottom + platforma.Height)
                    {
                        tmp = Convert.ToInt32(platforma.Margin.Bottom + platforma.Height);
                    }
                    if (tmp != 0) break;
                }

            }
            return tmp;
        }
        public static int MuzePadat(TNT tnt)
        {
            int tmp = 0;
            Thickness pozice = tnt.ReturnImage().Margin;
            if (pozice.Bottom <= Globalni.platformy[Globalni.platformy.Count - 2].Margin.Bottom+54) return Convert.ToInt32(Globalni.platformy[Globalni.platformy.Count - 2].Margin.Bottom + 54);
            foreach (Image platforma in platformy)
            {
                if (pozice.Left + (tnt.ReturnImage().Height / 2) > platforma.Margin.Left && pozice.Left < platforma.Margin.Left + platforma.Width)
                {
                    if (pozice.Bottom > platforma.Margin.Bottom + platforma.Height + 25)
                    {
                        tmp = 1;
                    }
                    else if (pozice.Bottom >= platforma.Margin.Bottom + platforma.Height)
                    {
                        tmp = Convert.ToInt32(platforma.Margin.Bottom + platforma.Height);
                    }
                    if (tmp != 0) break;
                }

            }
            return tmp;
        }
    }
}