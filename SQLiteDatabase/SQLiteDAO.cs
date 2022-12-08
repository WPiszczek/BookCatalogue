using PiszczekSzpotek.BookCatalogue.Interfaces;
using PiszczekSzpotek.BookCatalogue.SQLiteDatabase.Exceptions;
using PiszczekSzpotek.BookCatalogue.SQLiteDatabase.Models;
using Microsoft.EntityFrameworkCore;
using PiszczekSzpotek.BookCatalogue.Core.Enums;

namespace PiszczekSzpotek.BookCatalogue.SQLiteDatabase
{
    public class SQLiteDAO : IDAO<Book, Author, Review>
    {
        SQLiteDatabaseContext _context;

        public SQLiteDAO(SQLiteDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }


        public Task<IEnumerable<Book>> GetBooksByAuthor(int authorId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetBooksByCategory(BookCategory category)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetBooksByLanguage(string language)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> SearchBooksByTitle(string title)
        {
            throw new NotImplementedException();
        }
        public async Task<Book> GetBookById(int id)
        {
            if (_context.Books == null)
            {
                throw new ContextIsNullException();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                throw new ObjectNotFoundException();
            }

            return book;
        }

        public void CreateBook(Book book)
        {
            throw new NotImplementedException();
        }

        // TODO
        public void UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }

        // TODO
        public void DeleteBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Author>> GetAllAuthors()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Author>> SearchAuthorsByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Author> GetAuthorById(int id)
        {
            throw new NotImplementedException();
        }

        public void AddAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public void UpdateAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public void DeleteAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> GetReviewsByBook(int bookId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> GetReviewsByRating(int rating)
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetReviewById(int id)
        {
            throw new NotImplementedException();
        }

        public void AddReview(Review review)
        {
            throw new NotImplementedException();
        }

        public void UpdateReview(Review review)
        {
            throw new NotImplementedException();
        }

        public void DeleteReview(Review review)
        {
            throw new NotImplementedException();
        }
    }
}
