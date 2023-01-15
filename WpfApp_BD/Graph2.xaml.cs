using System;
using Npgsql;
using System.Data;
using ScottPlot;
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
    /// Interaction logic for Graph2.xaml
    /// </summary>
    public partial class Graph2 : Window
    {
        FormRead formread;
        int numberRoom;
        int maxItem;
        int minItem;

        public Graph2(FormRead formread)
        {
            InitializeComponent();
            this.formread = formread;
        }

        private void closeButton(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void backButton(object sender, RoutedEventArgs e)
        {
            this.Hide();
            formread.Show();
            this.Close();
        }

        private void WpfPlot2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Build_Click(object sender, RoutedEventArgs e)
        {
            this.chart1.Reset();

            if (!textBox1.Text.Any())
            {
                this.numberRoom = int.MaxValue;
            }
            else
            {
                int number;
                if (!Int32.TryParse(textBox1.Text, out number))
                {
                    MessageBox.Show("Неверный формат количества сотрудников!");
                    return;
                }

                this.numberRoom = number;
            }

            if (!textBox2.Text.Any())
            {
                this.maxItem = int.MaxValue;
            }
            else
            {
                int number;
                if (!Int32.TryParse(textBox2.Text, out number))
                {
                    MessageBox.Show("Неверный формат инвентаря!");
                    return;
                }

                this.maxItem = number;
            }

            if (!textBox3.Text.Any())
            {
                this.minItem = int.MinValue;
            }
            else
            {
                int number;
                if (!Int32.TryParse(textBox3.Text, out number))
                {
                    MessageBox.Show("Неверный формат инвентаря!");
                    return;
                }

                this.minItem = number;
            }


            //DataTable dt = new DataTable();
            //using (NpgsqlCommand command = new NpgsqlCommand())
            //{
            //    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
            //    {
            //        BD db = new BD();
            //        db.OpenConnection();
            //        command.CommandText = "SELECT название_помещения, count(Столовый_инвентарь) FROM Помещение LEFT JOIN Столовый_инвентарь ON Помещение.id = Столовый_инвентарь.помещение_id GROUP BY Помещение.id HAVING count(Столовый_инвентарь) <= @maxItem and count(Столовый_инвентарь) >= @minItem ORDER BY count(Столовый_инвентарь) DESC LIMIT @numberRoom";
            //        command.Connection = db.GetConnection();
            //        command.Parameters.AddWithValue("@maxItem", maxItem);
            //        command.Parameters.AddWithValue("@minItem", minItem);
            //        command.Parameters.AddWithValue("@numberRoom", numberRoom);
            //        adapter.SelectCommand = command;
            //        adapter.Fill(dt);
            //        db.CloseConnection();
            //    }
            //}

            using (var db = new MyDbContext())
            {
                var em = db.Invent.GroupBy(g => g.room.name).Select(p => new
                {
                    count = p.Count(),
                    name = p.Key
                }).Where(p => p.count <= maxItem && p.count >= minItem).OrderByDescending(p => p.count).Take(numberRoom);


                List<ScottPlot.Plottable.Bar> bars = new();
                int iter = 0;
                foreach (var row in em)
                {
                    ScottPlot.Plottable.Bar bar = new()
                    {
                        Value = row.count,
                        Position = iter,
                        FillColor = Palette.Category10.GetColor(7),
                        Label = row.name,
                        LineWidth = 1,
                    };
                    bars.Add(bar);
                    iter++;
                }
                this.chart1.Plot.AddBarSeries(bars);
                this.chart1.Refresh();
            }
        }
    }
}
