using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;

namespace KlantBeheer.WPF
{
    public static class Context
    {
        #region Properties
        //public static ICrud CustomerManager { get; } = new Repository.ADO.CustomerManager();

        private static ServiceProvider _serviceProvider;

        public static ServiceCollection ServiceCollection { get; set; } = new();
        private static IServiceScope _serviceScope;
        public static ServiceProvider ServiceProvider { get { if (_serviceProvider == null) _serviceProvider = ServiceCollection.BuildServiceProvider(true); _serviceScope = _serviceProvider.CreateScope(); return _serviceProvider; } }
        #endregion
    }
}
