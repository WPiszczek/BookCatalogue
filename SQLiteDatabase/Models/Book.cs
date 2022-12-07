﻿using PiszczekSzpotek.BookCatalogue.Core.Enums;
using PiszczekSzpotek.BookCatalogue.Interfaces;

namespace PiszczekSzpotek.BookCatalogue.SQLiteDatabase.Models
{
    public class Book : IBook
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public IAuthor Author { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Language { get; set; }
        public string? Description { get; set; }
        public string PhotoUrl { get; set; }
        public BookCategory Category { get; set; }
        public IEnumerable<IReview> Reviews { get; set; }
    }
}
