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
    /// Interaction logic for MainAdminScreen.xaml
    /// </summary>
    public partial class MainAdminScreen : UserControl
    {
        private readonly DataContext _dataContext;

        public MainAdminScreen(DataContext dataContext)
        {
            InitializeComponent();
            _dataContext = dataContext;
            dataGrid.ItemsSource = _dataContext.Book.ToList();
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
        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            NavigatorObject.Switch(new AddBookScreen(_dataContext));
        }
        private void DeleteBook_Click(object sender, RoutedEventArgs e)
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
            else
            {
                MessageBox.Show("Please select at least one item to delete.");
            }

        }

        private void EditBook_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dataGrid.SelectedItems;
            if (selectedItem.Count == 1)
            {
                var selectedBook = selectedItem[0] as Book;
                if (selectedBook != null)
                {
                    NavigatorObject.Switch(new EditBookScreen(_dataContext, selectedBook));
                }
            }
            else
            {
                MessageBox.Show("Please select at least one item to edit.");
            }
        }
        private void SellBook_Click(object sender, RoutedEventArgs e)
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
                MessageBox.Show("Book sold successfully!");
                dataGrid.ItemsSource = _dataContext.Book.ToList();
            }
            else
            {
                MessageBox.Show("Please select at least one item to sell.");
            }

        }

        private void WriteOfBook_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = dataGrid.SelectedItems;
            if (selectedItems.Count > 0)
            {
                try
                {
                    foreach (var selectedItem in selectedItems)
                    {
                        if (selectedItem is Book yourObject)
                        {
                            WriteOfBook writtenOffBook = new WriteOfBook
                            {
                                BookId = yourObject.Id,
                            };

                            _dataContext.WriteOfBooks.Add(writtenOffBook);
                        }
                    }

                    _dataContext.SaveChanges();
                    MessageBox.Show("Book(s) have been written off successfully!");
                    dataGrid.ItemsSource = _dataContext.Book.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while writing off the book(s): {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select at least one item to write off.");
            }
        }
        private void PutOnSaleBook_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = dataGrid.SelectedItems;
            if (selectedItems.Count > 0)
            {
                try
                {
                    foreach (var selectedItem in selectedItems)
                    {
                        if (selectedItem is Book yourObject)
                        {
                            yourObject.SellingPrice = (int)(yourObject.SellingPrice * 0.9);
                        }
                    }

                    _dataContext.SaveChanges();
                    MessageBox.Show("Book(s) have been put on sale successfully!");
                    dataGrid.ItemsSource = _dataContext.Book.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while putting the book(s) on sale: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select at least one item to put on sale.");
            }
        }
        private void PostponeBook_Click(object sender, RoutedEventArgs e)
        {
            var userIdDialog = new PostponeScreen();
            bool? result = userIdDialog.ShowDialog();

            if (result == true)
            {
                int userId = userIdDialog.UserId;
                var selectedBooks = dataGrid.SelectedItems.Cast<Book>().ToList();

                if (selectedBooks.Any())
                {
                    try
                    {
                        foreach (var book in selectedBooks)
                        {
                            PostponedBook postponedBook = new PostponedBook
                            {
                                BookId = book.Id,
                                UserId = userId,
                                PostponedDate = DateTime.Now
                            };

                            _dataContext.PostponedBooks.Add(postponedBook);
                        }

                        _dataContext.SaveChanges();
                        MessageBox.Show($"Selected books have been postponed for user with ID {userId}.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        dataGrid.ItemsSource = _dataContext.Book.ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please select at least one book to postpone.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
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
