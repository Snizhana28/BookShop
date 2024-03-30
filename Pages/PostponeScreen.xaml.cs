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

namespace WPF.App3.Pages
{
    /// <summary>
    /// Interaction logic for PostponeScreen.xaml
    /// </summary>
    public partial class PostponeScreen : Window
    {
        public int UserId { get; private set; }

        public PostponeScreen()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtUserId.Text, out int userId))
            {
                UserId = userId;
                DialogResult = true; // Позначаємо, що вікно було закрите з кнопкою "Ok"
            }
            else
            {
                MessageBox.Show("Please enter a valid User ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}