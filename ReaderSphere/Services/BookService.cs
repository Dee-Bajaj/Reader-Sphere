using DataAccess.Repositories;
using Models;
using ProjectSettings;
using System;
using System.Linq;

namespace ReaderSphere
{
    public class BookService : IBookService
    {
        private readonly IAppLogger _logger;
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository, IAppLogger logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }
        public BookInfomationResponse GetAllBooks()
        {
            BookInfomationResponse bookInfomationResponse = null;
            try
            {
                var bookList = _bookRepository.GetAll();
                if(bookList?.Any() == true)
                {
                    bookInfomationResponse = new BookInfomationResponse
                    {
                        Books = bookList,
                        TotalBooks = bookList.Count
                    };
                }

            }
            catch(Exception ex)
            {
                _logger.Log("Exception caught at BookService.GetAllBooks", ex);
            }
            return bookInfomationResponse;
        }

        public BookInfomationResponse FindBooks(FindBookRequest findBookRequest)
        {
            BookInfomationResponse bookInfomationResponse = null;
            try
            {
                var books =_bookRepository.FindBooks(findBookRequest);
                if(books?.Any() ?? false)
                {
                    bookInfomationResponse = new BookInfomationResponse
                    {
                        Books = books,
                        TotalBooks = books.Count
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Log("Exception caught at BookService.GetBookByTitle", ex);
            }
            return bookInfomationResponse;
        }
    }
}
