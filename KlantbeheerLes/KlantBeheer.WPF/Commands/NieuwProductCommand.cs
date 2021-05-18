using KlantBestellingen.WPF.ViewModels;
using Serilog;
using System;
using System.Windows.Input;

namespace KlantBestellingen.WPF.Commands
{
    public class NieuwProductCommand : ICommand // MVVM
    {
        #region Properties
        public event EventHandler CanExecuteChanged;
        private ProductenViewModel _source;
        #endregion

        #region Ctor
        public NieuwProductCommand(ProductenViewModel source) => _source = source;
        #endregion

        #region Interface methods ICommand
        public bool CanExecute(object parameter)
        {
            return true; // button is altijd "enabled"
        }

        public void Execute(object parameter)
        {
            // Volgende moet in principe reeds ok zijn: we kunnen namelijk niet klikken indien CanExecute() niet waar is
            if (!CanExecute(parameter))
                return;

            // Demo van SeriLog
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
        #endregion
    }
}
