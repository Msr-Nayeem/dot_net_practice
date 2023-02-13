using System;
using System.Data.SqlClient;

namespace WebApplication1.Models
{
    public class DB : IDisposable
    {
        private SqlConnection _conn;
        private SqlCommand _cmd;
        private string connectionString = @"server=DESKTOP-AVD9M43;Database=Student;Integrated Security=true";
        public DB(string query)
        {
            _conn = new SqlConnection(connectionString);
            _cmd = new SqlCommand(query, _conn);
        }

        public SqlDataReader ExecuteReader()
        {
            _conn.Open();
            return _cmd.ExecuteReader();
        }

        public void Dispose()
        {
            if (_conn != null)
            {
                _conn.Close();
                _conn.Dispose();
            }
        }
    }
}
