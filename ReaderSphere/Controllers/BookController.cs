using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace ReaderSphere.Controllers
{
    [ApiController]
    [ControllerRouteTemplate]
    public class BookController : ControllerBase
    {
        private IGenericReadersphereRepository<Book> _bookRepository;
        public BookController(IGenericReadersphereRepository<Book> genericReadersphereRepository)
        {
            _bookRepository = genericReadersphereRepository;
        }

        [HttpGet]
        [Route("~/api")]
        [Route("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            if (ModelState.IsValid)
                return Ok(_bookRepository.GetAll());
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("GetBookById")]
        public IActionResult GetBookById(int id)
        {
            if (ModelState.IsValid)
                return Ok(_bookRepository.GetById(id));
            else
                return BadRequest();
        }
    }
}
