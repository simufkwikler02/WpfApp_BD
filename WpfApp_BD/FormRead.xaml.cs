using System;
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
    /// Interaction logic for FormRead.xaml
    /// </summary>
    public partial class FormRead : Window
    {
        MainWindow mainForm;

        public FormRead(MainWindow mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void closeButton(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void backButton(object sender, RoutedEventArgs e)
        {
            this.Hide();
            mainForm.Show();
            this.Close();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void query1_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Graph1 graph1 = new Graph1(this);
            graph1.Show();
        }

        private void query2_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Graph2 graph2 = new Graph2(this);
            graph2.Show();
        }
    }
}
