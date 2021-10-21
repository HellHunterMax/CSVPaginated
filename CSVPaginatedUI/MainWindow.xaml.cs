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
        private int _page = 0;
        private int _numberOfPages;
        private int _numberOfUsers = 10;
        public IEnumerable<User> Users;
        private readonly IAccountService _accountService;
        private CollectionViewSource _itemCollectionViewSource;
        private UserFilterDto _filter = new UserFilterDto();
        public ObservableCollection<User> MyCollection { get; set; }

        public MainWindow()
        {
            IUserRepository repository = new UserRepository(@"C:\Code_Projects\2021\CSVPaginated\CSVPaginated\CSVPaginatedApp\Data\targets.csv");
            _accountService = new AccountService(repository);

            InitializeComponent();
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
            if (_page < 1)
            {
                return;
            }

            _page--;
            await UpdateUsersAsync();
        }

        private async Task NextPage()
        {
            if (_page +1 >= _numberOfPages)
            {
                return;
            }
            _page++;
            await UpdateUsersAsync();
        }

        private async Task UpdateUsersAsync()
        {
            UpdateFilter();
            Users = await _accountService.GetUsersAsync(_numberOfUsers, _page, _filter);
            
            _numberOfPages = _accountService.GetNumberOfPages(_numberOfUsers);

            if (_page + 1 > _numberOfPages )
            {
                _page = _numberOfPages -1;
            }

            MyCollection = new ObservableCollection<User>(Users);
            _itemCollectionViewSource.Source = MyCollection;

            PagesTextBlock.Text = $"Pages: {_numberOfPages}";
            PageNumberTextBlock.Text = $"Page: {_page + 1}";
        }

        private void UpdateFilter()
        {
            if (int.TryParse(MaximumAgeTextBox.Text, out int maxAge))
            {
                _filter.MaximumAge = maxAge;
            }
            if (int.TryParse(MinimumAgeTextBox.Text, out int minAge))
            {
                _filter.MinimumAge = minAge;
            }
            if (decimal.TryParse(MaximumSalaryTextBox.Text, out decimal maxSalary))
            {
                _filter.MaximumSalary = maxSalary;
            }
            if (decimal.TryParse(MinimumSalaryTextBox.Text, out decimal minSalary))
            {
                _filter.MinimumSalary = minSalary;
            }
        }

        private async void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            await UpdateUsersAsync();
        }
    }
}
