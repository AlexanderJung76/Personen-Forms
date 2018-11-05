using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Personen_Forms
{
    public partial class Personen_Window : Form
    {
        private List<Personen> mylist = new List<Personen>();
        private string path = @"c:\test\PersonenListenText.csv";
        private int index = 0;


        public Personen_Window()
        {
            InitializeComponent();
        }        

        
        private void BTN_CSV_Laden_Click(object sender, EventArgs e)
        {
            ReadCsv();
        }

        private void BTN_CSV_Schreiben_Click(object sender, EventArgs e)
        {
            WriteCsv();
        }

        private void BT_SchnellZurück_Click(object sender, EventArgs e)
        {
            if (index -5 < 0)
            {
                index = 0;
                MessageBox.Show("Liste am anfang!", "index anfang", MessageBoxButtons.OK, MessageBoxIcon.Error);
                zeigeFelder();
            }
            else
            {
                index -= 5;
                zeigeFelder();
            }
        
        }

        private void BT_Zurück_Click(object sender, EventArgs e)
        {
            if (index -1 < 0)
            {
                index = 0;
                MessageBox.Show("Liste am anfang!", "index anfang", MessageBoxButtons.OK, MessageBoxIcon.Error);
                zeigeFelder();
            }
            else
            {
                index--;
                zeigeFelder();                
            }
        }

        private void BT_Vorwärts_Click(object sender, EventArgs e)
        {
            if (index + 1 > mylist.Count -1)
            {
                index = mylist.Count- 1;
                MessageBox.Show("Liste am Ende!", "index ende", MessageBoxButtons.OK, MessageBoxIcon.Error);
                zeigeFelder();
            }
            else
            {
                index++;
                zeigeFelder();
            }
        }

        private void BT_Schnell_Vorwärts_Click(object sender, EventArgs e)
        {
            if (index + 5 > mylist.Count -1)
            {
                index = mylist.Count -1;
                MessageBox.Show("Liste am Ende!", "index ende", MessageBoxButtons.OK, MessageBoxIcon.Error);
                zeigeFelder();
            }
            else
            {
                index += 5;
                zeigeFelder();
            }
        }

        private void BTN_Person_HinzuFügen_Click(object sender, EventArgs e)
        {
            Personen me = new Personen();

            me.Anrede = TB_Anrede.Text;
            me.VorName = TB_VorName.Text;
            me.NachName = TB_Nachname.Text;
            me.Strasse = TB_Strasse.Text;
            me.PostLeitZahl = TB_PostleitZahl.Text;
            me.WohnOrt = TB_Wohnort.Text;
            me.Telefon = TB_Telefon.Text;
            me.Email = TB_Email.Text;

            mylist.Add(me);
            TextBoxClear();
        }

        private void BTN_Person_Anzeigen_Click(object sender, EventArgs e)
        {
            string content = "";
            foreach (var pers in mylist)
            {
                content += pers.Anrede + " " + pers.VorName + " " + pers.NachName + " " + pers.Strasse + " " + pers.PostLeitZahl + " " + pers.WohnOrt;
                content += Environment.NewLine;
            }
            RTB_Display.Text = content;
        }

        private void BTN_Clear_Click(object sender, EventArgs e)
        {
            TextBoxClear();
        }

        // Methoden

        private void WriteCsv()
        {
            string content = "";
            string trennzeichen = ";";

            if (mylist.Count > 0)
            {
                foreach (var pers in mylist)
                {
                    content += pers.Anrede + trennzeichen + pers.VorName + trennzeichen + pers.NachName + trennzeichen + pers.Strasse + trennzeichen + pers.PostLeitZahl + trennzeichen + pers.WohnOrt + trennzeichen + pers.Telefon + trennzeichen + pers.Email;
                    content += Environment.NewLine;
                }
                try
                {
                    File.WriteAllText(path, content);
                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                MessageBox.Show("Datei gespeichert", "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
            else MessageBox.Show("Liste noch leer!", "file not saved", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void ReadCsv()
        {
            string[] t_string;

            try
            {
                t_string = File.ReadAllLines(path);
                mylist.Clear();
                foreach (var satz in t_string)
                {
                    string[] t_personAtribute = satz.Split(';');
                    Personen t_person = new Personen();
                    t_person.Anrede = t_personAtribute[0];
                    t_person.VorName = t_personAtribute[1];
                    t_person.NachName = t_personAtribute[2];
                    t_person.Strasse = t_personAtribute[3];
                    t_person.PostLeitZahl = t_personAtribute[4];
                    t_person.WohnOrt = t_personAtribute[5];
                    t_person.Telefon = t_personAtribute[6];
                    t_person.Email = t_personAtribute[7];
                    mylist.Add(t_person);
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            MessageBox.Show("Datei geladen", "File loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TextBoxClear()
        {
            TB_Anrede.Clear();
            TB_VorName.Clear();
            TB_Nachname.Clear();
            TB_Strasse.Clear();
            TB_PostleitZahl.Clear();
            TB_Wohnort.Clear();
            TB_Telefon.Clear();
            TB_Email.Clear();
        }

        private void zeigeFelder()
        {
            TB_Anrede.Text = mylist[index].Anrede;
            TB_VorName.Text = mylist[index].VorName;
            TB_Nachname.Text = mylist[index].NachName;
            TB_Strasse.Text = mylist[index].Strasse;
            TB_PostleitZahl.Text = mylist[index].PostLeitZahl;
            TB_Wohnort.Text = mylist[index].WohnOrt;
            TB_Telefon.Text = mylist[index].Telefon;
            TB_Email.Text = mylist[index].Email;
        }

    }
}
