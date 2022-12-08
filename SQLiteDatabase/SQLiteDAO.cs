using PiszczekSzpotek.BookCatalogue.Interfaces;
using PiszczekSzpotek.BookCatalogue.SQLiteDatabase.Exceptions;
using PiszczekSzpotek.BookCatalogue.SQLiteDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace PiszczekSzpotek.BookCatalogue.SQLiteDatabase
{
    public class SQLiteDAO : IBooksDAO<Book>
    {
        SQLiteDatabaseContext _context;

        public SQLiteDAO(SQLiteDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync<Book>();
        }

        public async Task<Book> GetBook(int id)
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

    }
}
