using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Klantbeheer.Domain;
using System.Collections.Generic;
using KlantBeheer.WPF.Languages;
using Microsoft.Extensions.DependencyInjection;
using KlantBestellingen.WPF;

namespace KlantBeheer.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Properties
        private Klanten _customerWindow = new();
        private Producten _productenWindow = new();
        //private Bestellingen _bestellingenWindow = new();
        //private DetailsBestelling _bestellingDetailWindow = new();
        #endregion

        #region Ctor
        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
            _customerWindow.Closing += _Window_Closing;
            _productenWindow.Closing += _Window_Closing;
            //_bestellingenWindow.Closing += _Window_Closing;
            //_bestellingDetailWindow.Closing += _Window_Closing;
            //_bestellingDetailWindow.UpdateEvent += Refresh;
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// We verbergen de vensters in plaats van ze te sluiten: alles blijft klaarstaan; dit is sneller en efficienter bij vensters die maar eenmaal op het scherm komen tegelijkertijd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Sluit het venster niet echt af en verberg het: we kruipen tussen en vertellen WPF dat het afsluiten al gebeurd is
            // We moeten de Hide() uitvoeren op de UI-thread (main WPF thread) door Dispatcher te gebruiken: 
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, (DispatcherOperationCallback)delegate (object o)
            {
                /* Nuttige code: */
                ((Window)sender).Hide(); // (Window) is een cast: sender is van type object en kan niet als Window gebruikt worden zonder meer
                /* ... tot hier! */
                return null;
            }, null);
            // We zeggen nu dat de closing event afgehandeld is aan WPF:
            e.Cancel = true;
        }

        /// <summary>
        /// We sluiten de applicatie volledig af wanneer het hoofdvenster gesloten wordt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// openen van klantenscherm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Klanten_Click(object sender, RoutedEventArgs e)
        {
            _customerWindow?.Show();
        }

        /// <summary>
        /// openen van productenscherm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Producten_Click(object sender, RoutedEventArgs e)
        {
            _productenWindow?.Show();
        }

        /// <summary>
        /// openen van bestellingen scherm. Bij openen wordt er steeds de recenste lijst van bestellingen opgehaald.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Bestellingen_Click(object sender, RoutedEventArgs e)
        {
            //if (_bestellingenWindow != null)
            //    _bestellingenWindow.Refresh();
            //    _bestellingenWindow.Show();
        }

        /// <summary>
        /// We sluiten de volledige applicatie af
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemSluiten_Click(object sender, RoutedEventArgs e)
        {
            // Met volgend statement sluiten we de WPF applicatie volledig af:
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Methode voor zoekvak van de klantnaam. Deze methode zoekt alle klanten met overeenkomende tekst en geeft deze weer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // Als je een string toekrijgt, controleer dan steeds of deze wel een bruikbare waarde heeft bij aanvang (preconditie)
            if (string.IsNullOrEmpty(tbKlant.Text))
            {
                cbKlanten.ItemsSource = null;
                return;
            }
            // Tip: maak dit case insensitive voor "meer punten" ;-) Nog beter: reguliere expressies gebruiken
            var dataObjects = Context.ServiceProvider.GetRequiredService<Repository.ADO.ICustomerManager>().GetAll(); //var dataObjects = Context.CustomerManager.GetAll();

            List<Customer> allCustomers = new();
            foreach(var o in dataObjects)
            {
                allCustomers.Add(o as Customer);
            }

            var customers = allCustomers.Where(k => k.Name.ToLower().Contains(tbKlant.Text.ToLower())).ToList();// regex gebruiken
            cbKlanten.ItemsSource = customers;
            // Indien er effectief klanten zijn, maak dan dat de eerste klant in de lijst meteen voorgeselecteerd is in de combobox:
            if (customers.Count > 0)
            {
                cbKlanten.SelectedIndex = 0; // het 0-de item is de eerste klant want C# is zero-based
            }
        }
        /// <summary>
        /// Indien de combobox van de klanten veranderd van selectie, wordt er een nieuwe lijst van bestellingen opgeroepen
        /// voor deze klant en wordt de beschikbaarheid van de Nieuwebestellingknop veranderd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh(sender, e);
            BtnNieuweBestellingEnable();
        }

        /// <summary>
        /// Tip: interessant voor eindopdracht!
        /// Deze methode haalt de bestaande bestellingen van de klant op, telkens deze aangeroepen wordt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Refresh(object sender, EventArgs e)
        {
            if (cbKlanten.SelectedItem != null)
            {
                // Indien er een klant geselecteerd is, dan tonen we de bestellingen van die klant
                var selectedCustomer = cbKlanten.SelectedItem as Customer;
                //var orders = Context.OrderManager.GetOrdersFromCustomer(selectedCustomer);
                //// prijs betaald afronden op 2 decimalen na de komma
                //foreach(var product in orders)
                //{
                //    product.PricePaid = Math.Round(product.PricePaid,2);
                //}
                //dgOrderSelection.ItemsSource = orders;
            }
            else
            {
                // Indien er geen klant geselecteerd is, tonen we geen bestellingen
                dgOrderSelection.ItemsSource = null;
                // en resetten we de statusinformatietekst
                TbStatusInformation.Text = null;
            }
        }
        /// <summary>
        /// Method die het detailsscherm oproept om vervolgens een nieuwe bestelling te laten maken,
        /// Indien de knop enabled is en er een correcte klant aangeduid is.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaakBestelling_Click(object sender, RoutedEventArgs e)
        {
            // Indien het detailvenster voor de bestelling bestaat en dit bestaat eigenlijk altijd, en er is een klant geselecteerd, dan toon ik het venster voor die klant:
            //if (_bestellingDetailWindow == null || cbKlanten.SelectedIndex < 0)
            {
                return;
            }

            //_bestellingDetailWindow.Customer = cbKlanten.SelectedItem as Customer;
            //_bestellingDetailWindow.Order = null;
            //_bestellingDetailWindow.Show();
        }
        /// <summary>
        /// Deze method zorgt ervoor dat de button om een nieuwe bestelling te maken enabled wordt of disabled wordt,
        /// naargelang de precondities.
        /// </summary>
        private void BtnNieuweBestellingEnable()
        {
            if (cbKlanten.SelectedItem != null)
            {
                btnNieuweBestelling.IsEnabled = true;
            }
            else
            {
                btnNieuweBestelling.IsEnabled = false;
            }
        }
        /// <summary>
        /// Method die laat zien hoeveel producten er in een bestelling zit, wanneer er op geklikt wordt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgOrderSelection.SelectedItem != null)
            {
                //int amountOfProducts = 0;
                //var selectedOrder = (Order)dgOrderSelection.SelectedItem;
                //var orderFromDb = Context.OrderManager.GetOrder(selectedOrder.OrderID);

                //foreach (var item in orderFromDb.GetProducts())
                //{
                //    amountOfProducts += item.Value;
                //}

                //TbStatusInformation.Text = $"{Translations.NumberProductsTag} {amountOfProducts}";
            }
        }
        /// <summary>
        /// Dit wordt aangeroepen als er geklikt is op de verwijderknop. Deze methode verwijdert de geselecteerde bestelling.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgOrderSelection.SelectedIndex < 0)
                return;
            if (!(MessageBox.Show(Translations.DeleteOrder, Translations.Confirm, MessageBoxButton.YesNo) == MessageBoxResult.Yes))
            {
                // Cancel Delete.
                e.Handled = true;
            }
            else
            {
                var order = dgOrderSelection.SelectedItem as Order;

                //Context.OrderManager.RemoveOrder(order);
                Refresh(sender, e);
            }
        }
        /// <summary>
        /// Eventhandler die aangeroepen wordt als er dubbel geklikt wordt op een bestaande bestelling en 
        /// het detailsscherm aanroept voor deze bestelling.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgOrderSelection_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //if (_bestellingDetailWindow == null || cbKlanten.SelectedIndex < 0)
            {
                return;
            }
            //var selectedOrder = dgOrderSelection.SelectedItem as Order;
            //var OrderFromDb = Context.OrderManager.GetOrder(selectedOrder.OrderID);

            //_bestellingDetailWindow.Customer = cbKlanten.SelectedItem as Customer;
            //_bestellingDetailWindow.Order = OrderFromDb;
            //_bestellingDetailWindow.Paid = OrderFromDb.IsPaid;
            //_bestellingDetailWindow.Show();
        }
        #endregion
    }
}
