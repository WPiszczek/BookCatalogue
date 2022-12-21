using Microsoft.EntityFrameworkCore;
using PiszczekSzpotek.BookCatalogue.Interfaces;
using PiszczekSzpotek.BookCatalogue.Core.Exceptions;
using PiszczekSzpotek.BookCatalogue.Core.Enums;
using PiszczekSzpotek.BookCatalogue.SQLiteDatabase.Models;

namespace PiszczekSzpotek.BookCatalogue.SQLiteDatabase
{
    public class SQLiteDAO : IDAO
    {
        //private readonly SQLiteDatabaseContext _context;
        private readonly IDbContextFactory<SQLiteDatabaseContext> _contextFactory;

        public SQLiteDAO()
        {
            _contextFactory = new SQLiteDatabaseContextFactory();
        }

        //public SQLiteDAO(SQLiteDatabaseContext context)
        //{
        //    _context = context;
        //}

        public async Task<IEnumerable<IBook>> GetAllBooks()
        {
            using (var _context = _contextFactory.CreateDbContext())
                return await _context.Books.ToListAsync();
        }

        public async Task<IEnumerable<IBook>> GetBooksByAuthor(int authorId)
        {
            using (var _context = _contextFactory.CreateDbContext())
                return await _context.Books.Where(e => e.Author.Id == authorId).ToListAsync();
        }

        public async Task<IEnumerable<IBook>> GetBooksByCategory(BookCategory category)
        {
            using (var _context = _contextFactory.CreateDbContext())
                return await _context.Books.Where(e => e.Category == category).ToListAsync();
        }

        public async Task<IEnumerable<IBook>> GetBooksByLanguage(string language)
        {
            using (var _context = _contextFactory.CreateDbContext())
                return await _context.Books.Where(e => e.Language == language).ToListAsync();
        }

        public async Task<IEnumerable<IBook>> SearchBooksByTitle(string title)
        {
            using (var _context = _contextFactory.CreateDbContext())
                return await _context.Books.Where(e => e.Title.Contains(title)).ToListAsync();
        }

        public async Task<IBook> GetBookById(int id)
        {
            using (var _context = _contextFactory.CreateDbContext())
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
        }

        public async Task<bool> AddBook(IBook book)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                if (BookExists(book.Id))
                {
                    throw new ObjectIdAlreadyExistsException();
                }
                await _context.Books.AddAsync((Book) book);
                await _context.SaveChangesAsync();
                return true;

            }
        }

        public async Task<bool> UpdateBook(IBook book)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                if (!BookExists(book.Id))
                {
                    throw new ObjectNotFoundException();
                }
                _context.Update(book);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DeleteBook(int id)
        {
            using (var _context = _contextFactory.CreateDbContext())
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
        }

        public async Task<IEnumerable<IAuthor>> GetAllAuthors()
        {
            using (var _context = _contextFactory.CreateDbContext())
                return await _context.Authors.ToListAsync();
        }

        public async Task<IEnumerable<IAuthor>> SearchAuthorsByName(string name)
        {
            using (var _context = _contextFactory.CreateDbContext())
                return await _context.Authors.Where(e => e.Name.Contains(name)).ToListAsync();
        }

        public async Task<IAuthor> GetAuthorById(int id)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                var author = await _context.Authors.FirstOrDefaultAsync(e => e.Id == id);
                if (author == null)
                {
                    throw new ObjectNotFoundException();
                }
                return author;
            }
        }

        public async Task<bool> AddAuthor(IAuthor author)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                if (AuthorExists(author.Id))
                {
                    throw new ObjectIdAlreadyExistsException();
                }
                await _context.Authors.AddAsync((Author)author);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> UpdateAuthor(IAuthor author)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                if (!AuthorExists(author.Id))
                {
                    throw new ObjectNotFoundException();
                }
                _context.Authors.Update((Author)author);
                _context.SaveChanges();
                return true;
            }
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            using (var _context = _contextFactory.CreateDbContext())
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
        }

        public async Task<IEnumerable<IReview>> GetAllReviews()
        {
            using (var _context = _contextFactory.CreateDbContext())
                return await _context.Reviews.ToListAsync();
        }

        public async Task<IEnumerable<IReview>> GetReviewsByBook(int bookId)
        {
            using (var _context = _contextFactory.CreateDbContext())
                return await _context.Reviews.Where(e => e.Book.Id == bookId).ToListAsync();
        }

        public async Task<IEnumerable<IReview>> GetReviewsByRating(int rating)
        {
            using (var _context = _contextFactory.CreateDbContext())
                return await _context.Reviews.Where(e => e.Rating == rating).ToListAsync();
        }

        public async Task<IReview> GetReviewById(int id)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                var review = await _context.Reviews.FirstOrDefaultAsync(e => e.Id == id);
                if (review == null)
                {
                    throw new ObjectNotFoundException();
                }
                return review;

            }
        }

        public async Task<bool> AddReview(IReview review)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                if (ReviewExists(review.Id))
                {
                    throw new ObjectIdAlreadyExistsException();
                }
                _context.Reviews.Add((Review)review);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> UpdateReview(IReview review)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                if (!ReviewExists(review.Id))
                {
                    throw new ObjectNotFoundException();
                }
                _context.Update(review);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> DeleteReview(int id)
        {
            using (var _context = _contextFactory.CreateDbContext())
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
        }

        private bool BookExists(int id)
        {
            using (var _context = _contextFactory.CreateDbContext())
                return _context.Books.Any(e => e.Id == id);
        }

        private bool AuthorExists(int id)
        {
            using (var _context = _contextFactory.CreateDbContext()) 
                return _context.Authors.Any(e => e.Id == id);
        }

        private bool ReviewExists(int id)
        {
            using (var _context = _contextFactory.CreateDbContext())
                return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
