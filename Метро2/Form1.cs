using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace Метро2
{
    public partial class Form1 : Form
    {
        string[] doroga;
        int len_doroga;
        bool aktivno = false;
        bool point1 = false;
        bool point2 = false;
        PictureBox[] marshrut = new PictureBox[2];

        Dictionary<string, string[]> stations = new Dictionary<string, string[]>()
        {
            {"Kupchino", new string[3]{"Купчино",",синяя","Kupchino__Zvyozdnaya"}}

        };

        Dictionary<string, string[]> sosedi = new Dictionary<string, string[]>()
        {
            {"Kupchino", new string[]{"Zvyozdnaya"}},
            {"Zvyozdnaya", new string[]{ "Kupchino","Moskovskaya"}},
            {"Moskovskaya", new string[]{ "Zvyozdnaya", "Park_Pobedy"}},
            {"Park_Pobedy", new string[]{ "Moskovskaya", "Elektrosila"}},
            {"Elektrosila", new string[]{ "Park_Pobedy", "Moskovskie_Vorota"}},
            {"Moskovskie_Vorota", new string[]{ "Elektrosila", "Frunzenskaya"}},
            {"Frunzenskaya", new string[]{ "Moskovskie_Vorota", "Tekhnologichesky_Institut1___Tekhnologichesky_Institut2"}},
            {"Tekhnologichesky_Institut1___Tekhnologichesky_Institut2", new string[]{ "Frunzenskaya", "Sennaya_Ploshchad___Sadovaya___Spasskaya","Pushkinskaya___Zvenigorodskaya","Baltiyskaya"}},
            {"Sennaya_Ploshchad___Sadovaya___Spasskaya", new string[]{ "Tekhnologichesky_Institut1___Tekhnologichesky_Institut2", "Nevsky_Prospekt___Gostiny_Dvor","Vladimirskaya___Dostoyevskaya","Pushkinskaya___Zvenigorodskaya","Admiralteyskaya"}},
            {"Nevsky_Prospekt___Gostiny_Dvor", new string[]{ "Sennaya_Ploshchad___Sadovaya___Spasskaya", "Gorkovskaya", "Vasileostrovskaya", "Ploschad_Vosstania___Mayakovskaya"}},
            {"Gorkovskaya", new string[]{ "Nevsky_Prospekt___Gostiny_Dvor", "Petrogradskaya"}},
            {"Petrogradskaya", new string[]{ "Gorkovskaya", "Chornaya_Rechka"}},
            {"Chornaya_Rechka", new string[]{ "Petrogradskaya", "Pionerskaya"}},
            {"Pionerskaya", new string[]{ "Chornaya_Rechka", "Udelnaya"}},
            {"Udelnaya", new string[]{ "Pionerskaya", "Ozerki"}},
            {"Ozerki", new string[]{ "Udelnaya", "Prospekt_Prosvescheniya"}},
            {"Prospekt_Prosvescheniya", new string[]{ "Ozerki", "Parnas"}},
            {"Parnas", new string[]{ "Prospekt_Prosvescheniya"}},
            {"Prospekt_Veteranov", new string[]{ "Leninsky_Prospekt"}},
            {"Leninsky_Prospekt", new string[]{ "Prospekt_Veteranov","Avtovo"}},
            {"Avtovo", new string[]{ "Leninsky_Prospekt", "Kirovsky_Zavod"}},
            {"Kirovsky_Zavod", new string[]{ "Avtovo", "Narvskaya"}},
            {"Narvskaya", new string[]{ "Kirovsky_Zavod", "Baltiyskaya"}},
            {"Baltiyskaya", new string[]{ "Narvskaya", "Tekhnologichesky_Institut1___Tekhnologichesky_Institut2"}},
            {"Pushkinskaya___Zvenigorodskaya", new string[]{ "Tekhnologichesky_Institut1___Tekhnologichesky_Institut2", "Vladimirskaya___Dostoyevskaya","Sennaya_Ploshchad___Sadovaya___Spasskaya","Obvodny_Kanal"}},
            {"Vladimirskaya___Dostoyevskaya", new string[]{ "Pushkinskaya___Zvenigorodskaya", "Ploschad_Vosstania___Mayakovskaya", "Sennaya_Ploshchad___Sadovaya___Spasskaya", "Ligovsky_Prospekt"}},
            {"Ploschad_Vosstania___Mayakovskaya", new string[]{ "Vladimirskaya___Dostoyevskaya", "Chernyshevskaya", "Nevsky_Prospekt___Gostiny_Dvor", "Ploshchad_Alexandra_Nevskogo1___Ploshchad_Alexandra_Nevskogo2"}},
            {"Chernyshevskaya", new string[]{ "Ploschad_Vosstania___Mayakovskaya", "Ploshchad_Lenina"}},
            {"Ploshchad_Lenina", new string[]{ "Chernyshevskaya", "Vyborgskaya"}},
            {"Vyborgskaya", new string[]{ "Ploshchad_Lenina", "Lesnaya"}},
            {"Lesnaya", new string[]{ "Vyborgskaya", "Ploshchad_Muzhestva"}},
            {"Ploshchad_Muzhestva", new string[]{ "Lesnaya", "Politekhnicheskaya"}},
            {"Politekhnicheskaya", new string[]{ "Ploshchad_Muzhestva", "Akademicheskaya"}},
            {"Akademicheskaya", new string[]{ "Politekhnicheskaya", "Grazhdansky_Prospekt"}},
            {"Grazhdansky_Prospekt", new string[]{ "Akademicheskaya", "Devyatkino"}},
            {"Devyatkino", new string[]{ "Grazhdansky_Prospekt"}},
            {"Shushary", new string[]{ "Dunayskaya"}},
            {"Dunayskaya", new string[]{ "Shushary","Prospekt_Slavy"}},
            {"Prospekt_Slavy", new string[]{ "Dunayskaya", "Mezhdunarodnaya"}},
            {"Mezhdunarodnaya", new string[]{ "Prospekt_Slavy", "Bukharestskaya"}},
            {"Bukharestskaya", new string[]{ "Mezhdunarodnaya", "Volkovskaya"}},
            {"Volkovskaya", new string[]{ "Bukharestskaya", "Obvodny_Kanal"}},
            {"Obvodny_Kanal", new string[]{ "Volkovskaya", "Pushkinskaya___Zvenigorodskaya"}},
            {"Admiralteyskaya", new string[]{ "Sennaya_Ploshchad___Sadovaya___Spasskaya", "Sportivnaya"}},
            {"Sportivnaya", new string[]{ "Admiralteyskaya", "Chkalovskaya"}},
            {"Chkalovskaya", new string[]{ "Sportivnaya", "Krestovsky_Ostrov"}},
            {"Krestovsky_Ostrov", new string[]{ "Chkalovskaya", "Staraya_Derevnya"}},
            {"Staraya_Derevnya", new string[]{ "Krestovsky_Ostrov", "Komendantsky_Prospekt"}},
            {"Komendantsky_Prospekt", new string[]{ "Staraya_Derevnya"}},
            {"Rybatskoe", new string[]{ "Obukhovo"}},
            {"Obukhovo", new string[]{ "Rybatskoe", "Proletarskaya"}},
            {"Proletarskaya", new string[]{ "Obukhovo", "Lomonosovskaya"}},
            {"Lomonosovskaya", new string[]{ "Proletarskaya", "Yelizarovskaya"}},
            {"Yelizarovskaya", new string[]{ "Lomonosovskaya", "Ploshchad_Alexandra_Nevskogo1___Ploshchad_Alexandra_Nevskogo2"}},
            {"Ploshchad_Alexandra_Nevskogo1___Ploshchad_Alexandra_Nevskogo2", new string[]{ "Yelizarovskaya", "Ploschad_Vosstania___Mayakovskaya","Novocherkasskaya","Ligovsky_Prospekt"}},
            {"Vasileostrovskaya", new string[]{ "Nevsky_Prospekt___Gostiny_Dvor", "Primorskaya"}},
            {"Primorskaya", new string[]{ "Vasileostrovskaya", "Zenit"}},
            {"Zenit", new string[]{ "Primorskaya", "Begovaya"}},
            {"Begovaya", new string[]{"Zenit"}},
            {"Ulitsa_Dybenko", new string[]{ "Prospekt_Bolshevikov"}},
            {"Prospekt_Bolshevikov", new string[]{ "Ulitsa_Dybenko", "Ladozhskaya"}},
            {"Ladozhskaya", new string[]{ "Prospekt_Bolshevikov", "Novocherkasskaya"}},
            {"Novocherkasskaya", new string[]{ "Ladozhskaya", "Ploshchad_Alexandra_Nevskogo1___Ploshchad_Alexandra_Nevskogo2"}},
            {"Ligovsky_Prospekt", new string[]{ "Ploshchad_Alexandra_Nevskogo1___Ploshchad_Alexandra_Nevskogo2", "Vladimirskaya___Dostoyevskaya"}},




        };
        Dictionary<string, string[]> puti = new Dictionary<string, string[]>()
        {
            {"Kupchino", new string[]{"Kupchino__Zvyozdnaya"}},
            {"Zvyozdnaya", new string[]{ "Kupchino__Zvyozdnaya", "Zvyozdnaya__Moskovskaya"}},
            {"Moskovskaya", new string[]{ "Zvyozdnaya__Moskovskaya", "Moskovskaya__Park_Pobedy"}},
            {"Park_Pobedy", new string[]{ "Moskovskaya__Park_Pobedy", "Park_Pobedy__Elektrosila"}},
            {"Elektrosila", new string[]{ "Park_Pobedy__Elektrosila", "Elektrosila__Moskovskie_Vorota"}},
            {"Moskovskie_Vorota", new string[]{ "Elektrosila__Moskovskie_Vorota", "Moskovskie_Vorota__Frunzenskaya"}},
            {"Frunzenskaya", new string[]{ "Moskovskie_Vorota__Frunzenskaya", "Frunzenskaya__Tekhnologichesky_Institut1___Tekhnologichesky_Institut2"}},
            {"Tekhnologichesky_Institut1___Tekhnologichesky_Institut2", new string[]{ "Frunzenskaya__Tekhnologichesky_Institut1___Tekhnologichesky_Institut2", "Tekhnologichesky_Institut1___Tekhnologichesky_Institut2__Sennaya_Ploshchad___Sadovaya___Spasskaya","Tekhnologichesky_Institut1___Tekhnologichesky_Institut2__Pushkinskaya___Zvenigorodskaya","Baltiyskaya__Tekhnologichesky_Institut1___Tekhnologichesky_Institut2"}},
            {"Sennaya_Ploshchad___Sadovaya___Spasskaya", new string[]{ "Tekhnologichesky_Institut1___Tekhnologichesky_Institut2__Sennaya_Ploshchad___Sadovaya___Spasskaya", "Sennaya_Ploshchad___Sadovaya___Spasskaya__Nevsky_Prospekt___Gostiny_Dvor","Sennaya_Ploshchad___Sadovaya___Spasskaya__Vladimirskaya___Dostoyevskaya","Sennaya_Ploshchad___Sadovaya___Spasskaya__Pushkinskaya___Zvenigorodskaya","Sennaya_Ploshchad___Sadovaya___Spasskaya__Admiralteyskaya"}},
            {"Nevsky_Prospekt___Gostiny_Dvor", new string[]{ "Sennaya_Ploshchad___Sadovaya___Spasskaya__Nevsky_Prospekt___Gostiny_Dvor", "Nevsky_Prospekt___Gostiny_Dvor__Gorkovskaya", "Nevsky_Prospekt___Gostiny_Dvor__Vasileostrovskaya", "Ploschad_Vosstania___Mayakovskaya__Nevsky_Prospekt___Gostiny_Dvor"}},
            {"Gorkovskaya", new string[]{ "Nevsky_Prospekt___Gostiny_Dvor__Gorkovskaya", "Gorkovskaya__Petrogradskaya"}},
            {"Petrogradskaya", new string[]{ "Gorkovskaya__Petrogradskaya", "Petrogradskaya__Chornaya_Rechka"}},
            {"Chornaya_Rechka", new string[]{ "Petrogradskaya__Chornaya_Rechka", "Chornaya_Rechka__Pionerskaya"}},
            {"Pionerskaya", new string[]{ "Chornaya_Rechka__Pionerskaya", "Pionerskaya__Udelnaya"}},
            {"Udelnaya", new string[]{ "Pionerskaya__Udelnaya", "Udelnaya__Ozerki"}},
            {"Ozerki", new string[]{ "Udelnaya__Ozerki", "Ozerki__Prospekt_Prosvescheniya"}},
            {"Prospekt_Prosvescheniya", new string[]{ "Ozerki__Prospekt_Prosvescheniya", "Prospekt_Prosvescheniya__Parnas"}},
            {"Parnas", new string[]{ "Prospekt_Prosvescheniya__Parnas"}},
            {"Prospekt_Veteranov", new string[]{ "Prospekt_Veteranov__Leninsky_Prospekt"}},
            {"Leninsky_Prospekt", new string[]{ "Prospekt_Veteranov__Leninsky_Prospekt", "Leninsky_Prospekt__Avtovo"}},
            {"Avtovo", new string[]{ "Leninsky_Prospekt__Avtovo", "Avtovo__Kirovsky_Zavod"}},
            {"Kirovsky_Zavod", new string[]{ "Avtovo__Kirovsky_Zavod", "Kirovsky_Zavod__Narvskaya"}},
            {"Narvskaya", new string[]{ "Kirovsky_Zavod__Narvskaya", "Narvskaya__Baltiyskaya"}},
            {"Baltiyskaya", new string[]{ "Narvskaya__Baltiyskaya", "Baltiyskaya__Tekhnologichesky_Institut1___Tekhnologichesky_Institut2"}},
            {"Pushkinskaya___Zvenigorodskaya", new string[]{ "Tekhnologichesky_Institut1___Tekhnologichesky_Institut2__Pushkinskaya___Zvenigorodskaya", "Pushkinskaya___Zvenigorodskaya__Vladimirskaya___Dostoyevskaya", "Sennaya_Ploshchad___Sadovaya___Spasskaya__Pushkinskaya___Zvenigorodskaya", "Obvodny_Kanal__Pushkinskaya___Zvenigorodskaya"}},
            {"Vladimirskaya___Dostoyevskaya", new string[]{ "Pushkinskaya___Zvenigorodskaya__Vladimirskaya___Dostoyevskaya", "Vladimirskaya___Dostoyevskaya__Ploschad_Vosstania___Mayakovskaya", "Sennaya_Ploshchad___Sadovaya___Spasskaya__Vladimirskaya___Dostoyevskaya", "Ligovsky_Prospekt__Vladimirskaya___Dostoyevskaya"}},
            {"Ploschad_Vosstania___Mayakovskaya", new string[]{ "Vladimirskaya___Dostoyevskaya__Ploschad_Vosstania___Mayakovskaya", "Ploschad_Vosstania___Mayakovskaya__Chernyshevskaya", "Ploschad_Vosstania___Mayakovskaya__Nevsky_Prospekt___Gostiny_Dvor", "Ploshchad_Alexandra_Nevskogo1___Ploshchad_Alexandra_Nevskogo2__Ploschad_Vosstania___Mayakovskaya"}},
            {"Chernyshevskaya", new string[]{ "Ploschad_Vosstania___Mayakovskaya__Chernyshevskaya", "Chernyshevskaya__Ploshchad_Lenina"}},
            {"Ploshchad_Lenina", new string[]{ "Chernyshevskaya__Ploshchad_Lenina", "Ploshchad_Lenina__Vyborgskaya"}},
            {"Vyborgskaya", new string[]{ "Ploshchad_Lenina__Vyborgskaya", "Vyborgskaya__Lesnaya"}},
            {"Lesnaya", new string[]{ "Vyborgskaya__Lesnaya", "Lesnaya__Ploshchad_Muzhestva"}},
            {"Ploshchad_Muzhestva", new string[]{ "Lesnaya__Ploshchad_Muzhestva", "Ploshchad_Muzhestva__Politekhnicheskaya"}},
            {"Politekhnicheskaya", new string[]{ "Ploshchad_Muzhestva__Politekhnicheskaya", "Politekhnicheskaya__Akademicheskaya"}},
            {"Akademicheskaya", new string[]{ "Politekhnicheskaya__Akademicheskaya", "Akademicheskaya__Grazhdansky_Prospekt"}},
            {"Grazhdansky_Prospekt", new string[]{ "Akademicheskaya__Grazhdansky_Prospekt", "Grazhdansky_Prospekt__Devyatkino"}},
            {"Devyatkino", new string[]{ "Grazhdansky_Prospekt__Devyatkino"}},
            {"Shushary", new string[]{ "Shushary__Dunayskaya"}},
            {"Dunayskaya", new string[]{ "Shushary__Dunayskaya", "Dunayskaya__Prospekt_Slavy"}},
            {"Prospekt_Slavy", new string[]{ "Dunayskaya__Prospekt_Slavy", "Prospekt_Slavy__Mezhdunarodnaya"}},
            {"Mezhdunarodnaya", new string[]{ "Prospekt_Slavy__Mezhdunarodnaya", "Mezhdunarodnaya__Bukharestskaya"}},
            {"Bukharestskaya", new string[]{ "Mezhdunarodnaya__Bukharestskaya", "Bukharestskaya__Volkovskaya"}},
            {"Volkovskaya", new string[]{ "Bukharestskaya__Volkovskaya", "Volkovskaya__Obvodny_Kanal"}},
            {"Obvodny_Kanal", new string[]{ "Volkovskaya__Obvodny_Kanal", "Obvodny_Kanal__Pushkinskaya___Zvenigorodskaya"}},
            {"Admiralteyskaya", new string[]{ "Sennaya_Ploshchad___Sadovaya___Spasskaya__Admiralteyskaya", "Admiralteyskaya__Sportivnaya01","Admiralteyskaya__Sportivnaya02"}},
            {"Sportivnaya", new string[]{ "Admiralteyskaya__Sportivnaya01", "Admiralteyskaya__Sportivnaya02","Sportivnaya__Chkalovskaya"}},
            {"Chkalovskaya", new string[]{ "Sportivnaya__Chkalovskaya", "Chkalovskaya__Krestovsky_Ostrov"}},
            {"Krestovsky_Ostrov", new string[]{ "Chkalovskaya__Krestovsky_Ostrov", "Krestovsky_Ostrov__Staraya_Derevnya"}},
            {"Staraya_Derevnya", new string[]{ "Krestovsky_Ostrov__Staraya_Derevnya", "Staraya_Derevnya__Komendantsky_Prospekt"}},
            {"Komendantsky_Prospekt", new string[]{ "Staraya_Derevnya__Komendantsky_Prospekt"}},
            {"Rybatskoe", new string[]{ "Rybatskoe__Obukhovo"}},
            {"Obukhovo", new string[]{ "Rybatskoe__Obukhovo","Obukhovo__Proletarskaya"}},
            {"Proletarskaya", new string[]{ "Obukhovo__Proletarskaya", "Proletarskaya__Lomonosovskaya"}},
            {"Lomonosovskaya", new string[]{ "Proletarskaya__Lomonosovskaya", "Lomonosovskaya__Yelizarovskaya"}},
            {"Yelizarovskaya", new string[]{ "Lomonosovskaya__Yelizarovskaya", "Yelizarovskaya__Ploshchad_Alexandra_Nevskogo1___Ploshchad_Alexandra_Nevskogo2"}},
            {"Ploshchad_Alexandra_Nevskogo1___Ploshchad_Alexandra_Nevskogo2", new string[]{ "Yelizarovskaya__Ploshchad_Alexandra_Nevskogo1___Ploshchad_Alexandra_Nevskogo2", "Ploshchad_Alexandra_Nevskogo1___Ploshchad_Alexandra_Nevskogo2__Ploschad_Vosstania___Mayakovskaya","Novocherkasskaya__Ploshchad_Alexandra_Nevskogo1___Ploshchad_Alexandra_Nevskogo2","Ploshchad_Alexandra_Nevskogo1___Ploshchad_Alexandra_Nevskogo2__Ligovsky_Prospekt"}},
            {"Vasileostrovskaya", new string[]{ "Nevsky_Prospekt___Gostiny_Dvor__Vasileostrovskaya", "Vasileostrovskaya__Primorskaya"}},
            {"Primorskaya", new string[]{ "Vasileostrovskaya__Primorskaya", "Primorskaya_Zenit"}},
            {"Zenit", new string[]{ "Primorskaya_Zenit", "Zenit__Begovaya"}},
            {"Begovaya", new string[]{"Zenit__Begovaya"}},
            {"Ulitsa_Dybenko", new string[]{ "Ulitsa_Dybenko__Prospekt_Bolshevikov"}},
            {"Prospekt_Bolshevikov", new string[]{ "Ulitsa_Dybenko__Prospekt_Bolshevikov", "Prospekt_Bolshevikov__Ladozhskaya"}},
            {"Ladozhskaya", new string[]{ "Prospekt_Bolshevikov__Ladozhskaya", "Ladozhskaya__Novocherkasskaya"}},
            {"Novocherkasskaya", new string[]{ "Ladozhskaya__Novocherkasskaya", "Novocherkasskaya__Ploshchad_Alexandra_Nevskogo1___Ploshchad_Alexandra_Nevskogo2"}},
            {"Ligovsky_Prospekt", new string[]{ "Ploshchad_Alexandra_Nevskogo1___Ploshchad_Alexandra_Nevskogo2__Ligovsky_Prospekt", "Ligovsky_Prospekt__Vladimirskaya___Dostoyevskaya"}},




        };


        Dictionary<string, PictureBox[]> picts = new Dictionary<string, PictureBox[]>();


        public Form1()
        {
            InitializeComponent();
        }


        public void Form1_Load(object sender, EventArgs e)
        {

            foreach (var pict in Controls.OfType<PictureBox>())
            {
                PictureBox Copy = Clone(pict);
                Copy.Name = pict.Name + "_copy";
                Copy.Visible = false;
                picts.Add(pict.Name, new PictureBox[] { pict, Copy });

                if (sosedi.ContainsKey(pict.Name))
                {

                    pict.Click += new EventHandler(Point1);
                    Copy.Click += new EventHandler(Point1);

                }


            }
            string str = "Vladimirskaya___Dostoyevskaya";



            textBox1.Text = str;
        }

        public Dictionary<string, string[]> navigation(string start, string finish)
        {
            Dictionary<string, string[]> visid = new Dictionary<string, string[]>()
            {
                {start, new string[]{null} },
            };

            string station;
            Queue<string> queue_stations = new Queue<string>();
            for (int i = 0; i < sosedi[start].Length; i++)
            {
                queue_stations.Enqueue(sosedi[start][i]);
                string[] value = new string[] { start, puti[start][i] };
                visid.Add(sosedi[start][i], value);
            }
            while (queue_stations.Count > 0)
            {
                station = queue_stations.Dequeue();
                if (station == finish)
                {

                    break;
                }

                for (int i = 0; i < sosedi[station].Length; i++)
                    if (!visid.ContainsKey(sosedi[station][i]))
                    {
                        queue_stations.Enqueue(sosedi[station][i]);


                        
                        string[] value = new string[] { station, puti[station][i] };
                        visid.Add(sosedi[station][i], value);
                    }


            }

            return visid;
        }

        public void rascras(Dictionary<string, string[]> prohod, string finsih, string start)
        {
            doroga = new string[100];
            len_doroga = 0;
            string str = "";           
            string[] stations = prohod[finsih];
            while (true)
            {
                PictureBox picture = picts[stations[1]][1];
                doroga[len_doroga] = picts[stations[1]][0].Name;
                len_doroga += 1;
                picts[stations[1]][0].Visible = false;
                picture.Visible = true;
                perekras(picture, Color.Aqua);

                if (stations[0] == start)
                    break;

                picture = picts[stations[0]][1];
                doroga[len_doroga] = picts[stations[0]][0].Name;
                len_doroga += 1;
                picts[stations[0]][0].Visible = false;
                picture.Visible = true;
                perekras(picture, Color.Aqua);
                str = picture.Name + " --> " + str;
                textBox1.Text = str;
                stations = prohod[stations[0]];
            }
            aktivno = true;

        }


        public static void yarkost(PictureBox picture)
        {
            Bitmap bit = new Bitmap(picture.Image);

            float C = 2; //контрастность (норма = 1)
            float B = -1;//яркость (от -1 до 1, норма = 0)
            float[][] matrix =
                {
                new float[] { C, 0, 0, 0, 0 } , //R
                new float[] { 0, C, 0, 0, 0 } , //G
                new float[] { 0, 0, C, 0, 0 } , //B
                new float[] { 0, 0, 0, 0.1f, 0 } , //A
                new float[] { B, B, B, 0, 1 } //W
            };
            ImageAttributes attr = new ImageAttributes();
            attr.SetColorMatrix(new ColorMatrix(matrix));

            Rectangle rect = new Rectangle(0, 0, picture.Image.Width, picture.Image.Height);
            Graphics.FromImage(bit).DrawImage(bit, rect, 0, 0, picture.Width, picture.Height, GraphicsUnit.Pixel, attr);
            picture.Image = bit;
        }


        public void perekras(PictureBox picture, Color ColorElement)
        {
            Bitmap bit = new Bitmap(picture.Image);

            for (int x = 0; x < bit.Width; x++)
            {
                for (int y = 0; y < bit.Height; y++)
                {
                    if (bit.GetPixel(x, y).Name != "0")
                    {
                        bit.SetPixel(x, y, ColorElement);
                    }



                }
            }
            picture.Image = bit;
        }
        public void Point1(object sender, EventArgs e)
        {
            if (aktivno)
            {
                foreach (string el in doroga)
                {
                    if (el is null)
                        break;

                    picts[el][1].Visible = false;
                    picts[el][0].Visible = true;

                }
                PictureBox picturee = (PictureBox)sender;
                if (picturee.Name.IndexOf("copy") == -1 || marshrut[0].Name == picturee.Name || marshrut[1].Name == picturee.Name)             
                    aktivno = false;
            }

            PictureBox picture = (PictureBox)sender;
            Color pointColor = Color.Green;
            if (aktivno)
            {
                picture = picts[picture.Name.Substring(0, picture.Name.Length - 5)][0];
                aktivno = false;
            }

            if (point1 && !point2)
            {

                pointColor = Color.Red;
                if (picture.Name.IndexOf("copy") > -1)
                {
                    picture.Visible = false;
                    point1 = false;
                    string nameOriginal = picture.Name.Substring(0, picture.Name.Length - 5);
                    picts[nameOriginal][0].Visible = true;
                    textBox2.Text = Convert.ToString(point1) + " " + Convert.ToString(point2);

                    return;


                }
                else
                {

                    point2 = true;
                    marshrut[1] = picts[picture.Name][1];
                    textBox1.Text = marshrut[0].Name + " -- " + marshrut[1].Name;
                }

            }

            else if (point2)
            {

                if (picture.Name.IndexOf("copy") > -1)
                {
                    if (picture.Name == marshrut[0].Name)
                    {
                        marshrut[0] = marshrut[1];
                        perekras(marshrut[0], Color.Green);
                    }



                    point2 = false;
                    picture.Visible = false;
                    string nameOriginal = picture.Name.Substring(0, picture.Name.Length - 5);
                    picts[nameOriginal][0].Visible = true;
                    textBox2.Text = Convert.ToString(point1) + " ||| " + Convert.ToString(point2);

                    return;



                }
                else
                {
                    marshrut[0].Visible = false;
                    string nameOriginal = marshrut[0].Name.Substring(0, marshrut[0].Name.Length - 5);
                    picts[nameOriginal][0].Visible = true;
                    marshrut[0] = marshrut[1];
                    perekras(marshrut[0], Color.Green);
                    marshrut[1] = picts[picture.Name][1];
                    pointColor = Color.Red;
                    textBox1.Text = marshrut[0].Name + " -- " + marshrut[1].Name;

                }


            }

            else
            {
                point1 = true;
                marshrut[0] = picts[picture.Name][1];
                textBox1.Text = "one";
            }




            picture.Visible = false;
            picts[picture.Name][1].Visible = true;

            perekras(picts[picture.Name][1], pointColor);
            textBox2.Text = Convert.ToString(point1) + " " + Convert.ToString(point2);

            if (point1 && point2)
            {
                string start = marshrut[0].Name.Substring(0, marshrut[0].Name.Length - 5);
                string finish = marshrut[1].Name.Substring(0, marshrut[1].Name.Length - 5);
                rascras(navigation(start, finish), finish, start);




            }
        }

            public static T Clone<T>(T controlToClone) where T : Control
            {
                T instance = Activator.CreateInstance<T>();
                Type control = controlToClone.GetType();
                PropertyInfo[] info = control.GetProperties();
                object p = control.InvokeMember("", System.Reflection.BindingFlags.CreateInstance, null, controlToClone, null);
                foreach (PropertyInfo pi in info)
                {
                    if ((pi.CanWrite) && !(pi.Name == "WindowTarget") && !(pi.Name == "Capture"))
                    {
                        pi.SetValue(instance, pi.GetValue(controlToClone, null), null);
                    }
                }
                return instance;
            }

        private void Yelizarovskaya_Click(object sender, EventArgs e)
        {

        }

        private void Obukhovo_Click(object sender, EventArgs e)
        {

        }
    }
    } 
