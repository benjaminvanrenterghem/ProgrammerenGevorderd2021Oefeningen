using System;

namespace KleineOefeningen
{
    public class Medic : Soldier
    {
        // voert eerst constructor van base class uit
        public Medic() /*: base()*/ { Console.WriteLine("Who needs healing?"); }
        public Medic(bool canShoot) : base(canShoot) { Console.WriteLine("I need healing!"); }
    }
}
