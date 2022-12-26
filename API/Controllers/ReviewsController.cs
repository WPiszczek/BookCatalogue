using API;
using Microsoft.AspNetCore.Mvc;
using PiszczekSzpotek.BookCatalogue.API.Utils;
using PiszczekSzpotek.BookCatalogue.Core.Exceptions;
using PiszczekSzpotek.BookCatalogue.Interfaces;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PiszczekSzpotek.BookCatalogue.API.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IDAO _service = Program.GetDAO();
        private readonly Type _reviewType = Program.GetReviewType();
        private readonly ILogger<ReviewsController> _logger;

        public ReviewsController(ILogger<ReviewsController> logger)
        {
            _logger = logger;
        }

        // GET: api/reviews
        [HttpGet]
        public async Task<string> Get(int? bookId=null, int? rating=null)
        {
            _logger.LogInformation("GET api/reviews");

            try
            {
                var reviews = await _service.GetReviews(bookId, rating);

                return ResponseHelper.Data(reviews);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Error while fetching reviews. Try again.");
            }
        }

        // GET api/reviews/5
        [HttpGet("{id}")]
        public async Task<string> GetById(int id)
        {
            _logger.LogInformation($"GET: api/reviews/{id}");

            try
            {
                var review = await _service.GetReviewById(id);
                return ResponseHelper.Data(review);
            }
            catch (ObjectNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Review not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Error while fetching review. Try again.");
            }
        }

        // POST api/reviews
        [HttpPost]
        public async Task<string> Post([FromBody] JsonElement json)
        {
            _logger.LogInformation("POST: api/reviews");

            try
            {
                var review = Activator.CreateInstance(_reviewType) as IReview;

                review.Title = json.GetProperty("Title").GetString();
                review.Rating = json.GetProperty("Rating").GetInt32();
                review.Content = json.GetProperty("Content").GetString();
                review.Reviewer = json.GetProperty("Reviewer").GetString();
                review.DateAdded = DateTime.Now;
                review.BookId = json.GetProperty("BookId").GetInt32();

                bool success = await _service.AddReview(review);
                if (success) return ResponseHelper.Success("Review added successfully.");
                else return ResponseHelper.Error("Error while adding review. Try again.");
            }
            catch (ObjectIdAlreadyExistsException ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Review with given Id already exists.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Error while adding review. Try again.");
            }
        }

        // PUT api/reviews/5
        [HttpPut("{id}")]
        public async Task<string> Put(int id, [FromBody] JsonElement json)
        {
            _logger.LogInformation($"PUT: api/reviews/{id}");
            try
            {
                var review = Activator.CreateInstance(_reviewType) as IReview;

                review.Id = id;
                review.Title = json.GetProperty("Title").GetString();
                review.Rating = json.GetProperty("Rating").GetInt32();
                review.Content = json.GetProperty("Content").GetString();
                review.Reviewer = json.GetProperty("Reviewer").GetString();
                review.BookId = json.GetProperty("BookId").GetInt32();

                bool success = await _service.UpdateReview(review);
                if (success) return ResponseHelper.Success("Review updated successfully.");
                else return ResponseHelper.Error("Error while updating review. Try again.");
            }
            catch (ObjectNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Review with given Id not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Error while updating review. Try again.");
            }
        }

        // DELETE api/reviews/5
        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            _logger.LogInformation($"DELETE: api/reviews/{id}");

            try
            {
                bool success = await _service.DeleteReview(id);
                if (success) return ResponseHelper.Success("Review deleted successfully.");
                else return ResponseHelper.Error("Error while deleting review. Try again.");
            }
            catch (ObjectNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Review with given Id not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseHelper.Error("Error while deleting review. Try again.");
            }
        }
    }
}
