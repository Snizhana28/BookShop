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

namespace WPF.App3.Pages
{
    /// <summary>
    /// Interaction logic for MainUserScreen.xaml
    /// </summary>
    public partial class MainUserScreen : UserControl
    {
        private readonly DataContext _dataContext;
        public MainUserScreen(DataContext dataContext)
        {
            InitializeComponent();
            _dataContext = dataContext;
            dataGrid.ItemsSource = _dataContext.Book.ToList();
        }

        //BuyBook_Click
        private void BuyBook_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = dataGrid.SelectedItems;
            if (selectedItems.Count > 0)
            {
                List<int> selectedIds = new List<int>();

                foreach (var selectedItem in selectedItems)
                {
                    if (selectedItem is Book yourObject)
                    {
                        int id = yourObject.Id;
                        selectedIds.Add(id);
                    }
                }

                foreach (var id in selectedIds)
                {
                    Book item = _dataContext.Set<Book>().Find(id) ??
                    throw new ArgumentNullException();

                    _dataContext.Set<Book>().Remove(item);
                    _dataContext.SaveChanges();
                }
                dataGrid.ItemsSource = _dataContext.Book.ToList();
            }

            if (selectedItems == null)
            {
                MessageBox.Show("Please select a book to buy.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show($"You have successfully bought the book.", "\nBook Bought", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = tb_search.Text.Trim().ToLower();

            var books = _dataContext.Book
                                    .Where(b =>
                                            b.Title.ToLower().Contains(searchQuery) ||
                                            b.Author.ToLower().Contains(searchQuery) ||
                                            b.Genre.ToLower().Contains(searchQuery))
                                    .ToList();

            dataGrid.ItemsSource = books;
        }
        private void NewReleases_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = DateTime.Now.Date.AddDays(-7);
            DateTime endDate = DateTime.Now.Date;

            var newReleases = _dataContext.Book
                .Where(b => b.DateAdd >= startDate && b.DateAdd <= endDate)
                .ToList();

            StringBuilder message = new StringBuilder();
            message.AppendLine("New Releases for the Last 7 Days:");

            foreach (var book in newReleases)
            {
                message.AppendLine($"- {book.Title} by {book.Author} (Added on {book.DateAdd.ToShortDateString()})");
            }
            MessageBox.Show(message.ToString(), "New Releases", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void PopularBook_Click(object sender, RoutedEventArgs e)
        {
            var popularBooks = _dataContext.Book
                          .Where(a => a.RatingBook >= 5)
                          .OrderByDescending(a => a.RatingBook)
                          .ToList();

            StringBuilder message = new StringBuilder();
            message.AppendLine("Most Popular Books by Rating:");

            foreach (var book in popularBooks)
            {
                message.AppendLine($"- {book.Title} by {book.Author} (Rating: {book.RatingBook})");
            }
            MessageBox.Show(message.ToString(), "Popular Books by Rating", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void PopularAuthor_Click(object sender, RoutedEventArgs e)
        {
            var popularAuthors = _dataContext.Book
                          .Where(a => a.RatingAuthor >= 5)
                          .OrderByDescending(a => a.RatingAuthor)
                          .ToList();

            StringBuilder message = new StringBuilder();
            message.AppendLine("Best Authors by Average Rating:");

            foreach (var author in popularAuthors)
            {
                message.AppendLine($"- {author.Author} (Average Rating: {author.RatingAuthor})");
            }
            MessageBox.Show(message.ToString(), "Best Authors by Average Rating", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
