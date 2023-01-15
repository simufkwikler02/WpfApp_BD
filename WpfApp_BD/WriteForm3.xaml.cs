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
    /// Interaction logic for WriteForm3.xaml
    /// </summary>
    public partial class WriteForm3 : Window
    {
        WriteForm writeForm;

        public WriteForm3(WriteForm writeForm)
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

        private void AddJob(object sender, RoutedEventArgs e)
        {
            if (!textBoxSalary.Text.Any() || !textBoxName.Text.Any())
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            var name = textBoxName.Text;
            if (name.Length > 100)
            {
                MessageBox.Show("Неверный формат названия блюда!");
                return;
            }

            double salary;
            if (!double.TryParse(textBoxSalary.Text.Replace('.', ','), out salary))
            {
                MessageBox.Show("Неверный формат цены блюда!");
                return;
            }

            using (var db = new MyDbContext())
            {
                var new_job = new Job
                {
                    name = name,
                    salary = salary
                };

                db.Jobs.Add(new_job);
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


            //using (NpgsqlCommand command = new NpgsqlCommand())
            //{
            //    BD db = new BD();
            //    db.OpenConnection();
            //    command.CommandText = "INSERT INTO Должность (название_должности, зарплата) VALUES (@name, @salary)";
            //    command.Connection = db.GetConnection();
            //    command.Parameters.AddWithValue("@name", name);
            //    command.Parameters.AddWithValue("@salary", salary);

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
    }
}
