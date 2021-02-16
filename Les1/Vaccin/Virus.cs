using System;

namespace Vaccination
{

    public class Virus
    {
        #region Properties
        public string Naam { get; private set; }

        private int _doomCountDown;

        public int DoomCountDown
        {
            get { return _doomCountDown; }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Game Over");
                }
                _doomCountDown = value;
            }
        }

        private int _killcode;
        static Random _r = new Random();
        #endregion

        public Virus()
        {
            DoomCountDown = _r.Next(100, 200);
            _killcode = _r.Next(0, 99);

            for (int i = 0; i < 3; i++)
            {
                Naam += (char)_r.Next(65, 91); // A-Z, ascii code: van getal naar ascii: bijvoorbeeld https://www.ascii-code.com/
            }
            Naam += _r.Next(1, 100);
        }

        public bool TryVaccin(Vaccin vaccin)
        {
            var ktest = vaccin.TryKillCode();
            if (ktest == _killcode)
            {
                vaccin.Oplossing = ktest;
                return true;

            }
            DoomCountDown--;
            return false;
        }
    }
}
