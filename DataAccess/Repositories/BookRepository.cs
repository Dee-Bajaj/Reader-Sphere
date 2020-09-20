using DataAccess.Models;
using ProjectSettings;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DataAccess.Repositories
{
    public class BookRepository : ReaderDataRepository<Book>, IBookRepository
    {
        private readonly ReaderSphereContext _context;
        private readonly IAppLogger _appLogger;
        public BookRepository(ReaderSphereContext readerSphereContext, IAppLogger appLogger) : base(readerSphereContext, appLogger)
        {
            _context = readerSphereContext;
            _appLogger = appLogger;
        }
        public List<Book> GetBookByTitle(string title)
        {
            try
            {
                return _context.Book.Where(b => b.BookName.Contains(title)).ToList();
            }
            catch (Exception ex)
            {
                _appLogger.Log("Exception caught at BookRepository.GetBookByTitle", ex);
                return null;
            }
        }
    }
}
