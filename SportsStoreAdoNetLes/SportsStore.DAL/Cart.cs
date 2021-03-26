using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace SportsStore.DAL
{
    public static class Cart
    {
        #region Fields
        private static string _connStr = ConfigurationManager.ConnectionStrings["SportsStoreConn"].ToString();
        #endregion

        // CRUD: Create Read Update Delete
        // ... met ADO .NET: Create -> insert, READ -> select, update -> update, delete -> delete

        /// <summary>
        /// Update record into database
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public static void Update(Contracts.Cart cart)
        {
            // Precondities:
            if (cart.Id <= 0) throw new Exception("Cannot update cart without ID");

            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = $"UPDATE Car SET ShoppingDate = '" + cart.ShoppingDate.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE Id = " + cart.Id; // conversie van DateTimeOffset naar formaat dat SQLServer begrijpt
                var updateCommand = new SqlCommand(sql, conn);
                var numberOfRows = updateCommand.ExecuteNonQuery();

                //postconditie
                if (numberOfRows != 1)
                    throw new Exception("Did not update a single cart as is expected");
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
        /// Insert new row in database
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public static int Insert(Contracts.Cart cart)
        {
            Int32 id = 0;
            string sql = "INSERT INTO Cart ( ShoppingDate ) VALUES ( '" + cart.ShoppingDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ); "
                + "SELECT CAST(scope_identity() AS int);";
            // output INSERTED.ID typisch Microsoft SQL Server
            using (var conn = new SqlConnection(_connStr))
            {
                var cmd = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    id = (Int32)cmd.ExecuteScalar();
                    cart.Id = id;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return id;
        }

        public static int Insert(List<Contracts.Cart> carts)
        {
            Int32 id = 0;
            string sql = "INSERT INTO Cart ( ShoppingDate ) OUTPUT INSERTED.ID VALUES ( @ShoppingDate )";
            using (var conn = new SqlConnection(_connStr))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@ShoppingDDate", SqlDbType.NVarChar);
                try
                {
                    conn.Open();
                    foreach (var cart in carts)
                    {
                        cmd.Parameters["@ShoppingDate"].Value = cart.ShoppingDate;
                        id = (Int32)cmd.ExecuteScalar();
                        cart.Id = id;
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
        /// <param name="car"></param>
        public static void Delete(Contracts.Cart cart)
        {
            // Precondities:
            if (cart.Id <= 0) throw new Exception("Cannot update cart without ID");

            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = $"DELETE FROM Cart WHERE Id = " + cart.Id;
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
                // conn.Close();
                conn.Dispose();
            }
        }

        /// <summary>
        /// Get row from database
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public static bool Exists(Contracts.Cart cart)
        {
            if (cart.Id <= 0) return false;

            int count = 0;
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = "SELECT Id FROM Cart WHERE ";
                if (cart.Id != 0)
                    sql += " Id = " + cart.Id;
                var command = new SqlCommand(sql, conn);
                var dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        cart.Id = (int)dataReader[0];
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

        /// <summary>
        /// Get row from database
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public static bool Select(Contracts.Cart cart)
        {
            //int count = 0;
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = "SELECT Id, ShoppingDate FROM Cart WHERE ";
                if (cart.Id != 0)
                    sql += " Id = " + cart.Id;
                var command = new SqlCommand(sql, conn);
                var dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        cart.Id = (int)dataReader["Id"];
                        cart.ShoppingDate = (DateTimeOffset)dataReader["ShoppingDate"];
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
