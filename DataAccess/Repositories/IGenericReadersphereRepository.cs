using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public interface IGenericReadersphereRepository<T> where T : class
    {
        T Add(T obj);
        void Update(T obj);
        void Delete(T obj);
        List<T> GetAll();
        T GetById(int id);
    }
}
