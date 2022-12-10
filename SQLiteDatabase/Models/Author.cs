using PiszczekSzpotek.BookCatalogue.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace PiszczekSzpotek.BookCatalogue.SQLiteDatabase.Models
{
    [Table("Authors")]
    public class Author : IAuthor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}