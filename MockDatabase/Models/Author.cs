﻿using PiszczekSzpotek.BookCatalogue.Core.Enums;
using PiszczekSzpotek.BookCatalogue.Interfaces;

namespace PiszczekSzpotek.BookCatalogue.MockDatabase.Models
{
    public class Author : IAuthor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public AuthorStatus Status { get; set; }
        public string? ImageUrl { get; set; }
        public IEnumerable<IBook> Books { get; set; }
    }
}