using Models;

namespace ReaderSphere
{
    public interface IBookService
    {
        BookInfomationResponse GetAllBooks();
        BookInfomationResponse GetBookByTitle(string title);
    }
}
