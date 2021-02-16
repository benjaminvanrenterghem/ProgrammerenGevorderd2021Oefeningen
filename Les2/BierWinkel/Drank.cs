namespace BierWinkel2
{
    public class Drank
    {
        public double PrijsPerStuk { get; set; }
        public string Naam { get; set; }
        public int MinimumHoeveelheid { get; set; }

        public override string ToString()
        {
            return $"{PrijsPerStuk}, {Naam}, {MinimumHoeveelheid}";
        }
    }
}
