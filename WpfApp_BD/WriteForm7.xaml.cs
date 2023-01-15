using System;
using Npgsql;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp_BD
{
    /// <summary>
    /// Interaction logic for WriteForm7.xaml
    /// </summary>
    public partial class WriteForm7 : Window
    {
        WriteForm writeForm;

        public WriteForm7(WriteForm writeForm)
        {
            InitializeComponent();
            this.writeForm = writeForm;
        }
        private void closeButton(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void backButton(object sender, RoutedEventArgs e)
        {
            this.Hide();
            writeForm.Show();
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void AddToOrder(object sender, RoutedEventArgs e)
        {
            if (ComboBoxDish.SelectedItem is null || !textBoxNumber.Text.Any() || ComboBoxOrder.SelectedItem is null)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            int number;
            if (!int.TryParse(textBoxNumber.Text, out number))
            {
                MessageBox.Show("Неверный формат количества блюд!");
                return;
            }

            int order;
            if (!int.TryParse(ComboBoxOrder.SelectedItem.ToString(), out order))
            {
                MessageBox.Show("Неверный формат номера транзакции!");
                return;
            }

            var dish_name = ComboBoxDish.SelectedItem.ToString();

            using (var db = new MyDbContext())
            {
                var new_compound = new OrderCompound()
                {
                    order = db.Orders.Where(p => p.transactionNumber == order).FirstOrDefault(),
                    dish = db.Dishes.Where(p => p.name == dish_name).FirstOrDefault(),
                    amount = number
                };

                db.OrdComp.Add(new_compound);
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Блюдо добавлено к заказу!");
                }
                catch
                {
                    MessageBox.Show("Ошибка запроса");
                }
            }

            //using (NpgsqlCommand command = new NpgsqlCommand())
            //{
            //    BD db = new BD();
            //    db.OpenConnection();
            //    command.CommandText = "INSERT INTO Состав_заказа (заказ_id, блюдо_id, количество_порций) VALUES ((SELECT id FROM Заказ WHERE номер_транзакции = @order LIMIT 1), (SELECT id FROM Блюдо WHERE название_блюда = @dish_name LIMIT 1), @number)";
            //    command.Connection = db.GetConnection();
            //    command.Parameters.AddWithValue("@order", order);
            //    command.Parameters.AddWithValue("@dish_name", dish_name);
            //    command.Parameters.AddWithValue("@number", number);
            //    try
            //    {
            //        command.ExecuteNonQuery();
            //        MessageBox.Show("Блюдо добавлено к заказу!");
            //    }
            //    catch
            //    {
            //        MessageBox.Show("Ошибка запроса");
            //    }

            //    db.CloseConnection();
            //}
        }

        private void WriteForm7_Load(object sender, RoutedEventArgs e)
        {
            using (var db = new MyDbContext())
            {
                var orders = db.Orders.Select(p => new
                {
                    transactionNumber = p.transactionNumber
                });

                foreach (var order in orders)
                {
                    ComboBoxOrder.Items.Add(order.transactionNumber);
                }

                var dishes = db.Dishes.Select(p => new
                {
                    name = p.name
                });

                foreach (var dish in dishes)
                {
                    ComboBoxDish.Items.Add(dish.name);
                }
            }

            //DataTable dt = new DataTable();
            //using (NpgsqlCommand command = new NpgsqlCommand())
            //{
            //    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
            //    {
            //        BD db = new BD();
            //        db.OpenConnection();
            //        command.CommandText = "SELECT номер_транзакции FROM Заказ";
            //        command.Connection = db.GetConnection();
            //        adapter.SelectCommand = command;
            //        adapter.Fill(dt);
            //        db.CloseConnection();
            //    }
            //}

            //foreach (DataRow row in dt.Rows)
            //{
            //    ComboBoxOrder.Items.Add(row[0]);
            //}

            //dt = new DataTable();
            //using (NpgsqlCommand command = new NpgsqlCommand())
            //{
            //    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
            //    {
            //        BD db = new BD();
            //        db.OpenConnection();
            //        command.CommandText = "SELECT название_блюда FROM Блюдо";
            //        command.Connection = db.GetConnection();
            //        adapter.SelectCommand = command;
            //        adapter.Fill(dt);
            //        db.CloseConnection();
            //    }
            //}

            //foreach (DataRow row in dt.Rows)
            //{
            //    ComboBoxDish.Items.Add(row[0].ToString());
            //}
        }
    }
}
