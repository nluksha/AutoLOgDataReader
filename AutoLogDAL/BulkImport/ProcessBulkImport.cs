using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLogDAL.BulkImport
{
    public static class ProcessBulkImport
    {
        private static readonly string connectionString = @"Data Source=.; Integrated Security=true; Initial Catalog=AutoLog";
        private static SqlConnection sqlConnection = null;

        private static void OpenConnection()
        {
            sqlConnection = new SqlConnection { ConnectionString = connectionString };
            sqlConnection.Open();
        }

        private static void CloseConnection()
        {
            if (sqlConnection?.State != ConnectionState.Closed)
            {
                sqlConnection?.Close();
            }
        }

        public static void ExecuteBuilkImport<T>(IEnumerable<T> records, string tableName)
        {
            OpenConnection();

            using(SqlConnection con = sqlConnection)
            {
                SqlBulkCopy bulkCopy = new SqlBulkCopy(con)
                {
                    DestinationTableName = tableName
                };

                var dataReader = new MyDataReader<T>(records.ToList());
                try
                {
                    bulkCopy.WriteToServer(dataReader);
                }
                catch(Exception ex)
                {
                    //
                }
                finally
                {
                    CloseConnection();
                }
            }
        }
    }
}
