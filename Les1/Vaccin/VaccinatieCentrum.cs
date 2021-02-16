namespace Vaccination
{
    public class VaccinatieCentrum
    {
        #region Properties
        public static int Oplossing { get; set; } = -1;
        #endregion

        #region Methods
        public static void BewaarVaccin(int killCode)
        {
            Oplossing = killCode;
        }

        public Vaccin GeefVaccin()
        {
            if (Oplossing == -1) return null;

            return new Vaccin("DEOPLOSSING", Oplossing);
        }
        #endregion
    }
}
