using System.Data;
using System.Data.SqlClient;

namespace NannyApi.DAL.DBHelpers
{
    public static class Connection
    {
        public static SqlConnection CreateConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        public static SqlCommand CreateNewCommand(this SqlConnection connection, string commandName)
        {
            var command = new SqlCommand(commandName, connection);
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }
    }
}
