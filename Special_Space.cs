using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_Work_1._4
{
    public partial class Special_Space : Form
    {
        public Special_Space()
        {
            InitializeComponent();
        }

        private void get_income_button_Click(object sender, EventArgs e) //изъятие выручки
        {
            Int32 Cur_income = 0, Total_income = 0;
            Cur_income = Convert.ToInt32(income_label.Text);
            Total_income = Convert.ToInt32(label1.Text);
            Total_income += Cur_income;
            String income = Convert.ToString(Total_income);

            DB db = new DB();

            MySqlCommand command_1 = new MySqlCommand("UPDATE `income` SET `income` = @income WHERE `income`.`id` = 1;", db.getConnection()); //обновление размера общей выручки

            command_1.Parameters.Add("@income", MySqlDbType.VarChar).Value = income;

            db.openConnection();

            if (command_1.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Выручка изъята");
                income_label.Text = "0";
            }
            else
            {
                MessageBox.Show("Ошибка соединения");
            }

            MySqlCommand command_2 = new MySqlCommand("UPDATE `income` SET `income` = @income WHERE `income`.`id` = 2;", db.getConnection()); //обнуление текущей выручки

            command_2.Parameters.Add("@income", MySqlDbType.VarChar).Value = "0";

            if (command_2.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Сумма выручки обновлена в базу данных");
            }
            else
            {
                MessageBox.Show("Ошибка соединения");
            }

            db.closeConnection();
        }

        private void close_button_Click(object sender, EventArgs e) //закрытие специального отделения
        {
            this.Close();
            Main_Form main_form = new Main_Form();
            main_form.Show();
        }

        private void add_Coca_cola_button_Click(object sender, EventArgs e) //загрузка Кока Колы в автомат
        {
            Int32 Current_Number = 0, New_Number = 0;
            String[] Item = new string[5] { "0", "0", "0", "0", "0" };
            int i = 0;

            DB db = new DB();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM `sell_units` WHERE `sell_units`.`id`=1", db.getConnection()); //запрос текущего количества Кока Колы

            db.openConnection();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Item[i] = reader.GetString(1);
                i++;
            }

            reader.Close();

            db.closeConnection();

            Current_Number = Convert.ToInt32(Item[0]);

            New_Number = Convert.ToInt32(textBox_add_Coca_cola.Text) + Current_Number;

            MySqlCommand command = new MySqlCommand("UPDATE `sell_units` SET `Number` = @num WHERE `sell_units`.`id` = 1", db.getConnection()); //загрузка нового количества Кока Колы

            command.Parameters.Add("@num", MySqlDbType.VarChar).Value = Convert.ToString(New_Number);

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Coca Cola загружена в автомат");
            }
            else
            {
                MessageBox.Show("Ошибка соединения");
            }

            db.closeConnection();
        }

        private void add_Fanta_button_Click(object sender, EventArgs e) //тоже самое, что и add_Coca_cola_button_Click
        {
            Int32 Current_Number = 0, New_Number = 0;
            String[] Item = new string[5] { "0", "0", "0", "0", "0" };
            int i = 0;

            DB db = new DB();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM `sell_units` WHERE `sell_units`.`id`=2", db.getConnection());

            db.openConnection();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Item[i] = reader.GetString(1);
                i++;
            }

            reader.Close();

            db.closeConnection();

            Current_Number = Convert.ToInt32(Item[0]);

            New_Number = Convert.ToInt32(textBox_add_Fanta.Text) + Current_Number;

            MySqlCommand command = new MySqlCommand("UPDATE `sell_units` SET `Number` = @num WHERE `sell_units`.`id` = 2", db.getConnection());

            command.Parameters.Add("@num", MySqlDbType.VarChar).Value = Convert.ToString(New_Number);

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Fanta загружена в автомат");
            }
            else
            {
                MessageBox.Show("Ошибка соединения");
            }

            db.closeConnection();
        }

        private void button4_Click(object sender, EventArgs e) //тоже самое, что и add_Coca_cola_button_Click
        {
            Int32 Current_Number = 0, New_Number = 0;
            String[] Item = new string[5] { "0", "0", "0", "0", "0" };
            int i = 0;

            DB db = new DB();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM `sell_units` WHERE `sell_units`.`id`=3", db.getConnection());

            db.openConnection();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Item[i] = reader.GetString(1);
                i++;
            }

            reader.Close();

            db.closeConnection();

            Current_Number = Convert.ToInt32(Item[0]);

            New_Number = Convert.ToInt32(textBox_add_Sprite.Text) + Current_Number;

            MySqlCommand command = new MySqlCommand("UPDATE `sell_units` SET `Number` = @num WHERE `sell_units`.`id` = 3", db.getConnection());

            command.Parameters.Add("@num", MySqlDbType.VarChar).Value = Convert.ToString(New_Number);

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Sprite загружен в автомат");
            }
            else
            {
                MessageBox.Show("Ошибка соединения");
            }

            db.closeConnection();
        }

        private void add_Mars_button_Click(object sender, EventArgs e) //тоже самое, что и add_Coca_cola_button_Click
        {
            Int32 Current_Number = 0, New_Number = 0;
            String[] Item = new string[5] { "0", "0", "0", "0", "0" };
            int i = 0;

            DB db = new DB();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM `sell_units` WHERE `sell_units`.`id`=4", db.getConnection());

            db.openConnection();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Item[i] = reader.GetString(1);
                i++;
            }

            reader.Close();

            db.closeConnection();

            Current_Number = Convert.ToInt32(Item[0]);

            New_Number = Convert.ToInt32(textBox_add_Mars.Text) + Current_Number;

            MySqlCommand command = new MySqlCommand("UPDATE `sell_units` SET `Number` = @num WHERE `sell_units`.`id` = 4", db.getConnection());

            command.Parameters.Add("@num", MySqlDbType.VarChar).Value = Convert.ToString(New_Number);

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Mars загружен в автомат");
            }
            else
            {
                MessageBox.Show("Ошибка соединения");
            }

            db.closeConnection();
        }

        private void add_Twix_button_Click(object sender, EventArgs e) //тоже самое, что и add_Coca_cola_button_Click
        {
            Int32 Current_Number = 0, New_Number = 0;
            String[] Item = new string[5] { "0", "0", "0", "0", "0" };
            int i = 0;

            DB db = new DB();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM `sell_units` WHERE `sell_units`.`id`=5", db.getConnection());

            db.openConnection();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Item[i] = reader.GetString(1);
                i++;
            }

            reader.Close();

            db.closeConnection();

            Current_Number = Convert.ToInt32(Item[0]);

            New_Number = Convert.ToInt32(textBox_add_Twix.Text) + Current_Number;

            MySqlCommand command = new MySqlCommand("UPDATE `sell_units` SET `Number` = @num WHERE `sell_units`.`id` = 5", db.getConnection());

            command.Parameters.Add("@num", MySqlDbType.VarChar).Value = Convert.ToString(New_Number);

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Twix загружен в автомат");
            }
            else
            {
                MessageBox.Show("Ошибка соединения");
            }

            db.closeConnection();
        }

        private void Special_Space_Load(object sender, EventArgs e) //загрузка в окно суммы текущей выручки
        {
            String[] items = new string[20];
            int i = 0;
            DB db = new DB();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `income`", db.getConnection()); //запрос выручки из базы данных

            db.openConnection();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                items[i] = reader.GetString(1);
                i++;
            }

            reader.Close();

            db.closeConnection();

            income_label.Text = items[1];
            label1.Text = items[0];
        }
    }
}
