using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dz_27._03
{
    /// <summary>
    /// /my
    /// </summary>
    public class Repository:IDisposable
    {
        private readonly string connectionString;
        private SqlConnection? connection;

        public Repository()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("jsconfig.json");

            var config = builder.Build();
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        public SqlConnection OpenConnection()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public void CloseConnection()
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
        }

        public void Dispose()
        {
            CloseConnection();
        }
    }
}
