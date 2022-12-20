using PiszczekSzpotek.BookCatalogue.Core.Enums;

namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IBooksRepository
    {
        public Task<IEnumerable<IBook>> GetAllBooks();
        public Task<IEnumerable<IBook>> GetBooksByAuthor(int authorId);
        public Task<IEnumerable<IBook>> GetBooksByCategory(BookCategory category);
        public Task<IEnumerable<IBook>> SearchBooksByTitle(string title);
        public Task<IBook> GetBookById(int id);
        public Task<bool> AddBook(IBook book);
        public Task<bool> UpdateBook(IBook book);
        public Task<bool> DeleteBook(int id);
    }
}
