using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class ReaderDataRepository<T> : IGenericReadersphereRepository<T> where T : class
    {
        private ReaderSphereContext  _context = null;
        private DbSet<T> _dbSet = null;
        public ReaderDataRepository()
        {
            _context = new ReaderSphereContext();
            _dbSet = _context.Set<T>();
        }
        public void Add(T obj)
        {
            _dbSet.Add(obj);
            _context.SaveChanges();
        }

        public void Delete(T obj)
        {
            _dbSet.Remove(obj);
            _context.SaveChanges();

        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(T obj)
        {
            _dbSet.Update(obj);
            _context.SaveChanges();
        }
    }
}
