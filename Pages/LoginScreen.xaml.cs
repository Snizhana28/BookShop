using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static WPF.App3.Navigator.Navigator;

namespace WPF.App3.Pages
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : UserControl
    {
        private readonly DataContext _dataContext;

        public LoginScreen(DataContext dataContext)
        {
            InitializeComponent();
            _dataContext = dataContext;
        }
        private void Button_Click_login(object sender, RoutedEventArgs e)
        {
            if (tbRole.Text != "admin" && tbRole.Text != "user")
            {
                MessageBox.Show("Role can be only 'admin' or 'user'");
            }

            var user = _dataContext.User.FirstOrDefault(u => u.Login == tbLogin.Text);

            if (user != null)
            {
                if (user.Password == tbPassword.Password && user.Role == tbRole.Text)
                {
                    if (user.Role == "admin")
                    {
                        NavigatorObject.Switch(new MainAdminScreen(_dataContext));
                    }
                    else if (user.Role == "user")
                    {
                        NavigatorObject.Switch(new MainUserScreen(_dataContext));
                    }
                }
                else if (tbLogin.Text == "" || tbPassword.Password == "")
                    MessageBox.Show("Please enter login and password");
                else
                {
                    MessageBox.Show("Uncorrect login or password");
                }

            }
        }
    }
}
