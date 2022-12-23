using API;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PiszczekSzpotek.BookCatalogue.API.Utils;
using PiszczekSzpotek.BookCatalogue.Core.Exceptions;
using PiszczekSzpotek.BookCatalogue.Interfaces;
using System.Text.Json;

namespace PiszczekSzpotek.BookCatalogue.API.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IDAO _service = Program.GetDAO();
        private readonly Type _authorType = Program.GetAuthorType();
        private readonly ILogger<AuthorsController> _logger;

            
        public AuthorsController(ILogger <AuthorsController> logger) 
        {
            _logger = logger;
        }

        // GET: api/<AuthorsController>
        [HttpGet]
        public async Task<string> Get(string? name=null)
        {
            _logger.LogInformation("GET: api/authors");

            try
            {
                var authors = await _service.GetAuthors(name);

                return ResponseHelper.Data(authors);
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Error while fetching authors. Try again.");
            }
        }

        // GET api/<AuthorsController>/5
        [HttpGet("{id}")]
        public async Task<string> GetById(int id)
        {
            _logger.LogInformation($"GET: api/authors/{id}");

            try
            {
                var author = await _service.GetAuthorById(id);
                return ResponseHelper.Data(author);
            } catch (ObjectNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Author not found.");
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Error while fetching author. Try again.");
            }
        }

        // POST api/<AuthorsController>
        [HttpPost]
        public async Task<string> Post([FromBody] JsonElement json)
        {
            _logger.LogInformation("POST: api/authors");

            try
            {
                var author = Activator.CreateInstance(_authorType) as IAuthor;

                author.Name = json.GetProperty("Name").GetString();
                author.BirthDate = Convert.ToDateTime(json.GetProperty("BirthDate").GetString());

                bool success = await _service.AddAuthor(author);
                if (success) return ResponseHelper.Success("Author added successfully.");
                else return ResponseHelper.Error("Error while adding author. Try again.");
            } catch (ObjectIdAlreadyExistsException ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Author with given Id already exists.");
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Error while adding author. Try again.");
            }
        }

        // TODO

        // PUT api/<AuthorsController>/5
        [HttpPut("{id}")]
        public async Task<string> Put(int id, [FromBody] JsonElement json)
        {
            _logger.LogInformation($"PUT: api/authors/{id}");

            try
            {
                var author = Activator.CreateInstance(_authorType) as IAuthor;

                author.Id = id;
                author.Name = json.GetProperty("Name").GetString();
                author.BirthDate = Convert.ToDateTime(json.GetProperty("BirthDate").GetString());

                bool success = await _service.UpdateAuthor(author);
                if (success) return ResponseHelper.Success("Author updated successfully.");
                else return ResponseHelper.Error("Error while updating author. Try again.");
            }
            catch (ObjectNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Author not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Error while updating author. Try again.");
            }
        }

        // TODO

        // DELETE api/<AuthorsController>/5
        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            _logger.LogInformation($"DELETE: api/authors/{id}");

            try
            {
                bool success = await _service.DeleteAuthor(id);
                if (success) return ResponseHelper.Success("Author deleted successfully.");
                else return ResponseHelper.Error("Error while deleting author. Try again.");
            }
            catch (ObjectNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Author not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Error while deleting author. Try again.");
            }
        }
    }
}
