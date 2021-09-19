using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using AutoLogDAL;
using AutoLogDAL.Models;

namespace AutoLogDataReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AutoLogDataReader \n");

            /*
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
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            Console.WriteLine($"{dataReader.GetName(i)} = {dataReader.GetValue(i)}");
                        }
                        Console.WriteLine();
                    }
                }

            }
            */

            InventoryDAL dal = new InventoryDAL();
            var list = dal.GetAllInventory();

            foreach (var item in list)
            {
                Console.WriteLine($"{item.CarId} | {item.Make} | {item.Color} | {item.PetName}");
            }

            MoveCustomer(dal);

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

        public static void MoveCustomer(InventoryDAL dal)
        {
            Console.WriteLine("--- Transaction Test ---");

            bool throwex = true;
            dal.ProcessCreditRisk(throwex, 1);

            Console.WriteLine("--- Transaction Test Done ---");
        }
    }
}
