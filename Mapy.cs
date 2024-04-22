using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace _2DFightingGame
{
    static class Mapy
    {
        public static BitmapImage getMapa(int index)
        {
            Globalni.platformy = new List<Image>();
            switch (index)
            {
                case 0:
                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/platform1.png"));
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 751;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(585, 0, 0, 490);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/platform1.png"));
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 751;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(1169, 0, 0, 300);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/platform1.png"));
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 751;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(0, 0, 0, 300);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 2100;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(-150, 0, 0, 106);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 2100;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 10;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(-150, 0, 0, -500);

                    return new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/background.png"));

                case 1:
                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform2.png"));
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 1500;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(210, 0, 0, 530);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform1.png"));
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 430;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(0, 0, 0, 340);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform1.png"));
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 430;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(743, 0, 0, 340);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform1.png"));
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 430;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(1490, 0, 0, 340);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform1.png"));
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 751;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(0, 0, 0, 150);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform1.png"));
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 751;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(1169, 0, 0, 150);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 2100;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 10;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(-150, 0, 0, -500);

                    return new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/background.png"));

                case 2:
                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/platform1.png"));
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 1500;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(200, 0, 0, 650);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/platform1.png"));
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 1200;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(350, 0, 0, 500);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/platform1.png"));
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 1500;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(200, 0, 0, 350);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/platform1.png"));
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 2100;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(-150, 0, 0, 150);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 2100;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 10;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(-150, 0, 0, -500);

                    return new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/background.png"));


                case 3:
                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map4/platform1.png"));
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 751;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(585, 0, 0, 490);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map4/platform1.png"));
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 751;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(1169, 0, 0, 300);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map4/platform1.png"));
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 751;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(0, 0, 0, 300);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 2100;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(-150, 0, 0, 106);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 2100;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 10;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(-150, 0, 0, -500);

                    return new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map4/background.png"));
                case 4:
                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 751;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(585, 0, 0, 490);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 751;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(1169, 0, 0, 300);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 751;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(0, 0, 0, 300);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 751;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(1169, 0, 0, 106);
                    
                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 751;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(500, 0, 0, 106);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 751;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 54;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(0, 0, 0, 106);

                    Globalni.platformy.Add(new Image());
                    Globalni.platformy[Globalni.platformy.Count - 1].Width = 2100;
                    Globalni.platformy[Globalni.platformy.Count - 1].Height = 10;
                    Globalni.platformy[Globalni.platformy.Count - 1].Margin = new Thickness(-150, 0, 0, -500);

                    return new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/background.png"));
                default: return null;
            }
        }
        public static BitmapImage getMapaVez(int index)
        {
            Globalni.platformyVez = new List<Image>();
            switch (index)
            {
                case 0:
                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/platform1.png"));
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 751;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(585, 0, 0, 490);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/platform1.png"));
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 751;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(1169, 0, 0, 300);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/platform1.png"));
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 751;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(0, 0, 0, 300);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 2100;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(-150, 0, 0, 106);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 2100;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 10;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(-150, 0, 0, -500);

                    return new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/background.png"));

                case 1:
                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform2.png"));
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 1200;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(360, 0, 0, 530);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform1.png"));
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 430;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(0, 0, 0, 340);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform1.png"));
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 530;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(693, 0, 0, 340);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform1.png"));
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 430;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(1490, 0, 0, 340);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform1.png"));
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 751;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(0, 0, 0, 150);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform1.png"));
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 751;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(1169, 0, 0, 150);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 2100;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 10;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(-150, 0, 0, -500);

                    return new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/background.png"));

                case 2:
                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/platform1.png"));
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 1500;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(200, 0, 0, 650);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/platform1.png"));
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 1200;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(350, 0, 0, 500);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/platform1.png"));
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 1500;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(200, 0, 0, 350);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/platform1.png"));
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 2100;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(-150, 0, 0, 150);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 2100;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 10;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(-150, 0, 0, -500);

                    return new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/background.png"));


                case 3:
                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map4/platform1.png"));
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 751;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(585, 0, 0, 490);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map4/platform1.png"));
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 751;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(1169, 0, 0, 300);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map4/platform1.png"));
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 751;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(0, 0, 0, 300);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 2100;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(-150, 0, 0, 106);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 2100;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 10;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(-150, 0, 0, -500);

                    return new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map4/background.png"));
                case 4:
                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 751;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(585, 0, 0, 490);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 751;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(1169, 0, 0, 300);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 751;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(0, 0, 0, 300);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 751;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(1169, 0, 0, 106);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 751;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(500, 0, 0, 106);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 751;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 54;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(0, 0, 0, 106);

                    Globalni.platformyVez.Add(new Image());
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Width = 2100;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Height = 10;
                    Globalni.platformyVez[Globalni.platformyVez.Count - 1].Margin = new Thickness(-150, 0, 0, -500);

                    return new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/background.png"));
                default: return null;
            }
        }
    }
}
