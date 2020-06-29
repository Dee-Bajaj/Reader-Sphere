using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using ProjectSettings;

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
        public IActionResult GetAllBooks()
        {
            _logger.Log("GetAllBooks Called");
            return Ok(_bookRepository.GetAll());

        }

        [HttpGet]
        [Route("GetBookById/{id}")]
        public IActionResult GetBookById(int id)
        {
            return Ok(_bookRepository.GetById(id));
        }
    }
}
