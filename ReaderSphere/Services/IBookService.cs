using Models;

namespace ReaderSphere
{
    public interface IBookService
    {
        BookInfomation GetAllBooks();
        BookInfo GetBookById(int id);
        BookInfomation FindBooks(FindBookRequest findBookRequest);
        AddBookResponse AddBook(AddBookRequest addBookRequest);
    }
}
