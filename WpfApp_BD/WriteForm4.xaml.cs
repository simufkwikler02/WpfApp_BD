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
    /// Interaction logic for WriteForm4.xaml
    /// </summary>
    public partial class WriteForm4 : Window
    {
        WriteForm writeForm;

        public WriteForm4(WriteForm writeForm)
        {
            InitializeComponent();
            this.writeForm = writeForm;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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

        private void AddOrder(object sender, RoutedEventArgs e)
        {
            if (ComboBoxOperator.SelectedItem is null || !textBoxSumPrice.Text.Any() || !textBoxTransaction.Text.Any())
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            int transaction;
            if (!int.TryParse(textBoxTransaction.Text, out transaction))
            {
                MessageBox.Show("Неверный формат номера транзакции!");
                return;
            }

            double sumPrice;
            if (!double.TryParse(textBoxSumPrice.Text.Replace('.', ','), out sumPrice))
            {
                MessageBox.Show("Неверный формат суммы заказа!");
                return;
            }

            var operator_name = ComboBoxOperator.SelectedItem.ToString();

            using (var db = new MyDbContext())
            {
                var num = db.Orders.Where(g => g.transactionNumber == transaction).Count();
                if (num > 0)
                {
                    MessageBox.Show("Заказ с таким номером уже существует!");
                    return;
                }

            }
            //DataTable dt = new DataTable();
            //using (NpgsqlCommand command = new NpgsqlCommand())
            //{
            //    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
            //    {
            //        BD db = new BD();
            //        db.OpenConnection();
            //        command.CommandText = "SELECT номер_транзакции FROM Заказ WHERE номер_транзакции = @transaction LIMIT 1";
            //        command.Connection = db.GetConnection();
            //        command.Parameters.AddWithValue("@transaction", transaction);
            //        adapter.SelectCommand = command;
            //        adapter.Fill(dt);
            //        db.CloseConnection();
            //    }
            //}

            //if (dt.Rows.Count > 0)
            //{
            //    MessageBox.Show("Заказ с таким номером уже существует!");
            //    return;
            //}

            using (var db = new MyDbContext())
            {
                var new_order = new Order()
                {
                    sumOfOrder = sumPrice,
                    transactionNumber = transaction,
                    operatorEmployee = db.Empl.Where(p => p.fullName == operator_name).FirstOrDefault()
                };

                db.Orders.Add(new_order);
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Запись успешно создана!");
                }
                catch
                {
                    MessageBox.Show("Ошибка запроса");
                }
            }

            //dt = new DataTable();
            //using (NpgsqlCommand command = new NpgsqlCommand())
            //{
            //    BD db = new BD();
            //    db.OpenConnection();
            //    command.CommandText = $"INSERT INTO Заказ (сумма_заказа, номер_транзакции, оператор_id) VALUES (@sumPrice, @transaction, (SELECT id FROM Сотрудник WHERE фио = @operator_name LIMIT 1))";
            //    command.Connection = db.GetConnection();
            //    command.Parameters.AddWithValue("@sumPrice", sumPrice);
            //    command.Parameters.AddWithValue("@transaction", transaction);
            //    command.Parameters.AddWithValue("@operator_name", operator_name);
            //    try
            //    {
            //        command.ExecuteNonQuery();
            //        MessageBox.Show("Запись успешно создана!");
            //    }
            //    catch
            //    {
            //        MessageBox.Show("Ошибка запроса");
            //    }

                //    db.CloseConnection();
                //}
            }

        private void WriteForm4_Load(object sender, RoutedEventArgs e)
        {

            using (var db = new MyDbContext())
            {
                var employes = db.Empl.Select(p => new
                {
                    fullName = p.fullName
                });

                foreach (var employee in employes)
                {
                    ComboBoxOperator.Items.Add(employee.fullName);
                }
            }

            //DataTable dt = new DataTable();
            //using (NpgsqlCommand command = new NpgsqlCommand())
            //{
            //    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
            //    {
            //        BD db = new BD();
            //        db.OpenConnection();
            //        command.CommandText = "SELECT фио FROM Сотрудник";
            //        command.Connection = db.GetConnection();
            //        adapter.SelectCommand = command;
            //        adapter.Fill(dt);
            //        db.CloseConnection();
            //    }
            //}

            //foreach (DataRow row in dt.Rows)
            //{
            //    ComboBoxOperator.Items.Add(row[0].ToString());
            //}
        }
    }
}
