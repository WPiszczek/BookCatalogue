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
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Title { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Content { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Author { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime DateAdded { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IBook Book { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
