using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace NannyApi.DAL.DBHelpers
{
    public class StoredProcedureHelper
    {
        private SqlConnection _connection;

        public StoredProcedureHelper(SqlConnection connection)
        {
            _connection = connection;
        }

        public SqlCommand CreateCommand(string commandName)
        {
            var command = new SqlCommand(commandName, _connection);
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        public SqlParameter AddWithValue(string parameterName, object value, SqlDbType DBType)
        {
            var parameter = new SqlParameter(parameterName, value);
            parameter.SqlDbType = DBType;
            return parameter;
        }

        public object Single(SqlCommand command)
        {
            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
            }

            var result = command.ExecuteScalar();
            command.Connection.Close();
            return result;
        }
        
        public SqlDataReader ExecuteReader(SqlCommand command)
        {
            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
            }

            var result = command.ExecuteReader();
            command.Connection.Close();

            return result;
        } 

        public int ExecuteNonQuery(SqlCommand command)
        {
            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
            }

            var result = command.ExecuteNonQuery();
            command.Connection.Close();
            return result;
        }
    }
}
