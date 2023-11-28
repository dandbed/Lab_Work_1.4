using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Lab_Work_1._4
{
    internal class DB
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=labwork1");

        public void openConnection() //установка соединения
        {
            if(connection.State==System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void closeConnection() //закрытие соединения
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public MySqlConnection getConnection() //получение информации о состоянии соединения
        {
            return connection;
        }
    }
}
