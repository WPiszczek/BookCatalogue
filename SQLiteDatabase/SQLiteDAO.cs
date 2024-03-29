﻿using Microsoft.EntityFrameworkCore;
using PiszczekSzpotek.BookCatalogue.Interfaces;
using PiszczekSzpotek.BookCatalogue.Core.Exceptions;
using PiszczekSzpotek.BookCatalogue.Core.Enums;
using PiszczekSzpotek.BookCatalogue.SQLiteDatabase.Models;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace PiszczekSzpotek.BookCatalogue.SQLiteDatabase
{
    public class SQLiteDAO : IDAO
    {

        private readonly IDbContextFactory<SQLiteDatabaseContext> _contextFactory;

        public SQLiteDAO()
        {
            _contextFactory = new SQLiteDatabaseContextFactory();
        }

        // BooksRepository
        public async Task<IEnumerable<IBook>> GetBooks(
            string? title = null,
            int? authorId = null,
            BookCategory? category = null)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                return await _context.Books
                    .Where(e =>
                        (title == null || e.Title.ToLower().Contains(title.ToLower()))
                        && (authorId == null || e.Author.Id == authorId)
                        && (category == null || e.Category == category)
                    )
                    .Include(e => e.Author)
                    .Include(e => e.Author.Books)
                    .Include(e => e.Reviews)
                    .ToListAsync();                
            }
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
                    .Include(e => e.Author)
                    .Include(e => e.Author.Books)
                    .Include(e => e.Reviews)
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

                await _context.Books.AddAsync((Book)book);
                await _context.SaveChangesAsync();
                return true;

            }
        }

        public async Task<bool> UpdateBookImageUrl(int id, string imageUrl)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                if (!BookExists(id))
                {
                    throw new ObjectNotFoundException();
                }

                var book = await _context.Books
                    .FirstOrDefaultAsync(e => e.Id == id);
                _context.Books.Attach(book);
                book.ImageUrl = imageUrl;
                _context.Entry(book).Property(x => x.ImageUrl).IsModified = true;

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
                _context.Entry(book).Property(x => x.AverageRating).IsModified = false;
                _context.Entry(book).Property(x => x.ImageUrl).IsModified = false;
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
                DeleteImage("books", book.ImageUrl);
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        // AuthorsRepository
        public async Task<IEnumerable<IAuthor>> GetAuthors(string? name=null, AuthorStatus? status=null)
        {
            using (var _context = _contextFactory.CreateDbContext())
                return await _context.Authors
                    .Where(e =>
                        (name == null || e.Name.ToLower().Contains(name.ToLower()))
                        && (status == null || e.Status == status)
                    )
                    .Include(e => e.Books)
                    .ToListAsync();
        }

        public async Task<IAuthor> GetAuthorById(int id)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                var author = await _context.Authors
                    .Include(e => e.Books)
                    .FirstOrDefaultAsync(e => e.Id == id);
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

        public async Task<bool> UpdateAuthorImageUrl(int id, string imageUrl)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                if (!AuthorExists(id))
                {
                    throw new ObjectNotFoundException();
                }

                var author = await _context.Authors
                    .FirstOrDefaultAsync(e => e.Id == id);
                _context.Authors.Attach(author);
                author.ImageUrl = imageUrl;
                _context.Entry(author).Property(x => x.ImageUrl).IsModified = true;

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
                _context.Entry(author).Property(x => x.AverageRating).IsModified = false;
                _context.Entry(author).Property(x => x.ImageUrl).IsModified = false;
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
                DeleteImage("authors", author.ImageUrl);
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        // ReviewsRepository
        public async Task<IEnumerable<IReview>> GetReviews(int? bookId=null, string? search=null)
        {
            using (var _context = _contextFactory.CreateDbContext())
                return await _context.Reviews
                    .Where(e =>
                        (bookId == null || e.Book.Id == bookId)
                        && (search == null || e.Title.ToLower().Contains(search.ToLower()) || e.Content.ToLower().Contains(search.ToLower()))
                    )
                    .Include(e => e.Book)
                    .Include(e => e.Book.Author)
                    .ToListAsync();
        }

        public async Task<IReview> GetReviewById(int id)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                var review = await _context.Reviews
                    .Include(e => e.Book)
                    .Include(e => e.Book.Author)
                    .FirstOrDefaultAsync(e => e.Id == id);
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

                UpdateBookAverageRating(review.BookId);
                UpdateAuthorAverageRating(review.BookId);

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
                _context.Entry(review).Property(x => x.DateAdded).IsModified = false;
                await _context.SaveChangesAsync();

                UpdateBookAverageRating(review.BookId);
                UpdateAuthorAverageRating(review.BookId);

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

                UpdateBookAverageRating(review.BookId);
                UpdateAuthorAverageRating(review.BookId);

                return true;
            }
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
            } catch (FileNotFoundException) { }
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

        private async void UpdateBookAverageRating(int bookId)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                var book = await _context.Books
                    .FirstOrDefaultAsync(e => e.Id == bookId);
                _context.Books.Attach(book);
                try
                {
                    book.AverageRating = _context.Reviews
                        .Where(e => e.BookId == bookId)
                        .Average(e => e.Rating);

                } catch (Exception ex)
                {
                    book.AverageRating = null;
                }
                _context.Entry(book).Property(x => x.AverageRating).IsModified = true;
                await _context.SaveChangesAsync();
            }
        }

        private async void UpdateAuthorAverageRating(int bookId)
        {
            using (var _context = _contextFactory.CreateDbContext())
            {
                var book = await _context.Books
                    .FirstOrDefaultAsync(e => e.Id == bookId);
                var author = await _context.Authors
                    .FirstOrDefaultAsync(e => e.Id == book.AuthorId);
                _context.Authors.Attach(author);
                try
                {
                    author.AverageRating = _context.Books
                        .Where(e => e.AuthorId == author.Id)
                        .Average(e => e.AverageRating);
                }
                catch (Exception ex)
                {
                    author.AverageRating = null;
                }
                _context.Entry(author).Property(x => x.AverageRating).IsModified = true;
                await _context.SaveChangesAsync();
            }
        }

        private string GetPath(string directory, string name)
        {
            directory = GetImageDirectory(directory);
            return Path.Combine("..", "ImagesSQLite", directory, name);
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
