using API;
using Microsoft.AspNetCore.Mvc;
using PiszczekSzpotek.BookCatalogue.API.Exceptions;

namespace PiszczekSzpotek.BookCatalogue.API.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly ILogger<ImageController> _logger;

        public ImageController(ILogger<ImageController> logger)
        {
            _logger = logger;
        }

        // GET: api/image/1.png
        [HttpGet("{imageName}")]
        public IActionResult Get(string imageName)
        {
            try
            {
                string imageExtension;
                if (imageName.ToLower().EndsWith(".jpg")) imageExtension = "image/jpg";
                else if (imageName.ToLower().EndsWith(".jpeg")) imageExtension = "image/jpeg";
                else if (imageName.ToLower().EndsWith(".png")) imageExtension = "image/png";
                else if (imageName.ToLower().EndsWith(".bmp")) imageExtension = "image/bmp";
                else throw new InvalidImageExtensionException();

                var image = System.IO.File.OpenRead(Path.Combine("..", "Images", imageName));

                return File(image, imageExtension);

            } catch (InvalidImageExtensionException ex) 
            {
                _logger.LogError(ex.Message);
                return Json(new
                {
                    Status = "Fail",
                    Message = "Invalid file extension."
                });
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return Json(new
                {
                    Status = "Fail",
                    Message = "File not found."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(new
                {
                    Status = "Fail",
                    Message = "Error while getting file."
                });
            }
        }
    }
}
