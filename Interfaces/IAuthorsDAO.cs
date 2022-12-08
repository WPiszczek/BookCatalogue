namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IAuthorsDAO<Author> where Author : IAuthor
    {
        public Task<IEnumerable<Author>> GetAllAuthors();
        public Task<Author> GetAuthor(int id);
        public void UpdateAuthor(Author author);
        public void DeleteAuthor(Author author);
    }
}
