using PiszczekSzpotek.BookCatalogue.Interfaces;
using PiszczekSzpotek.BookCatalogue.SQLiteDatabase.Exceptions;
using PiszczekSzpotek.BookCatalogue.SQLiteDatabase.Models;
using Microsoft.EntityFrameworkCore;
using PiszczekSzpotek.BookCatalogue.Core.Enums;

namespace PiszczekSzpotek.BookCatalogue.SQLiteDatabase
{
    public class SQLiteDAO : IDAO
    {
        private readonly SQLiteDatabaseContext _context;

        public SQLiteDAO(SQLiteDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IBook>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
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

        public Task<IEnumerable<IBook>> SearchBooksByTitle(string title)
        {
            throw new NotImplementedException();
        }
        public async Task<IBook> GetBookById(int id)
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

        public async void CreateBook(IBook book)
        {
            if (BookExists(book.Id))
            {
                throw new ObjectIdAlreadyExistsException();
            }
            _context.Books.Add((Book) book);
            await _context.SaveChangesAsync();
        }

        public async void UpdateBook(IBook book)
        {
            if (!BookExists(book.Id))
            {
                throw new ObjectNotFoundException();
            }
            _context.Update(book);
            await _context.SaveChangesAsync();
        }

        public async void DeleteBook(IBook book)
        {
            if (!BookExists(book.Id))
            {
                throw new ObjectNotFoundException();
            }
            _context.Books.Remove((Book) book);
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<IAuthor>> GetAllAuthors()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IAuthor>> SearchAuthorsByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IAuthor> GetAuthorById(int id)
        {
            throw new NotImplementedException();
        }

        public void AddAuthor(IAuthor author)
        {
            throw new NotImplementedException();
        }

        public void UpdateAuthor(IAuthor author)
        {
            throw new NotImplementedException();
        }

        public void DeleteAuthor(IAuthor author)
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

        public Task<IReview> GetReviewById(int id)
        {
            throw new NotImplementedException();
        }

        public void AddReview(IReview review)
        {
            throw new NotImplementedException();
        }

        public void UpdateReview(IReview review)
        {
            throw new NotImplementedException();
        }

        public void DeleteReview(IReview review)
        {
            throw new NotImplementedException();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
