using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace SportsStore.DAL
{
    public static class Customer
    {
        #region Fields
        private static string _connStr = ConfigurationManager.ConnectionStrings["SportsStoreConn"].ToString();
        #endregion

        /// <summary>
        /// Update record into database
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static void Update(Contracts.Customer customer)
        {
            var conn = new SqlConnection(_connStr);
            try
            {
                UpsertCity(customer);
                conn.Open();
                string sql = $"UPDATE Customer SET CustomerName = '" + customer.CustomerName + "', Name ='" + customer.Name + "', FirstName = '" + customer.FirstName + "', Street = '" + customer.Street + "', CityId = " + customer.City.Id + " WHERE Id = " + customer.Id;
                var updateCommand = new SqlCommand(sql, conn);
                updateCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        private static void UpsertCity(Contracts.Customer customer) /// upsert: concatenatie van insert + update
        {
            // First insert/update the city
            if (!DAL.City.Exists(customer.City))
            {
                DAL.City.Insert(customer.City);
            }
            else
            {
                DAL.City.Update(customer.City);
            }
        }

        /// <summary>
        /// Insert new row in database
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static int Insert(Contracts.Customer customer)
        {
            UpsertCity(customer);
            Int32 id = 0;
            string sql = "INSERT INTO Customer ( CustomerName, Name, FirstName, Street, CityId ) VALUES ( '" + customer.CustomerName + "', '" + customer.Name + "', '" + customer.FirstName + "', '" + customer.Street + "', " + customer.City.Id + " ); "
                + "SELECT CAST(scope_identity() AS int);";
            // output INSERTED.ID typisch Microsoft SQL Server
            using (var conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    id = (Int32)cmd.ExecuteScalar();
                    customer.Id = id;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return id;
        }

        public static int Insert(List<Contracts.Customer> customers)
        {
            Int32 id = 0;
            string sql = "INSERT INTO Customer ( CustomerName, Name, FirstName, Street, CityId ) OUTPUT INSERTED.ID VALUES ( @CustomerName, @Name, @FirstName, @Street, @CityId )";
            using (var conn = new SqlConnection(_connStr))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@CustomerName", SqlDbType.NVarChar);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                cmd.Parameters.Add("@FirstName", System.Data.SqlDbType.NVarChar);
                cmd.Parameters.Add("@Street", System.Data.SqlDbType.NVarChar);
                cmd.Parameters.Add("@CityId", SqlDbType.Decimal);
                try
                {
                    conn.Open();
                    foreach (var customer in customers)
                    {
                        UpsertCity(customer);
                        cmd.Parameters["@CustomerName"].Value = customer.CustomerName;
                        cmd.Parameters["@Name"].Value = customer.Name;
                        cmd.Parameters["@FirstName"].Value = customer.FirstName;
                        // Store city if city id not available
                        DAL.City.Select(customer.City);
                        if (customer.City.Id == 0)
                            DAL.City.Insert(customer.City);
                        cmd.Parameters["@CityId"].Value = customer.City.Id;
                        id = (Int32)cmd.ExecuteScalar();
                        customer.Id = id;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return id;
        }

        /// <summary>
        /// Delete row from database
        /// </summary>
        /// <param name="customer"></param>
        public static void Delete(Contracts.Customer customer)
        {
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = $"DELETE FROM Customer WHERE Id = " + customer.Id;
                var updateCommand = new SqlCommand(sql, conn);
                updateCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public static bool Exists(Contracts.Customer customer)
        {
            if (customer.Id <= 0 && string.IsNullOrEmpty(customer.Name)) return false;

            //int count = 0;
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = "SELECT ID FROM Customer WHERE ";
                if (customer.Id != 0)
                    sql += " Id = " + customer.Id;
                else if (!string.IsNullOrEmpty(customer.Name))
                    sql += " Name = '" + customer.Name + "'";
                var command = new SqlCommand(sql, conn);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        customer.Id = (int)dataReader[0];
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

        /// <summary>
        /// Get row from database
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static bool Select(Contracts.Customer customer)
        {
            int count = 0;
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = "SELECT Id, Name, CustomerName, FirstName, Street, CityId FROM Customer WHERE ";
                if (customer.Id != 0)
                    sql += " Id = " + customer.Id;
                else if (!string.IsNullOrEmpty(customer.Name))
                    sql += " Name = '" + customer.Name + "'";
                var command = new SqlCommand(sql, conn);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        customer.Id = (int)dataReader["Id"];
                        customer.CustomerName = (string)dataReader["CustomerName"];
                        customer.Name = (string)dataReader["Name"];
                        customer.FirstName = (string)dataReader["FirstName"];
                        customer.Street = (string)dataReader["Street"];
                        customer.City = new Contracts.City
                        {
                            Id = (int)dataReader["CityId"]
                        };
                        if (customer.City.Id > 0)
                        {
                            DAL.City.Select(customer.City);
                        }
                        ++count;
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

