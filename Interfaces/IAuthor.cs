using PiszczekSzpotek.BookCatalogue.Core.Enums;

namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IAuthor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public AuthorStatus Status { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<IBook> Books { get; set; }
    }
}
