using KlantBestellingen.WPF.ViewModels;
using Serilog;
using System;
using System.Windows.Input;

namespace KlantBestellingen.WPF.Commands
{
    public class NieuwProductCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private ProductenViewModel _source;

        public NieuwProductCommand(ProductenViewModel source)
        {
            _source = source;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Log.Information("-> NieuwProductCommand::Execute");

            int a = 10, b = 0;
            try
            {
                Log.Debug("Dividing {A} by {B}", a, b);

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Something went wrong");
            }

            Log.Information("<- NieuwProductCommand::Execute");
        }
    }
}
