using Models;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public interface IBookRepository
    {
        List<Book> GetAll();
        List<Book> FindBooks(FindBookRequest findBookRequest);
    }
}
