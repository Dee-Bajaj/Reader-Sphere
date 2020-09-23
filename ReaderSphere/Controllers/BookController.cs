using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Models;
using ProjectSettings;
using System.Linq;

namespace ReaderSphere.Controllers
{
    [ApiController]
    [ControllerRouteTemplate]
    [Produces("application/json")]
    public class BookController : ControllerBase
    {
        private IBookService _bookService;
        private readonly IAppLogger _logger;
        public BookController(IBookService bookService, IAppLogger logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        [Route("~/api")]
        [Route("GetAllBooks")]
        [ProducesResponseType(typeof(BookInfomation),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAllBooks()
        {
            var books = _bookService.GetAllBooks();
            if (books?.Books?.Any() == true)
                return Ok(books);
            else
            {
                return NoContent();
            }
                
        }

        [HttpPost]
        [Route("GetSelectedBooks")]
        [ProducesResponseType(typeof(BookInfomation), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetSelectedBooks(FindBookRequest findBookRequest)
        {
            var bookInfomationResponse = _bookService.FindBooks(findBookRequest);
            if (bookInfomationResponse != null)
                return Ok(bookInfomationResponse);
            else
                return NoContent();
        }

        [HttpGet]
        [Route("GetBookById")]
        [ProducesResponseType(typeof(BookInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetBookById(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book != null)
                return Ok(book);
            else
                return NoContent();
        }

        [HttpPost]
        [Route("AddBook")]
        [ProducesResponseType(typeof(AddBookResponse), StatusCodes.Status200OK)]
        public IActionResult AddBook(AddBookRequest addBookRequest)
        {
            var addBookResponse = _bookService.AddBook(addBookRequest);
            if (addBookResponse != null)
                return Ok(addBookResponse);
            else
                return NoContent();
        }
    }
}
