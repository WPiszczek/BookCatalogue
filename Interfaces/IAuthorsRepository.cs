using PiszczekSzpotek.BookCatalogue.Core.Enums;

namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IAuthorsRepository
    {
        public Task<IEnumerable<IAuthor>> GetAuthors(string? name, AuthorStatus? status);
        public Task<IAuthor> GetAuthorById(int id);
        public Task<bool> AddAuthor(IAuthor author);
        public Task<bool> UpdateAuthorImageUrl(int authorId, string imageUrl);
        public Task<bool> UpdateAuthor(IAuthor author);
        public Task<bool> DeleteAuthor(int id);
    }
}
