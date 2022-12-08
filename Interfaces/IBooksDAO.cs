using PiszczekSzpotek.BookCatalogue.Core.Enums;

namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IBooksDAO<Book> where Book : IBook
    {
        public Task<IEnumerable<Book>> GetAllBooks();
        public Task<IEnumerable<Book>> GetBooksByAuthor(int authorId);
        public Task<IEnumerable<Book>> GetBooksByCategory(BookCategory category);
        public Task<IEnumerable<Book>> GetBooksByLanguage(string language);
        public Task<IEnumerable<Book>> SearchBooksByTitle(string title);
        public Task<Book> GetBookById(int id);
        public void CreateBook(Book book);
        public void UpdateBook(Book book);
        public void DeleteBook(Book book);
    }
}
