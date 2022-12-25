using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace PiszczekSzpotek.BookCatalogue.API.Utils
{
    public class FormModel
    {
        [FromForm(Name="Image")]
        public IFormFile Image { get; set; }
        [FromForm(Name="Json")]
        public string Json { get; set; }
    }
}
