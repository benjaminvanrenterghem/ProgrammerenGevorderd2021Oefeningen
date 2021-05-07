using KlantBeheer.WPF.Languages;
using Microsoft.Extensions.DependencyInjection;
using Repository.ADO;
using Serilog;
using Serilog.Formatting.Compact;
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
            // We registreren dat, indien we een service nodig hebben die de interface ICustomerManger implementeert, we een object van class CustomerManager teruggeven:
            Context.ServiceCollection.AddTransient<ICustomerManager, CustomerManager>();
            Translations.Culture = new System.Globalization.CultureInfo("nl-BE"); // en-US nl-BE

            // Tip, maar werk om het actief te krijgen: https://github.com/Analogy-LogViewer/Analogy.LogViewer

            Log.Logger = new LoggerConfiguration().
                MinimumLevel.Debug().
                Enrich.WithProperty("Application", "Klantenbeheer").
                Enrich.WithThreadId().
                Enrich.WithMemoryUsage().                
                //WriteTo.File(@"logs\Log_SerilogDemoWPF.txt", rollingInterval: RollingInterval.Day).
                WriteTo.File(new CompactJsonFormatter(), @"logs\log.json", rollingInterval: RollingInterval.Hour).
                WriteTo.Debug().
                CreateLogger();
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
