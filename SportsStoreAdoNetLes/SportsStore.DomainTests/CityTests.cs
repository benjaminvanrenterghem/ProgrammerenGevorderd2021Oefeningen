using Xunit;

// Te testen:
// 1. Normale verwachte gedrag
// 2. Exceptions
// 3. Border conditions: onverwachte waarden die normaal buiten scope vallen, niet gebruikt worden, ... 

namespace SportsStore.Domain.Tests
{
    public class CityTests
    {
        [Fact]
        public void CityTest()
        {
            var city = new City();
            Assert.Equal(0, city.Id);
        }

        [Fact]
        public void SaveTest()
        {
            var city = new City { Name = "Gent" };
            city.Save();
            var reloadedCity = new City { Name = "Gent" };
            reloadedCity.Load();
            Assert.Equal(city.Id, reloadedCity.Id);
        }
    }
}