using Models;
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

        public List<Book> FindBooks(FindBookRequest findBookRequest)
        {
            List<Book> filteredResult = null;
            try
            {
                filteredResult = _context.Book.ToList();
                if (!string.IsNullOrEmpty(findBookRequest.Title) && filteredResult?.Any() == true)
                    filteredResult = filteredResult.Where(b => b.BookName.ToUpperInvariant().Contains(findBookRequest.Title.ToUpperInvariant())).ToList();
                if (findBookRequest.Author != null && filteredResult?.Any() == true)
                {
                    IQueryable<BookAuthor> bookAuthors = null;
                   
                    if (!string.IsNullOrEmpty(findBookRequest.Author.FirstName) && !string.IsNullOrEmpty(findBookRequest.Author.LastName))
                         bookAuthors = _context.BookAuthor.Where(a => a.Author.FirstName.Equals(findBookRequest.Author.FirstName) && a.Author.LastName.Equals(findBookRequest.Author.LastName));
                    if (!string.IsNullOrEmpty(findBookRequest.Author.Pseudonym) && bookAuthors == null)
                        bookAuthors = _context.BookAuthor.Where(a => a.Author.Pseudonym.Equals(findBookRequest.Author.Pseudonym));
                    
                    if (bookAuthors?.Any() ?? false)
                        filteredResult = bookAuthors.Select(b => b.Book).Intersect(filteredResult).ToList();
                    else
                        filteredResult = null;
                }
                if (findBookRequest.Genre != GenreType.None && filteredResult?.Any() == true)
                    filteredResult = filteredResult.Where(b => b.Genre.Equals(findBookRequest.Genre.ToString(), StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (!string.IsNullOrEmpty(findBookRequest.Publisher) && filteredResult?.Any() == true)
                    filteredResult = filteredResult.Where(b => b.Publisher.ToUpperInvariant().Contains(findBookRequest.Publisher.ToUpperInvariant())).ToList();
            }
            catch (Exception ex)
            {
                _appLogger.Log("Exception caught at BookRepository.FindBooks", ex);

            }
            return filteredResult;
        }
    }
}
