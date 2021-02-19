
namespace ExceptionDemo
{
    public interface IBeing { /*bool IsAlive { get; set; }*/ }

    public interface IMan: IBeing // contract
    {
        #region Methods
        void Slaap();
        void WordtWakker();
        #endregion
    }

    public interface IWoman: IBeing
    {

    }
}
