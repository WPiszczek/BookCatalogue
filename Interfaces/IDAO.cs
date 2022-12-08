namespace PiszczekSzpotek.BookCatalogue.Interfaces

{
    public interface IDAO<Book, Author, Review> 
        : IAuthorsDAO<Author>, IBooksDAO<Book>, IReviewsDAO<Review> 
        where Book : IBook where Author : IAuthor where Review : IReview
    {
    }
}
