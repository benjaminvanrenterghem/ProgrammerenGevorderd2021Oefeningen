namespace SportsStore.Domain.Interfaces
{
    interface IPersist // overeenkomst tussen programmeurs: interface begint met letter I
    {
        // interface is een contract dat je oplegt: set of methods, ... die moet geimplementeerd worden
        void Save();
        void Load();
    }
}
