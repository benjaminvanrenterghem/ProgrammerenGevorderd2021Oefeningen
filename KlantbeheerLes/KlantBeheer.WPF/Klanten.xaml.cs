using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using Klantbeheer.Domain;
using Klantbeheer.Domain.Exceptions.ModelExceptions;
using KlantBeheer.WPF.Languages;
using Microsoft.Extensions.DependencyInjection;

namespace KlantBeheer.WPF
{
    /// <summary>
    /// Interaction logic for Klanten.xaml
    /// </summary>
    public partial class Klanten : Window
    {
        #region Properties
        // Interface INotifyPropertyChanged
        private ObservableCollection<Customer> _customers;
        #endregion

        #region Ctor
        public Klanten()
        {
            InitializeComponent();
            var objects = Context.ServiceProvider.GetRequiredService<Repository.ADO.ICustomerManager>().GetAll(); //Context.CustomerManager.GetAll();
            _customers = new ObservableCollection<Customer>(); 
            foreach(var o in objects)
            {
                _customers.Add(o as Customer);
            }
            dgKlanten.ItemsSource = _customers;
            _customers.CollectionChanged += _klanten_CollectionChanged;
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Doorgeven aan business laag dat klant werd toegevoegd of verwijderd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _klanten_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Customer customer in e.OldItems)
                {
                    Context.ServiceProvider.GetRequiredService<Repository.ADO.ICustomerManager>().Remove(customer); //Context.CustomerManager.GetAll();
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Customer customer in e.NewItems)
                {
                    // klant wordt toegevoegd en id wordt teruggeworpen
                    Context.ServiceProvider.GetRequiredService<Repository.ADO.ICustomerManager>().Add(customer); //customer.SetCustomerID(Context.CustomerManager.Add(customer));
                }
            }            
        }

        /// <summary>
        /// Kruip tussen wanneer de gebruiker met de delete toets een rij verwijdert uit een DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgKlanten_PreviewDeleteCommandHandler(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            if(e.Command == DataGrid.DeleteCommand)
            {
                if (!(MessageBox.Show(Translations.DeleteCustomer, Translations.Confirm, MessageBoxButton.YesNo) == MessageBoxResult.Yes))
                {
                    // Cancel Delete.
                    e.Handled = true;
                }
            }
        }

        private void BtnNieuweKlant_Click(object sender, RoutedEventArgs e)
        {
            // Preconditie
            if (string.IsNullOrEmpty(TbKlantNaam?.Text) || string.IsNullOrEmpty(TbKlantAdres?.Text))
            {
                MessageBox.Show(Translations.CustomerData);
                return;
            }

            var customer = new Customer(TbKlantNaam.Text, TbKlantAdres.Text);

            foreach (var klant in _customers)
            {
                if (klant.Name == customer.Name && klant.Address == customer.Address)
                {
                    TbKlantNaam.Text = null;
                    TbKlantAdres.Text = null;
                    BtnNieuweKlant.IsEnabled = false;
                    throw new CustomerException(Translations.CustomerInList);
                }
            }
            // Omdat we een ObservableCollection<Klant> gebruiken, wordt onze wijziging meteen doorgegeven naar de gui (.Items wijzigen zou threading problemen geven):
            // Omdat we ObservableCollection<Klant> gebruiken en er een event gekoppeld is aan delete/add hiervan, wordt ook de business layer aangepast!
            _customers.Add(customer);

            TbKlantNaam.Text = null;
            TbKlantAdres.Text = null;
            BtnNieuweKlant.IsEnabled = false;
        }

        private void Tb_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(TbKlantNaam.Text) && !string.IsNullOrEmpty(TbKlantAdres.Text))
            {
                BtnNieuweKlant.IsEnabled = true;
            }
            else
            {
                BtnNieuweKlant.IsEnabled = false;
            }
        }
        /// <summary>
        /// Deze methode wordt steeds uitgevoerd indien er geklikt wordt op de verwijderknop van de aanwezige producten en zorgt ervoor 
        /// dat het product uit de lijst verwijderd wordt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgKlanten.SelectedIndex < 0)
                return;
            if (!(MessageBox.Show(Translations.DeleteCustomer, Translations.Confirm, MessageBoxButton.YesNo) == MessageBoxResult.Yes))
            {
                // Cancel Delete.
                e.Handled = true;
            }
            else _customers.Remove(dgKlanten.SelectedItem as Customer);
        }
        #endregion
    }
}
