using PiszczekSzpotek.BookCatalogue.Core.Enums;
using PiszczekSzpotek.BookCatalogue.Interfaces;

namespace PiszczekSzpotek.BookCatalogue.MockDatabase.Models
{
    public class Book : IBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IAuthor Author { get; set; }
        public int AuthorId { get; set; }
        public int ReleaseYear { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public BookCategory Category { get; set; }
        public IEnumerable<IReview> Reviews { get; set; }
    }
}
