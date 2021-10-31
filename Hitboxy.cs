using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _2DFightingGame
{
    static class Hitboxy
    {
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
                return  hrac2;
            }
            else
            {
                return hrac1;
            }
        }

        public static List<Image> platformy;

        public static int MuzePadat(Postava hrac)
        {
            int tmp = 0;
            Thickness pozice = hrac.getImg().Margin;
            if (pozice.Bottom < 190) return 210;
            foreach (Image platforma in platformy)
            {
                if (pozice.Left+(hrac.getImg().Height/2) > platforma.Margin.Left && pozice.Left < platforma.Margin.Left + platforma.Width)
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
            Thickness pozice = tnt.getImg().Margin;
            if (pozice.Bottom < 190) return 210;
            foreach (Image platforma in platformy)
            {
                if (pozice.Left + (tnt.getImg().Height / 2) > platforma.Margin.Left && pozice.Left < platforma.Margin.Left + platforma.Width)
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
