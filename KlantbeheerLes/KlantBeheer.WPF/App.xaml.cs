using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Repository.ADO;
using Repository.Interfaces;

namespace KlantBeheer.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Register our types in the container
            //Context.ServiceCollection.AddScoped<ICustomerManager, CustomerManager>();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("An unhandled exception just occurred: "
            + e.Exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            e.Handled = true; // Aangeven dat je zelf de exception opving en dat de applicatie mag doorgaan in plaats van volledig te stoppen en alle ingaves zonder meer kwijt te zijn
        }
    }
}
