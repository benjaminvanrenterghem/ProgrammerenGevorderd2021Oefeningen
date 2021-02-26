using System;
using WinkelManagement.Events;

namespace WinkelManagement
{
    public class Winkel
    {
        #region Events
        public event EventHandler<WinkelEventArgs> ProductVerkocht; // Event
        #endregion

        #region Methods
        public void VerkoopProduct(Bestelling b)
        {
            OnProductVerkocht(b);
        }

        protected virtual void OnProductVerkocht(Bestelling b)
        {
            ProductVerkocht?.Invoke(this, new WinkelEventArgs(b));
        }
        #endregion
    }
}
