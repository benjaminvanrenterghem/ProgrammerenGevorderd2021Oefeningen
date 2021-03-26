using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace SportsStore.DAL
{
    public static class Category
    {
        #region Fields
        private static string _connStr = ConfigurationManager.ConnectionStrings["SportsStoreConn"].ToString();
        #endregion

        /// <summary>
        /// Update record into database
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static void Update(Contracts.Category category)
        {
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = $"UPDATE Category SET Name = '" + category.Name + "' WHERE CategoryId = " + category.CategoryId;
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
        /// <param name="category"></param>
        /// <returns></returns>
        public static int Insert(Contracts.Category category)
        {
            Int32 id = 0;
            string sql = "INSERT INTO Category ( Name ) VALUES ( '" + category.Name + "' ); "
                + "SELECT CAST(scope_identity() AS int);";
            // output INSERTED.ID typisch Microsoft SQL Server
            using (var conn = new SqlConnection(_connStr))
            {
                var cmd = new SqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    id = (Int32)cmd.ExecuteScalar();
                    category.CategoryId = id;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return id;
        }

        public static int Insert(List<Contracts.Category> categories)
        {
            Int32 id = 0;
            string sql = "INSERT INTO Category ( Name ) OUTPUT INSERTED.ID VALUES ( @Name )";
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                try
                {
                    conn.Open();
                    foreach (var category in categories)
                    {
                        cmd.Parameters["@Name"].Value = category.Name;
                        id = (Int32)cmd.ExecuteScalar();
                        category.CategoryId = id;
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
        /// <param name="category"></param>
        public static void Delete(Contracts.Category category)
        {
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = $"DELETE FROM Category WHERE CategoryId = " + category.CategoryId;
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
        /// <param name="category"></param>
        /// <returns></returns>
        public static bool Exists(Contracts.Category category)
        {
            if (category.CategoryId <= 0 && string.IsNullOrEmpty(category.Name)) return false;

            int count = 0;
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = "SELECT CategoryId FROM Category WHERE ";
                if (category.CategoryId != 0)
                    sql += " CategoryId = " + category.CategoryId;
                else if (!string.IsNullOrEmpty(category.Name))
                    sql += " Name = '" + category.Name + "'";
                var command = new SqlCommand(sql, conn);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        category.CategoryId = (int)dataReader[0];
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
        /// <param name="category"></param>
        /// <returns></returns>
        public static bool Select(Contracts.Category category)
        {
            int count = 0;
            var conn = new SqlConnection(_connStr);
            try
            {
                conn.Open();
                string sql = "SELECT CategoryId, Name FROM Category WHERE ";
                if (category.CategoryId != 0)
                    sql += " CategoryId = " + category.CategoryId;
                else if (!string.IsNullOrEmpty(category.Name))
                    sql += " Name = '" + category.Name + "'";
                var command = new SqlCommand(sql, conn);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        category.CategoryId = (int)dataReader["CategoryId"];
                        category.Name = (string)dataReader["Name"];
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
