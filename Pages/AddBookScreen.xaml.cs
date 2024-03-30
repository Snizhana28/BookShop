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
            Book book1 = new Book
            {
                Title = "The Lord of the Rings",
                Author = "J.R.R. Tolkien",
                Publisher = "George Allen & Unwin",
                PageCount = 1009,
                Genre = "Fantasy",
                Year = 1954,
                CostPrice = 15,
                SellingPrice = 30,
                IsContinuation = true,
                RatingBook = 4,
                RatingAuthor = 4,
                DateAdd = DateTime.Now
            };
            Book book2 = new Book
            {
                Title = "The Hobbit",
                Author = "J.R.R. Tolkien",
                Publisher = "George Allen & Unwin",
                PageCount = 310,
                Genre = "Fantasy",
                Year = 1937,
                CostPrice = 8,
                SellingPrice = 18,
                IsContinuation = true,
                RatingBook = 8,
                RatingAuthor = 9,
                DateAdd = DateTime.Now
            };
            Book book3 = new Book
            {
                Title = "Brave New World",
                Author = "Aldous Huxley",
                Publisher = "Chatto & Windus",
                PageCount = 311,
                Genre = "Dystopian Fiction",
                Year = 1932,
                CostPrice = 10,
                SellingPrice = 21,
                IsContinuation = false,
                RatingBook = 8,
                RatingAuthor = 8,
                DateAdd = DateTime.Now
            };
            Book book4 = new Book
            {
                Title = "To the Lighthouse",
                Author = "Virginia Woolf",
                Publisher = "Hogarth Press",
                PageCount = 209,
                Genre = "Modernist Literature",
                Year = 1927,
                CostPrice = 13,
                SellingPrice = 28,
                IsContinuation = false,
                RatingBook = 10,
                RatingAuthor = 5,
                DateAdd = DateTime.Now
            };
            _dataContext.Book.Add(book1);
            _dataContext.Book.Add(book2);
            _dataContext.Book.Add(book3);
            _dataContext.Book.Add(book4);
            _dataContext.SaveChanges();
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
