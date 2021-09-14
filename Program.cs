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
                connection.ConnectionString = @"Data Source =.; Initial Catalog = AutoLog; Integrated Security = True";
                connection.Open();

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
    }
}
