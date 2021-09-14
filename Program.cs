using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AutoLogDataReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AutoLogDataReader \n");


            using (SqlConnection connection = new SqlConnection())
            {
                var stringBuilder = new SqlConnectionStringBuilder
                {
                    InitialCatalog = "AutoLog",
                    DataSource = @".",
                    ConnectTimeout = 30,
                    IntegratedSecurity = true
                };

                connection.ConnectionString = stringBuilder.ConnectionString;
                connection.Open();
                ShowConnectionStatus(connection);


                string sql = "Select * From Inventory";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Console.WriteLine($"-> Make: {dataReader["Make"]}, PetName: {dataReader["PetName"]}, Color: {dataReader["Color"]}");
                    }
                }

            }
            
            Console.WriteLine("\n Done");
            Console.ReadLine();
        }

        public static void ShowConnectionStatus(SqlConnection connection)
        {
            Console.WriteLine($"Database location: {connection.DataSource}");
            Console.WriteLine($"Database name: {connection.Database}");
            Console.WriteLine($"Database timeout: {connection.ConnectionTimeout}");
            Console.WriteLine($"Database state: {connection.State}");
        }
    }
}
