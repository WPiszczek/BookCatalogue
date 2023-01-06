using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PiszczekSzpotek.BookCatalogue.Core.Enums;
using PiszczekSzpotek.BookCatalogue.Core.Exceptions;
using PiszczekSzpotek.BookCatalogue.Interfaces;
using PiszczekSzpotek.BookCatalogue.MockDatabase.Models;
using System.Xml.Linq;

namespace PiszczekSzpotek.BookCatalogue.MockDatabase
{
    public class MockDAO : IDAO
    {
        private List<Book> _books;
        private List<Author> _authors;
        private List<Review> _reviews;

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
            foreach(var book in _books)
            {
                UpdateBookAverageRating(book);
            }
            foreach(var author in _authors)
            {
                UpdateAuthorBooks(author);
                UpdateAuthorAverageRating(author);
            }
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
                UpdateAuthorBooks(book.Author);
            }
            return Task.FromResult(books as IEnumerable<IBook>);
        }

        public Task<IBook> GetBookById(int id)
        {
            var book = _books.FirstOrDefault(e => e.Id == id);
            if (book == null)
            {
                throw new ObjectNotFoundException();
            }
            return Task.FromResult(book as IBook);
        }

        public Task<bool> AddBook(IBook book)
        {
            var _book = (Book)book;
            _book.Author = _authors.FirstOrDefault(e => e.Id == _book.AuthorId);
            UpdateAuthorBooks(_book.Author);
            _book.Id = _books.Max(e => e.Id) + 1;
            _books.Add((Book)book);
            return Task.FromResult(true);
        }
        public Task<bool> UpdateBook(IBook book)
        {
            var index = _books.FindIndex(e => e.Id == book.Id);
            if (index == -1)
            {
                throw new ObjectNotFoundException();
            }
            _books[index].Author = _authors.FirstOrDefault(e => e.Id == book.AuthorId);
            _books[index].AuthorId = book.AuthorId;
            _books[index].Title = book.Title;
            _books[index].ReleaseYear = book.ReleaseYear;
            _books[index].Description = book.Description;
            _books[index].Category = book.Category;
            return Task.FromResult(true);
        }

        public Task<bool> UpdateBookImageUrl(int bookId, string imageUrl)
        {
            if (!BookExists(bookId))
            {
                throw new ObjectNotFoundException();
            }
            var book = _books.FirstOrDefault(e => e.Id == bookId);
            book.ImageUrl = imageUrl;
            return Task.FromResult(true);
        }
        public Task<bool> DeleteBook(int id)
        {
            if (!BookExists(id))
            {
                throw new ObjectNotFoundException();
            }
            var book = _books.FirstOrDefault(e => e.Id == id);
            _books.Remove(book);
            UpdateAuthorBooks(book.Author);
            DeleteBookReviews(book);
            return Task.FromResult(true);
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
            var author = _authors.FirstOrDefault(e => e.Id == id);
            if (author == null)
            {
                throw new ObjectNotFoundException();
            }
            return Task.FromResult<IAuthor>(author);
        }

        public Task<bool> AddAuthor(IAuthor author)
        {
            author.Id = _authors.Max(e => e.Id) + 1;
            _authors.Add((Author)author);
            return Task.FromResult(true);
        }
        
        public Task<bool> UpdateAuthor(IAuthor author)
        {
            var index = _authors.FindIndex(e => e.Id == author.Id);
            if (index == -1)
            {
                throw new ObjectNotFoundException();
            }
            _authors[index].BirthDate = author.BirthDate;
            _authors[index].Name = author.Name;
            _authors[index].DeathDate = author.DeathDate;
            _authors[index].Country = author.Country;
            _authors[index].Status = author.Status;

            foreach(var book in _books.Where(e => e.AuthorId == author.Id))
            {
                book.Author = (Author)author;
            }
            return Task.FromResult(true);
        }

        public Task<bool> UpdateAuthorImageUrl(int authorId, string imageUrl)
        {
            if (!AuthorExists(authorId))
            {
                throw new ObjectNotFoundException();
            }
            var author = _authors.FirstOrDefault(e => e.Id == authorId);
            author.ImageUrl = imageUrl;
            return Task.FromResult(true);
        }
        
        public Task<bool> DeleteAuthor(int id)
        {
            if (!AuthorExists(id))
            {
                throw new ObjectNotFoundException();
            }
            var author = _authors.FirstOrDefault(e => e.Id == id);
            _authors.Remove(author);
            foreach(var book in _books.Where(e => e.AuthorId == id))
            {
                DeleteBookReviews(book);
            }
            _books.RemoveAll(e => e.AuthorId == id);
            return Task.FromResult(true);
        }


        // ReviewsRepository
        public Task<IEnumerable<IReview>> GetReviews(int? bookId, string? search)
        {
            var reviews = _reviews.Where(e =>
            (bookId == null || e.Book.Id == bookId)
                        && (search == null || e.Title.ToLower().Contains(search.ToLower()) 
                        || e.Content.ToLower().Contains(search.ToLower())));
            return Task.FromResult(
                reviews as IEnumerable<IReview>
            );
        }
        public Task<IReview> GetReviewById(int id)
        {
            var review = _reviews.FirstOrDefault(e => e.Id == id);
            if (review == null)
            {
                throw new ObjectNotFoundException();
            }
            return Task.FromResult<IReview>(review);
        }
        public Task<bool> AddReview(IReview review)
        {
            var book = _books.FirstOrDefault(e => e.Id == review.BookId);
            if (book == null)
            {
                throw new ObjectNotFoundException();
            }
            review.Id = _reviews.Max(e => e.Id) + 1;
            review.Book = book;
            _reviews.Add((Review)review);
            var author = _authors.FirstOrDefault(e => e.Id == book.AuthorId);
            if (author == null)
            {
                throw new ObjectNotFoundException();
            }
            UpdateBookAverageRating(book);
            UpdateAuthorAverageRating(author);
            return Task.FromResult(true);
        }
        public Task<bool> UpdateReview(IReview review)
        {
            var index = _reviews.FindIndex(e => e.Id == review.Id);
            if (index == -1)
            {
                throw new ObjectNotFoundException();
            }
            _reviews[index].Reviewer = review.Reviewer;
            _reviews[index].Title = review.Title;
            _reviews[index].Content = review.Content;
            _reviews[index].BookId  = review.BookId;
            _reviews[index].Rating = review.Rating;

            var book = _books.FirstOrDefault(e => e.Id == review.BookId);
            var author = _authors.FirstOrDefault(e => e.Id == book.AuthorId);
            UpdateBookAverageRating(book);
            UpdateAuthorAverageRating(author);

            return Task.FromResult(true);
        }

        public Task<bool> DeleteReview(int id)
        {
            var review = _reviews.FirstOrDefault(e => e.Id == id);
            if (review == null)
            {
                throw new ObjectNotFoundException();
            }
            _reviews.RemoveAll(e => e.Id == id);

            var book = _books.FirstOrDefault(e => e.Id == review.BookId);
            var author = _authors.FirstOrDefault(e => e.Id == book.AuthorId);
            UpdateBookAverageRating(book);
            UpdateAuthorAverageRating(author);

            return Task.FromResult(true);
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

        private void DeleteBookReviews(Book book)
        {
            _reviews.RemoveAll(e => e.BookId == book.Id);
        }
        private bool BookExists(int id)
        {
            return _books.Any(e => e.Id == id);
        }

        private bool AuthorExists(int id)
        {
             return _authors.Any(e => e.Id == id);
        }

        private bool ReviewExists(int id)
        {
             return _reviews.Any(e => e.Id == id);
        }

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

        private void UpdateAuthorBooks(Author author)
        {
            author.Books = _books.Where(e => e.AuthorId == author.Id);
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
