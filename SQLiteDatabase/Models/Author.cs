using PiszczekSzpotek.BookCatalogue.Interfaces;

namespace PiszczekSzpotek.BookCatalogue.SQLiteDatabase.Models
{
    public class Author : IAuthor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public IEnumerable<IBook> Books { get; set; }
    }
}