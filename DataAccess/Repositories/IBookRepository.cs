using Models;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public interface IBookRepository
    {
        Book GetById(int id);
        List<Book> GetAll();
        List<Book> FindBooks(FindBookRequest findBookRequest);
        Book Add(Book obj);
    }
}
