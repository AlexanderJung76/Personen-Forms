using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Personen_Forms
{
    public partial class Personen_Window : Form
    {
        private List<Personen> mylist = new List<Personen>();
        //private string path = @"c:\test\PersonenListenText.csv";
        private int index = 0;
        static MySqlConnection myConnection;
        static MySqlCommand myCommand;
        static string myConnectionString = "SERVER=127.0.0.1;" + "DATABASE=my indisoft;" + "USER ID=Alex;" + "PASSWORD=TestME;";


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
                ZeigeFelder();
            }
            else
            {
                index -= 5;
                ZeigeFelder();
            }
        
        }

        private void BT_Zurück_Click(object sender, EventArgs e)
        {
            if (index -1 < 0)
            {
                index = 0;
                MessageBox.Show("Liste am anfang!", "index anfang", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ZeigeFelder();
            }
            else
            {
                index--;
                ZeigeFelder();                
            }
        }

        private void BT_Vorwärts_Click(object sender, EventArgs e)
        {
            if (index + 1 > mylist.Count -1)
            {
                index = mylist.Count- 1;
                MessageBox.Show("Liste am Ende!", "index ende", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ZeigeFelder();
            }
            else
            {
                index++;
                ZeigeFelder();
            }
        }

        private void BT_Schnell_Vorwärts_Click(object sender, EventArgs e)
        {
            if (index + 5 > mylist.Count -1)
            {
                index = mylist.Count -1;
                MessageBox.Show("Liste am Ende!", "index ende", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ZeigeFelder();
            }
            else
            {
                index += 5;
                ZeigeFelder();
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

        private void BTN_Person_Entfernen_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("Achtung Eintrag wird gelöscht!", "Warnung!!!",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                mylist.RemoveAt(index);
                if (index + 1 < mylist.Count - 1)
                {
                    index++;
                }
                else index--;
                ZeigeFelder();
            }                
        }

        private void BTN_Clear_Click(object sender, EventArgs e)
        {
            TextBoxClear();
        }

        // Methoden

        private void WriteCsv()
        {
   
            myConnection = new MySqlConnection(myConnectionString);
            myCommand = myConnection.CreateCommand();

            if (mylist.Count > 0)
            {
                try
                {
                    myConnection.Open();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                foreach (var pers in mylist)
                {
                    myCommand.CommandText = "INSERT INTO person (anrede, vorname, nachname, strasse, postleitzahl, ort, telefon, email)"
                        + "VALUES('" + pers.Anrede + "', '" + pers.VorName + "', '" + pers.NachName + "', '" + pers.Strasse + "', '" + pers.PostLeitZahl + "', '" + pers.WohnOrt + "','" + pers.Telefon + "','" + pers.Email + "');";

                    myCommand.ExecuteNonQuery();
                    //MessageBox.Show("Datenbank gespeichert", "DB saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                myConnection.Close();

            }
            else MessageBox.Show("Liste noch leer!", "file not saved", MessageBoxButtons.OK, MessageBoxIcon.Error);            
        }

        private void ReadCsv()
        {            
            myConnection = new MySqlConnection(myConnectionString);
            myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "Select * from person;";

            MySqlDataReader myReader;

            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                Personen t_person = new Personen();
                string row = "";
                for (int i = 0; i < myReader.FieldCount; i++)
                {                    
                    t_person.Anrede = myReader.GetValue(1).ToString();
                    t_person.VorName = myReader.GetValue(2).ToString();
                    t_person.NachName = myReader.GetValue(3).ToString();
                    t_person.Strasse = myReader.GetValue(4).ToString();
                    t_person.PostLeitZahl = myReader.GetValue(5).ToString();
                    t_person.WohnOrt = myReader.GetValue(6).ToString();
                    t_person.Telefon = myReader.GetValue(7).ToString();
                    t_person.Email = myReader.GetValue(8).ToString();                    
                }
                mylist.Add(t_person);
                Console.WriteLine(row);
            }
            myConnection.Close();
            MessageBox.Show("Datenbank geladen", "DB loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void ZeigeFelder()
        {
            if (mylist.Count > 0)    // need to replace an better check if list is empty before display, added for scroll feature
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
            else MessageBox.Show("Liste noch leer!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        

    }
}
