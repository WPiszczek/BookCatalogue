namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IAuthorsRepository
    {
        public Task<IEnumerable<IAuthor>> GetAllAuthors();
        public Task<IEnumerable<IAuthor>> SearchAuthorsByName(string name);
        public Task<IAuthor> GetAuthorById(int id);
        public Task<bool> AddAuthor(IAuthor author);
        public Task<bool> UpdateAuthor(IAuthor author);
        public Task<bool> DeleteAuthor(int id);
    }
}
