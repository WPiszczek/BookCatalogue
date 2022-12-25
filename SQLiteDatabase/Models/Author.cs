using PiszczekSzpotek.BookCatalogue.Core.Enums;
using PiszczekSzpotek.BookCatalogue.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace PiszczekSzpotek.BookCatalogue.SQLiteDatabase.Models
{
    [Table("Authors")]
    public class Author : IAuthor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime BirthDate { get; set; }

        [NotMapped]
        private DateTime? _DeathDate;
        public DateTime? DeathDate { 
            get { return _DeathDate; } 
            set { _DeathDate = value == DateTime.MinValue ? null : value; } 
        }
        public AuthorStatus Status { get; set; }
        public string? ImageUrl { get; set; }
        public IEnumerable<Book> Books { get; set; }
        IEnumerable<IBook> IAuthor.Books
        {
            get => Books;
            set
            {
                Books = value as IEnumerable<Book>; 
            }
        }
    }
}