using PiszczekSzpotek.BookCatalogue.Core.Enums;
using PiszczekSzpotek.BookCatalogue.Interfaces;
using PiszczekSzpotek.BookCatalogue.MockDatabase.Models;

namespace PiszczekSzpotek.BookCatalogue.MockDatabase
{
    public class MockDAO : IDAO
    {
        private IEnumerable<IBook> _books;
        private IEnumerable<IAuthor> _authors;
        private IEnumerable<IReview> _reviews;

        public MockDAO()
        {
            _authors = new List<IAuthor>()
            {
                new Author()
                {
                    Id = 1,
                    Name = "Andrzej Sapkowski",
                    BirthDate = new DateTime(1948, 6, 21),
                    Books = new List<Book>() {}
                },
                new Author()
                {
                    Id = 2,
                    Name = "Jo Nesbo",
                    BirthDate = new DateTime(1960, 3, 29),
                    Books = new List<Book>() {}
                }
            };

            _books = new List<IBook>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Ostatnie Życzenie",
                    AuthorId = 1,
                    ReleaseYear = 1993,
                    Category = BookCategory.Fantastyka,
                    Reviews = new List<Review>()
                },
                new Book()
                {
                    Id = 2,
                    Title = "Narrenturm",
                    AuthorId = 1,
                    ReleaseYear = 2002,
                    Category = BookCategory.Historia,
                    Reviews = new List<Review>()
                },
                new Book()
                {
                    Id = 3,
                    Title = "Czerwone Gardło",
                    AuthorId = 2,
                    ReleaseYear = 2000,
                    Category= BookCategory.Kryminal_Sensacja,
                    Reviews = new List<Review>()
                }
            };

            _reviews = new List<Review>()
            {
                new Review()
                {
                    Id = 1,
                    Rating = 10,
                    Title = "Wspaniała",
                    Reviewer = "Mariusz",
                    Content = "Najlepsza książka jaką czytałem",
                    DateAdded = new DateTime(2022, 12, 9, 20, 0, 0),
                    BookId = 1
                },
                new Review()
                {
                    Id = 2,
                    Rating = 3,
                    Title = "Rozczarowanie",
                    Reviewer = "Mariusz",
                    Content = "Beznadziejna książka",
                    DateAdded = new DateTime(2022, 12, 8, 19, 33, 32),
                    BookId = 2
                }
            };
        }

        public void AddAuthor(IAuthor author)
        {
            throw new NotImplementedException();
        }

        public void AddReview(IReview review)
        {
            throw new NotImplementedException();
        }

        public void CreateBook(IBook book)
        {
            throw new NotImplementedException();
        }

        public void DeleteAuthor(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteReview(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IAuthor>> GetAllAuthors()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IBook>> GetAllBooks()
        {
            foreach (Book book in _books)
            {
                book.Author = _authors.FirstOrDefault(a => a.Id == 1) as Author;
                //Console.WriteLine(book.Author.Name);
            }
            return Task.FromResult(_books);
        }

        public Task<IAuthor> GetAuthorById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IBook> GetBookById(int id)
        {
            return Task.FromResult(_books.FirstOrDefault(book => book.Id == id));
        }

        public Task<IEnumerable<IBook>> GetBooksByAuthor(int authorId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IBook>> GetBooksByCategory(BookCategory category)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IBook>> GetBooksByLanguage(string language)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IReview>> GetAllReviews()
        {
            throw new NotImplementedException();
        }

        public Task<IReview> GetReviewById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IReview>> GetReviewsByBook(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IReview>> GetReviewsByRating(int rating)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IAuthor>> SearchAuthorsByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IBook>> SearchBooksByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public void UpdateAuthor(IAuthor author)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(IBook book)
        {
            throw new NotImplementedException();
        }

        public void UpdateReview(IReview review)
        {
            throw new NotImplementedException();
        }
    }
}
