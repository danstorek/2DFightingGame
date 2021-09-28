using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace _2DFightingGame
{
    public partial class MainWindow : Window
    {
        Postava pos1;
        Postava pos2;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Plocha_Loaded(object sender, RoutedEventArgs e)
        {
            pos1 = new Postava_1(Plocha, postava1, false);
            pos2 = new Postava_1(Plocha, postava2, false);

            DispatcherTimer gameTick = new DispatcherTimer();
            gameTick.Interval = TimeSpan.FromMilliseconds(1000 / 60);
            gameTick.Tick += GameTick_Tick;
            gameTick.Start();
        }

        private void GameTick_Tick(object sender, EventArgs e)
        {
            pos1.Tick();
            pos2.Tick();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                //Hráč 1
                case Key.Left:
                    pos1.setVlevo(true);
                    break;
                case Key.Right:
                    pos1.setVpravo(true);
                    break;
                case Key.Up:
                    pos1.setSkokTrigger(true);
                    break;
                case Key.M:
                    pos1.setUtok1(true);
                    break;
                case Key.N:
                    pos1.setUtok2(true);
                    break;


                //Hráč 2
                case Key.A:
                    pos2.setVlevo(true);
                    break;
                case Key.D:
                    pos2.setVpravo(true);
                    break;
                case Key.W:
                    pos2.setSkokTrigger(true);
                    break;
                case Key.Q:
                    pos2.setUtok1(true);
                    break;
                case Key.E:
                    pos2.setUtok2(true);
                    break;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                //Hráč 1
                case Key.Left:
                    pos1.setVlevo(false);
                    break;
                case Key.Right:
                    pos1.setVpravo(false);
                    break;
                case Key.Up:
                    pos1.setSkokTrigger(false);
                    break;
                case Key.M:
                    pos1.setUtok1(false);
                    break;
                case Key.N:
                    pos1.setUtok2(false);
                    break;

                //Hráč 2
                case Key.A:
                    pos2.setVlevo(false);
                    break;
                case Key.D:
                    pos2.setVpravo(false);
                    break;
                case Key.W:
                    pos2.setSkokTrigger(false);
                    break;
                case Key.Q:
                    pos2.setUtok1(false);
                    break;
                case Key.E:
                    pos2.setUtok2(false);
                    break;
            }
        }
    }
}
