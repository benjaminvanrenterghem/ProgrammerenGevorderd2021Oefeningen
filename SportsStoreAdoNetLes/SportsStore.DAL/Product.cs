using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace SportsStore.DAL
{
    public static class Product
    {
        #region Fields
        private static string _connStr = ConfigurationManager.ConnectionStrings["SportsStoreConn"].ToString();
        #endregion

        /// <summary>
        /// Update record into database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static void Update(Contracts.Product product)
        {
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = $"UPDATE Product SET Name = '" + product.Name + "', Description ='" + product.Description + "', Price = " + product.Price.ToString().Replace(',', '.') + " WHERE ProductId = " + product.ProductId;
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

        /// <summary>
        /// Insert new row in database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static int Insert(Contracts.Product product)
        {
            Int32 id = 0;
            string sql = "INSERT INTO Product ( CategoryId, Name, Description, Price ) VALUES ( " + product.CategoryId + ", '" + product.Name + "', '" + product.Description + "', " + product.Price.ToString().Replace(',','.') + " ); "
                + "SELECT CAST(scope_identity() AS int);";
            // output INSERTED.ID typisch Microsoft SQL Server
            using (var conn = new SqlConnection(_connStr))
            {
                var cmd = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    id = (Int32)cmd.ExecuteScalar();
                    product.ProductId = id;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return id;
        }

        public static int Insert(List<Contracts.Product> products)
        {
            Int32 id = 0;
            string sql = "INSERT INTO Product ( CategoryId, Name, Description, Price ) OUTPUT INSERTED.ID VALUES ( @CategoryId, @Name, @Description, @Price )";
            using (var conn = new SqlConnection(_connStr))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@CategoryId", SqlDbType.Int);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                cmd.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar);                    
                cmd.Parameters.Add("@Price", SqlDbType.Decimal);
                try
                {
                    conn.Open();
                    foreach (var product in products)
                    {
                        cmd.Parameters["@CategoryId"].Value = product.CategoryId;
                        cmd.Parameters["@Name"].Value = product.Name;
                        cmd.Parameters["@Description"].Value = product.Description;
                        cmd.Parameters["@Price"].Value = product.Price;
                        id = (Int32)cmd.ExecuteScalar();
                        product.ProductId = id;
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
        /// <param name="product"></param>
        public static void Delete(Contracts.Product product)
        {
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = $"DELETE FROM Product WHERE ProductId = " + product.ProductId;
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

        /// <summary>
        /// Get row from database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static bool Exists(Contracts.Product product)
        {
            if (product.ProductId <= 0 && string.IsNullOrEmpty(product.Name)) return false;

            int count = 0;
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = "SELECT ProductId FROM Product WHERE ";
                if (product.ProductId != 0)
                    sql += " ProductId = " + product.ProductId;
                else if (!string.IsNullOrEmpty(product.Name))
                    sql += " Name = '" + product.Name + "'";
                var command = new SqlCommand(sql, conn);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        product.ProductId = (int)dataReader[0];
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
                conn.Close();
                conn.Dispose();
            }
            return false;
        }

        /// <summary>
        /// Get row from database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static bool Select(Contracts.Product product)
        {
            int count = 0;
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = "SELECT CategoryId, ProductId, Name, Description, Price FROM Product WHERE ";
                if (product.ProductId != 0)
                    sql += " ProductId = " + product.ProductId;
                else if (!string.IsNullOrEmpty(product.Name))
                    sql += " Name = '" + product.Name + "'";
                var command = new SqlCommand(sql, conn);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        product.CategoryId = (int)dataReader["CategoryId"];
                        product.ProductId = (int)dataReader["ProductId"];
                        product.Name = (string)dataReader["Name"];
                        product.Description = (string)dataReader["Description"];
                        product.Price = (decimal)dataReader["Price"];
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
                conn.Close();
                conn.Dispose();
            }
            return false;
        }
    }
}
