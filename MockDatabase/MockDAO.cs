using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        Task<bool> IAuthorsRepository.AddAuthor(IAuthor author)
        {
            throw new NotImplementedException();
        }

        Task<bool> IBooksRepository.AddBook(IBook book)
        {
            throw new NotImplementedException();
        }

        Task<bool> IReviewsRepository.AddReview(IReview review)
        {
            throw new NotImplementedException();
        }

        Task<bool> IAuthorsRepository.DeleteAuthor(int id)
        {
            throw new NotImplementedException();
        }

        Task<bool> IBooksRepository.DeleteBook(int id)
        {
            throw new NotImplementedException();
        }

        bool IImageRepository.DeleteImage(string directory, string name)
        {
            throw new NotImplementedException();
        }

        Task<bool> IReviewsRepository.DeleteReview(int id)
        {
            throw new NotImplementedException();
        }

        Task<IAuthor> IAuthorsRepository.GetAuthorById(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<IAuthor>> IAuthorsRepository.GetAuthors(string? name)
        {
            throw new NotImplementedException();
        }

        Task<IBook> IBooksRepository.GetBookById(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<IBook>> IBooksRepository.GetBooks(string? title, int? authorId, BookCategory? category)
        {
            throw new NotImplementedException();
        }

        FileStreamResult IImageRepository.GetImage(string name, string directory)
        {
            throw new NotImplementedException();
        }

        Task<IReview> IReviewsRepository.GetReviewById(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<IReview>> IReviewsRepository.GetReviews(int? bookId, int? rating)
        {
            throw new NotImplementedException();
        }

        Task<string> IImageRepository.PostImage(IFormFile file, string directory)
        {
            throw new NotImplementedException();
        }

        Task<string> IImageRepository.PutImage(IFormFile file, string directory, string currentName)
        {
            throw new NotImplementedException();
        }

        Task<bool> IAuthorsRepository.UpdateAuthor(IAuthor author)
        {
            throw new NotImplementedException();
        }

        Task<bool> IBooksRepository.UpdateBook(IBook book)
        {
            throw new NotImplementedException();
        }

        Task<bool> IBooksRepository.UpdateBookImageUrl(int bookId, string imageUrl)
        {
            throw new NotImplementedException();
        }

        Task<bool> IReviewsRepository.UpdateReview(IReview review)
        {
            throw new NotImplementedException();
        }
    }
}
