using CSVPaginatedApp.Models;
using CSVPaginatedApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CSVPaginatedUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _Page = 0;
        private int _NumberOfPages;
        private int _NumberOfUsers = 10;
        public IEnumerable<User> _Users;
        private readonly IUserRepository repo;
        private CollectionViewSource itemCollectionViewSource;
        public ObservableCollection<User> MyCollection { get; set; }

        public MainWindow()
        {
            repo = new UserRepository();
            _NumberOfPages = repo.GetNumberOfPages(_NumberOfUsers);

            InitializeComponent();
            PagesTextBlock.Text += $"{_NumberOfPages}";
            itemCollectionViewSource = (CollectionViewSource)(FindResource("MyCollection"));

            new Action(async () => await UpdateUsersAsync())();
        }

        private async void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            await PreviousPage();
        }

        private async void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            await NextPage();
        }

        private async Task PreviousPage()
        {
            if (_Page < 1)
            {
                return;
            }

            _Page--;
            await UpdateUsersAsync();
        }

        private async Task NextPage()
        {
            if (_Page +1 >= _NumberOfPages)
            {
                return;
            }
            _Page++;
            await UpdateUsersAsync();
        }

        private async Task UpdateUsersAsync()
        {
            _Users = await repo.GetUsersAsync(_NumberOfUsers, _Page);
            MyCollection = new ObservableCollection<User>(_Users);
            itemCollectionViewSource.Source = MyCollection;
            PageNumberTextBlock.Text = $"Page: {_Page + 1}";
        }
    }
}
