using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace SportsStore.DAL
{   
    public static class City
    {
        #region Properties
        private static string _connStr = @"Data Source=.\SQLEXPRESS;Initial Catalog=SportsStore;Integrated Security=True";// ConfigurationManager.ConnectionStrings["SportsStoreConn"].ToString();
        #endregion

        // CRUD: Create Read Update Delete
        // ... met ADO .NET: Create -> insert, READ -> select, update -> update, delete -> delete

        /// <summary>
        /// Insert new city row in database
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public static int Insert(Contracts.City city)
        {
            var id = 0;
            string sql = "INSERT INTO City ( Name, PostalCode ) VALUES ( '" + city.Name + "', '" + city.PostalCode + "' ); "
                + "SELECT CAST(scope_identity() AS int);";
            // output INSERTED.ID typisch Microsoft SQL Server
            using (var conn = new SqlConnection(_connStr))
            {
                var cmd = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    id = (int)cmd.ExecuteScalar();
                    city.Id = id;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Insert City failed: " + ex.Message);
                    throw;
                }
            }
            return id;
        }

        /// <summary>
        /// Wanneer we meer cities in de database steken, dan heeft het gebruik van parameters zeker voordelen (snelheid)
        /// </summary>
        /// <param name="cities"></param>
        /// <returns></returns>
        public static int Insert(List<Contracts.City> cities)
        {
            Int32 id = 0;
            string sql = "INSERT INTO City ( Name, PostalCode ) OUTPUT INSERTED.ID VALUES ( @Name, @PostalCode )";
            using (var conn = new SqlConnection(_connStr))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                cmd.Parameters.Add("@PostalCode", System.Data.SqlDbType.NVarChar);
                try
                {
                    conn.Open();
                    foreach (var city in cities)
                    {
                        cmd.Parameters["@Name"].Value = city.Name;
                        cmd.Parameters["@PostalCode"].Value = city.PostalCode;
                        id = (Int32)cmd.ExecuteScalar();
                        city.Id = id;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            return id;
        }

        /// <summary>
        /// We werken hier zonder using: let op finally!!
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public static bool Select(Contracts.City city)
        {
            // Preconditions
            if (city.Id == 0 && string.IsNullOrEmpty(city.Name))
                return false;

            // enkel na de preconditie heeft het zin om door te gaan:
            //var count = 0;
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = "SELECT Id, Name, PostalCode FROM City WHERE ";
                if (city.Id != 0)
                    sql += " Id = " + city.Id;
                else // if (!string.IsNullOrEmpty(city.Name))
                    sql += " Name = '" + city.Name + "'";
                // else
                //    return false;

                var command = new SqlCommand(sql, conn);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        city.Id = (int)dataReader["Id"];
                        city.Name = (string)dataReader["Name"];
                        city.PostalCode = (string)dataReader["PostalCode"];
                        break; // neem eerste city in plaats van laatste
                        //++count;
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Select city failed: " + e.Message);
                throw; // Best practice: vang exception op en propageer deze door - app zal stoppen
            }
            finally
            {
                //conn.Close(); // niet expliciet nodig maar mag: wordt uitgevoerd door Dispose
                conn.Dispose();
            }
            return false;
        }

        /// <summary>
        /// Update city record into database
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public static void Update(Contracts.City city)
        {
            // Precondities:
            if (city.Id == 0)
                throw new Exception("Cannot update City: id unknown");

            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = $"UPDATE City SET Name = '" + city.Name + "', PostalCode ='" + city.PostalCode + "' WHERE Id = " + city.Id;
                var updateCommand = new SqlCommand(sql, conn);
                var numberOfAffectedRows = updateCommand.ExecuteNonQuery();
                if (numberOfAffectedRows != 1)
                    throw new Exception("City not deleted");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                //conn.Close();
                conn.Dispose();
            }
        }

        /// <summary>
        /// Delete city row from database
        /// </summary>
        /// <param name="city"></param>
        public static void Delete(Contracts.City city)
        {
            // Precondities:
            if (city.Id == 0)
                throw new Exception("Cannot delete City: id unknown");

            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = $"DELETE FROM City WHERE CityId = " + city.Id;
                var updateCommand = new SqlCommand(sql, conn);
                var numberOfAffectedRows = updateCommand.ExecuteNonQuery();
                if (numberOfAffectedRows != 1)
                    throw new Exception("City not deleted");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                //conn.Close();
                conn.Dispose();
            }
        }

        // Makkelijk om te coderen op hoger niveau: Exists() voorzien; zit object wel in database of niet? Dit laat toe verschil te maken tussen insert en update
        /// <summary>
        /// Get row from database
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public static bool Exists(Contracts.City city)
        {
            // Precondities: voorwaarden die voldaan moeten zijn om de method te kunnen uitvoeren
            if (city.Id <= 0 && string.IsNullOrEmpty(city.Name)) return false;

            //int count = 0;
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = "SELECT Id, Name, PostalCode FROM City WHERE ";
                if (city.Id != 0)
                    sql += " Id = " + city.Id;
                else if (!string.IsNullOrEmpty(city.Name))
                    sql += " Name = '" + city.Name + "'";
                var command = new SqlCommand(sql, conn);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        city.Id = (int)dataReader[0];
                        city.Name = (string)dataReader[1];
                        city.PostalCode = (string)dataReader[2];
                        //++count;
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                //conn.Close();
                conn.Dispose();
            }
            return false;
        }

    }
}
