namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IAuthorsRepository
    {
        public Task<IEnumerable<IAuthor>> GetAuthors(string? name);
        public Task<IAuthor> GetAuthorById(int id);
        public Task<bool> AddAuthor(IAuthor author);
        public Task<bool> UpdateAuthorImageUrl(int authorId, string imageUrl);
        public Task<bool> UpdateAuthor(IAuthor author);
        public Task<bool> DeleteAuthor(int id);
    }
}
