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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static WPF.App3.Navigator.Navigator;
using WPF.App3.Models;

namespace WPF.App3.Pages
{
    /// <summary>
    /// Interaction logic for RegisterScreen.xaml
    /// </summary>
    public partial class RegisterScreen : UserControl
    {
        private readonly DataContext dataContext;
        public RegisterScreen(DataContext _dataContext)
        {
            InitializeComponent();
            dataContext = _dataContext;
        }
        private void Button_Click_Register(object sender, RoutedEventArgs e)
        {
            if (tbRole.Text != "admin" && tbRole.Text != "user")
            {
                MessageBox.Show("Role can be only 'admin' or 'user'");
            }
            else if(string.IsNullOrWhiteSpace(tbLogin.Text) || string.IsNullOrWhiteSpace(tbPassword.Password))
            {
                MessageBox.Show("Please enter login and password");
            }
            else if (dataContext.User.Any(u => u.Login == tbLogin.Text))
            {
                MessageBox.Show("User with this login already exists!");
            }
            else if (tbPassword.Password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long!");
            }
            else
            {
                var user = new User
                {
                    Login = tbLogin.Text,
                    Password = tbPassword.Password,
                    Role = tbRole.Text
                };
                dataContext.User.Add(user);
                dataContext.SaveChanges();
                MessageBox.Show("You have successfully registered!");
                NavigatorObject.Switch(new LoginScreen(dataContext));
            }
        }
    }
}
