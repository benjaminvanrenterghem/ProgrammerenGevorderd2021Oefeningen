// HOGENT
using BusinessLayer.Events;
using System;

namespace BusinessLayer
{
    public class BestellingsSysteem
    {
        #region Events
        public event EventHandler<BestelEventArgs> BestellingEvent;
        #endregion

        #region Methods
        public void GeefBestellingIn(BestelEventArgs args)
        {
            /*
            if (BestellingEvent != null)
                BestellingEvent(this, args);
            */
            BestellingEvent?.Invoke(this, args);
        }
        #endregion
    }
}
