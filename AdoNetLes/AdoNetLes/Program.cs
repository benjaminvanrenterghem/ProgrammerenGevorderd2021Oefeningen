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

        static void Main(string[] args)
        {
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
