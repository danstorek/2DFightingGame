using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DFightingGame
{
    class Ukladani
    {
        List<String> soubor;

        public Ukladani()
        {
            if (!File.Exists("profil.sav"))
            {
                File.WriteAllLines("profil.sav", new string[]{ "start", "0" });
            }
            soubor = File.ReadAllLines("profil.sav").ToList();
        }

        void Ulozit()
        {
            File.WriteAllLines("profil.sav",soubor);
        }

        public void PridatSkore(string jmeno, int skore)
        {
            soubor[0] += ";" + jmeno;
            soubor[1] += ";" + skore.ToString();
            Ulozit();
        }

        public Dictionary<string, int> SerazeneSkore()
        {
            Dictionary<string, int> tmp = new Dictionary<string, int>();
            for(int i = 1; i<soubor[0].Split(';').Length; i++)
            {
                tmp.Add(soubor[0].Split(';')[i], Convert.ToInt32(soubor[1].Split(';')[i]));
            }

            return tmp.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        public void VymazatZebricek()
        {
            soubor[0] = "start";
            soubor[1] = "0";
            Ulozit();
        }
    }
}
