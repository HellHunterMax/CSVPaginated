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
        //DataContext="{Binding Path=MyCollection}"
        private int _Page = 0;
        private int _NumberOfUsers = 10;
        public IEnumerable<User> _Users;
        private readonly IUserRepository repo;
        private CollectionViewSource itemCollectionViewSource;
        public ObservableCollection<User> MyCollection { get; set; }

        public MainWindow()
        {
            repo = new UserRepository();

            InitializeComponent();
            itemCollectionViewSource = (CollectionViewSource)(FindResource("MyCollection"));

            UpdateUsersAsync().Wait();
            // this.UsersDatagrid.ItemsSource = MyCollection;
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            PreviousPage();
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            NextPage();
        }

        private void PreviousPage()
        {
            if (_Page < 1)
            {
                return;
            }

            _Page--;
            UpdateUsersAsync().Wait();
        }

        private void NextPage()
        {
            if (_Users.Count() < 10)
            {
                return;
            }
            _Page++;
            UpdateUsersAsync().Wait();
        }

        private async Task UpdateUsersAsync()
        {
            _Users = await repo.GetUsersAsync(_NumberOfUsers, _Page);
            MyCollection = new ObservableCollection<User>(_Users);
            itemCollectionViewSource.Source = MyCollection;
        }
    }
}
