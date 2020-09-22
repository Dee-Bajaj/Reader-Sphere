using Models;

namespace ReaderSphere
{
    public interface IBookService
    {
        BookInfomationResponse GetAllBooks();
        BookInfomationResponse FindBooks(FindBookRequest findBookRequest);
    }
}
