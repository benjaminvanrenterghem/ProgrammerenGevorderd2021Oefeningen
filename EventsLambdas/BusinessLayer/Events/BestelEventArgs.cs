// HOGENT
using System;

namespace BusinessLayer.Events
{
    // Enkel public property members
    public class BestelEventArgs : EventArgs // voorwaarde: moet overerven van klasse EventArgs
    {
        public string KlantNaam { get; set; }

        public string ProductNaam { get; set; }
    }
}
