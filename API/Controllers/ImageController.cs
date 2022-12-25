using API;
using Microsoft.AspNetCore.Mvc;
using PiszczekSzpotek.BookCatalogue.Core.Exceptions;
using PiszczekSzpotek.BookCatalogue.Interfaces;

namespace PiszczekSzpotek.BookCatalogue.API.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly ILogger<ImageController> _logger;
        private readonly IDAO _service = Program.GetDAO();

        public ImageController(ILogger<ImageController> logger)
        {
            _logger = logger;
        }

        // GET: api/image/1.png
        [HttpGet("{directory}/{imageName}")]
        public IActionResult Get(string directory, string imageName)
        {
            try
            {
                _logger.LogInformation($"GET: api/{directory}/{imageName}");
                return _service.GetImage(imageName, directory);

            } 
            catch (InvalidImageExtensionException ex) 
            {
                _logger.LogError(ex.Message);
                return new JsonResult(new
                {
                    Status = "Fail",
                    Message = "Invalid file extension."
                });
            }
            catch (InvalidImageDirectoryException ex)
            {
                _logger.LogError(ex.Message);
                return new JsonResult(new
                {
                    Status = "Fail",
                    Message = "Invalid directory."
                });
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return new JsonResult(new
                {
                    Status = "Fail",
                    Message = "File not found."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new JsonResult(new
                {
                    Status = "Fail",
                    Message = "Error while getting file."
                });
            }
        }
    }
}
