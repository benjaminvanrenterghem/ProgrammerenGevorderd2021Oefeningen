using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;

namespace KlantBeheer.WPF
{
    public static class Context
    {
        #region Properties
        //public static ICrud CustomerManager { get; } = new Repository.ADO.CustomerManager();
        public static ICrud ProductManager { get; } = new Repository.Ado.ProductManager();

        private static ServiceProvider _serviceProvider;

        public static ServiceCollection ServiceCollection { get; set; } = new();        
        private static IServiceScope _serviceScope;

        // Voorbeeld van een singleton pattern: LAZY
        public static ServiceProvider ServiceProvider { get { if (_serviceProvider == null) { _serviceProvider = ServiceCollection.BuildServiceProvider(true); _serviceScope = _serviceProvider.CreateScope(); } return _serviceProvider; } }
        #endregion
    }
}
