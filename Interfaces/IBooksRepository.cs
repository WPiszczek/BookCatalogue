using PiszczekSzpotek.BookCatalogue.Core.Enums;

namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IBooksRepository
    {
        public Task<IEnumerable<IBook>> GetBooks(string? title, int? authorId, BookCategory? category);
        public Task<IBook> GetBookById(int id);
        public Task<bool> AddBook(IBook book);
        public Task<bool> UpdateBook(IBook book);
        public Task<bool> DeleteBook(int id);
    }
}
