using API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using PiszczekSzpotek.BookCatalogue.Interfaces;

namespace PiszczekSzpotek.BookCatalogue.API.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IDAO _service = Program.GetDAO();
        private readonly ILogger<AuthorsController> _logger;

            
        public AuthorsController(ILogger <AuthorsController> logger) 
        {
            _logger = logger;
        }

        // GET: api/<AuthorsController>
        [HttpGet]
        public async Task<IEnumerable<IAuthor>> Get()
        {
            _logger.LogInformation("GET: api/authors");
            return await _service.GetAllAuthors();
        }

        // GET api/<AuthorsController>/5
        [HttpGet("{id}")]
        public async Task<IAuthor> Get(int id)
        {
            _logger.LogInformation($"GET: api/authors/{id}");
            return await _service.GetAuthorById(id);
        }

        // POST api/<AuthorsController>
        [HttpPost]
        public void Post([FromBody] IAuthor author)
        {
            _logger.LogInformation($"POST: api/authors");
            _service.AddAuthor(author);
        }

        // PUT api/<AuthorsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] IAuthor author)
        {
            _logger.LogInformation($"PUT: api/authors/{id}");
            _service.UpdateAuthor(author);
        }

        // DELETE api/<AuthorsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _logger.LogInformation($"DELETE: api/authors/{id}");
            _service.DeleteAuthor(id);
        }
    }
}
