namespace ADONETGeneric
{
    public class Klas
    {
        #region Properties
        public int Id { get; set; }
        public string Naam { get; set; }
        #endregion

        #region Ctor
        public Klas(string naam)
        {
            this.Naam = naam;
        }

        public Klas(int id, string naam)
        {
            this.Id = id;
            this.Naam = naam;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"[Klas:{Id},{Naam}]";
        }
        #endregion
    }
}
