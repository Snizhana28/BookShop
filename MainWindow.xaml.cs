using WPF.App3.Navigator;
using WPF.App3.Pages;
using System;
using System.Windows;
using System.Windows.Controls;
using static WPF.App3.Navigator.Navigator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WPF.App3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DataContext _dataContext;
        public MainWindow()
        {
            InitializeComponent();
            NavigatorObject.pageSwitcher = this;
            var config = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();

            DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer(config.GetConnectionString("Default"))
                .Options;
            _dataContext = new DataContext(options);
           // NavigatorObject.Switch(new AddBookScreen(_dataContext));
        }

        public Action? CloseAction { get; set; }

        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        public void Navigate(UserControl nextPage, object state)
        {
            this.Content = nextPage;
            INavigator? s = nextPage as INavigator;

            if (s != null)
                s.UtilizeState(state);
            else
                throw new ArgumentException("NextPage is not INavigator! " + nextPage.Name.ToString());
        }

        private void Button_Sign_In(object sender, RoutedEventArgs e) //увійти
        {

            NavigatorObject.Switch(new LoginScreen(_dataContext));
        }

        private void Button_Sign_Up(object sender, RoutedEventArgs e) //зареєструватися
        {
            NavigatorObject.Switch(new RegisterScreen(_dataContext));
        }
    }
}
