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
    /// Interaction logic for WriteForm1.xaml
    /// </summary>
    public partial class WriteForm1 : Window
    {
        WriteForm writeForm;

        public WriteForm1(WriteForm writeForm)
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

        private void AddEmployee_Click(object sender, EventArgs e)
        {
            if (!textBoxFIO.Text.Any() || !textBoxDateOfBirth.Text.Any() || ComboBoxJob.SelectedItem is null || !textBoxPass.Text.Any() || !textBoxTel.Text.Any())
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            var name = textBoxFIO.Text;
            if (name.Length > 200)
            {
                MessageBox.Show("Неверный формат фио!");
                return;
            }

            DateTime date;
            if (!DateTime.TryParse(textBoxDateOfBirth.Text, out date))
            {
                MessageBox.Show("Неверный формат даты!");
                return;
            }

            var phoneNumber = textBoxTel.Text;
            foreach (var sim in phoneNumber)
            {
                if ((!char.IsNumber(sim) && !sim.Equals('+')) || phoneNumber.Length > 30)
                {
                    MessageBox.Show("Неверный формат номера телефона!");
                    return;
                }
            }

            var passport = textBoxPass.Text;
            var jobName = ComboBoxJob.SelectedItem.ToString();

            using (var db = new MyDbContext())
            {
                var new_employee = new Employee
                {
                    fullName = name,
                    dateOfBirth = date,
                    phone = phoneNumber,
                    passport = passport,
                    postEmployee = db.Jobs.Where(p => p.name == jobName).FirstOrDefault()
                };

                db.Empl.Add(new_employee);
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


            //using (NpgsqlConnection connection = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=qwerty1234;Database=testdb;"))
            //{          
            //    using (NpgsqlCommand command = new NpgsqlCommand())
            //    {
            //        connection.Open();
            //        command.CommandText = "INSERT INTO Сотрудник (фио, дата_рождения, телефон, паспорт, должность_id) VALUES (@name, @date, @phoneNumber, @passport, (SELECT id FROM Должность WHERE название_должности = @jobName LIMIT 1))";
            //        command.Connection = connection;
            //        command.Parameters.AddWithValue("@name", name);
            //        command.Parameters.AddWithValue("@date", date);
            //        command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
            //        command.Parameters.AddWithValue("@passport", passport);
            //        command.Parameters.AddWithValue("@jobName", jobName);

            //        try
            //        {
            //            command.ExecuteNonQuery();
            //            MessageBox.Show("Запись успешно создана!");
            //        }
            //        catch
            //        {
            //            MessageBox.Show("Ошибка запроса");
            //        }

            //        connection.Close();
            //    }
            //}
        }

        private void WriteForm1_Load(object sender, EventArgs e)
        {
            using (var db = new MyDbContext())
            {
                var jobs = db.Jobs.Select(p => new
                {
                    name = p.name
                });

                foreach (var job in jobs)
                {
                    ComboBoxJob.Items.Add(job.name);
                }
            }
            
            //DataTable dt = new DataTable();
            //using (NpgsqlConnection connection = new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=qwerty1234;Database=testdb;"))
            //{
            //    using (NpgsqlCommand command = new NpgsqlCommand())
            //    {
            //        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
            //        {
            //            connection.Open();
            //            command.CommandText = "SELECT название_должности FROM Должность";
            //            command.Connection = connection;
            //            adapter.SelectCommand = command;
            //            adapter.Fill(dt);
            //            connection.Close();
            //        }
            //    }
            //}

            //foreach (DataRow row in dt.Rows)
            //{
            //    ComboBoxJob.Items.Add(row[0].ToString());
            //}
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
