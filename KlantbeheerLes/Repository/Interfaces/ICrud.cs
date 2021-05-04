using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface ICrud
    {
        /// <summary>
        ///  Create
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        int Add(Klantbeheer.Domain.DataObject o);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="o"></param>
        void Update(Klantbeheer.Domain.DataObject o);
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="o"></param>
        void Remove(Klantbeheer.Domain.DataObject o);
        /// <summary>
        /// Read
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Klantbeheer.Domain.DataObject Get(int id);
        /// <summary>
        /// Read single
        /// </summary>
        /// <returns></returns>
        IEnumerable<Klantbeheer.Domain.DataObject> GetAll();
    }
}