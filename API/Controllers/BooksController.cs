using API;
using Microsoft.AspNetCore.Mvc;
using PiszczekSzpotek.BookCatalogue.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PiszczekSzpotek.BookCatalogue.API.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IDAO _service = Program.GetDAO(); 
        private readonly ILogger<BooksController> _logger;


        public BooksController(ILogger<BooksController> logger) 
        {
            _logger = logger;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public async Task<IEnumerable<IBook>> Get()
        {
            _logger.LogInformation("GET: api/books");
            return await _service.GetAllBooks();
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public async Task<IBook> Get(int id)
        {
            _logger.LogInformation($"GET: api/books/{id}");
            return await _service.GetBookById(id);
        }

        // POST api/<BooksController>
        [HttpPost]
        public void Post([FromBody] IBook book)
        {
            _logger.LogInformation("POST: api/books");
            _service.AddBook(book);
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] IBook book)
        {
            _logger.LogInformation($"PUT: api/books/{id}");
            _service.UpdateBook(book);
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _logger.LogInformation($"DELETE: api/books/{id}");
            _service.DeleteBook(id);
        }
    }
}
