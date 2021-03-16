namespace ADONETGeneric
{
    public class Cursus
    {
        #region Properties
        public int Id { get; set; }
        public string Naam { get; set; }
        #endregion

        #region Ctor
        public Cursus(string naam)
        {
            this.Naam = naam;
        }

        public Cursus(int id, string naam)
        {
            this.Id = id;
            this.Naam = naam;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"Cursus[{Id},{Naam}]";
        }
        #endregion
    }
}
