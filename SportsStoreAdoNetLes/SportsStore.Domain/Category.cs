using SportsStore.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Domain
{
    public class Category : IPersist
    {
        #region Fields
        private Contracts.Category _data;
        private bool _isDirty = false;
        #endregion

        #region Properties
        public Contracts.Category Data { get { return _data; } }

        public string Name { get { return _data.Name; } set { if (_data.Name != value) { _data.Name = value; _isDirty = true; } } }
        public int Id { get { return _data.CategoryId; } set { if (_data.CategoryId != value) { _data.CategoryId = value; _isDirty = true; } } }

        public List<Product> Products { get; set; } = new List<Product>();
        #endregion

        #region Ctor
        public Category()
        {
            _isDirty = true;
            _data = new Contracts.Category();
        }

        public Category(Contracts.Category category)
        {
            _data = category;
            if (DAL.Category.Exists(category))
                Load();
            else
                _isDirty = true;
        }
        #endregion

        #region Methods       
        public void AddProduct(string naam, decimal price, string description)
        {
            if (Products.FirstOrDefault(p => p.Name == naam) == null)
            {
                Products.Add(new Product { CategoryId = Id, Description = description, Name = naam, Price = price });
            }
        }

        // Method: lambda met LINQ:
        public Product FindProduct(string naam) => Products.FirstOrDefault(p => p.Name == naam);

        #region IPersist


        public void Load()
        {
            if (_data.CategoryId == 0) return;
            DAL.Category.Select(_data);
        }

        public void Save()
        {
            if(_isDirty)
            {
                if(!DAL.Category.Exists(_data))
                {
                    DAL.Category.Insert(_data);
                }
                else
                {
                    DAL.Category.Update(_data);
                }
                foreach(var p in Products)
                {
                    p.Save();
                }
            }
        }    
        #endregion
        #endregion
    }
}
