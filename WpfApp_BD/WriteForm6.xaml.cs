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
    /// Interaction logic for WriteForm6.xaml
    /// </summary>
    public partial class WriteForm6 : Window
    {
        WriteForm writeForm;

        public WriteForm6(WriteForm writeForm)
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

        private void AddInventory(object sender, RoutedEventArgs e)
        {
            if (!textBoxDate.Text.Any() || !textBoxName.Text.Any() || ComboBoxRoom.SelectedItem is null || !textBoxRules.Text.Any())
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            var name = textBoxName.Text;
            if (name.Length > 200)
            {
                MessageBox.Show("Неверный формат названия столового инвенторя!");
                return;
            }

            DateTime date;
            if (!DateTime.TryParse(textBoxDate.Text, out date))
            {
                MessageBox.Show("Неверный формат даты ввода в эксплуатацию!");
                return;
            }

            var rules = textBoxRules.Text;
            var room_name = ComboBoxRoom.SelectedItem.ToString();

            using (var db = new MyDbContext())
            {
                var new_inventory= new Inventory()
                {
                    name = name,
                    dateOfCommissioning = date,
                    rulesExploitation = rules,
                    room = db.Rooms.Where(p => p.name == room_name).FirstOrDefault()
                };

                db.Invent.Add(new_inventory);
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
            //    command.CommandText = $"INSERT INTO Столовый_инвентарь (название_столового_инвентаря, дата_ввода_в_эксплуатацию, правила_эксплуатации, помещение_id) VALUES (@name,@date,@rules,(SELECT id FROM Помещение WHERE название_помещения = @room_name LIMIT 1))";
            //    command.Connection = db.GetConnection();
            //    command.Parameters.AddWithValue("@name", name);
            //    command.Parameters.AddWithValue("@date", date);
            //    command.Parameters.AddWithValue("@rules", rules);
            //    command.Parameters.AddWithValue("@room_name", room_name);
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

        private void WriteForm6_Load(object sender, RoutedEventArgs e)
        {
            using (var db = new MyDbContext())
            {
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
