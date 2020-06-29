using Microsoft.EntityFrameworkCore;
using ProjectSettings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class ReaderDataRepository<T> : IGenericReadersphereRepository<T> where T : class
    {
        private readonly ReaderSphereContext _context;
        private DbSet<T> _dbSet = null;
        private readonly IAppLogger _appLogger;
        public ReaderDataRepository(ReaderSphereContext readerSphereContext, IAppLogger appLogger)
        {
            _context = readerSphereContext;
            _dbSet = _context.Set<T>();
            _appLogger = appLogger;
        }
        public void Add(T obj)
        {
            try
            {
                _dbSet.Add(obj);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _appLogger.Log("Exception caught at ReaderDataRepository.Add", ex);
            }
        }

        public void Delete(T obj)
        {
            try
            {
                _dbSet.Remove(obj);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _appLogger.Log("Exception caught at ReaderDataRepository.Delete", ex);
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                return _dbSet.ToList();
            }
            catch (Exception ex)
            {
                _appLogger.Log("Exception caught at ReaderDataRepository.GetAll", ex);
                return null;
            }

        }

        public T GetById(int id)
        {
            try
            {
                return _dbSet.Find(id);
            }
            catch (Exception ex)
            {
                _appLogger.Log("Exception caught at ReaderDataRepository.GetById", ex);
                return null;
            }
        }

        public void Update(T obj)
        {
            try
            {
                _dbSet.Update(obj);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _appLogger.Log("Exception caught at ReaderDataRepository.Update", ex);
            }
        }
    }
}
