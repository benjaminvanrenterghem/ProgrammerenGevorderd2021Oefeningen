using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace AdoNetConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // StudentDB
            try
            {
                string ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=StudentDB;Integrated Security=True;Pooling=False";
                using (var connection = new SqlConnection(ConnectionString))
                {
                    // Geen Open() meer op de connectie! Dit is daarom "disconnected"
                    var dataAdapter = new SqlDataAdapter("SELECT * FROM student", connection);
                    var dataTable = new DataTable();
                    dataAdapter.Fill(dataTable); // ik vraag adapter om de data table op te vullen

                    // DataTable is een copie in RAM van de tabel in de database
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Console.WriteLine(row["Name"] + ",  " + row["Email"] + ",  " + row["Mobile"]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong.\n" + e);
            }

            // PersonenDb
            try
            {
                string ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=PersonenDb;Integrated Security=True;Pooling=False";
                using (var connection = new SqlConnection(ConnectionString))
                {
                    // Geen Open() meer op de connectie! Dit is daarom "disconnected"
                    var dataAdapter = new SqlDataAdapter("SELECT * FROM Personen", connection);
                    var dataTable = new DataTable();
                    dataAdapter.Fill(dataTable); // ik vraag adapter om de data table op te vullen

                    // DataTable is een copie in RAM van de tabel in de database
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Console.WriteLine(row["Naam"] + ",  " + row["Email"] + ",  " + row["Geboortedatum"]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong.\n" + e);
            }

            Console.ReadKey();
        }
    }
}