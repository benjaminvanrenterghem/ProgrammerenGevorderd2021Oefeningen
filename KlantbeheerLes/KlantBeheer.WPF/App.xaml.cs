using Klantbeheer.Domain;
using KlantBeheer.WPF.Languages;
using Microsoft.Extensions.DependencyInjection;
using Repository.ADO;
using Serilog;
using Serilog.Formatting.Compact;
using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace KlantBeheer.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Me1() // heel klein beetje stack geheugen
        {
            int i = 5; // staat op stack

            Console.WriteLine(i);

            Me2(3.14); // we geven een value type, namelijk int, door als parameter aan de method: er wordt een COPIE gemaakt van de waarde
            double pi = 3.14;
            Me2(pi); // een COPIE wordt doorgegeven
            var v = Me4(pi, ref pi);

            //double w;
            Student s = new Student();
            var v1 = Me5(pi, out double w, s);
            // elke sluitende accolade kuist stack op
        }
        
        private double Me5(double v, out double w, Student s = null)
        {            
            w = v + 1;           
            return v;
        }

        private double Me4(double v, ref double w) // v is een COPIE
        {
            v = 3.19;
            w = 3.18;
            return v; // we hebben maar 1 return value: double; willen we meer variabelen teruggeven, dan kunnen we ref parameters gebruiken
        }

        private void Me2(double x) // double x ook op de stack
        {
            int i = 5;
            { // nieuw blok
                double y = 5.6; // ook op de locale stack
            }
            Console.WriteLine(x);
            // y is alweer verdwenen van de stack en opgekuist!

            Me3();
        }

        private void Me3()
        {
            var s = new Student { Age = 25 }; // s komt van de heap en is een wijzer/pointer naar een adres in het geheugen dat op de stack geplaatst wordt
        }

        private App()
        {
            Me1();
            var info = GC.GetGCMemoryInfo();
            var totalMemoryBeforeCleanup = GC.GetTotalMemory(false); 
            var totalMemoryAfterCleanup = GC.GetTotalMemory(true); // effectief zoveel mogelijk vrijgeven naar het OS met true

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
