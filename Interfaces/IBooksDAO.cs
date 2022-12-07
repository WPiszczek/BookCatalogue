namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IBooksDAO
    {
        public IEnumerable<IBook> GetAllBooks();
        public IBook GetBook(int id);
        public void UpdateBook(IBook book);
        public void DeleteBook(IBook book);
    }
}
