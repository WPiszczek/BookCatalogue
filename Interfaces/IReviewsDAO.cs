namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IReviewsDAO<Review> where Review : IReview
    {
        public Task<IEnumerable<Review>> GetAllBookReviews(int bookId); 
        public Task<Review> GetReview(int id);
        public void UpdateReview(Review author);
        public void DeleteReview(Review author);
    }
}
