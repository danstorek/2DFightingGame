using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace _2DFightingGame
{
    /// <summary>
    /// Interakční logika pro VezStatistika.xaml
    /// </summary>
    public partial class VezStatistika : Window
    {
        DispatcherTimer animacePrechod = new DispatcherTimer();
        bool klik = false;

        public VezStatistika()
        {
            InitializeComponent();

            //Achievement
            if(Globalni.vez >= 5)
            {
                Globalni.ukl.PridatPrubeh(6, 1);
            }

            lbObdrzene.Content = Globalni.obdrzeneCelkem;
            lbProjita.Content = Globalni.vez + "/5";
            lbSkore.Content = Globalni.skoreCelkem;

            if(Globalni.vez == 5) lbProjita.Foreground = Brushes.Lime;
            else lbProjita.Foreground = Brushes.Red;


            //Animace přechodu
            animacePrechod.Tick += AnimacePrechod_Tick;
            animacePrechod.Interval = TimeSpan.FromMilliseconds(1000 / 90);
            animacePrechod.Start();
        }

        private void AnimacePrechod_Tick(object sender, EventArgs e)
        {
            if (prechod2.Width > 1) prechod2.Width -= 120;
            if (klik && prechod1.Width < 1920) prechod1.Width += 120;

            if (klik && prechod1.Width >= 1920)
            {
                animacePrechod.Stop();
                HlavniMenu okno = new HlavniMenu();
                okno.Show();
                System.Threading.Thread.Sleep(50);
                this.Close();
            }
        }

        private void Navrat(object sender, RoutedEventArgs e)
        {
            klik = true;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Globalni.zmenitScale((Grid)sender);
        }
    }
}
