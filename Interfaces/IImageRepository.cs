using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IImageRepository
    {
        public FileStreamResult GetImage(string name, string directory);
        public Task<string> PostImage(IFormFile file, string directory);
        public Task<string> PutImage(IFormFile file, string directory, string currentName);
        public bool DeleteImage(string directory, string name);
    }
}
