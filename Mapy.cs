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
            Hitboxy.platformy = new List<Image>();
            switch (index)
            {
                case 0:
                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 751;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(585, 0, 0, 490);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 751;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(1169, 0, 0, 300);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 751;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(0, 0, 0, 300);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 2100;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(-150, 0, 0, 106);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 2100;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 10;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(-150, 0, 0, -500);

                    return new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/background.png"));

                case 1:
                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform2.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 1500;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(210, 0, 0, 530);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 430;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(0, 0, 0, 340);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 430;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(743, 0, 0, 340);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 430;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(1490, 0, 0, 340);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 751;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(0, 0, 0, 150);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 751;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(1169, 0, 0, 150);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 2100;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 10;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(-150, 0, 0, -500);

                    return new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/background.png"));

                case 2:
                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 1500;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(200, 0, 0, 650);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 1200;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(350, 0, 0, 500);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 1500;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(200, 0, 0, 350);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 2100;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(-150, 0, 0, 150);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 2100;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 10;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(-150, 0, 0, -500);

                    return new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/background.png"));


                case 3:
                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map4/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 751;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(585, 0, 0, 490);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map4/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 751;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(1169, 0, 0, 300);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map4/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 751;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(0, 0, 0, 300);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 2100;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(-150, 0, 0, 106);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 2100;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 10;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(-150, 0, 0, -500);

                    return new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map4/background.png"));
                case 4:
                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 751;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(585, 0, 0, 490);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 751;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(1169, 0, 0, 300);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 751;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(0, 0, 0, 300);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 751;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(1169, 0, 0, 106);
                    
                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 751;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(500, 0, 0, 106);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 751;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(0, 0, 0, 106);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 2100;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 10;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(-150, 0, 0, -500);

                    return new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/background.png"));
                default: return null;
            }
        }
        public static BitmapImage getMapaVez(int index)
        {
            Hitboxy.platformyVez = new List<Image>();
            switch (index)
            {
                case 0:
                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/platform1.png"));
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 751;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(585, 0, 0, 490);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/platform1.png"));
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 751;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(1169, 0, 0, 300);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/platform1.png"));
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 751;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(0, 0, 0, 300);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 2100;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(-150, 0, 0, 106);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 2100;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 10;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(-150, 0, 0, -500);

                    return new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/background.png"));

                case 1:
                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform2.png"));
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 1200;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(360, 0, 0, 530);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform1.png"));
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 430;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(0, 0, 0, 340);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform1.png"));
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 530;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(693, 0, 0, 340);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform1.png"));
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 430;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(1490, 0, 0, 340);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform1.png"));
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 751;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(0, 0, 0, 150);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/platform1.png"));
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 751;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(1169, 0, 0, 150);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 2100;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 10;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(-150, 0, 0, -500);

                    return new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map3/background.png"));

                case 2:
                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/platform1.png"));
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 1500;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(200, 0, 0, 650);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/platform1.png"));
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 1200;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(350, 0, 0, 500);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/platform1.png"));
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 1500;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(200, 0, 0, 350);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/platform1.png"));
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 2100;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(-150, 0, 0, 150);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 2100;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 10;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(-150, 0, 0, -500);

                    return new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/background.png"));


                case 3:
                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map4/platform1.png"));
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 751;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(585, 0, 0, 490);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map4/platform1.png"));
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 751;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(1169, 0, 0, 300);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map4/platform1.png"));
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 751;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(0, 0, 0, 300);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 2100;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(-150, 0, 0, 106);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 2100;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 10;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(-150, 0, 0, -500);

                    return new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map4/background.png"));
                case 4:
                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 751;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(585, 0, 0, 490);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 751;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(1169, 0, 0, 300);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 751;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(0, 0, 0, 300);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 751;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(1169, 0, 0, 106);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 751;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(500, 0, 0, 106);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/platform1.png"));
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 751;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 54;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(0, 0, 0, 106);

                    Hitboxy.platformyVez.Add(new Image());
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Width = 2100;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Height = 10;
                    Hitboxy.platformyVez[Hitboxy.platformyVez.Count - 1].Margin = new Thickness(-150, 0, 0, -500);

                    return new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map5/background.png"));
                default: return null;
            }
        }
    }
}
