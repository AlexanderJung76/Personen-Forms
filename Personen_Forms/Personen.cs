using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personen_Forms
{
    class Personen
    {
        private string anRede, vorName, nachName, strasse, postLeitZahl, wohnOrt, telefon, email;

        public string Anrede
        {
            get { return anRede; }
            set { anRede = value; }
        }

        public string VorName
        {
            get { return vorName; }
            set { vorName = value; }
        }

        public string NachName
        {
            get { return nachName; }
            set { nachName = value; }
        }

        public string Strasse
        {
            get { return strasse; }
            set { strasse = value; }
        }

        public string PostLeitZahl
        {
            get { return postLeitZahl; }
            set { postLeitZahl = value; }
        }

        public string WohnOrt
        {
            get { return wohnOrt; }
            set { wohnOrt = value; }
        }

        public string Telefon
        {
            get { return telefon; }
            set { telefon = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        // Methoden

        // Konstruktoren

        public Personen()
        {

        }

        public Personen(string anrede, string v_Name, string n_Name, string strasse, string plz, string wohnort, string telefon, string email)
        {
            this.Anrede = anrede;
            this.VorName = v_Name;
            this.NachName = n_Name;            
            this.Strasse = strasse;
            this.PostLeitZahl = plz;
            this.WohnOrt = wohnort;
            this.Telefon = telefon;
            this.Email = email;
        }

        //Eigener Destruktor

        ~Personen()
        {

        }
    }
}
