using System;
using System.Collections.Generic;
using System.Text;

namespace ADONETGeneric
{
    public class Student
    {
        #region Properties
        public int Id { get; set; }
        public string Naam { get; set; }
        public List<Cursus> Cursussen { get; private set; }
        public Klas Klas { get; set; }
        #endregion

        #region Ctor
        public Student(string naam, Klas klas)
        {
            this.Naam = naam;
            this.Klas = klas;
            this.Cursussen = new List<Cursus>();
        }

        public Student(int studentId, string naam, Klas klas)
        {
            this.Id = studentId;
            this.Naam = naam;
            this.Klas = klas;
            this.Cursussen = new List<Cursus>();
        }
        #endregion

        #region Methods
        public void VoegCursusToe(Cursus c)
        {
            Cursussen.Add(c);
        }

        public void ShowStudent()
        {
            Console.WriteLine($"{Id},{Naam},{Klas}");
            foreach (Cursus c in Cursussen)
            {
                Console.WriteLine($"{c}");
            }
        }
        #endregion
    }
}
