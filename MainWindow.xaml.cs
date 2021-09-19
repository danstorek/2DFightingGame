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
        List<Updatable> aktivni_projektily = new List<Updatable>();

        bool vlevo = false;
        bool vpravo = false;
        bool smer = true;
        bool skokTrigger = false;
        bool veVzduchu = false;
        bool skok = false;
        bool utok1 = false;
        bool utok2 = false;
        DateTime cooldown = DateTime.Now;

        int pohybX = 0;
        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer gameTick = new DispatcherTimer();
            gameTick.Interval = TimeSpan.FromMilliseconds(1000 / 60);
            gameTick.Tick += GameTick_Tick;
            gameTick.Start();
        }

        private void GameTick_Tick(object sender, EventArgs e)
        {
            Thickness pozice1 = postava1.Margin;

            //Aktualizace aktivních projektilů ve hře
            foreach (Updatable i in aktivni_projektily)
            {
                if (i.getAktivni()) i.Tick();
                else Plocha.Children.Remove(i.ReturnImage());
            }

            //Útok 1 - hráč 1
            if (utok1)
            {
                if (DateTime.Now > cooldown)
                {
                    Fireball fireball = new Fireball(postava1, smer);
                    Plocha.Children.Add(fireball.ReturnImage());
                    aktivni_projektily.Add(fireball);
                    cooldown = DateTime.Now.AddMilliseconds(fireball.cooldown);
                }

            }

            //Útok 2 - hráč 1
            if (utok2)
            {
                if (DateTime.Now > cooldown)
                {
                    TNT sw = new TNT(postava1);
                    Plocha.Children.Add(sw.ReturnImage());
                    aktivni_projektily.Add(sw);
                    cooldown = DateTime.Now.AddMilliseconds(sw.cooldown);
                }
            }
            //Pohyb - hráč 1
            if (vpravo)
            {
                if (pohybX < 30) pohybX += 3;
                postava1.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/right/1.png"));
                smer = true;
            }
            else if (vlevo)
            {
                if (pohybX > -30) pohybX -= 3;
                postava1.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/chars/char1/left/1.png"));
                smer = false;
            }
            else
            {
                if (pohybX > 0) pohybX -= 3;
                if (pohybX < 0) pohybX += 3;
            }

            if (pohybX > 0)
            {
                if (pozice1.Left + pohybX < 1800) pozice1.Left += pohybX;
            }
            else
            {
                if (pozice1.Left + pohybX > -70) pozice1.Left += pohybX;
            }

            //Skok - hráč 1
            if (skokTrigger)
            {
                if (!veVzduchu)
                {
                    veVzduchu = true;
                    skok = true;
                }
            }
            if (skok)
            {
                pozice1.Bottom += 20;
                if (pozice1.Bottom >= 500) skok = false;
            }

            //Gravitace - hráč 1
            if (veVzduchu)
            {
                if (!skok)
                {
                    pozice1.Bottom -= 25;
                    if (pozice1.Bottom < 211)
                    {
                        pozice1.Bottom = 211;
                        veVzduchu = false;
                    }
                }
            }

            postava1.Margin = pozice1;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    vlevo = true;
                    break;
                case Key.Right:
                    vpravo = true;
                    break;
                case Key.Up:
                    skokTrigger = true;
                    break;
                case Key.NumPad1:
                    utok1 = true;
                    break;
                case Key.NumPad2:
                    utok2 = true;
                    break;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    vlevo = false;
                    break;
                case Key.Right:
                    vpravo = false;
                    break;
                case Key.Up:
                    skokTrigger = false;
                    break;
                case Key.NumPad1:
                    utok1 = false;
                    break;
                case Key.NumPad2:
                    utok2 = false;
                    break;
            }
        }
    }
}
