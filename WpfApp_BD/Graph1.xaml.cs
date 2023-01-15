using System;
using ScottPlot;
using Npgsql;
using System.Data;
using System.Data.Entity;
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
using System.Diagnostics;

namespace WpfApp_BD
{
    /// <summary>
    /// Interaction logic for Graph1.xaml
    /// </summary>
    public partial class Graph1 : Window
    {
        FormRead formread;
        int numberEmployee;
        int maxSalary;
        int minSalary;

        public Graph1(FormRead formread)
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

        private void Build_Click(object sender, RoutedEventArgs e)
        {
            this.chart1.Reset();

            if (!textBox1.Text.Any())
            {
                this.numberEmployee = int.MaxValue;
            }
            else
            {
                int number;
                if (!Int32.TryParse(textBox1.Text, out number))
                {
                    MessageBox.Show("Неверный формат количества сотрудников!");
                    return;
                }

                this.numberEmployee = number;
            }

            if (!textBox2.Text.Any())
            {
                this.maxSalary = int.MaxValue;
            }
            else
            {
                int number;
                if (!Int32.TryParse(textBox2.Text, out number))
                {
                    MessageBox.Show("Неверный формат зарплаты!");
                    return;
                }

                this.maxSalary = number;
            }

            if (!textBox3.Text.Any())
            {
                this.minSalary = int.MinValue;
            }
            else
            {
                int number;
                if (!Int32.TryParse(textBox3.Text, out number))
                {
                    MessageBox.Show("Неверный формат зарплаты!");
                    return;
                }

                this.minSalary = number;
            }

            //DataTable dt = new DataTable();
            //using (NpgsqlCommand command = new NpgsqlCommand())
            //{
            //    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
            //    {
            //        BD db = new BD();
            //        db.OpenConnection();
            //        command.CommandText = "SELECT фио, зарплата FROM Сотрудник LEFT JOIN Должность ON Сотрудник.должность_id = Должность.id WHERE зарплата <= @maxSalary and зарплата >= @minSalary ORDER BY зарплата DESC LIMIT @numberEmployee";
            //        command.Connection = db.GetConnection();
            //        command.Parameters.AddWithValue("@maxSalary", maxSalary);
            //        command.Parameters.AddWithValue("@minSalary", minSalary);
            //        command.Parameters.AddWithValue("@numberEmployee", numberEmployee);
            //        adapter.SelectCommand = command;
            //        adapter.Fill(dt);
            //        db.CloseConnection();
            //    }
            //}


            using (var db = new MyDbContext())
            {
                var em = db.Empl.Select(p => new
                {
                    fullNmae = p.fullName,
                    salary = p.postEmployee.salary
                }).Where(p => p.salary <= maxSalary && p.salary >= minSalary).OrderByDescending(p => p.salary).Take(numberEmployee);


                List<ScottPlot.Plottable.Bar> bars = new();
                int iter = 0;
                foreach (var row in em)
                {
                    ScottPlot.Plottable.Bar bar = new()
                    {
                        Value = row.salary,
                        Position = iter,
                        FillColor = Palette.Category10.GetColor(7),
                        Label = row.fullNmae,
                        LineWidth = 1,
                    };
                    bars.Add(bar);
                    iter++;
                }

                this.chart1.Plot.AddBarSeries(bars);
                this.chart1.Refresh();
            }
        }

        private void WpfPlot1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
