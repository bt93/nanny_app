using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NannyApi.DAL
{
    public class SessionSqlDAO : ISessionDAO
    {
        private string connectionString { get; set; }
        /// <summary>
        /// Creates a sql based caretaker DAO
        /// </summary>
        /// <param name="dbconnectionString"></param>
        public SessionSqlDAO(string dbconnectionString)
        {
            this.connectionString = dbconnectionString;
        }


    }
}
