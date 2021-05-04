using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Klantbeheer.Domain.Exceptions.ModelExceptions;
using Klantbeheer.Domain;
using Repository.ADO;

namespace Repository.Ado
{
    public class ProductManager : IProductManager
    {
        public int Add(DataObject p)
        {
            if (p == null) throw new ProductException("Add: Product is NULL.");
            var product = p as Product;
            if (product == null) throw new ProductException("Add: Product is NULL.");

            if (String.IsNullOrEmpty(product.Name) || product.Name.Trim().Length < 2) throw new ProductException("Add: product name is invalid.");

            SqlConnection connection = Repository.DbConfig.Connection;
            string query = "INSERT INTO [dbo].[Product] (Name, Price, IsActive) output INSERTED.ProductID VALUES(@Name, @Price, @IsActive)";

            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@Price", SqlDbType.Float));
                    command.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit));
                    command.CommandText = query;
                    command.Parameters["@Name"].Value = product.Name;
                    command.Parameters["@Price"].Value = product.Price;
                    command.Parameters["@IsActive"].Value = true;
                    int newID = (int)command.ExecuteScalar();

                    transaction.Commit();

                    return newID;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new ProductException("AddProduct", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void Remove(DataObject p)
        {
            if (p == null) throw new ProductException("RemoveProduct: product is NULL.");
            var product = p as Product;
            if (product == null) throw new ProductException("Add: Product is NULL.");

            if (String.IsNullOrEmpty(product.Name) || product.Id <= 1) throw new ProductException("RemoveProduct: product is invalid.");

            SqlConnection connection = Repository.DbConfig.Connection;
            string queryP = "UPDATE [dbo].[Product] SET IsActive = @IsActive WHERE ProductID = @id";

            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                {
                    // Ik heb ervoor gekozen om een extra kolom toe te voegen (IsActive) en wanneer men een product verwijderd, wordt deze
                    // op false gezet. Zo verlies ik geen producten uit de bestelling.
                    command.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit));
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    command.CommandText = queryP;
                    command.Parameters["@IsActive"].Value = false;
                    command.Parameters["@id"].Value = product.Id;
                    command.ExecuteNonQuery();
                    // merk op. Bestellingen die gemaakt zijn met deze producten worden bewaard(prijs, datum)
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new ProductException("RemoveProduct", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public DataObject Get(int id)
        {
            if (id <= 0) throw new ProductException("ProductID is invalid.");

            SqlConnection connection = Repository.DbConfig.Connection;
            string query = "SELECT * FROM [dbo].[Product] WHERE ProductID = @id";

            using (SqlCommand command = connection.CreateCommand())
            {
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.CommandText = query;
                command.Parameters["@id"].Value = id;
                connection.Open();
                try
                {
                    SqlDataReader dataReader = command.ExecuteReader();
                    dataReader.Read();
                    var p = new Product((int)dataReader["ProductID"], (string)dataReader["Name"], (double)dataReader["Price"]) 
                    { 
                        IsActive = (bool)dataReader["IsActive"]
                    };
                    dataReader.Close();
                    return p;
                }
                catch (Exception ex)
                {
                    throw new ProductException("GetProduct", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public DataObject Get(string name)
        {
            if (String.IsNullOrEmpty(name) || name.Trim().Length <= 1) throw new ProductException("Name of product is invalid.");

            SqlConnection connection = Repository.DbConfig.Connection;
            string query = "SELECT * FROM [dbo].[Product] WHERE Name = @Name";

            using (SqlCommand command = connection.CreateCommand())
            {
                command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar));
                command.CommandText = query;
                command.Parameters["@Name"].Value = name;
                connection.Open();
                try
                {
                    SqlDataReader dataReader = command.ExecuteReader();
                    dataReader.Read();
                    var p = new Product((int)dataReader["ProductID"], (string)dataReader["Name"], (double)dataReader["Price"])
                    {
                        IsActive = (bool)dataReader["IsActive"]
                    }; 
                    dataReader.Close();
                    
                    return p;
                }
                catch (Exception ex)
                {
                    throw new ProductException("GetProduct", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public IEnumerable<DataObject> GetAll()
        {
            var lp = new List<Product>();
            SqlConnection connection = Repository.DbConfig.Connection;
            string query = "SELECT * FROM [dbo].[Product] WHERE IsActive = @isActive";

            using (SqlCommand command = connection.CreateCommand())
            {
                command.Parameters.Add(new SqlParameter("@isActive", SqlDbType.Bit));
                command.CommandText = query;
                command.Parameters["@isActive"].Value = true;
                connection.Open();
                try
                {
                    SqlDataReader dataReader = command.ExecuteReader();
                    
                    if (dataReader.Read())
                    {
                        int id = (int)dataReader["ProductID"];
                        string name = (string)dataReader["Name"];
                        double price = (double)dataReader["Price"];
                        bool isActive = (bool)dataReader["IsActive"];
                        lp.Add(new Product(id, name, price));
                    }
                    while (dataReader.Read())
                    {
                        int id = (int)dataReader["ProductID"];
                        string name = (string)dataReader["Name"];
                        double price = (double)dataReader["Price"];
                        bool isActive = (bool)dataReader["IsActive"];
                        lp.Add(new Product(id, name, price));
                    }
                    dataReader.Close();
                }
                catch (Exception ex)
                {
                    throw new ProductException("GetAllProducts", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return lp.AsReadOnly();
        }

        public void Update(DataObject p)
        {
            if (p == null) throw new ProductException("Add: Product is NULL.");
            var product = p as Product;
            if (product == null) throw new ProductException("Add: Product is NULL.");

            var connection = Repository.DbConfig.Connection;

            string q = "UPDATE [Product] Name = @Name, Price = @Price, IsActive = @IsActive WHERE ProductID = @Id";

            using (var command = connection.CreateCommand())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                { // laagste niveau ADO .NET; SqlDataSet/DataTable/SQlAdapter mag ook
                    command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@Id", System.Data.SqlDbType.Int));
                    command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@Name", System.Data.SqlDbType.NVarChar));
                    command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@Price", System.Data.SqlDbType.NVarChar));
                    command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@IsActive", System.Data.SqlDbType.Bit));
                    command.CommandText = q;
                    command.Parameters["@Id"].Value = product.Id;
                    command.Parameters["@Name"].Value = product.Name;
                    command.Parameters["@Price"].Value = product.Price;
                    command.Parameters["@IsActive"].Value = product.IsActive;
                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new CustomerException("Cannot update product to database: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
