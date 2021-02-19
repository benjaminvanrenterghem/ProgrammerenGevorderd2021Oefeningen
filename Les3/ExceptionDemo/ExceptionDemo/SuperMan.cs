
namespace ExceptionDemo
{

    // interfaces zonder implementatie werkt ook:
    public interface IX { }
    public interface IY { }
    public interface IZ { }

    // we kunnen niet van twee klassen overerven:
    public class LevendWezen
    {

    }

    // we kunnen wel meer dan een enkele interface opleggen als contract aan een klasse:
    public class SuperMan : Man, /* LevendWezen */ ISuperHeld, IMan, IX, IY, IZ
    {
        #region ISuperHeld

        public void SchietLasers()
        {
        }
        public int VerlaagKracht(bool isZwak)
        {
            return 0;
        }        
        #endregion

        #region IMan
        public void Slaap()
        {
        }

        public void WordtWakker()
        {
        }
        #endregion
    }
}
