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
    /// Interaction logic for WriteForm.xaml
    /// </summary>
    public partial class WriteForm : Window
    {
        MainWindow mainForm;

        public WriteForm(MainWindow mainForm)
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
            WriteForm1 writeForm1 = new WriteForm1(this);
            writeForm1.Show();
        }

        private void query2_Click(object sender, RoutedEventArgs e)
        {

            this.Hide();
            WriteForm2 writeForm2 = new WriteForm2(this);
            writeForm2.Show();
        }

        private void query3_Click(object sender, RoutedEventArgs e)
        {

            this.Hide();
            WriteForm3 writeForm3 = new WriteForm3(this);
            writeForm3.Show();
        }

        private void query4_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            WriteForm4 writeForm4 = new WriteForm4(this);
            writeForm4.Show();
        }

        private void query5_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            WriteForm5 writeForm5 = new WriteForm5(this);
            writeForm5.Show();
        }

        private void query6_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            WriteForm6 writeForm6 = new WriteForm6(this);
            writeForm6.Show();
        }

        private void query7_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            WriteForm7 writeForm7 = new WriteForm7(this);
            writeForm7.Show();
        }

        private void query8_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            WriteForm8 writeForm8 = new WriteForm8(this);
            writeForm8.Show();
        }
    }
}
