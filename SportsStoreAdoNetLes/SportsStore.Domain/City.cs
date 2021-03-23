using SportsStore.Domain.Interfaces;

namespace SportsStore.Domain
{
    public class City: IPersist
    {
        #region Fields
        private Contracts.City _data;
        // Voor optimalisatie van databank toegang: dit kan veel snelheidswinst betekenen!
        private bool _isDirty;
        #endregion

        #region Properties
        public int Id { get => _data.Id; set { if (_data.Id != value) { _data.Id = value; } } }
        public string PostalCode { get { return _data.PostalCode; } set { if (_data.PostalCode != value) { _data.PostalCode = value; _isDirty = true; } } }
        public string Name { get { return _data.Name; } set { if (_data.Name != value) { _data.Name = value; _isDirty = true; } } }
        #endregion

        #region Ctor
        public City()
        {
            // nieuwe city is altijd "dirty" - zit nog niet in database
            _isDirty = true;
            _data = new Contracts.City();
        }

        public City(Contracts.City city)
        {
            _data = city;
            if (DAL.City.Exists(city))
                Load();
            else
                _isDirty = true;
        }
        #endregion

        #region Interface IPersist
        public void Load()
        {
            // Preconditie: defensief programmeren - niks doen als het geen zin heeft
            if (Id == 0 && string.IsNullOrEmpty(Name)) return;

            if (DAL.City.Select(_data))
            {
                _isDirty = false;
            }
        }

        public void Save()
        {
            if (_isDirty)
            {
                // UPSERT patroon: update of insert afhankelijk van of het object al in de database zit of niet
                if (!DAL.City.Exists(_data))
                {
                    if (DAL.City.Insert(_data) > 0)
                        _isDirty = false;
                }
                else
                {
                    // TODO(lvet): Update return id or bool                    
                    DAL.City.Update(_data);
                    _isDirty = false;
                }
            }
        }
        #endregion
    }
}
