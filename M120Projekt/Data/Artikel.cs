using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace M120Projekt.Data
{
    public class Artikel
    {
        #region Datenbankschicht
        [Key]
        public Int64 ArtikelID { get; set; }
        [Required]
        public String Bezeichnung { get; set; }
        [Required]
        public Int32 Anzahl { get; set; }
        [Required]
        public String Kategorie { get; set; }
        [Required]
        public DateTime KaufenBis { get; set; }
        [Required]
        public Boolean Eingekauft { get; set; }
        #endregion
        #region Applikationsschicht
        public Artikel() { }
        [NotMapped]
        public String BerechnetesAttribut
        {
            get
            {
                return "Im Getter kann Code eingefügt werden für berechnete Attribute";
            }
        }
        public static List<Artikel> LesenAlle()
        {
            using (var db = new Context())
            {
                return (from record in db.Artikel select record).ToList();
            }
        }
        public static Artikel LesenID(Int64 klasseAId)
        {
            using (var db = new Context())
            {
                return (from record in db.Artikel where record.ArtikelID == klasseAId select record).FirstOrDefault();
            }
        }
        public static List<Artikel> LesenAttributGleich(String suchbegriff)
        {
            using (var db = new Context())
            {
                return (from record in db.Artikel where record.Bezeichnung == suchbegriff select record).ToList();
            }
        }
        public static List<Artikel> LesenAttributWie(String suchbegriff)
        {
            using (var db = new Context())
            {
                return (from record in db.Artikel where record.Bezeichnung.Contains(suchbegriff) select record).ToList();
            }
        }
        public Int64 Erstellen()
        {
            if (this.Bezeichnung == null || this.Bezeichnung == "") this.Bezeichnung = "leer";
            if (this.KaufenBis == null) this.KaufenBis = DateTime.MinValue;
            using (var db = new Context())
            {
                db.Artikel.Add(this);
                db.SaveChanges();
                return this.ArtikelID;
            }
        }
        public Int64 Aktualisieren()
        {
            using (var db = new Context())
            {
                db.Entry(this).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return this.ArtikelID;
            }
        }
        public void Loeschen()
        {
            using (var db = new Context())
            {
                db.Entry(this).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }
        public override string ToString()
        {
            return ArtikelID.ToString(); // Für bessere Coded UI Test Erkennung
        }
        #endregion
    }
}
