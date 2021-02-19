
namespace ExceptionDemo
{
    public interface ISuperHeld // "superheld contract"
    {
        #region Properties
        int Power { get; set; }
        #endregion

        #region Methods
        void SchietLasers();
        int VerlaagKracht(bool isZwak);
        #endregion
    }
}
