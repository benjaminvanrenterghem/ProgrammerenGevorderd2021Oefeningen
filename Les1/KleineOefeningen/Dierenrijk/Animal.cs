using System;

namespace KleineOefeningen
{
    public class Animal
    {
        #region Properties
        public bool CanMove { get; set; } = true;
        protected int _weight = 0;
        #endregion Properties

        #region Methods
        public virtual void ToonInfo()
        {
            Console.WriteLine($"Kan voortbewegen: {CanMove} ({_weight})");
        }
        #endregion
    }
}
