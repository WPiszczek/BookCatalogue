using PiszczekSzpotek.BookCatalogue.Interfaces;

namespace PiszczekSzpotek.BookCatalogue.MockDatabase.Models
{
    public class Review : IReview
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Reviewer { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }
        public IBook? Book { get; set; }
        public int BookId { get; set; }
        public int Rating { get; set; }
    }
}
