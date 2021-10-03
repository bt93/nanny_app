using System.Data;
using System.Data.SqlClient;

namespace NannyApi.DAL.DBHelpers
{
    public static class Command
    {
        public static SqlCommand AddWithValue(this SqlCommand command, string parameterName, object value, SqlDbType DBType)
        {
            var parameter = command.Parameters.AddWithValue(parameterName, value);
            parameter.SqlDbType = DBType;
            return command;
        }

        public static SqlDataReader Single(this SqlCommand command)
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

        public static SqlDataReader ToList(this SqlCommand command)
        {
            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
            }

            return command.ExecuteReader();
        }

        public static int Modify(this SqlCommand command)
        {
            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
            }

            return command.ExecuteNonQuery();
        }

        public static object ModifyAndReturn(this SqlCommand command)
        {
            if (command.Connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
            }

            return command.ExecuteScalar();
        }
    }
}
