using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
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
    public partial class Main_Form : Form
    {
        Int32[] Arr = new Int32[5] { 0, 0, 0, 0, 0 }; //массив - корзина покупок

        public Main_Form()
        {
            InitializeComponent();
        }

        private void button_SpSpace_Open_Click(object sender, EventArgs e) //кнопка для открытия специального отделения
        {
            String key = key_field.Text; //введенный ключ

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `key` WHERE `key` = @k", db.getConnection()); //запрос ключей, который совпадают с введенным, и ввод этих ключей в таблицу
            command.Parameters.Add("@k", MySqlDbType.VarChar).Value = key;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0) //если есть ключи, которые совпадают, то скрываем основное окно и открываем специальное отделение
            {
                this.Hide();
                Special_Space sp = new Special_Space();
                sp.Show();
            }
            else //если нет совпадений, то отказываем в доступе к специальному отделению
            {
                MessageBox.Show("Ошибка! Вы ввели неправильный код доступа.");
            }
        }

        private void button_conf_card_Click(object sender, EventArgs e) //подтверждение покупки, оплата по карте
        {
            Int32 Total_sum = 0, coca_cola_units = 0, fanta_units = 0, sprite_units = 0, mars_units = 0, twix_units = 0, current_income = 0;
            String[] Inc = new String[10];
            int j = 0;

            coca_cola_units = Convert.ToInt32(Coca_cola_units.Text); //внесение количества товаров в переменные
            fanta_units = Convert.ToInt32(Fanta_units.Text);
            sprite_units = Convert.ToInt32(Sprite_units.Text);
            mars_units = Convert.ToInt32(Mars_units.Text);
            twix_units = Convert.ToInt32(Twix_units.Text);
            Total_sum = Convert.ToInt32(total_price.Text);

            DB db = new DB();

            MySqlCommand cmd = new MySqlCommand("SELECT `income` FROM `income` WHERE `income`.`id`=2", db.getConnection()); //запрос текущей выручки

            db.openConnection();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Inc[j] = reader.GetString(0);
                j++;
            }

            reader.Close();

            db.closeConnection();

            current_income = Convert.ToInt32(Inc[0]); 

            if (Total_sum != 0) //если сумма купленных товаров не 0, то проверить ПИН-код карты
            {
                String PIN_code = textbox_card_pin.Text;

                DataTable table = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("SELECT * FROM `pin-code` WHERE `PIN-code` = @pc", db.getConnection()); //запрос совпадающих ПИН-кодов
                command.Parameters.Add("@pc", MySqlDbType.VarChar).Value = PIN_code;

                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0) //если есть совпадающий ПИН-код, то провести транзакцию
                {
                    current_income += Total_sum;

                    MySqlCommand command_1 = new MySqlCommand("UPDATE `income` SET `income` = @income WHERE `income`.`id` = 2;", db.getConnection());

                    command_1.Parameters.Add("@income", MySqlDbType.VarChar).Value = Convert.ToInt32(current_income);

                    db.openConnection();

                    command_1.ExecuteNonQuery();

                    db.closeConnection();

                    for (int i = 0; i < 5; i++) //вывод изображений купленных товаров
                    {
                        if (Arr[i] != 0)
                        {
                            if (i == 0)
                            {
                                pictureBox_Coca_cola.Image = Image.FromFile("C:\\Users\\dandb\\OneDrive\\Документы\\Университет\\ТиМП\\ЛР №1 (C#)\\Lab_Work_1.4\\Coca_Cola.jpg");
                            }

                            if (i == 1)
                            {
                                pictureBox_Fanta.Image = Image.FromFile("C:\\Users\\dandb\\OneDrive\\Документы\\Университет\\ТиМП\\ЛР №1 (C#)\\Lab_Work_1.4\\Fanta.jpg");
                            }

                            if (i == 2)
                            {
                                pictureBox_Sprite.Image = Image.FromFile("C:\\Users\\dandb\\OneDrive\\Документы\\Университет\\ТиМП\\ЛР №1 (C#)\\Lab_Work_1.4\\Sprite.jpg");
                            }

                            if (i == 3)
                            {
                                pictureBox_Mars.Image = Image.FromFile("C:\\Users\\dandb\\OneDrive\\Документы\\Университет\\ТиМП\\ЛР №1 (C#)\\Lab_Work_1.4\\Mars.jpg");
                            }

                            if (i == 4)
                            {
                                pictureBox_Twix.Image = Image.FromFile("C:\\Users\\dandb\\OneDrive\\Документы\\Университет\\ТиМП\\ЛР №1 (C#)\\Lab_Work_1.4\\Twix.jpg");
                            }
                        }
                    }

                    total_price.Text = "0";

                    coca_cola_units -= Arr[0]; //очистка корзины
                    Coca_cola_units.Text = Convert.ToString(coca_cola_units);

                    fanta_units -= Arr[1];
                    Fanta_units.Text = Convert.ToString(fanta_units);

                    sprite_units -= Arr[2];
                    Sprite_units.Text = Convert.ToString(sprite_units);

                    mars_units -= Arr[3];
                    Mars_units.Text = Convert.ToString(mars_units);

                    twix_units -= Arr[4];
                    Twix_units.Text = Convert.ToString(twix_units);

                    for (int i = 0; i < 5; i++)
                    {
                        Arr[i] = 0;
                    }

                    MessageBox.Show("Заберите товар");
                }
                else //если нет совпадающего ПИН-кода, то отклонить транзакцию
                {
                    MessageBox.Show("Ошибка! Вы ввели неправильный ПИН-код.");
                }
            }
            else //сумма товаров в корзине равна 0, выдать сообщение, чтобы покупатель положил товары в корзину
            {
                MessageBox.Show("Выберите товар");
            }
        }

        private void button_buy_Coca_cola_Click(object sender, EventArgs e) //добавление Кока Колы в корзину
        {
            Int32 Units = 0, Cost = 0, Total_Price = 0;
            Cost = Convert.ToInt32(Coca_cola_price.Text);
            Units = Convert.ToInt32(Coca_cola_units.Text);
            Total_Price = Convert.ToInt32(total_price.Text);

            if (Units == 0) //проверка на наличие товара в автомате: если есть, то добавить; если нет, то выдать сообщение, что товара нет
            {
                MessageBox.Show("Товар закончилсяю. Выберите, пожалуйста, другой товар");
            }
            else
            {
                Total_Price += Cost;
                total_price.Text = Convert.ToString(Total_Price);
                Arr[0] += 1;
            }
        }

        private void Main_Form_Load(object sender, EventArgs e) //загрузка главного окна
        {
            String[] items = new string[20];

            int i = 0;

            DB db = new DB();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `sell_units`", db.getConnection()); //запрос информации о количестве товаров в автомате

            db.openConnection();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                items[i] = reader.GetString(1);
                i++;
            }

            reader.Close();

            db.closeConnection();

            Coca_cola_units.Text = items[0]; //вывод в окно количества товаров
            Fanta_units.Text = items[1];
            Sprite_units.Text = items[2];
            Mars_units.Text = items[3];
            Twix_units.Text = items[4];
        }

        private void button_buy_Fanta_Click(object sender, EventArgs e) //тоже самое, что и button_buy_Coca_cola
        {
            Int32 Units = 0, Cost = 0, Total_Price = 0;
            Cost = Convert.ToInt32(Fanta_price.Text);
            Units = Convert.ToInt32(Fanta_units.Text);
            Total_Price = Convert.ToInt32(total_price.Text);
            if (Units == 0)
            {
                MessageBox.Show("Товар закончилсяю. Выберите, пожалуйста, другой товар");
            }
            else
            {
                Total_Price += Cost;
                total_price.Text = Convert.ToString(Total_Price);
                Arr[1] += 1;
            }
        }

        private void button_buy_Sprite_Click(object sender, EventArgs e) //тоже самое, что и button_buy_Coca_cola
        {
            Int32 Units = 0, Cost = 0, Total_Price = 0;
            Cost = Convert.ToInt32(Sprite_price.Text);
            Units = Convert.ToInt32(Sprite_units.Text);
            Total_Price = Convert.ToInt32(total_price.Text);
            if (Units == 0)
            {
                MessageBox.Show("Товар закончилсяю. Выберите, пожалуйста, другой товар");
            }
            else
            {
                Total_Price += Cost;
                total_price.Text = Convert.ToString(Total_Price);
                Arr[2] += 1;
            }
        }

        private void button_buy_Mars_Click(object sender, EventArgs e) //тоже самое, что и button_buy_Coca_cola
        {
            Int32 Units = 0, Cost = 0, Total_Price = 0;
            Cost = Convert.ToInt32(Mars_price.Text);
            Units = Convert.ToInt32(Mars_units.Text);
            Total_Price = Convert.ToInt32(total_price.Text);
            if (Units == 0)
            {
                MessageBox.Show("Товар закончилсяю. Выберите, пожалуйста, другой товар");
            }
            else
            {
                Total_Price += Cost;
                total_price.Text = Convert.ToString(Total_Price);
                Arr[3] += 1;
            }
        }

        private void button_buy_Twix_Click(object sender, EventArgs e) //тоже самое, что и button_buy_Coca_cola
        {
            Int32 Units = 0, Cost = 0, Total_Price = 0;
            Cost = Convert.ToInt32(Twix_price.Text);
            Units = Convert.ToInt32(Twix_units.Text);
            Total_Price = Convert.ToInt32(total_price.Text);
            if (Units == 0)
            {
                MessageBox.Show("Товар закончилсяю. Выберите, пожалуйста, другой товар");
            }
            else
            {
                Total_Price += Cost;
                total_price.Text = Convert.ToString(Total_Price);
                Arr[4] += 1;
            }
        }

        private void button_conf_cash_Click(object sender, EventArgs e) //потверждение покупки корзины за наличные
        {
            Int32 Cash = 0, Total_Price = 0, Change = 0, coca_cola_units = 0, fanta_units = 0, sprite_units = 0, mars_units = 0, twix_units = 0, current_income = 0;
            String[] Inc = new String[10] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
            int j = 0;

            Cash = Convert.ToInt32(textBox_cash.Text); //перевод введенной суммы, цены корзины в переменные
            Total_Price = Convert.ToInt32(total_price.Text);
            coca_cola_units = Convert.ToInt32(Coca_cola_units.Text);
            fanta_units = Convert.ToInt32(Fanta_units.Text);
            sprite_units = Convert.ToInt32(Sprite_units.Text);
            mars_units = Convert.ToInt32(Mars_units.Text);
            twix_units = Convert.ToInt32(Twix_units.Text);

            DB db = new DB();

            MySqlCommand cmd = new MySqlCommand("SELECT `income` FROM `income` WHERE `income`.`id`=2", db.getConnection()); //запрос текущей выручки

            db.openConnection();

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Inc[j] = reader.GetString(0);
                j++;
            }

            reader.Close();

            db.closeConnection();

            current_income = Convert.ToInt32(Inc[0]);

            if (Total_Price != 0) //если цена корзины  не равна 0, то проверить введенную сумму на корректность
            {
                if (Cash < Total_Price) //если введенная сумма не корректна, то выдать сообщение, что сумма недостаточна
                {
                    MessageBox.Show("Недостаточно денег");
                }
                else //в противном случае проверить введенную сумму на наличие сдачи
                {
                    for (int i = 0; i < 5; i++) //выдача товаров из корзины в лоток
                    {
                        if (Arr[i] != 0)
                        {
                            if (i == 0)
                            {
                                pictureBox_Coca_cola.Image = Image.FromFile("C:\\Users\\dandb\\OneDrive\\Документы\\Университет\\ТиМП\\ЛР №1 (C#)\\Lab_Work_1.4\\Coca_Cola.jpg");
                            }

                            if (i == 1)
                            {
                                pictureBox_Fanta.Image = Image.FromFile("C:\\Users\\dandb\\OneDrive\\Документы\\Университет\\ТиМП\\ЛР №1 (C#)\\Lab_Work_1.4\\Fanta.jpg");
                            }

                            if (i == 2)
                            {
                                pictureBox_Sprite.Image = Image.FromFile("C:\\Users\\dandb\\OneDrive\\Документы\\Университет\\ТиМП\\ЛР №1 (C#)\\Lab_Work_1.4\\Sprite.jpg");
                            }

                            if (i == 3)
                            {
                                pictureBox_Mars.Image = Image.FromFile("C:\\Users\\dandb\\OneDrive\\Документы\\Университет\\ТиМП\\ЛР №1 (C#)\\Lab_Work_1.4\\Mars.jpg");
                            }

                            if (i == 4)
                            {
                                pictureBox_Twix.Image = Image.FromFile("C:\\Users\\dandb\\OneDrive\\Документы\\Университет\\ТиМП\\ЛР №1 (C#)\\Lab_Work_1.4\\Twix.jpg");
                            }
                        }
                    }

                    current_income += Total_Price;

                    MySqlCommand command = new MySqlCommand("UPDATE `income` SET `income` = @income WHERE `income`.`id` = 2;", db.getConnection()); //обновление текущей выручки

                    command.Parameters.Add("@income", MySqlDbType.VarChar).Value = Convert.ToString(current_income);

                    db.openConnection();

                    command.ExecuteNonQuery();

                    db.closeConnection();

                    if (Cash == Total_Price) //если введенная сумма не требует выдачи сдачи, то выдать сообщение, что товар можно забрать
                    {
                        change.Text = "0";
                        total_price.Text = "0";

                        coca_cola_units -= Arr[0]; //уменьшение количества доступных для покупателей товаров после последних изменений
                        Coca_cola_units.Text = Convert.ToString(coca_cola_units);

                        fanta_units -= Arr[1];
                        Fanta_units.Text = Convert.ToString(fanta_units);

                        sprite_units -= Arr[2];
                        Sprite_units.Text = Convert.ToString(sprite_units);

                        mars_units -= Arr[3];
                        Mars_units.Text = Convert.ToString(mars_units);

                        twix_units -= Arr[4];
                        Twix_units.Text = Convert.ToString(twix_units);

                        for (int i = 0; i < 5; i++) //очистка корзины
                        {
                            Arr[i] = 0;
                        }

                        MessageBox.Show("Сдачи нет. Заберите товар");
                    }
                    else //если требуется выдать сдачу, то отобразить сумму сдачи и выдать сообщение, что можно забрать товары и сдачу
                    {
                        Change = Cash - Total_Price;
                        change.Text = Convert.ToString(Change);
                        total_price.Text = "0";

                        coca_cola_units -= Arr[0]; //уменьшение количества доступных для покупателей товаров после последних изменений
                        Coca_cola_units.Text = Convert.ToString(coca_cola_units);

                        fanta_units -= Arr[1];
                        Fanta_units.Text = Convert.ToString(fanta_units);

                        sprite_units -= Arr[2];
                        Sprite_units.Text = Convert.ToString(sprite_units);

                        mars_units -= Arr[3];
                        Mars_units.Text = Convert.ToString(mars_units);

                        twix_units -= Arr[4];
                        Twix_units.Text = Convert.ToString(twix_units);

                        for (int i = 0; i < 5; i++) //очистка корзины
                        {
                            Arr[i] = 0;
                        }

                        MessageBox.Show("Заберите сдачу и товар");
                    }
                }
            }
            else //если цена корзины равна 0, то выдать сообщение, чтобы покупатель выбрал товары для покупки 
            {
                MessageBox.Show("Выберите товар");
            }
        }

        private void button_give_change_Click(object sender, EventArgs e) //выдача сдачи
        {
            Int32 Change = 0;
            Change = Convert.ToInt32(change.Text);

            if (Change == 0) //если после оплаты остались деньги, то выдать их
            {
                MessageBox.Show("Сдачи нет");
            }
            else //покупка была без сдачи или была попытка запроса сдачи с пустой корзиной, то выдать сообщение, что сдача выдана
            {
                change.Text = "0";
                MessageBox.Show("Сдача выдана");
            }
        }

        private void close_button_Click(object sender, EventArgs e) //закрытие главного окна
        {
            DB db = new DB();

            MySqlCommand command_1 = new MySqlCommand("UPDATE `sell_units` SET `Number` = @num WHERE `sell_units`.`id` = 1", db.getConnection()); //загрузка данных о количестве Кока Колы в автомате в базу данных

            command_1.Parameters.Add("@num", MySqlDbType.VarChar).Value = Coca_cola_units.Text;

            db.openConnection();

            if (command_1.ExecuteNonQuery() == 1) //сообщение об успешной загрузке
            {
                MessageBox.Show("Количество Coca Cola обновлено в базе данных");
            }
            else //если не удалось загрузить, то выдать сообщение, что загрузка не произошла
            {
                MessageBox.Show("Ошибка соединения");
            }

            MySqlCommand command_2 = new MySqlCommand("UPDATE `sell_units` SET `Number` = @num WHERE `sell_units`.`id` = 2", db.getConnection()); //загрузка данных о количестве Фанты в автомате в базу данных

            command_2.Parameters.Add("@num", MySqlDbType.VarChar).Value = Fanta_units.Text;

            if (command_2.ExecuteNonQuery() == 1) //сообщение об успешной загрузке
            {
                MessageBox.Show("Количество Fanta обновлено в базе данных");
            }
            else //если не удалось загрузить, то выдать сообщение, что загрузка не произошла
            {
                MessageBox.Show("Ошибка соединения");
            }

            MySqlCommand command_3 = new MySqlCommand("UPDATE `sell_units` SET `Number` = @num WHERE `sell_units`.`id` = 3", db.getConnection()); //загрузка данных о количестве Спрайта в автомате в базу данных

            command_3.Parameters.Add("@num", MySqlDbType.VarChar).Value = Sprite_units.Text;

            if (command_3.ExecuteNonQuery() == 1) //сообщение об успешной загрузке
            {
                MessageBox.Show("Количество Sprite обновлено в базе данных");
            }
            else //если не удалось загрузить, то выдать сообщение, что загрузка не произошла
            {
                MessageBox.Show("Ошибка соединения");
            }

            MySqlCommand command_4 = new MySqlCommand("UPDATE `sell_units` SET `Number` = @num WHERE `sell_units`.`id` = 4", db.getConnection()); //загрузка данных о количестве Марса в автомате в базу данных

            command_4.Parameters.Add("@num", MySqlDbType.VarChar).Value = Mars_units.Text;

            if (command_4.ExecuteNonQuery() == 1) //сообщение об успешной загрузке
            {
                MessageBox.Show("Количество Mars обновлено в базе данных");
            }
            else //если не удалось загрузить, то выдать сообщение, что загрузка не произошла
            {
                MessageBox.Show("Ошибка соединения");
            }

            MySqlCommand command_5 = new MySqlCommand("UPDATE `sell_units` SET `Number` = @num WHERE `sell_units`.`id` = 5", db.getConnection()); //загрузка данных о количестве Твикса в автомате в базу данных

            command_5.Parameters.Add("@num", MySqlDbType.VarChar).Value = Twix_units.Text;


            if (command_5.ExecuteNonQuery() == 1) //сообщение об успешной загрузке
            {
                MessageBox.Show("Количество Twix обновлено в базе данных");
            }
            else //если не удалось загрузить, то выдать сообщение, что загрузка не произошла
            {
                MessageBox.Show("Ошибка соединения");
            }

            db.closeConnection();

            Application.Exit();
        }

        private void pictureBox_Coca_cola_Click(object sender, EventArgs e) //взятие купленной Кока Колы
        {
            if (pictureBox_Coca_cola.Image != null) //если Кока кола есть в лотке, то товар забирается
            {
                pictureBox_Coca_cola.Image = null;
                MessageBox.Show("Товар забран");
            }
            else //если Кока Колы нет, то выдать сообщение, что товара нет
            {
                MessageBox.Show("Товара в лотке нет. Купите товар, чтобы его забрать");
            }
        }

        private void pictureBox_Fanta_Click(object sender, EventArgs e) //тоже самое, что и pictureBox_Coca_cola_Click
        {
            if (pictureBox_Fanta.Image != null)
            {
                pictureBox_Fanta.Image = null;
                MessageBox.Show("Товар забран");
            }
            else
            {
                MessageBox.Show("Товара в лотке нет. Купите товар, чтобы его забрать");
            }
        }

        private void pictureBox_Sprite_Click(object sender, EventArgs e) //тоже самое, что и pictureBox_Coca_cola_Click
        {
            if (pictureBox_Sprite.Image != null)
            {
                pictureBox_Sprite.Image = null;
                MessageBox.Show("Товар забран");
            }
            else
            {
                MessageBox.Show("Товара в лотке нет. Купите товар, чтобы его забрать");
            }
        }

        private void pictureBox_Mars_Click(object sender, EventArgs e) //тоже самое, что и pictureBox_Coca_cola_Click
        {
            if (pictureBox_Mars.Image != null)
            {
                pictureBox_Mars.Image = null;
                MessageBox.Show("Товар забран");
            }
            else
            {
                MessageBox.Show("Товара в лотке нет. Купите товар, чтобы его забрать");
            }
        }

        private void pictureBox_Twix_Click(object sender, EventArgs e) //тоже самое, что и pictureBox_Coca_cola_Click
        {
            if (pictureBox_Twix.Image != null)
            {
                pictureBox_Twix.Image = null;
                MessageBox.Show("Товар забран");
            }
            else
            {
                MessageBox.Show("Товара в лотке нет. Купите товар, чтобы его забрать");
            }
        }
    }
}
