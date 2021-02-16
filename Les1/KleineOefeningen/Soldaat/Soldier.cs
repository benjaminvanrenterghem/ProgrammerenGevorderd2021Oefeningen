using System;

namespace KleineOefeningen
{
    public class Soldier
    {
        private bool _canShoot = false;

        public Soldier() { Initialize(); }

        public Soldier(bool canShoot)
        {
            _canShoot = canShoot;
            Initialize();
        }

        private void Initialize()
        {
            Console.WriteLine("Soldier reporting in");
        }
    }
}
