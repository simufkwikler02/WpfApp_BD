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
    /// Interaction logic for WriteForm8.xaml
    /// </summary>
    public partial class WriteForm8 : Window
    {
        WriteForm writeForm;

        public WriteForm8(WriteForm writeForm)
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

        private void AddRespond(object sender, RoutedEventArgs e)
        {
            if (ComboBoxRoom.SelectedItem is null || !textBoxRespond.Text.Any() || ComboBoxEmployee.SelectedItem is null)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            var room_name = ComboBoxRoom.SelectedItem.ToString();
            var employee_name = ComboBoxEmployee.SelectedItem.ToString();
            var responsibility = textBoxRespond.Text;


            using (var db = new MyDbContext())
            {
                var new_respons = new Responsibilites()
                {
                    employee = db.Empl.Where(p => p.fullName == employee_name).FirstOrDefault(),
                    room = db.Rooms.Where(p => p.name == room_name).FirstOrDefault(),
                    responsibility = responsibility
                };

                db.Respons.Add(new_respons);
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Обязанность успешно добавлена!");
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
            //    command.CommandText = $"INSERT INTO Рабочая_смена (сотрудник_id, помещение_id, обязанности) VALUES ((SELECT id FROM Сотрудник WHERE фио = @employee_name LIMIT 1), (SELECT id FROM Помещение WHERE название_помещения = @room_name LIMIT 1), @respond)";
            //    command.Connection = db.GetConnection();
            //    command.Parameters.AddWithValue("@employee_name", employee_name);
            //    command.Parameters.AddWithValue("@room_name", room_name);
            //    command.Parameters.AddWithValue("@respond", respond);
            //    try
            //    {
            //        command.ExecuteNonQuery();
            //        MessageBox.Show("Обязанность добавлена сотруднику!");
            //    }
            //    catch
            //    {
            //        MessageBox.Show("Ошибка запроса");
            //    }

            //    db.CloseConnection();
            //}
        }

        private void WriteForm8_Load(object sender, RoutedEventArgs e)
        {
            using (var db = new MyDbContext())
            {
                var employes = db.Empl.Select(p => new
                {
                    fullName = p.fullName
                });

                foreach (var employee in employes)
                {
                    ComboBoxEmployee.Items.Add(employee.fullName);
                }

                var rooms = db.Rooms.Select(p => new
                {
                    name = p.name
                });

                foreach (var room in rooms)
                {
                    ComboBoxRoom.Items.Add(room.name);
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
            //    ComboBoxEmployee.Items.Add(row[0]);
            //}

            //dt = new DataTable();
            //using (NpgsqlCommand command = new NpgsqlCommand())
            //{
            //    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
            //    {
            //        BD db = new BD();
            //        db.OpenConnection();
            //        command.CommandText = "SELECT название_помещения FROM Помещение";
            //        command.Connection = db.GetConnection();
            //        adapter.SelectCommand = command;
            //        adapter.Fill(dt);
            //        db.CloseConnection();
            //    }
            //}

            //foreach (DataRow row in dt.Rows)
            //{
            //    ComboBoxRoom.Items.Add(row[0].ToString());
            //}
        }
    }
}
