using PiszczekSzpotek.BookCatalogue.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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