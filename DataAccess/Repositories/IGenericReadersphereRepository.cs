using System.Collections.Generic;

namespace DataAccess
{
    public interface IGenericReadersphereRepository<T> where T : class
    {
        void Add(T obj);
        void Update(T obj);
        void Delete(T obj);
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}
