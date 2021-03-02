// HOGENT
using BusinessLayer.Events;
using System;

namespace BusinessLayer
{
    // Publisher kant: stelt event beschikbaar en roept deze op
    public class Bel
    {
        #region Events
        // Generiek event mechanisme: EventHandler<> met 1 parameter die typisch altijd Args in de naam heeft; ik moet geen apart delegate type definieren
        public event EventHandler<BestelEventArgs> RingEvent;
        #endregion

        #region Methods
        public void Ring(BestelEventArgs args)
        {
            //if (RingEvent != null)
            //    RingEvent(this, args);
            RingEvent?.Invoke(this, args); // verkort uitgewerkt; een oproep van de generieke event EventHandler impliceert altijd als eerste parameter jezelf (this)
        }
        #endregion
    }
}
