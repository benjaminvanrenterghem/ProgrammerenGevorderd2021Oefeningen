namespace Klantbeheer.Domain
{
    public class Student
    {
        #region Properties
        public int Age { get; set; }
        public string FirstName { get; set; }
        #endregion

        #region Ctor
        public Student() // constructor
        {
            Age = 18;
        }
        #endregion

        #region Dtor
        ~Student() // destructor
        {
            System.Diagnostics.Debug.WriteLine("Destroying student...");
        }
        #endregion
    }
}
