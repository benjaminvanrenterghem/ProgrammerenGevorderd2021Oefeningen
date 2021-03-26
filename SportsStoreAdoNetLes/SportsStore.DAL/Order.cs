using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
namespace SportsStore.DAL
{
    public static class Order
    {
        #region Fields
        private static string _connStr = ConfigurationManager.ConnectionStrings["SportsStoreConn"].ToString();
        #endregion

        /// <summary>
        /// Update record into database
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static void Update(Contracts.Order order)
        {
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = $"UPDATE [Order] SET CartId = " + order.CartId + ", OrderDate = " + order.OrderDate + "', DeliveryDate = " + order.DeliveryDate + ", GiftWrapping = " + (order.GiftWrapping ? 1 : 0) + ", ShippingStreet = '" + order.ShippingStreet + "', ShippingCityId = " + order.ShippingCity.Id  + " WHERE Id = " + order.OrderId;
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
                //conn.Close();
                conn.Dispose();
            }
        }

        /// <summary>
        /// Insert new row in database
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static int Insert(Contracts.Order order)
        {
            Int32 id = 0;
            string sql = "INSERT INTO [Order] ( CustomerId, CartId, OrderDate, DeliveryDate, GiftWrapping, ShippingStreet, ShippingCityId ) VALUES ( " + order.CustomerId + ", " + order.CartId + ", '" 
                + order.OrderDate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + order.DeliveryDate.ToString("yyyy-MM-dd HH:mm:ss") + "', " 
                + (order.GiftWrapping ? 1 : 0) + ", '" + order.ShippingStreet + "', " + order.ShippingCity.Id + " );"
                + "SELECT CAST(scope_identity() AS int);";
            // output INSERTED.ID typisch Microsoft SQL Server
            using (var conn = new SqlConnection(_connStr))
            {
                var cmd = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    id = (Int32)cmd.ExecuteScalar();
                    order.OrderId = id;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return id;
        }

        public static int Insert(List<Contracts.Order> orders)
        {
            Int32 id = 0;
            string sql = "INSERT INTO [Order] ( CustomerId, CartId, OrderDate, DeliveryDate, GiftWrapping, ShippingStreet, ShippingCityId ) OUTPUT INSERTED.ID VALUES ( @CustomerId, @CartId, @OrderDate, @DeliveryDate, @GiftWrapping, @ShippingStreet, @ShippingCityId )";
            using (var conn = new SqlConnection(_connStr))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@CustomerId", SqlDbType.Int);
                cmd.Parameters.Add("@CartId", SqlDbType.Int);
                cmd.Parameters.Add("@OrderDate", SqlDbType.DateTimeOffset);
                cmd.Parameters.Add("@DeliveryDate", System.Data.SqlDbType.DateTimeOffset);
                cmd.Parameters.Add("@GiftWrapping", SqlDbType.Int);
                cmd.Parameters.Add("@ShippingStreet", SqlDbType.NVarChar);
                cmd.Parameters.Add("@ShippingCityId", SqlDbType.Int);
                try
                {
                    conn.Open();
                    foreach (var order in orders)
                    {
                        cmd.Parameters["@CustomerId"].Value = order.CustomerId;
                        cmd.Parameters["@CartId"].Value = order.CartId;
                        cmd.Parameters["@OrderDate"].Value = order.OrderDate;
                        cmd.Parameters["@DeliveryDate"].Value = order.DeliveryDate;
                        cmd.Parameters["@GiftWrapping"].Value = order.GiftWrapping;
                        cmd.Parameters["@ShippingStreet"].Value = order.ShippingStreet;
                        cmd.Parameters["@ShippingCityId"].Value = order.ShippingCity.Id;
                        id = (Int32)cmd.ExecuteScalar();
                        order.OrderId = id;
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
        /// <param name="order"></param>
        public static void Delete(Contracts.Order order)
        {
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = $"DELETE FROM [Order] WHERE OrderId = " + order.OrderId;
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
        /// <param name="order"></param>
        /// <returns></returns>
        public static bool Exists(Contracts.Order order)
        {
            if (order.OrderId <= 0) return false;

            int count = 0;
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = "SELECT OrderId FROM [Order] WHERE ";
                if (order.OrderId != 0)
                    sql += " OrderId = " + order.OrderId;
                var command = new SqlCommand(sql, conn);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        order.OrderId = (int)dataReader[0];
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
        /// <param name="order"></param>
        /// <returns></returns>
        public static bool Select(Contracts.Order order)
        {
            int count = 0;
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = "SELECT CustomerId, CartId, OrderId, OrderDate, DeliveryDate, GiftWrapping, ShippingStreet, ShippingCityId FROM [Order] WHERE ";
                if (order.OrderId != 0)
                    sql += " OrderId = " + order.OrderId;
                var command = new SqlCommand(sql, conn);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        order.CustomerId = (int)dataReader["CustomerId"];
                        order.CartId = (int)dataReader["CartId"];
                        order.OrderId = (int)dataReader["OrderId"];
                        order.OrderDate = (DateTimeOffset)dataReader["OrderDate"];
                        order.DeliveryDate = (DateTimeOffset)dataReader["DeliveryDate"];
                        order.GiftWrapping = (bool)dataReader["GiftWrapping"];
                        order.ShippingStreet = (string)dataReader["ShippingStreet"];
                        order.ShippingCity = (Contracts.City)dataReader["ShippingCityId"];
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
