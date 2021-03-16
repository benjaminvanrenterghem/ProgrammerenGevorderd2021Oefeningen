using System;
using System.Collections.Generic;

namespace ADONETsqlserver
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=AdresBeheer;Integrated Security=True";
            
            DataBeheer db = new(connectionString);

            //voeg cursussen toe
            Cursus c1 = new("Web1");
            Cursus c2 = new("Web2");
            Cursus c3 = new("Programmeren3");
            db.VoegCursusToe(c1);
            db.VoegCursusToe(c2);
            db.VoegCursusToe(c3);
            Cursus c4 = new Cursus("Programmeren 123");
            db.VoegCursusToe(c4);
            Cursus c5 = new Cursus("Programmeren 223");
            db.VoegCursusToe(c5);
            Cursus c6 = new Cursus("Programmeren 323");
            db.VoegCursusToe(c6);

            //lees cursussen
            foreach (Cursus eenCursus in db.GeefCursussen())
            {
                Console.WriteLine($"{eenCursus}");
            }

            //voeg klas toe
            Klas k1 = new Klas("1D");
            Klas k2 = new Klas("2A");
            Klas k3 = new Klas("1A");
            Klas k4 = new Klas("1B");
            Klas k5 = new Klas("1C");
            List<Klas> kl = new List<Klas>() { k1, k2, k3, k4 };
            db.VoegKlassenToe(kl);
            db.VoegKlasToe(k5);

            //voeg student toe
            Klas k = db.GeefKlas(4);
            Student s1 = new Student("Inge", k);
            db.VoegStudentToe(s1);
            //Klas k = db.GeefKlas(4);
            Student s2 = new Student("Marcel", k);
            db.VoegStudentToe(s2);


            db.KoppelCursusAanStudent(2, new List<int>() { 1, 3 });


            Klas k6 = db.GeefKlas(3);
            Student smc = new Student("Eli", k6);
            smc.Cursussen.AddRange(db.GeefCursussen());
            db.VoegStudentMetCursussenToe(smc);

            Student s = db.GeefStudent(3);
            s.ShowStudent();

            db.VerwijderCursussen(new List<int>() { 7, 8, 9 });

            Cursus cursus = db.GeefCursus(4);
            cursus.Naam = "Programmeren c#";
            db.UpdateCursus(cursus);           
        }
    }
}
