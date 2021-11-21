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
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 430;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(0, 0, 0, 720);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 430;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(743, 0, 0, 720);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 430;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(1490, 0, 0, 720);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 751;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(585, 0, 0, 530);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 430;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(0, 0, 0, 340);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 430;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(743, 0, 0, 340);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map2/platform1.png"));
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

                default:
                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 1500;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 10;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(200, 0, 0, 650);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 1200;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 10;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(350, 0, 0, 500);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Source = new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/platform1.png"));
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 1500;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 10;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(200, 0, 0, 350);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 2100;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 54;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(-150, 0, 0, 156);

                    Hitboxy.platformy.Add(new Image());
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Width = 2100;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Height = 10;
                    Hitboxy.platformy[Hitboxy.platformy.Count - 1].Margin = new Thickness(-150, 0, 0, -500);

                    return new BitmapImage(new Uri("pack://application:,,,/imgs/maps/map1/background.png"));
            }
        }
    }
}
