namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IAuthorsDAO
    {
        public IEnumerable<IAuthor> GetAllAuthors();
        public IAuthor GetAuthor(int id);
        public void UpdateAuthor(IAuthor author);
        public void DeleteAuthor(IAuthor author);
    }
}
