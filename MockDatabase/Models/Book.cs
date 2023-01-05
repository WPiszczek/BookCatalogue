using PiszczekSzpotek.BookCatalogue.Core.Enums;
using PiszczekSzpotek.BookCatalogue.Interfaces;

namespace PiszczekSzpotek.BookCatalogue.MockDatabase.Models
{
    public class Book : IBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        IAuthor IBook.Author
        {
            get => Author;
            set
            {
                Author = value as Author;
            }
        }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public int ReleaseYear { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public BookCategory Category { get; set; }
        public double? AverageRating { get; set; }
        public virtual IEnumerable<Review> Reviews { get; set; }
        IEnumerable<IReview> IBook.Reviews
        {
            get => Reviews;
            set
            {
                Reviews = value as IEnumerable<Review>;
            }
        }
    }
}
