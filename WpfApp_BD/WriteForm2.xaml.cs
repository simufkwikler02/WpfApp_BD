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
    /// Interaction logic for WriteForm2.xaml
    /// </summary>
    public partial class WriteForm2 : Window
    {
        WriteForm writeForm;

        public WriteForm2(WriteForm writeForm)
        {
            InitializeComponent();
            this.writeForm = writeForm;
        }

        private void AddDish_Click(object sender, EventArgs e)
        {
            if (ComboBoxAuthor.SelectedItem is null || !textBoxCompound.Text.Any() || !textBoxName.Text.Any() || !textBoxPrice.Text.Any() || !textBoxWeight.Text.Any())
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

            double price;
            if (!double.TryParse(textBoxPrice.Text.Replace('.', ','), out price))
            {
                MessageBox.Show("Неверный формат цены блюда!");
                return;
            }

            double weight;
            if (!double.TryParse(textBoxWeight.Text.Replace('.', ','), out weight))
            {
                MessageBox.Show("Неверный формат веса блюда!");
                return;
            }

            var compound = textBoxCompound.Text;
            if (compound.Length > 300)
            {
                MessageBox.Show("Неверный формат состава блюда!");
                return;
            }

            var author_name = ComboBoxAuthor.SelectedItem.ToString();

            using (var db = new MyDbContext())
            {
                var new_dish = new Dish
                {
                    name = name,
                    price = price,
                    weight = weight,
                    compound = compound,
                    authorEmployee = db.Empl.Where(p => p.fullName == author_name).FirstOrDefault()
                };

                db.Dishes.Add(new_dish);
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
            //    command.CommandText = "INSERT INTO Блюдо (название_блюда, цена_блюда, вес_блюда, состав_блюда, автор_id) VALUES (@name,@price,@weight,@compound,(SELECT id FROM Сотрудник WHERE фио = @author_name LIMIT 1))";
            //    command.Connection = db.GetConnection();
            //    command.Parameters.AddWithValue("@name", name);
            //    command.Parameters.AddWithValue("@price", price);
            //    command.Parameters.AddWithValue("@weight", weight);
            //    command.Parameters.AddWithValue("@compound", compound);
            //    command.Parameters.AddWithValue("@author_name", author_name);

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

        private void closeButton(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void WriteForm2_Load(object sender, RoutedEventArgs e)
        {
            using (var db = new MyDbContext())
            {
                var employes = db.Empl.Select(p => new
                {
                    fullName = p.fullName
                });

                foreach (var employee in employes)
                {
                    ComboBoxAuthor.Items.Add(employee.fullName);
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
            //    ComboBoxAuthor.Items.Add(row[0].ToString());
            //}
        }

        private void backButton(object sender, RoutedEventArgs e)
        {
            this.Hide();
            writeForm.Show();
            this.Close();
        }
    }
}
