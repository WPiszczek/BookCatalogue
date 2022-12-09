using Microsoft.AspNetCore.Mvc;
using PiszczekSzpotek.BookCatalogue.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PiszczekSzpotek.BookCatalogue.API.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IDAO _service;

        public BooksController(IDAO service)
        {
            _service = service;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public async Task<IEnumerable<IBook>> Get()
        {
            return await _service.GetAllBooks();
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BooksController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
