using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace _2DFightingGame
{
    class Ukladani
    {
        public List<Key> nastaveniKlaves = new List<Key>()
        {
            Key.Up,Key.Down,Key.Left,Key.Right,
            Key.N,Key.M,
            Key.W,Key.S,Key.A,Key.D,
            Key.Q,Key.E
        };

        List<String> soubor = new List<string> { "start", "0", "0;0;0;0;0;0;0" };

        public Ukladani()
        {
            //Načtení achievementů ze souboru
            if (File.Exists("profil.xml"))
            {
                soubor = ReadFromXML<List<String>>("profil.xml");
            }

            //Načtení nastavení kláves ze souboru
            if (File.Exists("inputs.xml"))
            {
                nastaveniKlaves = ReadFromXML<List<Key>>("inputs.xml");
            }
        }

        public void Ulozit()
        {
            WriteToXML("profil.xml", soubor);
            WriteToXML("inputs.xml", nastaveniKlaves);
        }

        public int ZiskatPrubeh(int id) {
            return Convert.ToInt32(soubor[2].Split(';')[id]);
        } 

        public void PridatPrubeh(int id, int rozdil)
        {
            String[] tmpAchievementy = soubor[2].Split(';');
            if(Convert.ToInt32(tmpAchievementy[id]) < Achievementy.getSplneni(id)) tmpAchievementy[id] = (Convert.ToInt32(tmpAchievementy[id]) + rozdil).ToString();
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

        void WriteToXML<T>(string filePath, T objectToWrite, bool append = false)
        {
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(filePath, append);
                serializer.Serialize(writer, objectToWrite);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
        T ReadFromXML<T>(string filePath)
        {
            TextReader reader = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                reader = new StreamReader(filePath);
                return (T)serializer.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}
