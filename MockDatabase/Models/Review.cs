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
        public Book Book { get; set; }
        IBook IReview.Book
        {
            get => Book;
            set
            {
                Book = value as Book;
            }
        }
        public int BookId { get; set; }
        public int Rating { get; set; }
    }
}
