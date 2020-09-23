using DataAccess.Repositories;
using Models;
using ProjectSettings;
using System;
using System.Globalization;
using System.Linq;

namespace ReaderSphere
{
    public class BookService : IBookService
    {
        private readonly IAppLogger _logger;
        private readonly IBookRepository _bookRepository;
        private readonly IGenericReadersphereRepository<Author> _authorRepository;
        private readonly IGenericReadersphereRepository<BookAuthor> _bookAuthRepository;
        public BookService(IBookRepository bookRepository, IAppLogger logger, IGenericReadersphereRepository<Author> authorRepository,
           IGenericReadersphereRepository<BookAuthor> bookAuthRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _bookAuthRepository = bookAuthRepository;
            _logger = logger;
        }
        public BookInfomation GetAllBooks()
        {
            BookInfomation bookInfomationResponse = null;
            try
            {
                var bookList = _bookRepository.GetAll();
                if (bookList?.Any() == true)
                {
                    bookInfomationResponse = new BookInfomation
                    {
                        Books = bookList,
                        TotalBooks = bookList.Count
                    };
                }

            }
            catch (Exception ex)
            {
                _logger.Log("Exception caught at BookService.GetAllBooks", ex);
            }
            return bookInfomationResponse;
        }

        public BookInfomation FindBooks(FindBookRequest findBookRequest)
        {
            BookInfomation bookInfomationResponse = null;
            try
            {
                var books = _bookRepository.FindBooks(findBookRequest);
                if (books?.Any() ?? false)
                {
                    bookInfomationResponse = new BookInfomation
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

        public BookInfo GetBookById(int id)
        {
            try
            {
                var book = _bookRepository.GetById(id);
                var authorid = _bookAuthRepository.GetAll().FirstOrDefault(x => x.BookId.Equals(id))?.AuthorId;
                if (authorid == null)
                    return null;

                var author = _authorRepository.GetById((int)authorid);
                var bookInfo = new BookInfo
                {
                    BookId = book.BookId,
                    BookName = book.BookName,
                    Genre = book.Genre,
                    IsReviewAdded = book.ReviewAdded,
                    Price = book.Price,
                    Publisher = book.Publisher,
                    PublishYear = book.PublishYear?.ToString("yyyy"),
                    Review = new Review
                    {
                        ReviewDate = book.ReviewDate,
                        CriticReview = book.CriticReview,
                        ShortReview = book.ShortReview
                    },
                    Author = new Writer
                    {
                        FirstName = author.FirstName,
                        LastName  = author.LastName,
                        Pseudonym = author.Pseudonym
                    }
                };

                return bookInfo;
            }
            catch (Exception ex)
            {
                _logger.Log("Exception caught at BookService.GetBookById", ex);
                return null;
            }

        }

        public AddBookResponse AddBook(AddBookRequest addBookRequest)
        {
            AddBookResponse addBookResponse;
            int authId;
            try
            {
                addBookResponse = new AddBookResponse();
                var book = ParseBookFromRequest(addBookRequest);
                var addedBook = _bookRepository.Add(book);

                if (addedBook == null)
                {
                    addBookResponse.Status = Status.AlreadyExisting;
                    return addBookResponse;
                }
                else
                {
                    authId = TryAddAuthor(addBookRequest);
                    var bookAuth = new BookAuthor
                    {
                        BookId = addedBook.BookId,
                        AuthorId = authId
                    };

                    if (_bookAuthRepository.Add(bookAuth) != null)
                    {
                        addBookResponse = new AddBookResponse
                        {
                            BookId = bookAuth.BookId,
                            AuthorId = bookAuth.AuthorId,
                            Status = Status.BookAdded
                        };
                    }
                    return addBookResponse;
                }

            }
            catch (Exception ex)
            {
                _logger.Log("Exception caught at BookService.AddBook", ex);
                return null;
            }
        }

        private Book ParseBookFromRequest(AddBookRequest addBookRequest)
        {
            var book = new Book
            {
                BookName = addBookRequest.BookName,
                Genre = addBookRequest.Genre.ToString(),
                Publisher = addBookRequest.Publisher,
                PublishYear = DateTime.ParseExact(addBookRequest.PublishYear, "yyyy", CultureInfo.InvariantCulture),
                Price = addBookRequest.Price,
            };
            if (addBookRequest.Review != null)
            {
                book.CriticReview = addBookRequest.Review.CriticReview;
                book.ShortReview = addBookRequest.Review.ShortReview;
                book.ReviewDate = DateTime.Now;
                book.ReviewAdded = true;
            }

            return book;
        }

        private int TryAddAuthor(AddBookRequest addBookRequest)
        {
            int authId;
            if (string.IsNullOrEmpty(addBookRequest.Author.Pseudonym))
                addBookRequest.Author.Pseudonym = addBookRequest.Author.FirstName;
            var author = new Author
            {
                FirstName = addBookRequest.Author.FirstName,
                LastName = addBookRequest.Author.LastName,
                Pseudonym = addBookRequest.Author.Pseudonym
            };
            var authKey = author.FirstName + author.LastName + author.Pseudonym;
            var existingKey = _authorRepository.GetAll().FirstOrDefault(x => (x.FirstName + x.LastName + x.Pseudonym).Equals(authKey));
            if (existingKey == null)
                authId = _authorRepository.Add(author).AuthorId;
            else
                authId = existingKey.AuthorId;
            return authId;
        }
    }
}
