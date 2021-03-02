// HOGENT

using BusinessLayer.Events;

namespace BusinessLayer.Interfaces
{
    public interface IBel
    {
        void Ring(object sender, BestelEventArgs args);
    }
}
