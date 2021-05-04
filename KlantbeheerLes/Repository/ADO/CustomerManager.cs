using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Klantbeheer.Domain.Exceptions.ModelExceptions;

namespace Repository.ADO
{

    public class CustomerManager: ICustomerManager
    {
        public CustomerManager()
        {
            System.Diagnostics.Debug.WriteLine("Creating new instance of CustomerManager");
        }

        public int Add(Klantbeheer.Domain.DataObject o)
        {
            // Precondities
            if (o is not Klantbeheer.Domain.Customer customer) throw new CustomerException("Object not of class Customer");

            var connection = Repository.DbConfig.Connection;

            string q = "INSERT INTO [Customer] (Name, Address) output INSERTED.CustomerID VALUES(@Name, @Address)";

            using(var command = connection.CreateCommand())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                { // laagste niveau ADO .NET; SqlDataSet/DataTable/SQlAdapter mag ook
                    command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@Name", System.Data.SqlDbType.NVarChar));
                    command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar));
                    command.CommandText = q;
                    command.Parameters["@Name"].Value = customer.Name;
                    command.Parameters["@Address"].Value = customer.Address;
                    int newId = (int)command.ExecuteScalar();

                    transaction.Commit();

                    return newId;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new CustomerException("Cannot add customer to database: " + ex.Message);                   
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void Update(Klantbeheer.Domain.DataObject o)
        {
            // Precondities
            if (o is not Klantbeheer.Domain.Customer customer) throw new CustomerException("Object not of class Customer");

            var connection = Repository.DbConfig.Connection;

            string q = "UPDATE [Customer] Name = @Name, Address = @Address WHERE CustomerID = @Id";

            using (var command = connection.CreateCommand())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                try
                { // laagste niveau ADO .NET; SqlDataSet/DataTable/SQlAdapter mag ook
                    command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@Id", System.Data.SqlDbType.Int));
                    command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@Name", System.Data.SqlDbType.NVarChar));
                    command.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("@Address", System.Data.SqlDbType.NVarChar));
                    command.CommandText = q;
                    command.Parameters["@Id"].Value = customer.Id;
                    command.Parameters["@Name"].Value = customer.Name;
                    command.Parameters["@Address"].Value = customer.Address;
                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new CustomerException("Cannot update customer to database: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        // indien klant verwijderd wordt, worden zijn bestellingen wel nog bewaard.
        public void Remove(Klantbeheer.Domain.DataObject o)
        {
            // Precondities
            if (o is not Klantbeheer.Domain.Customer customer) throw new CustomerException("Object not of class Customer");

            if (customer == null) throw new CustomerException("Remove customer: customer is NULL");
            if (customer?.Id == null || customer.Id <= 0) throw new CustomerException("Remove customer: Customer Id is invalid.");

            var connection = Repository.DbConfig.Connection;
            string queryO = "UPDATE [Order] SET CustomerID = @customerID WHERE CustomerID = @cID";
            string queryC = "DELETE FROM [Customer] WHERE CustomerID = @cID";

            using (SqlCommand command1 = connection.CreateCommand())
            using (SqlCommand command2 = connection.CreateCommand())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                command1.Transaction = transaction;
                command2.Transaction = transaction;
                try
                {
                    // eerst customerID's van de bestellingen van deze klant op null zetten
                    command1.Parameters.Add("@customerID", SqlDbType.Int);
                    command1.Parameters.Add("@cID", SqlDbType.Int);
                    command1.CommandText = queryO;
                    command1.Parameters["@customerID"].Value = DBNull.Value;
                    command1.Parameters["@cID"].Value = customer.Id;
                    command1.ExecuteNonQuery();

                    //als laatste de klant verwijderen
                    command2.CommandText = queryC;
                    command2.Parameters.Add("@cID", SqlDbType.Int);
                    command2.Parameters["@cID"].Value = customer.Id;
                    command2.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new CustomerException("RemoveCustomer", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public IEnumerable<Klantbeheer.Domain.DataObject> GetAll()
        {
            var customers = new List<Klantbeheer.Domain.DataObject>();
            var connection = Repository.DbConfig.Connection;
            string query = "SELECT * FROM [Customer]";

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                connection.Open();
                try
                {
                    SqlDataReader dataReader = command.ExecuteReader();
                    // ophalen van eerste rij, anders wordt deze overgeslagen
                    if (dataReader.Read())
                    {
                        int id = (int)dataReader["CustomerID"];
                        string name = (string)dataReader["Name"];
                        string address = (string)dataReader["Address"];
                        customers.Add(new Klantbeheer.Domain.Customer(id, name, address));
                    }
                    // indien er meer rijen zijn, worden deze ook toegevoegd aan de lijst
                    while (dataReader.Read())
                    {
                        int id = (int)dataReader["CustomerID"];
                        string name = (string)dataReader["Name"];
                        string address = (string)dataReader["Address"];
                        customers.Add(new Klantbeheer.Domain.Customer(id, name, address));
                    }
                    dataReader.Close();
                }
                catch (Exception ex)
                {
                    throw new CustomerException("Cannot get all customers", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return customers;
        }

        public Klantbeheer.Domain.DataObject Get(int id)
        {
            // Precondities
            if (id <= 0) throw new CustomerException("Customer Id is invalid.");

            var connection = Repository.DbConfig.Connection;
            string query = "SELECT * FROM [Customer] WHERE CustomerID = @id";

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;
                connection.Open();
                try
                {
                    SqlDataReader dataReader = command.ExecuteReader();
                    dataReader.Read();
                    var customer = new Klantbeheer.Domain.Customer((int)dataReader["CustomerID"], (string)dataReader["Name"], (string)dataReader["Address"]);
                    dataReader.Close();
                    return customer;
                }
                catch (Exception ex)
                {
                    throw new CustomerException("GetCustomer", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
