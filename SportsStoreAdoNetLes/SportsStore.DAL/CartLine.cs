using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace SportsStore.DAL
{
    public static class CartLine
    {
        #region Fields
        private static string _connStr = ConfigurationManager.ConnectionStrings["SportsStoreConn"].ToString();
        #endregion

        /// <summary>
        /// Update record into database
        /// </summary>
        /// <param name="cartLine"></param>
        /// <returns></returns>
        public static void Update(Contracts.CartLine cartLine)
        {
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = $"UPDATE CartLine SET ProductId = " + cartLine.Product.ProductId + ", Quantity = " + cartLine.Quantity + " WHERE Id = " + cartLine.Id;
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
        /// <param name="cartLine"></param>
        /// <returns></returns>
        public static int Insert(Contracts.CartLine cartLine)
        {
            Int32 id = 0;
            string sql = "INSERT INTO CartLine ( ProductId, Quantity ) VALUES ( " + cartLine.Product.ProductId + ", " + cartLine.Quantity + " ); "
                + "SELECT CAST(scope_identity() AS int);";
            // output INSERTED.ID typisch Microsoft SQL Server
            using (var conn = new SqlConnection(_connStr))
            {
                var cmd = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    id = (Int32)cmd.ExecuteScalar();
                    cartLine.Id = id;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return id;
        }

        public static int Insert(List<Contracts.CartLine> cartLines)
        {
            Int32 id = 0;
            string sql = "INSERT INTO CartLine ( ProductId, Quantity ) OUTPUT INSERTED.ID VALUES ( @ProductId, @Quantity )";
            using (var conn = new SqlConnection(_connStr))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@ProductId", SqlDbType.Int);
                cmd.Parameters.Add("@Quantity", System.Data.SqlDbType.Int);
                try
                {
                    conn.Open();
                    foreach (var cartLine in cartLines)
                    {
                        DAL.Product.Select(cartLine.Product);
                        if (cartLine.Product.ProductId == 0)
                            DAL.Product.Insert(cartLine.Product);
                        cmd.Parameters["@ProductId"].Value = cartLine.Product.ProductId;
                        cmd.Parameters["@Quantity"].Value = cartLine.Quantity;
                        id = (Int32)cmd.ExecuteScalar();
                        cartLine.Id = id;
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
        /// <param name="cartLine"></param>
        public static void Delete(Contracts.CartLine cartLine)
        {
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = $"DELETE FROM CartLine WHERE Id = " + cartLine.Id;
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
        /// <param name="cartLine"></param>
        /// <returns></returns>
        public static bool Exists(Contracts.CartLine cartLine)
        {
            if (cartLine.Id <= 0) return false;

            int count = 0;
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = "SELECT Id FROM CartLine WHERE ";
                if (cartLine.Id != 0)
                    sql += " Id = " + cartLine.Id;
                var command = new SqlCommand(sql, conn);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        cartLine.Id = (int)dataReader[0];
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
        /// <param name="cartLine"></param>
        /// <returns></returns>
        public static bool Select(Contracts.CartLine cartLine)
        {
            //int count = 0;
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = "SELECT Id, ProductId, Quantity FROM CartLine WHERE ";
                if (cartLine.Id != 0)
                    sql += " Id = " + cartLine.Id;
                var command = new SqlCommand(sql, conn);
                var dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        cartLine.Id = (int)dataReader["Id"];
                        cartLine.Product = (Contracts.Product)dataReader["ProductId"];
                        cartLine.Quantity = (int)dataReader["Quantity"];
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
