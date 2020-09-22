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
        [ProducesResponseType(typeof(BookInfomationResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAllBooks()
        {
            var books = _bookService.GetAllBooks();
            if (books?.Books?.Any() == true)
                return Ok(books);
            else
            {
                _logger.Log("No books found");
                return NoContent();
            }
                
        }

        [HttpPost]
        [Route("GetSelectedBooks")]
        [ProducesResponseType(typeof(BookInfomationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetBookByTitle(FindBookRequest findBookRequest)
        {
            var bookInfomationResponse = _bookService.FindBooks(findBookRequest);
            if (bookInfomationResponse != null)
                return Ok(bookInfomationResponse);
            else
                return NoContent();
        }
    }
}
