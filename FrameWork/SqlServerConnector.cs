using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace FrameWork
{
    public class SqlServerConnector
    {
        private SqlConnection conn = null;
        string _connectionString;

        public SqlServerConnector()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            _connectionString = root.GetSection("ConnectionStrings").GetSection("SqlServer").Value;
        }
        public void OpenConnection()
        {
            conn = new SqlConnection(_connectionString);
            conn.Open();
        }

        public SqlConnection GetConnection()
        {
            return this.conn;
        }

        public void CloseConnection()
        {
            try
            {
                this.conn.Close();
            }
            catch (Exception)
            {

                this.conn = null;
            }
        }
    }
}
