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
                File.WriteAllLines("profil.sav", new string[]{ "start", "0", "0;0;0;0;0" });
            }
            soubor = File.ReadAllLines("profil.sav").ToList();
        }

        void Ulozit()
        {
            File.WriteAllLines("profil.sav",soubor);
        }

        public int ziskatPrubehAchievementu(int id) {
            return Convert.ToInt32(soubor[2].Split(';')[id]);
        } 

        public void PridatPrubeh(int id, int rozdil)
        {
            String[] tmpAchievementy = soubor[2].Split(';');
            tmpAchievementy[id] = (Convert.ToInt32(tmpAchievementy[id]) + rozdil).ToString();
            string tmpString = "";
            for(int i = 0; i<tmpAchievementy.Length; i++)
            {
                tmpString += tmpAchievementy[i];
                if(i != tmpAchievementy.Length - 1)
                {
                    tmpString += ";";
                }
            }
            soubor[2] = tmpString;
            Ulozit();
        }

        public void PridatSkore(string jmeno, int skore)
        {
            soubor[0] += ";" + jmeno;
            soubor[1] += ";" + skore.ToString();
            Ulozit();
        }

        public List<KeyValuePair<string, int>> SerazeneSkore()
        {
            List<KeyValuePair<string, int>> tmp = new List<KeyValuePair<string, int>>();
            for(int i = 1; i<soubor[0].Split(';').Length; i++)
            {
                tmp.Add(new KeyValuePair<string,int>(soubor[0].Split(';')[i], Convert.ToInt32(soubor[1].Split(';')[i])));
            }

            return tmp.OrderByDescending(x => x.Value).ToList();
        }

        public void VymazatZebricek()
        {
            soubor[0] = "start";
            soubor[1] = "0";
            Ulozit();
        }
    }
}
