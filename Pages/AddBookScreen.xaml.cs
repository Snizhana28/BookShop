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
    /// Interaction logic for AddBookScreen.xaml
    /// </summary>
    public partial class AddBookScreen : UserControl
    {
        private readonly DataContext _dataContext;
        public AddBookScreen(DataContext dataContext)
        {
            InitializeComponent();
            _dataContext = dataContext;
        }
        private void AddBook_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _dataContext.Book.Add(new Book
            {
                Title = tb_title.Text,
                Author = tb_author.Text,
                Publisher = tb_publisher.Text,
                PageCount = Convert.ToInt32(tb_pagesCount.Text),
                Genre = tb_genre.Text,
                Year = Convert.ToInt32(tb_year.Text),
                CostPrice = Convert.ToInt32(tb_costPrice.Text),
                SellingPrice = Convert.ToInt32(tb_sellingPrice.Text),
                IsContinuation = cb_IsContinuation.IsChecked ?? false,
                RatingBook = Convert.ToInt32(tb_ratingBook.Text),
                RatingAuthor = Convert.ToInt32(tb_ratingAuthor.Text),
                DateAdd = DateTime.Now

            });
            _dataContext.SaveChanges();
            MessageBox.Show("Book added successfully!");
            NavigatorObject.Switch(new MainAdminScreen(_dataContext));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigatorObject.Switch(new MainAdminScreen(_dataContext));
        }
    }
}
