using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AutoLogDAL
{
    public class InventoryDAL
    {
        private readonly string connectionString;
        private SqlConnection sqlConnection = null;

        public InventoryDAL() : this(@"Data Source=.; Integrated Security=true; Initial Catalog=AutoLog")
        {
        }

        public InventoryDAL(string connectionString) => this.connectionString = connectionString;

        private void OpenCOnnection()
        {
            sqlConnection = new SqlConnection { ConnectionString = connectionString };
            sqlConnection.Open();
        }

        private void CloseConnection()
        {
            if(sqlConnection?.State != ConnectionState.Closed)
            {
                sqlConnection?.Close();
            }
        }
    }
}
