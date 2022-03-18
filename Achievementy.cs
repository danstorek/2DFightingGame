using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace _2DFightingGame
{
    static class Achievementy
    {
        public static int getSplneni(int id)
        {
            switch (id)
            {
                case 0: return 1;
                case 1: return 10;
                case 2: return 50;
                case 3: return 1;
                case 4: return 1;
                case 5: return 1;
                case 6: return 1;
                default: return 0;
            }
        }
        public static Grid achievementUkazatel(int id)
        {
            Grid plocha = new Grid();
            plocha.Width = 800;
            plocha.Height = 170;
            plocha.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            plocha.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

            string nazev = "";
            string popis = "";
            int splneni = getSplneni(id);
            int progressNyni = 0;

            Image obrazek = new Image();
            obrazek.Width = 170;
            obrazek.Height = 170;
            obrazek.Margin = new System.Windows.Thickness(0, 0, 0, 0);
            obrazek.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            obrazek.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

            switch (id)
            {
                case 0:
                    nazev = "Začátečník";
                    popis = "Odehraj 1 zápas v režimu pro jednoho hráče.\nOdemkne novou postavu: Válečník";
                    progressNyni = Globalni.ukl.ZiskatPrubeh(id);
                    obrazek.Source = new BitmapImage(new Uri(String.Format("pack://application:,,,/imgs/achievements/{0}.png", id)));
                    break;
                case 1:
                    nazev = "Amatér";
                    popis = "Odehraj 10 zápasů v režimu pro jednoho hráče.";
                    progressNyni = Globalni.ukl.ZiskatPrubeh(id);
                    obrazek.Source = new BitmapImage(new Uri(String.Format("pack://application:,,,/imgs/achievements/{0}.png", id)));
                    break;
                case 2:
                    nazev = "Profesionál";
                    popis = "Odehraj 50 zápasů v režimu pro jednoho hráče.";
                    progressNyni = Globalni.ukl.ZiskatPrubeh(id);
                    obrazek.Source = new BitmapImage(new Uri(String.Format("pack://application:,,,/imgs/achievements/{0}.png", id)));
                    break;
                case 3:
                    nazev = "Rychlostřelec";
                    popis = "Poraž soupeře v prvních 12 sekundách kola\nv režimu pro jednoho hráče.";
                    progressNyni = Globalni.ukl.ZiskatPrubeh(id);
                    obrazek.Source = new BitmapImage(new Uri(String.Format("pack://application:,,,/imgs/achievements/{0}.png", id)));
                    break;
                case 4:
                    nazev = "Ostrostřelec";
                    popis = "Vyhraj zápas s úspěšností střel větší než 75%\nv režimu pro jednoho hráče.";
                    progressNyni = Globalni.ukl.ZiskatPrubeh(id);
                    obrazek.Source = new BitmapImage(new Uri(String.Format("pack://application:,,,/imgs/achievements/{0}.png", id)));
                    break;
                case 5:
                    nazev = "Co je to za věž?";
                    popis = "Vyzkoušej si zahrát režim \"průchod věží.\"\nOdemkne novou postavu: Kouzelník";
                    progressNyni = Globalni.ukl.ZiskatPrubeh(id);
                    obrazek.Source = new BitmapImage(new Uri(String.Format("pack://application:,,,/imgs/achievements/{0}.png", id)));
                    break;
                case 6:
                    nazev = "Krásný pohled ze shora";
                    popis = "Projdi celou věž a poraž bosse.\nOdemkne novou postavu: Minotaur";
                    progressNyni = Globalni.ukl.ZiskatPrubeh(id);
                    obrazek.Source = new BitmapImage(new Uri(String.Format("pack://application:,,,/imgs/achievements/{0}.png", id)));
                    break;
            }
            plocha.Children.Add(obrazek);

            Label lbNazev = new Label();
            lbNazev.Content = nazev;
            lbNazev.FontSize = 30;
            lbNazev.Foreground = Brushes.White;
            lbNazev.FontWeight = System.Windows.FontWeights.Bold;
            lbNazev.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
            lbNazev.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
            lbNazev.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            lbNazev.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            lbNazev.Margin = new System.Windows.Thickness(180, 20, 0, 0);
            plocha.Children.Add(lbNazev);

            Label lbPopis = new Label();
            lbPopis.Content = popis;
            lbPopis.FontSize = 20;
            lbPopis.Foreground = Brushes.White;
            lbPopis.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
            lbPopis.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
            lbPopis.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            lbPopis.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            lbPopis.Margin = new System.Windows.Thickness(180, 60, 0, 0);
            plocha.Children.Add(lbPopis);

            if (progressNyni > splneni) progressNyni = splneni;

            Label lbSplneni = new Label();
            lbSplneni.Content = String.Format("Splněno: {0}/{1} ({2}%)", progressNyni, splneni, progressNyni * 100 / splneni);
            lbSplneni.FontSize = 20;
            if (progressNyni >= splneni) lbSplneni.Foreground = Brushes.LimeGreen;
            else lbSplneni.Foreground = Brushes.Red;
            lbSplneni.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
            lbSplneni.VerticalContentAlignment = System.Windows.VerticalAlignment.Top;
            lbSplneni.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            lbSplneni.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            lbSplneni.Margin = new System.Windows.Thickness(180, 120, 0, 0);
            plocha.Children.Add(lbSplneni);

            return plocha;
        }
    }
}
