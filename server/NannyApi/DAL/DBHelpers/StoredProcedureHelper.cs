using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace NannyApi.DAL.DBHelpers
{
    public class StoredProcedureHelper
    {
        public SqlConnection CreateConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        public SqlCommand CreateCommand(string commandName, SqlConnection connection)
        {
            var command = new SqlCommand(commandName, connection);
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        public SqlCommand AddWithValue(SqlCommand command, string parameterName, object value, SqlDbType DBType)
        {
            var parameter = command.Parameters.AddWithValue(parameterName, value);
            parameter.SqlDbType = DBType;
            return command;
        }

        public SqlDataReader Single(SqlCommand command)
        {
            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
            }

            var result = command.ExecuteReader();

            if (result.RecordsAffected > 1)
            {
                throw new System.Exception("Returned too many rows");
            }

            return result;
        }
        
        public SqlDataReader ToList(SqlCommand command)
        {
            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
            }

            var result = command.ExecuteReader();

            return result;
        } 

        public int ExecuteNonQuery(SqlCommand command)
        {
            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
            }

            var result = command.ExecuteNonQuery();
            return result;
        }
    }
}
