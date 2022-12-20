using Microsoft.EntityFrameworkCore;
using PiszczekSzpotek.BookCatalogue.Interfaces;
using PiszczekSzpotek.BookCatalogue.Core.Exceptions;
using PiszczekSzpotek.BookCatalogue.Core.Enums;
using PiszczekSzpotek.BookCatalogue.SQLiteDatabase.Models;

namespace PiszczekSzpotek.BookCatalogue.SQLiteDatabase
{
    public class SQLiteDAO : IDAO
    {
        private readonly SQLiteDatabaseContext _context;

        public SQLiteDAO()
        {
            _context = new SQLiteDatabaseContext();
        }

        public SQLiteDAO(SQLiteDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IBook>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();

        }

        public async Task<IEnumerable<IBook>> GetBooksByAuthor(int authorId)
        {
            return await _context.Books.Where(e => e.Author.Id == authorId).ToListAsync();
        }

        public async Task<IEnumerable<IBook>> GetBooksByCategory(BookCategory category)
        {
            return await _context.Books.Where(e => e.Category == category).ToListAsync();
        }

        public async Task<IEnumerable<IBook>> GetBooksByLanguage(string language)
        {
            return await _context.Books.Where(e => e.Language == language).ToListAsync();
        }

        public async Task<IEnumerable<IBook>> SearchBooksByTitle(string title)
        {
            return await _context.Books.Where(e => e.Title.Contains(title)).ToListAsync();
        }

        public async Task<IBook> GetBookById(int id)
        {
            if (_context.Books == null)
            {
                throw new ContextIsNullException();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(e => e.Id == id);

            if (book == null)
            {
                throw new ObjectNotFoundException();
            }

            return book;
        }

        public async Task<bool> AddBook(IBook book)
        {
            if (BookExists(book.Id))
            {
                throw new ObjectIdAlreadyExistsException();
            }
            _context.Books.Add((Book) book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateBook(IBook book)
        {
            if (!BookExists(book.Id))
            {
                throw new ObjectNotFoundException();
            }
            _context.Update(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                throw new ObjectNotFoundException();
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<IAuthor>> GetAllAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<IEnumerable<IAuthor>> SearchAuthorsByName(string name)
        {
            return await _context.Authors.Where(e => e.Name.Contains(name)).ToListAsync();
        }

        public async Task<IAuthor> GetAuthorById(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(e => e.Id == id);
            if (author == null)
            {
                throw new ObjectNotFoundException();
            }
            return author;
        }

        public async Task<bool> AddAuthor(IAuthor author)
        {
            if (AuthorExists(author.Id))
            {
                throw new ObjectIdAlreadyExistsException();
            }
            _context.Authors.Add((Author)author);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAuthor(IAuthor author)
        {
            if (!AuthorExists(author.Id))
            {
                throw new ObjectNotFoundException();
            }
            _context.Update(author);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                throw new ObjectNotFoundException();
            }
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<IReview>> GetAllReviews()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<IEnumerable<IReview>> GetReviewsByBook(int bookId)
        {
            return await _context.Reviews.Where(e => e.Book.Id == bookId).ToListAsync();
        }

        public async Task<IEnumerable<IReview>> GetReviewsByRating(int rating)
        {
            return await _context.Reviews.Where(e => e.Rating == rating).ToListAsync();
        }

        public async Task<IReview> GetReviewById(int id)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(e => e.Id == id);
            if (review == null)
            {
                throw new ObjectNotFoundException();
            }
            return review;
        }

        public async Task<bool> AddReview(IReview review)
        {
            if (ReviewExists(review.Id))
            {
                throw new ObjectIdAlreadyExistsException();
            }
            _context.Reviews.Add((Review)review);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateReview(IReview review)
        {
            if (!ReviewExists(review.Id))
            {
                throw new ObjectNotFoundException();
            }
            _context.Update(review);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                throw new ObjectNotFoundException();
            }
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
