﻿using API;
using Microsoft.AspNetCore.Mvc;
using PiszczekSzpotek.BookCatalogue.API.Utils;
using PiszczekSzpotek.BookCatalogue.Core.Enums;
using PiszczekSzpotek.BookCatalogue.Core.Exceptions;
using PiszczekSzpotek.BookCatalogue.Interfaces;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PiszczekSzpotek.BookCatalogue.API.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IDAO _service = Program.GetDAO(); 
        private readonly Type _bookType = Program.GetBookType();
        private readonly ILogger<BooksController> _logger;

        public BooksController(ILogger<BooksController> logger) 
        {
            _logger = logger;
        }

        // GET: api/books
        [HttpGet]
        public async Task<string> Get(
            string? title=null, 
            int? authorId=null, 
            BookCategory? category=null)
        {
            _logger.LogInformation("GET: api/books");
            
            try
            {
                var books = await _service.GetBooks(title, authorId, category);
                return ResponseHelper.Data(books);
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return ResponseHelper.Error("Error while fetching books. Try again.");
            }
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public async Task<string> GetById(int id)
        {
            _logger.LogInformation($"GET: api/books/{id}");

            try
            {
                var book = await _service.GetBookById(id);
                return ResponseHelper.Data(book);
            }
            catch (ObjectNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Book not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Error while fetching book. Try again.");
            }
        }

        // POST api/books
        [HttpPost]
        public async Task<string> Post([FromForm] FormModel formModel)
        {
            _logger.LogInformation("POST: api/books");

            try
            {
                JsonElement json = JsonDocument.Parse(formModel.Json).RootElement;
                var image = formModel.Image;
                var book = Activator.CreateInstance(_bookType) as IBook;

                book.Title = json.GetProperty("Title").GetString();
                book.ReleaseYear = json.GetProperty("ReleaseYear").GetInt32();
                book.Description = json.GetProperty("Description").GetString();
                book.Category = (BookCategory)json.GetProperty("Category").GetInt32();
                book.AuthorId = json.GetProperty("AuthorId").GetInt32();

                book.ImageUrl = await _service.PostImage(image, "books");

                bool success = await _service.AddBook(book);
                if (success) return ResponseHelper.Success("Book added successfully.");
                else return ResponseHelper.Error("Error while adding book. Try again.");
            }
            catch (ObjectIdAlreadyExistsException ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Book with given Id already exists.");
            }
            catch (InvalidBookCategoryException ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Invalid book category.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Error while adding book. Try again.");
            }
        }

        // PATCH api/books/5
        [HttpPatch("{id}")]
        public async Task<string> UpdateBookImage(int id, [FromForm] FormModel formModel)
        {
            _logger.LogInformation($"PATCH: api/books/{id}");

            try
            {
                var book = await _service.GetBookById(id);
                string imageUrl = book.ImageUrl;
                string newImageUrl = await _service.PutImage(formModel.Image, "books", imageUrl);
                bool success = await _service.UpdateBookImageUrl(id, newImageUrl);

                if (success) return ResponseHelper.Success("Book image updated successfully.");
                else return ResponseHelper.Error("Error while updating book image. Try again.");
            }
            catch (ObjectNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Book not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return ResponseHelper.Error("Error while updating book image. Try again.");
            }
        }

        // PUT api/books/5
        [HttpPut("{id}")]
        public async Task<string> Put(int id, [FromBody] JsonElement json)
        {
            _logger.LogInformation($"PUT: api/books/{id}");
            try
            {
                var book = Activator.CreateInstance(_bookType) as IBook;

                book.Id = id;
                book.Title = json.GetProperty("Title").GetString();
                book.ReleaseYear = json.GetProperty("ReleaseYear").GetInt32();
                book.Description = json.GetProperty("Description").GetString();
                //book.ImageUrl = json.GetProperty("ImageUrl").GetString();
                book.Category = (BookCategory)json.GetProperty("Category").GetInt32();
                book.AuthorId = json.GetProperty("AuthorId").GetInt32();

                bool success = await _service.UpdateBook(book);
                if (success) return ResponseHelper.Success("Book updated successfully.");
                else return ResponseHelper.Error("Error while updating book. Try again.");
            }
            catch (ObjectNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Book with given Id not found.");
            }
            catch (InvalidBookCategoryException ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Invalid book category.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Error while updating book. Try again.");
            }
        }

        // DELETE api/books/5
        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            _logger.LogInformation($"DELETE: api/books/{id}");

            try
            {
                bool success = await _service.DeleteBook(id);
                if (success) return ResponseHelper.Success("Book deleted successfully.");
                else return ResponseHelper.Error("Error while deleting book. Try again.");
            }
            catch (ObjectNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Book with given Id not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Error while deleting book. Try again.");
            }
        }
    }
}
