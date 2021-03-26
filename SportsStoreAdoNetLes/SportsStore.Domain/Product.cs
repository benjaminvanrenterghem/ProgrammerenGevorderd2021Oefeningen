using SportsStore.Domain.Interfaces;
using System;

namespace SportsStore.Domain
{
    public class Product: IPersist
    {
        #region Fields
        private Contracts.Product _data;
        private bool _isDirty = false;
        #endregion

        #region Properties
        public Contracts.Product Data { get { return _data; } }
        public int ProductId { get { return _data.ProductId; } set { if (_data.ProductId != value) { _data.ProductId = value; _isDirty = true; } } }
        public int CategoryId { get { return _data.CategoryId; } set { if (_data.CategoryId != value) { _data.CategoryId = value; _isDirty = true; } } }
        public string Name { get { return _data.Name; } set { if (_data.Name != value) { _data.Name = value; _isDirty = true; } } }
        public string Description { get { return _data.Description; } set { if (_data.Description != value) { _data.Description = value; _isDirty = true; } } }
        public decimal Price { get { return _data.Price; } set { if (_data.Price != value) { _data.Price = value; _isDirty = true; } } }
        #endregion

        #region Ctor
        public Product()
        {
            _isDirty = true;
            _data = new Contracts.Product();
        }

        public Product(Contracts.Product product)
        {
            _data = product;
            if (DAL.Product.Exists(product))
                Load();
            else
                _isDirty = true;
        }
        #endregion

        #region Methods
        #region IPersist 
        public void Load()
        {
            if (_data.ProductId == 0) return;
            DAL.Product.Select(_data);
        }

        public void Save()
        {
            if (_isDirty)
            {
                if (!DAL.Product.Exists(_data))
                {
                    DAL.Product.Insert(_data);
                }
                else
                {
                    DAL.Product.Update(_data);
                }
            }
        }
        #endregion

        public override bool Equals(object obj)
        {
            return (obj is Product p) && p.ProductId == _data.ProductId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_data.ProductId);
        }
        #endregion
    }
}
