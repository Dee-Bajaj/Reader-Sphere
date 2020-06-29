using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using ProjectSettings;
using System.Collections.Generic;
using System.Linq;

namespace ReaderSphere.Controllers
{
    [ApiController]
    [ControllerRouteTemplate]
    public class BookController : ControllerBase
    {
        private IGenericReadersphereRepository<Book> _bookRepository;
        private readonly IAppLogger _logger;
        public BookController(IGenericReadersphereRepository<Book> genericReadersphereRepository, IAppLogger logger)
        {
            _bookRepository = genericReadersphereRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("~/api")]
        [Route("GetAllBooks")]
        [ProducesResponseType(typeof(IEnumerable<Book>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAllBooks()
        {
            var books = _bookRepository.GetAll();
            if (books != null && books.Any())
                return Ok(books);
            else
                return NoContent();
        }

        [HttpGet]
        [Route("GetBookById/{id}")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetBookById(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book != null)
                return Ok(book);
            else
                return NoContent();
        }
    }
}
