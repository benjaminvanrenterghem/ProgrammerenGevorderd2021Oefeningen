using Microsoft.Data.SqlClient;
using System;
using System.Data;

// CRUD: Create, Read, Update, Delete
// muget versionering: M.m.r Major: breaking change, minor: nieuwe features maar compatibel; revision: bug fixes, ...

namespace AdoNetLes
{
    class Program
    {
        /// <summary>
        /// Druk personen af op console
        /// </summary>
        /// <param name="c">Een open database connectie</param>
        public static void PrintPersonsTable(SqlConnection c)
        {
            SqlCommand command = new("SELECT Id, Naam, Email, Geboortedatum FROM Personen", c);
            using var dataReader = command.ExecuteReader(); // zet je klaar en geef een reader naar het resultaat  terug
            if (dataReader.HasRows) // indien er rijen zijn
            {
                while (dataReader.Read()) // we lezen rij per rij van de tabel die het resultaat is van de sql query
                {
                    var id = (int)dataReader["Id"]; // kolomnamen kunnen als index in de array gebruikt worden
                    var naam = (string)dataReader["Naam"]; // cast naar gewenst type is nodig
                    var email = (string)dataReader["Email"];
                    var geboortedatum = (DateTime)dataReader["Geboortedatum"];

                    Console.WriteLine($"{id} | {naam} | {email} | {geboortedatum.ToShortDateString()}");
                }
            }
            else { Console.WriteLine("Geen personen gevonden."); }
            //dataReader.Close(); // Niet meer nodig door using
        }

        /// <summary>
        /// We voegen een rij toe aan de Personen tabel
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        static public int AddPerson(string name, string email, SqlConnection conn)
        {
            var newProdID = 0;
            // sql query, maar met 3 parameters; een parameter heeft @ ervoor;cde parameternaam kies je zelf
            // SQL: twee queries in een
            string sql = "INSERT INTO Personen (Naam, Email, Geboortedatum) VALUES (@Name, @Email, @BirthDate); SELECT CAST(scope_identity() AS int)";

            // scope_identity() geeft de auto increment primary key terug die gemaakt geweest is in de scope bij het aanmaken van de nieuwe rij
            // door select statement geven we dit terug als getal

            // HET (!) grote voordeel is: slechts 1 round trip naar de database. Dit is veel sneller dan insert en terug select uitvoeren in aparte query.
            // NOG BELANGRIJKER: we krijgen altijd onze eigen nieuwe id terug, zelfs indien er andere programma's tegelijk rijen toevoegen aan de tabel.

            SqlCommand cmd = new(sql, conn);
            // kies bij een parameter steeds voor de in de sql query gebruikte naam - die met een @ ervoor
            // maak elke parameter bekend met zijn type en waarde
            cmd.Parameters.Add("@Name", SqlDbType.VarChar);
            cmd.Parameters.Add("@Email", SqlDbType.VarChar);
            cmd.Parameters.Add("@BirthDate", SqlDbType.DateTime);
            try
            {

                cmd.Parameters["@Name"].Value = name;
                cmd.Parameters["@Email"].Value = email;
                cmd.Parameters["@BirthDate"].Value = DateTime.Now;

                newProdID = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return newProdID;
        }

        private static void PrintAccountsData(SqlConnection conn)
        {
            SqlCommand cmd = new("Select * from Accounts", conn);
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                while (sdr.Read())
                {
                    Console.WriteLine(sdr["AccountNumber"] + ",  " + sdr["CustomerName"] + ",  " + sdr["Balance"]);
                }
            }
        }

        private static void MoneyTransfer(SqlConnection conn)
        {
            // Create the transaction object by calling the BeginTransaction method on connection object
            var transaction = conn.BeginTransaction();
            try
            {
                // Associate the first update command with the transaction
                SqlCommand cmd = new("UPDATE Accounts SET Balance = Balance - 500 WHERE AccountNumber = 'Account1'", conn, transaction);
                var numberOfRowsCommand1 = cmd.ExecuteNonQuery();
                // Associate the second update command with the transaction
                cmd = new SqlCommand("UPDATE Accounts SET Balance = Balance + 500 WHERE AccountNumber = 'Account2'", conn, transaction);
                var numberOfRowsCommand2 = cmd.ExecuteNonQuery();
                // If everythinhg goes well then commit the transaction
                if (numberOfRowsCommand1 == 1 && numberOfRowsCommand2 == 2)
                {
                    transaction.Commit();
                    Console.WriteLine("Transaction committed");
                }
                else
                {
                    transaction.Rollback();
                    Console.WriteLine("Transaction rolled back");
                }
            }
            catch
            {
                // If anything goes wrong, rollback the transaction
                transaction.Rollback();
                Console.WriteLine("Transaction rolled back");
            }
        }    
        
        static void PrintPlanes(SqlConnection conn, string airport)
        {
            // %: 0, 1 ... n karakters
            // ?: 0 of 1 karakter
            // LIKE '%@Name%' werkt niet! Het werkt wel als je de %-tekens concateneert; @Name moet herkend kunnen worden als parameter
            SqlCommand cmd = new("SELECT * FROM Plane, Airport where Plane.AirportId = Airport.Id AND Airport.Name LIKE '%' + @Name + '%'", conn);

            cmd.Parameters.Add("@Name", SqlDbType.VarChar);
            cmd.Parameters["@Name"].Value = airport;

            using SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                Console.WriteLine(sdr["Name"]);
            }
        }

        static void Add(SqlConnection conn, string[] values)
        {
            // Create the transaction object by calling the BeginTransaction method on connection object
            var transaction = conn.BeginTransaction();
            var ok = true;
            try
            {
                for (int i = 0; i < values.Length; i++)
                {
                    SqlCommand cmd = new(values[i], conn, transaction);
                    var numberOfRows = cmd.ExecuteNonQuery();
                    if (numberOfRows != 1)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Transaction rolled back");
                        ok = false;
                        break;
                    }
                }
                if (ok)
                    transaction.Commit();
            }
            catch
            {
                // If anything goes wrong, rollback the transaction
                transaction.Rollback();
                Console.WriteLine("Exception: transaction rolled back");
            }
        }

        static void AddAirports(SqlConnection conn)
        {
            string[] airports = new string[] {
                "INSERT INTO Airport(Name, City, Country) VALUES('Sa Carneiro', 'Porto', 'Portugal')",
                "INSERT INTO Airport(Name, City, Country) VALUES('Portela','Lisboa','Portugal')",
                "INSERT INTO Airport(Name, City, Country) VALUES('Faro','Faro','Portugal')",
                "INSERT INTO Airport(Name, City, Country) VALUES('Madeira','Funchal','Portugal')",
                "INSERT INTO Airport(Name, City, Country) VALUES('Ponta Delgada','S. Miguel','Portugal')",
                "INSERT INTO Airport(Name, City, Country) VALUES('Orly','Paris','France')",
                "INSERT INTO Airport(Name, City, Country) VALUES('Charles de Gaule','Paris','France')",
                "INSERT INTO Airport(Name, City, Country) VALUES('Heathrow','Londres','United Kingdom')",
                "INSERT INTO Airport(Name, City, Country) VALUES('Gatwick','Londres','United Kingdom')"
            };
            Add(conn, airports);
        }

        static void AddPlanes(SqlConnection conn)
        {
            // Via sub query id zoeken omdat deze door de autoincrement altijd oploopt
            string[] planes = new string[]
            {
                "INSERT INTO Plane(Name, AirportId) SELECT 'Douglas DC-10', id FROM Airport WHERE Name = 'Orly'",
                "INSERT INTO Plane(Name, AirportId) SELECT 'Boeing 737', id FROM Airport WHERE Name = 'Madeira'",
                "INSERT INTO Plane(Name, AirportId) SELECT 'Boeing 747', id FROM Airport WHERE Name = 'Orly'",
                "INSERT INTO Plane(Name, AirportId) SELECT 'Airbus A300', id FROM Airport WHERE Name = 'Gatwick'",
                "INSERT INTO Plane(Name, AirportId) SELECT 'Airbus A340', id FROM Airport WHERE Name = 'Faro'"
            };
            Add(conn, planes);
        }

        static void ClearPlaneDb(SqlConnection conn)
        {
            try
            {
                // -----------------
                // let op volgorde!! Veeg eerst de vliegtuigen uit, want anders kan je tabel Airport niet leegmaken door referentiele integriteitsregel
                // -----------------
                SqlCommand deleteCommand = new("DELETE FROM Plane", conn); // maak tabel Plane leeg
                var numberOfRows = deleteCommand.ExecuteNonQuery();
                deleteCommand = new("DELETE FROM Airport", conn);
                numberOfRows = deleteCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Delete faalde: " + e.Message);
            }
        }

        /*
         -- clustered index op id; slechts 1 clustered index per tabel in sqlserver; index: om vragen dramatisch te versnellen
         CREATE TABLE   Airport (
                        Id      INT            IDENTITY (1, 1) NOT NULL,
                        Name    NVARCHAR (500) NOT NULL,
                        City    NVARCHAR (128) NOT NULL,
                        Country NVARCHAR (128) NOT NULL,
                        PRIMARY KEY CLUSTERED (Id ASC)
         );

        CREATE TABLE Plane (
                        Id        INT            IDENTITY (1, 1) NOT NULL,
                        Name      NVARCHAR (128) NOT NULL,
                        AirportId INT            NOT NULL,
                        PRIMARY KEY CLUSTERED ([Id] ASC),
                        FOREIGN KEY (AirportId) REFERENCES Airport (Id)
        );
         */

        static void AirportAssignment()
        {
            // We maken van een groter probleem verschillende kleintjes...
            using (SqlConnection conn = new(@"Data Source=.\SQLEXPRESS;Initial Catalog=PlaneDb;Integrated Security=True;Pooling=False"))
            {                
                conn.Open();
                ClearPlaneDb(conn); // we maken de databank leeg
                PrintPlanes(conn, "Orly"); // we drukken alle vliegtuigen op luchthaven Orly af
                AddAirports(conn); // we voegen luchthavens toe aan de database
                AddPlanes(conn); // we voegen vliegtuigen toe aan de database
                PrintPlanes(conn, "Orly");
            }
        }

        static void Main(string[] args)
        {
            AirportAssignment();

            SqlConnection conn = null; // in dit geval zou using een betere optie zijn... 
            try
            {
                string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=PersonenDb;Integrated Security=True;Pooling=False";
                conn = new(connectionString); // C# 9.0
                conn.Open();
                PrintPersonsTable(conn);
                AddPerson("Luca", "luca.joos@student.hogent.be", conn);
                PrintPersonsTable(conn);

                try
                {
                    Console.Write("Aanpassen naam in Ahmed van persoon met id?: ");
                    string updateSql = $"UPDATE Personen SET Naam = 'Ahmed' WHERE Id = {Console.ReadLine()}";
                    SqlCommand updateCommand = new(updateSql, conn);
                    var numberOfRows = updateCommand.ExecuteNonQuery(); // eender welke insert/update/delete query kan je hiermee uitvoeren

                    PrintPersonsTable(conn);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Update faalde: " + e.Message);
                }

                try
                {
                    Console.WriteLine("Invoegen nieuwe persoon met naam Laura, geboortedatum 10 oktober 1979...");
                    string insertSql = $"INSERT INTO Personen (Naam, Email, Geboortedatum) VALUES ('Laura', 'laura@mail.be', '1979-10-31')";
                    SqlCommand insertCommand = new(insertSql, conn);
                    var numberOfRows = insertCommand.ExecuteNonQuery();

                    PrintPersonsTable(conn);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Insert faalde: " + e.Message);
                }

                try
                {
                    Console.Write("Verwijderen persoon met id?: ");
                    string deleteSql = $"DELETE FROM Personen WHERE Id = {Console.ReadLine()}";
                    SqlCommand deleteCommand = new(deleteSql, conn);
                    var numberOfRows = deleteCommand.ExecuteNonQuery();
                    if(numberOfRows <= 0)
                    {
                        Console.WriteLine("Kon rij niet vinden om deze te verwijderen");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Delete faalde: " + e.Message);
                }

                PrintAccountsData(conn);
                MoneyTransfer(conn);
                PrintAccountsData(conn);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally // wordt altijd uitgevoerd - bij exception en ook zonder!
            {
                conn.Close(); // zo wordt de database connection altijd afgesloten
            }
        }
    }
}
