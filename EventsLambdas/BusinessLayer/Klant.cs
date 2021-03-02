using System;

namespace BusinessLayer
{
    public class Klant
    {
        #region Properties
        public string Naam { get; set; }
        #endregion

        #region Ctor
        public Klant(string naam)
        {
            Naam = naam;
        }
        #endregion

        #region Methods
        public void Betaal(string product)
        {
            System.Diagnostics.Debug.WriteLine("Ik ben " + Naam + " en ik betaal: " + product);
        }

        public void Consumeer(string product)
        {
            System.Diagnostics.Debug.WriteLine("Ik ben " + Naam + " en ik speel binnen: " + product);
        }

        public void Bestel(Ober ober, string product)
        {
            if (ober == null || string.IsNullOrEmpty(product)) return; // preconditie

            ober.BrengBestelling(this, product);
        }
        #endregion
    }
}
