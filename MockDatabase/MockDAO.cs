using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PiszczekSzpotek.BookCatalogue.Core.Enums;
using PiszczekSzpotek.BookCatalogue.Core.Exceptions;
using PiszczekSzpotek.BookCatalogue.Interfaces;
using PiszczekSzpotek.BookCatalogue.MockDatabase.Models;

namespace PiszczekSzpotek.BookCatalogue.MockDatabase
{
    public class MockDAO : IDAO
    {
        private IEnumerable<Book> _books;
        private IEnumerable<Author> _authors;
        private IEnumerable<Review> _reviews;

        public MockDAO()
        {
            _authors = new List<Author>()
            {
                new Author()
                {
                    Id = 1,
                    Name = "Andrzej Sapkowski",
                    Country = "Polska",
                    BirthDate = new DateTime(1948, 6, 21),
                    Status = AuthorStatus.Retired,
                    ImageUrl = "andrzej-sapkowski.jpg",
                    Books = new List<Book>() {}
                },
                new Author()
                {
                    Id = 2,
                    Name = "Jo Nesbo",
                    Country = "Norwegia",
                    BirthDate = new DateTime(1960, 3, 29),
                    Status = AuthorStatus.Active,
                    ImageUrl = "jo-nesbo.jpg",
                    Books = new List<Book>() {}
                }
            };

            _books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Ostatnie Życzenie",
                    AuthorId = 1,
                    Author = _authors.First(e => e.Id == 1),
                    ReleaseYear = 1993,
                    Category = BookCategory.Fantastyka,
                    ImageUrl = "ostatnie-zyczenie.jpg",
                    Reviews = new List<Review>()
                },
                new Book()
                {
                    Id = 2,
                    Title = "Narrenturm",
                    AuthorId = 1,
                    Author = _authors.First(e => e.Id == 1),
                    ReleaseYear = 2002,
                    Category = BookCategory.Historia,
                    ImageUrl = "narrenturm.jpg",
                    Reviews = new List<Review>()
                },
                new Book()
                {
                    Id = 3,
                    Title = "Czerwone Gardło",
                    AuthorId = 2,
                    Author = _authors.First(e => e.Id == 2),
                    ReleaseYear = 2000,
                    Category= BookCategory.KryminalSensacja,
                    ImageUrl = "czerwone-gardlo.jpg",
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
                    BookId = 1,
                    Book = _books.First(e => e.Id == 1)
                },
                new Review()
                {
                    Id = 2,
                    Rating = 3,
                    Title = "Rozczarowanie",
                    Reviewer = "Mariusz",
                    Content = "Beznadziejna książka",
                    DateAdded = new DateTime(2022, 12, 8, 19, 33, 32),
                    BookId = 2,
                    Book = _books.First(e => e.Id == 2)
                }
            };
        }


        // BooksRepository
        public Task<IEnumerable<IBook>> GetBooks(string? title, int? authorId, BookCategory? category)
        {
            var books = _books.Where(e =>
                    (title == null || e.Title.ToLower().Contains(title.ToLower()))
                    && (authorId == null || e.Author.Id == authorId)
                    && (category == null || e.Category == category)
                );

            foreach (var book in books)
            {
                UpdateBookAverageRating(book);
            }
            return Task.FromResult(books as IEnumerable<IBook>);
        }

        public Task<IBook> GetBookById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddBook(IBook book)
        {
            throw new NotImplementedException();
        }
        public Task<bool> UpdateBook(IBook book)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBookImageUrl(int bookId, string imageUrl)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteBook(int id)
        {
            throw new NotImplementedException();
        }


        // AuthorsRepository
        public Task<IEnumerable<IAuthor>> GetAuthors(string? name, AuthorStatus? status)
        {
            var authors = _authors.Where(e =>
                    (name == null || e.Name.ToLower().Contains(name.ToLower()))
                    && (status == null || e.Status == status)
                );

            foreach (var author in authors)
            {
                UpdateAuthorAverageRating(author);
            }
            return Task.FromResult(
                authors as IEnumerable<IAuthor>
            );
        }

        public Task<IAuthor> GetAuthorById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddAuthor(IAuthor author)
        {
            throw new NotImplementedException();
        }
        
        public Task<bool> UpdateAuthor(IAuthor author)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAuthorImageUrl(int authorId, string imageUrl)
        {
            throw new NotImplementedException();
        }
        
        public Task<bool> DeleteAuthor(int id)
        {
            throw new NotImplementedException();
        }


        // ReviewsRepository
        public Task<IEnumerable<IReview>> GetReviews(int? bookId, string? search)
        {
            throw new NotImplementedException();
        }
        public Task<IReview> GetReviewById(int id)
        {
            throw new NotImplementedException();
        }
        public Task<bool> AddReview(IReview review)
        {
            throw new NotImplementedException();
        }
        public Task<bool> UpdateReview(IReview review)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteReview(int id)
        {
            throw new NotImplementedException();
        }


        // ImageRepository
        public FileStreamResult GetImage(string name, string directory)
        {
            string imageExtension = GetImageExtension(name);

            string path = GetPath(directory, name);
            var image = File.OpenRead(path);

            return new FileStreamResult(image, imageExtension);
        }

        public async Task<string> PostImage(IFormFile file, string directory)
        {
            string newFilename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + Path.GetFileName(file.FileName);

            string path = GetPath(directory, newFilename);

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return newFilename;
        }

        public async Task<string> PutImage(IFormFile file, string directory, string currentName)
        {
            try
            {
                DeleteImage(directory, currentName);
            }
            catch (FileNotFoundException) { }
            return await PostImage(file, directory);
        }

        public bool DeleteImage(string directory, string name)
        {
            string path = GetPath(directory, name);
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            File.Delete(path);
            return true;
        }

        // Utilities
        private void UpdateBookAverageRating(Book book)
        {   
            book.Reviews = _reviews.Where(e => e.Id == book.Id);
            try
            {
                book.AverageRating = book.Reviews.Average(e => e.Rating);
            }
            catch (Exception ex)
            {
                book.AverageRating = null;
            }
        }

        private void UpdateAuthorAverageRating(Author author)
        {
            try
            {
                var reviews = _reviews.Where(e => e.Book.AuthorId == author.Id);
                author.AverageRating = reviews.Average(e => e.Rating);
            }
            catch (Exception ex)
            {
                author.AverageRating = null;
            }
        }

        private string GetPath(string directory, string name)
        {
            directory = GetImageDirectory(directory);
            return Path.Combine("..", "ImagesMock", directory, name);
        }

        private string GetImageExtension(string name)
        {
            if (name.ToLower().EndsWith(".jpg")) return "image/jpg";
            else if (name.ToLower().EndsWith(".jpeg")) return "image/jpeg";
            else if (name.ToLower().EndsWith(".png")) return "image/png";
            else if (name.ToLower().EndsWith(".bmp")) return "image/bmp";
            else throw new InvalidImageExtensionException();
        }

        private string GetImageDirectory(string directory)
        {
            if (directory.ToLower() == "authors") return "Authors";
            else if (directory.ToLower() == "books") return "Books";
            else throw new InvalidImageDirectoryException();
        }
    }
}
