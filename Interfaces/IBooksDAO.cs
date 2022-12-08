namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IBooksDAO<Book> where Book : IBook
    {
        public Task<IEnumerable<Book>> GetAllBooks();
        public Task<Book> GetBook(int id);
        public void UpdateBook(Book book);
        public void DeleteBook(Book book);
    }
}
