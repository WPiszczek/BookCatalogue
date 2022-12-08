namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IReviewsDAO<Review> where Review : IReview
    {
        public Task<IEnumerable<Review>> GetReviewsByBook(int bookId); 
        public Task<IEnumerable<Review>> GetReviewsByRating(int rating);
        public Task<Review> GetReviewById(int id);
        public void AddReview(Review review);
        public void UpdateReview(Review review);
        public void DeleteReview(Review review);
    }
}
