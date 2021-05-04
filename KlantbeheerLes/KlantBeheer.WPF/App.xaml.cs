using KlantBeheer.WPF.Languages;
using Microsoft.Extensions.DependencyInjection;
using Repository.ADO;
using System.Windows;

namespace KlantBeheer.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private App()
        {
            Context.ServiceCollection.AddTransient<ICustomerManager, CustomerManager>();
            Translations.Culture = new System.Globalization.CultureInfo("nl-BE"); // en-US nl-BE
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(Translations.ExceptionMessage
            + e.Exception.Message, Translations.ExceptionSample, MessageBoxButton.OK, MessageBoxImage.Warning);
            // We zeggen hier dat de exception door ons afgehandeld is
            e.Handled = true;
        }
    }
}
