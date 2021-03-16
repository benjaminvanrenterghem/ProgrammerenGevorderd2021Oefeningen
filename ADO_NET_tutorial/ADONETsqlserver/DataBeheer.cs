using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ADONETsqlserver
{
    public class DataBeheer
    {
        #region Properties
        private string _connectionString;
        #endregion

        #region Ctor
        public DataBeheer(string connectionString)
        {
            this._connectionString = connectionString;           
        }
        #endregion

        #region Methods
        private SqlConnection getConnection()
        {
            SqlConnection connection = new(_connectionString);           
            return connection;
        }

        public void VoegCursusToe(Cursus c)
        {
            SqlConnection connection = getConnection();
            string query = "INSERT INTO dbo.cursusSQL (cursusnaam) VALUES(@cursusnaam)";
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {            
                    command.Parameters.Add(new SqlParameter("@cursusnaam",SqlDbType.NVarChar));
                    command.CommandText = query;
                    command.Parameters["@cursusnaam"].Value = c.Naam;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public void VoegKlasToe(Klas k)
        {
            SqlConnection connection = getConnection();
            string query = "INSERT INTO dbo.klasSQL (klasnaam) VALUES(@klasnaam)";
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@klasnaam",SqlDbType.NVarChar));
                    command.CommandText = query;
                    command.Parameters["@klasnaam"].Value = k.Naam;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public void VoegKlassenToe(List<Klas> klist)
        {
            SqlConnection connection = getConnection();
            string query1 = "SELECT * FROM dbo.klasSQL";
            try
            {
                connection.Open();
                using (SqlDataAdapter adapter = new())
                {
                    SqlCommand command = new(query1, connection);                  
                    adapter.SelectCommand = command;

                    SqlCommandBuilder builder = new(adapter);
                    //builder.DataAdapter = adapter;
                    adapter.InsertCommand = builder.GetInsertCommand();

                    DataTable table = new();
                    adapter.Fill(table);

                    foreach (Klas k in klist)
                    {
                        DataRow row = table.NewRow();
                        row["klasnaam"] = k.Naam;
                        table.Rows.Add(row);
                    }
                    adapter.Update(table);
                }
            }            
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

        }
        public void VoegStudentToe(Student s)
        {
            SqlConnection connection = getConnection();
            string queryS = "INSERT INTO dbo.studentSQL(naam,klasId) VALUES(@naam,@klasId)";

            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@naam",SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@klasId",SqlDbType.Int));

                    command.CommandText = queryS;
                    command.Parameters["@naam"].Value = s.Naam;
                    command.Parameters["@klasId"].Value = s.Klas.Id;
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public void VoegStudentMetCursussenToe(Student s)
        {
            SqlConnection connection = getConnection();
            string queryS = "INSERT INTO dbo.studentSQL(naam,klasId) output INSERTED.ID VALUES(@naam,@klasId)";
            string querySC = "INSERT INTO dbo.student_cursusSQL(cursusId,studentId) VALUES(@cursusId,@studentId)";

            using (SqlCommand command1 = connection.CreateCommand())
            using (SqlCommand command2 = connection.CreateCommand())
            {                
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                command1.Transaction = transaction;
                command2.Transaction = transaction;
                try
                {
                    //student toevoegen
                    SqlParameter parNaam = new()
                    {
                        ParameterName = "@naam",
                        SqlDbType = SqlDbType.NVarChar
                    };
                    command1.Parameters.Add(parNaam);
                    SqlParameter parKlas = new()
                    {
                        ParameterName = "@klasId",
                        DbType = DbType.Int32
                    };
                    ///check !!!!!!!!!!!!!!!!!!!
                    command1.Parameters.Add(parKlas);
                    command1.CommandText = queryS;
                    command1.Parameters["@naam"].Value = s.Naam;
                    command1.Parameters["@klasId"].Value = s.Klas.Id;
                    //command1.ExecuteNonQuery();
                    int newID = (int)command1.ExecuteScalar();
                    //Cursussen toevoegen
                    command2.Parameters.Add(new SqlParameter("@cursusId",SqlDbType.Int));
                    command2.Parameters.Add(new SqlParameter("@studentId",SqlDbType.Int));

                    command2.CommandText = querySC;
                    command2.Parameters["@studentId"].Value = newID;

                    foreach (var cursus in s.Cursussen)
                    {
                        command2.Parameters["@cursusId"].Value = cursus.Id;
                        command2.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }        
        public void KoppelCursusAanStudent(int studentId,List<int> cursusIds)
        {
            SqlConnection connection = getConnection();
            string queryS = "INSERT INTO dbo.student_cursusSQL(cursusId,studentId) VALUES(@cursusId,@studentId)";

            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@cursusId",SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@studentId",SqlDbType.Int));

                    command.CommandText = queryS;
                    command.Parameters["@studentId"].Value = studentId;

                    foreach (int cursusId in cursusIds)
                    {
                        command.Parameters["@cursusId"].Value = cursusId;
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public IEnumerable<Cursus> GeefCursussen()
        {
            SqlConnection connection = getConnection();
            IList<Cursus> lg = new List<Cursus>();
            string query = "SELECT * FROM dbo.cursusSQL";
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                connection.Open();
                try
                {
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        int id = (int)dataReader["id"];
                        string cursusnaam = dataReader.GetString(1); //verschillende methodes om data op te vragen !
                        lg.Add(new Cursus(id, cursusnaam));
                    }
                    dataReader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
            return lg;
        }
        public Klas GeefKlas(int id)
        {
            SqlConnection connection = getConnection();
            string query = "SELECT * FROM dbo.klasSQL WHERE id=@id";
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                SqlParameter paramId = new()
                {
                    ParameterName = "@Id",
                    DbType = DbType.Int32,
                    Value = id
                };
                command.Parameters.Add(paramId);
                connection.Open();
                try
                {
                    IDataReader reader = command.ExecuteReader(); //of SqlDataReader
                    reader.Read();
                    Klas klas = new((int)reader["Id"],(string)reader["klasnaam"]);
                    reader.Close();
                    return klas;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public Student GeefStudent(int id)
        {
            SqlConnection connection = getConnection();
            string queryS = "SELECT * FROM dbo.studentSQL WHERE id=@id";
            string querySC = "SELECT * FROM [adresBeheer].[dbo].[cursusSQL] t1,[adresBeheer].[dbo].[student_cursusSQL] t2 "
                +"where t1.Id = t2.cursusid and t2.studentid = @id";
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = queryS;
                SqlParameter paramId = new();
                paramId.ParameterName = "@Id";
                paramId.DbType = DbType.Int32;
                paramId.Value = id;
                command.Parameters.Add(paramId);
                connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    int studentId = (int)reader["Id"];
                    string studentnaam = (string)reader["naam"];
                    int klasId = (int)reader["klasId"];
                    reader.Close();
                    Klas klas = GeefKlas(klasId);
                    Student student = new(studentId,studentnaam,klas);

                    command.CommandText = querySC;
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Cursus cursus = new(reader.GetInt32(0), reader.GetString(1));
                        student.VoegCursusToe(cursus);
                    }
                    reader.Close();
                    return student;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public Cursus GeefCursus(int id)
        {
            SqlConnection connection = getConnection();
            string query = "SELECT * FROM dbo.cursusSQL WHERE id=@id";
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@Id";
                paramId.DbType = DbType.Int32;
                paramId.Value = id;
                command.Parameters.Add(paramId);
                connection.Open();
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    Cursus cursus = new((int)reader["Id"], (string)reader["cursusnaam"]);
                    reader.Close();
                    return cursus;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public void UpdateCursus(Cursus c)
        {
            SqlConnection connection = getConnection();
            Cursus cursusDB = GeefCursus(c.Id);
            string query = "SELECT * FROM dbo.cursusSQL WHERE Id=@Id";
           
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                try
                {
                    SqlParameter paramId = new SqlParameter();
                    paramId.ParameterName = "@Id";
                    paramId.DbType = DbType.Int32;
                    paramId.Value = c.Id;
                    SqlCommandBuilder builder = new();
                    builder.DataAdapter = adapter;
                    adapter.SelectCommand = new SqlCommand
                    {
                        CommandText = query,
                        Connection = connection
                    };
                    adapter.SelectCommand.Parameters.Add(paramId);
                    adapter.UpdateCommand = builder.GetUpdateCommand();
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    table.Rows[0]["cursusnaam"] = c.Naam;
                    
                    adapter.Update(table);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void VerwijderCursussen(List<int> ids)
        {
            string query = "SELECT * FROM dbo.cursusSQL";
            DataSet ds = new();
            SqlConnection connection = getConnection();
            using(SqlDataAdapter adapter = new())
            {
                try
                {
                    SqlCommandBuilder builder = new()
                    {
                        DataAdapter = adapter
                    };
                    adapter.SelectCommand = new SqlCommand
                    {
                        CommandText = query,
                        Connection = connection
                    };
                    adapter.DeleteCommand = builder.GetDeleteCommand();
                    adapter.FillSchema(ds, SchemaType.Source, "cursus");
                    adapter.Fill(ds, "cursus");
                    
                    foreach (int id in ids)
                    {
                        DataRow r = ds.Tables["cursus"].Rows.Find(id);
                        r.Delete();
                    }
                    adapter.Update(ds, "cursus");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);                    
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        #endregion
    }
}
