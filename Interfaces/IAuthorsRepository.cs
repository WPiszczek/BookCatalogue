namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IAuthorsRepository
    {
        public Task<IEnumerable<IAuthor>> GetAllAuthors();
        public Task<IEnumerable<IAuthor>> SearchAuthorsByName(string name);
        public Task<IAuthor> GetAuthorById(int id);
        public void AddAuthor(IAuthor author);
        public void UpdateAuthor(IAuthor author);
        public void DeleteAuthor(int id);
    }
}
