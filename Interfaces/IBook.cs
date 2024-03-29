﻿using PiszczekSzpotek.BookCatalogue.Core.Enums;

namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IAuthor Author { get; set; }
        public int AuthorId { get; set; }
        public int ReleaseYear { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public BookCategory Category { get; set; }
        public double? AverageRating { get; set; }
        public IEnumerable<IReview> Reviews { get; set; }
    }
}
