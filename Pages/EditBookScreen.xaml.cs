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
using WPF.App3.Models;
using static WPF.App3.Navigator.Navigator;

namespace WPF.App3.Pages
{
    /// <summary>
    /// Interaction logic for EditBookScreen.xaml
    /// </summary>
    public partial class EditBookScreen : UserControl
    {
        private readonly DataContext _dataContext;
        private readonly Book _book;
        public EditBookScreen(DataContext dataContext, Book book)
        {
            InitializeComponent();
            _dataContext = dataContext;
            _book = book;
            tb_title.Text = book.Title;
            tb_author.Text = book.Author;
            tb_publisher.Text = book.Publisher;
            tb_pagesCount.Text = Convert.ToString(book.PageCount);
            tb_genre.Text = book.Genre;
            tb_year.Text = Convert.ToString(book.Year);
            tb_costPrice.Text = Convert.ToString(book.CostPrice);
            tb_sellingPrice.Text = Convert.ToString(book.SellingPrice);
            cb_IsContinuation.IsChecked = book.IsContinuation;
            tb_ratingBook.Text = Convert.ToString(book.RatingBook);
            tb_ratingAuthor.Text = Convert.ToString(book.RatingAuthor);


        }
        private void EditBook_Click(object sender, RoutedEventArgs e)
        {
            _book.Title = tb_title.Text;
            _book.Author = tb_author.Text;
            _book.Publisher = tb_publisher.Text;
            _book.PageCount = Convert.ToInt32(tb_pagesCount.Text);
            _book.Genre = tb_genre.Text;
            _book.Year = Convert.ToInt32(tb_year.Text);
            _book.CostPrice = Convert.ToInt32(tb_costPrice.Text);
            _book.SellingPrice = Convert.ToInt32(tb_sellingPrice.Text);
            _book.IsContinuation = cb_IsContinuation.IsChecked ?? false;
            _book.RatingBook = Convert.ToInt32(tb_ratingBook.Text);
            _book.RatingAuthor = Convert.ToInt32(tb_ratingAuthor.Text);
            _book.DateAdd = DateTime.Now;

            _dataContext.SaveChanges();
            MessageBox.Show("Book edited successfully!");
            NavigatorObject.Switch(new MainAdminScreen(_dataContext));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigatorObject.Switch(new MainAdminScreen(_dataContext));
        }
    }
}
