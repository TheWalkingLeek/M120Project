using System;
using System.Diagnostics;

namespace M120Projekt
{
    static class APIDemo
    {
        #region Artikel
        // Create
        public static void ArtikelCreate()
        {
            Debug.Print("--- DemoACreate ---");
            // Artikel
            Data.Artikel ArtikelA1 = new Data.Artikel();
            ArtikelA1.Bezeichnung = "Brot";
            ArtikelA1.Kategorie = "Lebensmittel";
            ArtikelA1.Anzahl = 1;
            ArtikelA1.KaufenBis = DateTime.Today;
            ArtikelA1.Eingekauft = false;
            Int64 ArtikelA1Id = ArtikelA1.Erstellen();
            Debug.Print("Artikel erstellt mit Id:" + ArtikelA1Id);
        }
        public static void ArtikelCreateKurz()
        {
            Data.Artikel ArtikelA2 = new Data.Artikel { Bezeichnung = "Drucker", Kategorie = "Elektronik", Anzahl = 1, Eingekauft = true, KaufenBis = DateTime.Today };
            Int64 ArtikelA2Id = ArtikelA2.Erstellen();
            Debug.Print("Artikel erstellt mit Id:" + ArtikelA2Id);
        }

        // Read
        public static void ArtikelRead()
        {
            Debug.Print("--- DemoARead ---");
            // Demo liest alle
            foreach (Data.Artikel klasseA in Data.Artikel.LesenAlle())
            {
                Debug.Print("Artikel Id:" + klasseA.ArtikelID + " Name:" + klasseA.Bezeichnung);
            }
        }
        // Update
        public static void ArtikelUpdate()
        {
            Debug.Print("--- DemoAUpdate ---");
            // KlasseA ändert Attribute
            Data.Artikel klasseA1 = Data.Artikel.LesenID(1);
            klasseA1.Bezeichnung = "Artikel 1 nach Update";
            klasseA1.Aktualisieren();
        }
        // Delete
        public static void ArtikelDelete()
        {
            Debug.Print("--- DemoADelete ---");
            Data.Artikel.LesenID(2).Loeschen();
            Debug.Print("Artikel mit Id 2 gelöscht");
        }
        #endregion
    }
}
