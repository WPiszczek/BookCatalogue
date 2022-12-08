using PiszczekSzpotek.BookCatalogue.Core.Enums;

namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IAuthor Author { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Language { get; set; }
        public string? Description { get; set; }
        public string PhotoUrl { get; set; }
        public BookCategory Category { get; set; }
        public IEnumerable<IReview> Reviews { get; set; }
    }
}
