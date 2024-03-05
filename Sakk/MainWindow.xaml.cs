using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
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

namespace Sakk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<List<string>> lepesekListaja = new List<List<string>>();
        public MainWindow()
        {
            InitializeComponent();
            LoadFromFile("jatszmak.txt");
            MessageBox.Show(AFeladat(), "A, Feladat");
            MessageBox.Show(BFeladat().ToString(), "B, Feladat");
            MessageBox.Show(CFeladat(), "C, Feladat");
            MessageBox.Show(DFeladat().ToString(), "D, Feladat");
            MessageBox.Show(EFeladat().ToString(), "E, Feladat");
            MessageBox.Show(FFeladat().ToString(), "F, Feladat");
            this.Close();
        }
        public void LoadFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                var kiolvasottSorok = File.ReadAllLines(fileName);
                foreach (var sor in kiolvasottSorok)
                {
                    lepesekListaja.Add(sor.Trim().Split("\t").ToList());
                }
            }
            else
            {
                MessageBox.Show("Nem Létezik a file!");
            }
        }



        public string AFeladat()
        {
            string nyeresSorozat = "";

            for (int i = 0; i < lepesekListaja.Count; i++)
            {
                if (lepesekListaja[i].Count % 2 == 0)
                {
                    nyeresSorozat += "s";
                }
                else
                {
                    nyeresSorozat += "v";
                }
            }

            return nyeresSorozat;
        }



        public int BFeladat()
        {
            int huszarLepesekSzama = 0;

            foreach (var jatszma in lepesekListaja)
            {
                foreach (var lepes in jatszma)
                {
                    if (char.ToLower(lepes[0]) == 'h')
                    {
                        huszarLepesekSzama += 4;
                    }
                }
            }

            return huszarLepesekSzama;
        }



        public string CFeladat()
        {
            string jatszmak = "";
            string vVezer = "d1";
            string sVezer = "d8";
            int mostJatszma = 1;
            foreach (var jatszma in lepesekListaja)
            {
                for (int i = 1; i < jatszma.Count; i++)
                {
                    if (char.ToLower(jatszma[i][0]) == 'v')
                    {
                        if (i % 2 != 0)
                        {
                            vVezer = jatszma[i][jatszma[i].Count() - 2] + jatszma[i][jatszma[i].Count() - 1].ToString();
                        }
                        else if (i % 2 == 0)
                        {
                            sVezer = jatszma[i][jatszma[i].Count() - 2] + jatszma[i][jatszma[i].Count() - 1].ToString();
                        }
                    }
                    else
                    {
                        if (char.ToLower(jatszma[i][0]) == 'x' || char.ToLower(jatszma[i][1]) == 'x')
                        {
                            if (jatszma[i][jatszma[i].Count() - 2] + jatszma[i][jatszma[i].Count() - 1].ToString() == vVezer || jatszma[i][jatszma[i].Count() - 2] + jatszma[i][jatszma[i].Count() - 1].ToString() == sVezer)
                            {
                                jatszmak += mostJatszma.ToString() + ";";
                            }
                        }
                    }
                }
                mostJatszma++;
            }

            return jatszmak;
        }



        public int DFeladat()
        {
            int lepesekSzama = 0;
            string vVezer = "d1";
            string sVezer = "d8";
            foreach (var jatszma in lepesekListaja)
            {
                for (int i = 1; i < jatszma.Count; i++)
                {
                    if (char.ToLower(jatszma[i][0]) == 'v')
                    {
                        string temp_Steps = jatszma[i][jatszma[i].Count() - 2] + jatszma[i][jatszma[i].Count() - 1].ToString();
                        if (i % 2 != 0)
                        {

                            lepesekSzama += CalculateSteps(vVezer, jatszma[i][jatszma[i].Count() - 2] + jatszma[i][jatszma[i].Count() - 1].ToString());
                            vVezer = temp_Steps;
                        }
                        else if (i % 2 == 0)
                        {
                            lepesekSzama += CalculateSteps(sVezer, jatszma[i][jatszma[i].Count() - 2] + jatszma[i][jatszma[i].Count() - 1].ToString());
                            sVezer = temp_Steps;
                        }
                    }
                }
            }

            return lepesekSzama;
        }
        public int CalculateSteps(string elso, string masodik)
        {
            int lepesek = 0;
            if (elso[0] == masodik[0])
            {
                lepesek = Math.Abs(Convert.ToInt32(masodik[1]) - Convert.ToInt32(elso[1]));
            }
            else if (elso[1] == masodik[1])
            {
                lepesek = Math.Abs(Convert.ToInt32(LetterToNumber(masodik[0])) - Convert.ToInt32(LetterToNumber(elso[0])));
            }
            else
            {
                if (Convert.ToInt32(LetterToNumber(masodik[0])) - Convert.ToInt32(LetterToNumber(elso[0]))>0)
                {
                    lepesek = Math.Abs(Convert.ToInt32(LetterToNumber(masodik[0])) - Convert.ToInt32(LetterToNumber(elso[0])));
                }
            }

            return lepesek;
        }
        public int LetterToNumber(char hely)
        {
            int num = 1;
            if (char.ToLower(hely) == 'b')
            {
                num = 2;
            }
            else if (char.ToLower(hely) == 'c')
            {
                num = 3;
            }
            else if (char.ToLower(hely) == 'd')
            {
                num = 4;
            }
            else if (char.ToLower(hely) == 'e')
            {
                num = 5;
            }
            else if (char.ToLower(hely) == 'f')
            {
                num = 6;
            }
            else if (char.ToLower(hely) == 'g')
            {
                num = 7;
            }
            else if (char.ToLower(hely) == 'h')
            {
                num = 8;
            }
            return num;
        }


        
        public int EFeladat()
        {
            int kiralyNemMozgott = 0;

            foreach (var jatszma in lepesekListaja)
            {
                bool lepett = false;
                for (int i = 1; i < jatszma.Count; i++)
                {
                    
                    if (char.ToLower(jatszma[i][0]) == 'k' && i % 2 != 0)
                    {
                        lepett = true;
                        break;
                    }
                }
                if (!lepett)
                {
                    kiralyNemMozgott++;
                }
            }

            return kiralyNemMozgott;
        }




        public int FFeladat()
        {
            int merkozesekSzama = 0;

            foreach (var jatszma in lepesekListaja)
            {
                int babukSzama = 32;
                foreach (var lepes in jatszma)
                {
                    if (char.ToLower(lepes[0]) == 'x' || char.ToLower(lepes[1]) == 'x')
                    {
                        babukSzama--;
                    }
                }
                if (babukSzama > 20)
                {
                    merkozesekSzama++;
                }
            }

            return merkozesekSzama;
        }
    }
}