using Klantbeheer.Domain;
using KlantBeheer.WPF.UserModels;
using Repository.ADO;
using System;
//using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace KlantBeheer.WPF
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Voor WPF updating: gebruik niet List<> maar ObservableCollection<>; deze gebruikt de uiterst belangrijke interface INotifyPropertyChanged
        private ObservableCollection<UserViewModel> _users = new();     

        private CustomerManager _customerManager = new();

        public MainWindow()
        {
            InitializeComponent();


            _users.Add(new UserViewModel(new User { Id = 1, Name = "John Doe", Birthday = new DateTime(1971, 7, 23) }));
            _users.Add(new UserViewModel(new User { Id = 2, Name = "Jane Doe", Birthday = new DateTime(1974, 1, 17) }));
            _users.Add(new UserViewModel(new User { Id = 3, Name = "Sammy Doe", Birthday = new DateTime(1991, 9, 2) }));

            dgSimple.ItemsSource = _users;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            _users.Add(new UserViewModel( new User { Id = 4, Name = "Luc Vervoort", Birthday = new DateTime(1967, 9, 2) }));

            //_customerManager.Add(new Customer("Luc", "Wondelgem"));
        }

        private void btnUpdateDetail_Click(object sender, RoutedEventArgs e)
        {
            _users[3].Name = "Jefke";
        }
    }
}
