using SportsStore.Domain.Interfaces;

namespace SportsStore.Domain
{
    public class OrderLine: IPersist
    {
        #region Fields
        private Contracts.OrderLine _data;

        private bool _isDirty = false;
        #endregion

        #region Properties
        public Contracts.OrderLine Data { get { return _data; } }
        public int Id { get { return _data.Id; } set { if(_data.Id != value) { _data.Id = value; _isDirty = true; } } }
        public int OrderId { get { return _data.OrderId; } set { if (_data.OrderId != value) { _data.OrderId = value; _isDirty = true; } } }

        private Product _product;
        public Product Product { get { return _product; } set { if (_product == null || _product != value) { _product = value; _isDirty = true; } } }
        public decimal Price { get { return _data.Price; } set { if (_data.Price != value) { _data.Price = value; _isDirty = true; } } }
        #endregion

        #region Ctor
        public OrderLine() 
        {
            _isDirty = true;
            _data = new Contracts.OrderLine();
        }

        public OrderLine(Contracts.OrderLine orderLine)
        {
            _data = orderLine;
            if (DAL.OrderLine.Exists(orderLine))
                Load();
            else
                _isDirty = true;
        }
        #endregion

        #region Methods
        #region IPersist
        public void Load()
        {
            if (_data.Id == 0) return;
            DAL.OrderLine.Select(_data);
        }      
        
        public void Save()
        {
            if (_isDirty)
            {
                if (!DAL.OrderLine.Exists(_data))
                {
                    DAL.OrderLine.Insert(_data);
                }
                else
                {
                    DAL.OrderLine.Update(_data);
                }
            }
        }
        #endregion
        #endregion
    }
}
