using Klantbeheer.Domain;
using System.ComponentModel;

namespace KlantBeheer.WPF.UserModels
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        private Customer _customer; // we dit encapsulatie van een object

        public Customer Value => _customer;

        public event PropertyChangedEventHandler PropertyChanged;

        // Omwille van overerving van DataObject hebben we altijd een member Id

        public int Id
        {
            get => _customer.Id;
            set
            {
                // Waarom if? Indien er geen wijziging nodig is, wordt user interface niet updated -> sneller!!
                if (_customer.Id != value)
                {
                    _customer.Id = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Id)));
                }
            }
        }

        public string Name
        {
            get => _customer.Name;
            set
            {
                if (_customer.Name != value) // we doen deze bescherming voor performantie: als de waarde dezelfde is, geen update van de user interface!
                {
                    _customer.Name = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }

        public string Address
        {
            get => _customer.Address;
            set
            {
                if (_customer.Address != value)
                {
                    _customer.Address = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Address)));
                }
            }
        }

        public int Discount
        {
            get => _customer.Discount;
            set
            {
                if (_customer.Discount != value)
                {
                    _customer.Discount = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Discount)));
                }
            }
        }

        #region Ctor
        public CustomerViewModel(Customer customer)
        {
            _customer = customer;
        }
        #endregion
    }
}
