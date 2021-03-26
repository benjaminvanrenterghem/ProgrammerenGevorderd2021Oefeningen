using SportsStore.Domain.Interfaces;

namespace SportsStore.Domain
{
    public class CartLine: IPersist
    {
        #region Fields
        private Contracts.CartLine _data;
        private bool _isDirty = false;
        #endregion

        #region Properties
        //public Contracts.CartLine Data { get { return _data; } }
        public int Id { get { return _data.Id; } set { if (_data.Id != value) { _data.Id = value; _isDirty = true; } } }

        private Product _product;
        public Product Product { get { return _product; } set { if (_product == null || _product.ProductId != value.ProductId) { _product = value; _data.Product = value.Data;  _isDirty = true; } } }
        public int Quantity { get { return _data.Quantity; } set { if (_data.Quantity != value) { _data.Quantity = value; _isDirty = true; } } }

        public decimal Total => _data.Product.Price * _data.Quantity;

        #endregion

        #region Ctor
        public CartLine()
        {
            _isDirty = true;
            _data = new Contracts.CartLine();
        }

        public CartLine(Contracts.CartLine cartLine)
        {
            _data = cartLine;
            if (DAL.CartLine.Exists(cartLine))
                Load();
            else
                _isDirty = true;
        }
        #endregion

        #region IPersist
        public void Save()
        {
            if (_isDirty)
            {
                if (!DAL.CartLine.Exists(_data))
                {
                    DAL.CartLine.Insert(_data);
                }
                else
                {
                    DAL.CartLine.Update(_data);
                }
            }
        }

        public void Load()
        {
            if (_data.Id == 0) return;
            DAL.CartLine.Select(_data);
        }
        #endregion
    }
}
