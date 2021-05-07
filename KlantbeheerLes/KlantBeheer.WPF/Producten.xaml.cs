using Klantbeheer.Domain;
using Klantbeheer.Domain.Exceptions.ModelExceptions;
using KlantBeheer.WPF;
using KlantBeheer.WPF.Languages;
using KlantBestellingen.WPF.ViewModels;
using Serilog;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace KlantBestellingen.WPF
{

    /// <summary>
    /// Interaction logic for Producten.xaml
    /// </summary>
    public partial class Producten : Window
    {
        #region Properties
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        #endregion

        #region Ctor
        public Producten()
        {
            InitializeComponent();            
            this.DataContext = new ProductenViewModel();

            var objects = Context.ProductManager.GetAll();

            _products = new ObservableCollection<Product>();
            foreach (var o in objects)
            {
                _products.Add(o as Product);
            }
            _products.CollectionChanged += _producten_CollectionChanged;
            dgProducten.ItemsSource = _products;

            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Log.Debug("-> Timer_Tick");
            lblTime.Content = DateTime.Now.ToLongTimeString();
            Log.Information(lblTime.Content.ToString());
            Log.Debug("<- Timer_Tick");
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Doorgeven aan business laag dat klant werd toegevoegd of verwijderd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _producten_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Product product in e.OldItems)
                {
                    Context.ProductManager.Remove(product);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Product product in e.NewItems)
                {
                    product.SetProductID(Context.ProductManager.Add(product)); // Product wordt toegevoegd en id wordt teruggeworpen 
                }
            }
        }

        /// <summary>
        /// Kruip tussen wanneer de gebruiker met de delete toets een rij verwijdert uit een DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgProducten_PreviewDeleteCommandHandler(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            if(e.Command == DataGrid.DeleteCommand)
            {
                if (!(MessageBox.Show(Translations.DeleteProduct, Translations.Confirm, MessageBoxButton.YesNo) == MessageBoxResult.Yes))
                {
                    // Cancel Delete.
                    e.Handled = true;
                }
            }
        }

        private void BtnNieuwProduct_Click(object sender, RoutedEventArgs e)
        {
            // Preconditie
            if (string.IsNullOrEmpty(TbProductNaam?.Text) || string.IsNullOrEmpty(TbProductPrijs?.Text))
            {
                MessageBox.Show(Translations.ProductData);
                return;
            }
            double TbProductPrijsDouble;
            double.TryParse(TbProductPrijs.Text, out TbProductPrijsDouble);
            var product = new Product(TbProductNaam.Text, TbProductPrijsDouble);

            foreach (var p in _products)
            {
                if (p.Name == product.Name)
                {
                    TbProductNaam.Text = null;
                    TbProductPrijs.Text = null;
                    BtnNieuwProduct.IsEnabled = false;
                    throw new ProductException(Translations.ProductInList);
                }
            }
            // Omdat we een ObservableCollection<Klant> gebruiken, wordt onze wijziging meteen doorgegeven naar de gui (.Items wijzigen zou threading problemen geven):
            // Omdat we ObservableCollection<Klant> gebruiken en er een event gekoppeld is aan delete/add hiervan, wordt ook de business layer aangepast!
            _products.Add(product);

            TbProductNaam.Text = null;
            TbProductPrijs.Text = null;
            BtnNieuwProduct.IsEnabled = false;
        }

        private void Tb_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(TbProductNaam.Text) && !string.IsNullOrEmpty(TbProductPrijs.Text))
            {
                BtnNieuwProduct.IsEnabled = true;
            }
            else
            {
                BtnNieuwProduct.IsEnabled = false;
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
            if (dgProducten.SelectedIndex < 0)
                return;

            if (!(MessageBox.Show(Translations.DeleteProduct, Translations.Confirm, MessageBoxButton.YesNo) == MessageBoxResult.Yes))
            {
                // Cancel Delete.
                e.Handled = true;
            }
            else _products.Remove(dgProducten.SelectedItem as Product);
        }
        #endregion
    }
}
