using PiszczekSzpotek.BookCatalogue.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiszczekSzpotek.BookCatalogue.SQLiteDatabase.Models
{
    public class Review : IReview
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime DateAdded { get; set; }
        public IBook Book { get; set; }
        public int Rating { get; set; }
        public string Reviewer { get; set; }
    }
}
