using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAOPersistence
{
    public class DatabaseAccess
    {
        private SqlCommand _command;
        private SqlConnection _connection;
        private SqlDataAdapter _adapter;

        public DatabaseAccess()
        {

            
            //string connectionString = "Data Source= 127.0.0.1; Initial Catalog = PESDB; User ID=sa; Password = Leo@123456;";
            string connectionString = "Server=;Database=Unsplash;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=false;";

            _connection = new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            try
            {
                //
                //TODO check why so many opens and closes are done here at startup?
                //and check why this or was there?
                //Closed = 0,Broken = 16 -> open (again)
                //Open = 1, Connecting = 2, Executing = 4, Fetching = 8, => do nothing
                //
                if ((_connection.State == ConnectionState.Closed) || (_connection.State == ConnectionState.Broken))
                {
                    _connection.Open();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            catch (Exception)
            {
            }
        }


        public SqlDataReader SelectRecords(string query, SqlParameter[] parameters = null)
        {
            try
            {
                _command = new SqlCommand();
                _command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    _command.Parameters.AddRange(parameters);

                _command.Connection = _connection;
                _command.CommandText = query;

                return _command.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public bool DeleteRecord(string query, SqlParameter[] parameters = null)
        {
            try
            {
                _command = new SqlCommand();
                _adapter = new SqlDataAdapter();
                _command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    _command.Parameters.AddRange(parameters);

                _command.Connection = _connection;
                _command.CommandText = query;
                _adapter.DeleteCommand = _command;
                _command.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public int SaveRecordWithIdReturned(string query, SqlParameter[] parameters = null)
        {
            
            
            try
            {
                _command = new SqlCommand();
                _adapter = new SqlDataAdapter();
                _command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    _command.Parameters.AddRange(parameters);

                _command.Connection = _connection;
                _command.CommandText = query;
                _adapter.InsertCommand = _command;


                _command.ExecuteNonQuery();

                int idValue = Convert.ToInt32(_command.Parameters["@Id"].Value);
                return idValue;

                //return _command.ExecuteReader(CommandBehavior.CloseConnection);

                //return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            
        }

    }
}
