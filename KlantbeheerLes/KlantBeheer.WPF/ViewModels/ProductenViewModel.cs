using KlantBestellingen.WPF.Commands;
using System.ComponentModel;
using System.Windows.Input;

namespace KlantBestellingen.WPF.ViewModels
{
    public class ProductenViewModel : INotifyPropertyChanged
    {
        public ICommand BtnNieuwProductCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ProductenViewModel()
        {
            BtnNieuwProductCommand = new NieuwProductCommand(this);
        }
    }
}
