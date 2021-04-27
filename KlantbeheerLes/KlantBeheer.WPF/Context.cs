using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;

namespace KlantBeheer.WPF
{
    public static class Context
    {
        #region Properties
        public static ICrud CustomerManager { get; } = new Repository.ADO.CustomerManager();

        private static ServiceProvider _serviceProvider;

        public static ServiceCollection ServiceCollection { get; set; } = new();
        public static ServiceProvider ServiceProvider { get { if (_serviceProvider == null) _serviceProvider = ServiceCollection.BuildServiceProvider(); return _serviceProvider; } }
        #endregion
    }
}
