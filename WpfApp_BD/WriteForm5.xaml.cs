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
    /// Interaction logic for WriteForm5.xaml
    /// </summary>
    public partial class WriteForm5 : Window
    {
        WriteForm writeForm;

        public WriteForm5(WriteForm writeForm)
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

        private void AddRoom(object sender, RoutedEventArgs e)
        {
            if (!textBoxDateCleaning.Text.Any() || !textBoxName.Text.Any())
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            var name = textBoxName.Text;
            if (name.Length > 200)
            {
                MessageBox.Show("Неверный формат названия помещения!");
                return;
            }

            DateTime date;
            if (!DateTime.TryParse(textBoxDateCleaning.Text, out date))
            {
                MessageBox.Show("Неверный формат даты уборки!");
                return;
            }

            using (var db = new MyDbContext())
            {
                var new_room = new Room()
                {
                    name = name,
                    dateOfLastCleaning = date,
                };

                db.Rooms.Add(new_room);
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
            //    command.CommandText = "INSERT INTO Помещение (название_помещения, дата_последней_уборки) VALUES (@name, @date)";
            //    command.Connection = db.GetConnection();
            //    command.Parameters.AddWithValue("@name", name);
            //    command.Parameters.AddWithValue("@date", date);
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
