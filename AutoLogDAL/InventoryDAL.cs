using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using AutoLogDAL.Models;

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

        private void OpenConnection()
        {
            sqlConnection = new SqlConnection { ConnectionString = connectionString };
            sqlConnection.Open();
        }

        private void CloseConnection()
        {
            if (sqlConnection?.State != ConnectionState.Closed)
            {
                sqlConnection?.Close();
            }
        }

        public List<Car> GetAllInventory()
        {
            OpenConnection();

            var inventory = new List<Car>();

            string sql = "Select * From Inventory";

            using (SqlCommand command = new SqlCommand(sql, sqlConnection))
            {
                command.CommandType = CommandType.Text;
                SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    inventory.Add(new Car
                    {
                        CarId = (int)dataReader["CarId"],
                        Color = (string)dataReader["Color"],
                        Make = (string)dataReader["Make"],
                        PetName = (string)dataReader["PetName"]
                    });
                }

                dataReader.Close();
            }


            return inventory;
        }

        public Car GetCar(int id)
        {
            OpenConnection();

            Car car = null;
            string sql = $"Select * From Inventory where CarId = {id}";

            using (SqlCommand command = new SqlCommand(sql, sqlConnection))
            {
                command.CommandType = CommandType.Text;
                SqlDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    car = new Car
                    {
                        CarId = (int)dataReader["CarId"],
                        Color = (string)dataReader["Color"],
                        Make = (string)dataReader["Make"],
                        PetName = (string)dataReader["PetName"]
                    };
                }

                dataReader.Close();
            }

            return car;
        }

        public void InsertAuto(string color, string make, string petName)
        {
            OpenConnection();

            string sql = $"Insert Into Inventory (Make, Color, PetName) Values ('{make}', '{color}', '{petName}')";
            using (SqlCommand command = new SqlCommand(sql, sqlConnection))
            {
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }

            CloseConnection();
        }

        public void InsertAuto(Car car)
        {
            OpenConnection();

            string sql = $"Insert Into Inventory (Make, Color, PetName) Values (@Make, @Color, @PetName)";
            using (SqlCommand command = new SqlCommand(sql, sqlConnection))
            {
                command.Parameters.AddRange(
                    new SqlParameter[]
                    {
                        new SqlParameter
                        {
                            ParameterName = "@Make",
                            Value = car.Make,
                            SqlDbType = SqlDbType.Char,
                            Size = 10
                        },
                        new SqlParameter
                        {
                            ParameterName = "@Color",
                            Value = car.Color,
                            SqlDbType = SqlDbType.Char,
                            Size = 10
                        },
                        new SqlParameter
                        {
                            ParameterName = "@PetName",
                            Value = car.PetName,
                            SqlDbType = SqlDbType.Char,
                            Size = 10
                        }
                    });

                command.ExecuteNonQuery();
            }

            CloseConnection();
        }

        public void DeleteCar(int id)
        {
            OpenConnection();

            string sql = $"Delete From Inventory where CarId = '{id}'";
            using (SqlCommand command = new SqlCommand(sql, sqlConnection))
            {
                try
                {
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Exception error = new Exception("Sorry! THat car is on order!", ex);

                    throw error;
                }

            }

            CloseConnection();
        }

        public void UpdatePetName(int id, string newPetName)
        {
            OpenConnection();

            string sql = $"Update Inventory Set PetName = '{newPetName}' Where CarId = '{id}'";
            using (SqlCommand command = new SqlCommand(sql, sqlConnection))
            {
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }

            CloseConnection();
        }
    }
}
