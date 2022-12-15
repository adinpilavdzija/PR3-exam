using DLWMS.WinForms.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DLWMS.WinForms
{
    public class Student
    {
        KonekcijaNaBazu _db = DLWMSdb.Baza;
        public int Id { get; set; }
        public string Indeks { get; set; }
        public int GodinaStudija { get; set; }
        // public Image Slika { get; set; }
        public byte[] Slika { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public bool Aktivan { get; set; }
        public override string ToString()
        {
            return $"{Ime} {Prezime}";
        }
        [NotMapped]
        public string ImePrezime { get; set; }
        [NotMapped]
        public float Prosjek { get; set; }
        public void ProsjecnaOcjena()
        {
            float suma = 0.00f;
            int brojac = 0;

            foreach (var instanca in _db.StudentiPredmeti)
            {
                if (instanca.Student.Id == Id)
                {
                    suma += instanca.Ocjena;
                    brojac++;
                }
            }
            if (brojac == 0)
                Prosjek = 5;
            else
                Prosjek = suma / brojac;
        }
    }
}