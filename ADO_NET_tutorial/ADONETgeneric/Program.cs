using ADONETGeneric;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace ADONETtest
{
    class Program
    {
        static void Main(string[] args)
        {
            DbProviderFactories.RegisterFactory("sqlserver", SqlClientFactory.Instance);
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=AdresBeheer;Integrated Security=True";
            DbProviderFactory sqlFactory = DbProviderFactories.GetFactory("sqlserver");

            DataBeheer db = new(sqlFactory,connectionString);

            ////voeg cursussen toe
            Cursus c1 = new("Web1");
            Cursus c2 = new("Web2");
            Cursus c3 = new("Programmeren3");
            db.VoegCursusToe(c1);
            db.VoegCursusToe(c2);
            db.VoegCursusToe(c3);
            Cursus c4 = new("Programmeren 123");
            db.VoegCursusToe(c4);
            Cursus c5 = new("Programmeren 223");
            db.VoegCursusToe(c5);
            Cursus c6 = new("Programmeren 323");
            db.VoegCursusToe(c6);

            //lees cursussen
            foreach(Cursus eenCursus in db.GeefCursussen())
            {
                Console.WriteLine($"{eenCursus}");
            }

            //voeg klas toe
            Klas k1 = new("1D");
            Klas k2 = new("2A");
            List<Klas> kl = new() { k1, k2 };
            db.VoegKlassenToe(kl);
            //db.VoegKlasToe(k1);

            //voeg student toe
            Klas k = db.GeefKlas(4);
            Student s1 = new Student("Inge", k);
            db.VoegStudentToe(s1);
            //Klas k = db.GeefKlas(4);
            Student s2 = new Student("Marcel", k);
            db.VoegStudentToe(s2);

            db.KoppelCursusAanStudent(2, new List<int>() { 1, 3 });

            //Klas k = db.GeefKlas(3);
            Student smc = new Student("Eli", k);
            smc.Cursussen.AddRange(db.GeefCursussen());
            db.VoegStudentMetCursussenToe(smc);

            Student s = db.GeefStudent(10);
            s.ShowStudent();

            db.VerwijderCursussen(new List<int>() { 5, 6, 7 });

            Cursus cursus = db.GeefCursus(4);
            cursus.Naam = "Programmeren c#";
            db.UpdateCursus(cursus);
        }
    }
}
