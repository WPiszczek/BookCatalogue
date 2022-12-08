namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IAuthorsDAO<Author> where Author : IAuthor
    {
        public Task<IEnumerable<Author>> GetAllAuthors();
        public Task<IEnumerable<Author>> SearchAuthorsByName(string name);
        public Task<Author> GetAuthorById(int id);
        public void AddAuthor(Author author);
        public void UpdateAuthor(Author author);
        public void DeleteAuthor(Author author);
    }
}
