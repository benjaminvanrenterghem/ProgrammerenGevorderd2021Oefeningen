using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace SportsStore.DAL
{
    public class OrderLine
    {
        #region Fields
        private static string _connStr = ConfigurationManager.ConnectionStrings["SportsStoreConn"].ToString();
        #endregion

        /// <summary>
        /// Update record into database
        /// </summary>
        /// <param name="orderLine"></param>
        /// <returns></returns>
        public static void Update(Contracts.OrderLine orderLine)
        {
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = $"UPDATE [OrderLine] SET OrderId = " + orderLine.OrderId + ", ProductId ='" + orderLine.ProductId + "', Price = " + orderLine.Price + " WHERE Id = " + orderLine.Id;
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
        /// <param name="orderLine"></param>
        /// <returns></returns>
        public static int Insert(Contracts.OrderLine orderLine)
        {
            Int32 id = 0;
            string sql = "INSERT INTO [OrderLine] ( OrderId, ProductId, Price ) VALUES ( " + orderLine.OrderId + ", " + orderLine.ProductId + ", " + orderLine.Price + " ); "
                + "SELECT CAST(scope_identity() AS int);";
            // output INSERTED.ID typisch Microsoft SQL Server
            using (var conn = new SqlConnection(_connStr))
            {
                var cmd = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    id = (Int32)cmd.ExecuteScalar();
                    orderLine.Id = id;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return id;
        }

        public static int Insert(List<Contracts.OrderLine> orderLines)
        {
            Int32 id = 0;
            string sql = "INSERT INTO [OrderLine] ( OrderId, ProductId, Price ) OUTPUT INSERTED.ID VALUES ( @OrderId, @ProductId, @Price )";
            using (var conn = new SqlConnection(_connStr))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@OrderId", SqlDbType.Int);
                cmd.Parameters.Add("@ProductId", System.Data.SqlDbType.Int);
                cmd.Parameters.Add("@Price", SqlDbType.Decimal);
                try
                {
                    conn.Open();
                    foreach (var orderLine in orderLines)
                    {
                        cmd.Parameters["@OrderId"].Value = orderLine.OrderId;
                        cmd.Parameters["@ProductId"].Value = orderLine.ProductId;
                        cmd.Parameters["@Price"].Value = orderLine.Price;
                        id = (Int32)cmd.ExecuteScalar();
                        orderLine.Id = id;
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
        /// <param name="orderLine"></param>
        public static void Delete(Contracts.OrderLine orderLine)
        {
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = $"DELETE FROM [OrderLine] WHERE Id = " + orderLine.Id;
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
        /// <param name="orderLine"></param>
        /// <returns></returns>
        public static bool Exists(Contracts.OrderLine orderLine)
        {
            if (orderLine.Id <= 0) return false;

            int count = 0;
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = "SELECT Id FROM [OrderLine] WHERE ";
                if (orderLine.Id != 0)
                    sql += " OrderLineId = " + orderLine.Id;
                var command = new SqlCommand(sql, conn);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        orderLine.Id = (int)dataReader[0];
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
        /// <param name="orderLine"></param>
        /// <returns></returns>
        public static bool Select(Contracts.OrderLine orderLine)
        {
            int count = 0;
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = "SELECT Id, OrderId, ProductId, Price FROM [OrderLine] WHERE ";
                if (orderLine.Id != 0)
                    sql += " OrderLineId = " + orderLine.Id;
                var command = new SqlCommand(sql, conn);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    { 
                        orderLine.Id = (int)dataReader["Id"];                   
                        orderLine.OrderId = (int)dataReader["OrderId"];
                        orderLine.ProductId = (int)dataReader["ProductId"];
                        orderLine.Price = (int)dataReader["Price"];
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
