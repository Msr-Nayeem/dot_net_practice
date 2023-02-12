using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DatabaseConnection
    {
        private string connectionString = @"server=DESKTOP-AVD9M43;Database=Student;Integrated Security=true";
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}