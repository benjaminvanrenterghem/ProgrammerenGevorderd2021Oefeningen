namespace Ziekenhuis
{
    public class VerzekerdePatient: Patient
    {
        #region Properties
        private const double _korting = 0.1; // Procent korting is zo: 10%
        #endregion

        #region Methods
        public override double BerekenKost()
        {
            var totaalBasisKost = base.BerekenKost();
            return totaalBasisKost - (totaalBasisKost * _korting);
        }
        #endregion
    }
}
