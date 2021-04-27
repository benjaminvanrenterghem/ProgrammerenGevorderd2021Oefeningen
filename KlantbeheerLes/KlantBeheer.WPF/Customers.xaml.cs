using Klantbeheer.Domain;
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
using System.Windows.Shapes;

namespace KlantBeheer.WPF
{
    /// <summary>
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class Customers : Window
    {
        private ObservableCollection<Klantbeheer.Domain.Customer> _customers;

        public Customers()
        {
            InitializeComponent();

            _customers = new ObservableCollection<Klantbeheer.Domain.Customer>();

            var objects = Context.CustomerManager.GetAll();

            foreach(var o in objects)
            {
                var c = o as Customer;
                if(c != null)
                    _customers.Add(c);
            }

            dgKlanten.ItemsSource = _customers;
        }

        private void BtnNieuweKlant_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Tb_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void DgKlanten_PreviewDeleteCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
