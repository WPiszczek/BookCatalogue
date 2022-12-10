using PiszczekSzpotek.BookCatalogue.Core.Enums;
using PiszczekSzpotek.BookCatalogue.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace PiszczekSzpotek.BookCatalogue.SQLiteDatabase.Models
{
    [Table("Books")]
    public class Book : IBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public int ReleaseYear { get; set; }
        public string Language { get; set; }
        public string? Description { get; set; }
        public string PhotoUrl { get; set; }
        public BookCategory Category { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}
