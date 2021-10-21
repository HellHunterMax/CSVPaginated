using CSVPaginatedApp.Models;
using CSVPaginatedApp.Repository;
using CSVPaginatedApp.Service;
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
        private readonly IAccountService _accountService;
        private CollectionViewSource _itemCollectionViewSource;
        public ObservableCollection<User> MyCollection { get; set; }

        public MainWindow()
        {
            IUserRepository repository = new UserRepository(@"C:\Code_Projects\2021\CSVPaginated\CSVPaginated\CSVPaginatedApp\Data\targets.csv");
            _accountService = new AccountService(repository);
            _NumberOfPages = repository.GetNumberOfPages(_NumberOfUsers);

            InitializeComponent();
            PagesTextBlock.Text += $"{_NumberOfPages}";
            _itemCollectionViewSource = (CollectionViewSource)(FindResource("MyCollection"));

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
            _Users = await _accountService.GetUsersAsync(_NumberOfUsers, _Page);
            MyCollection = new ObservableCollection<User>(_Users);
            _itemCollectionViewSource.Source = MyCollection;
            PageNumberTextBlock.Text = $"Page: {_Page + 1}";
        }
    }
}
