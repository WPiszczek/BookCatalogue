namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IReviewsRepository
    {
        public Task<IEnumerable<IReview>> GetAllReviews();
        public Task<IEnumerable<IReview>> GetReviewsByBook(int bookId); 
        public Task<IEnumerable<IReview>> GetReviewsByRating(int rating);
        public Task<IReview> GetReviewById(int id);
        public Task<bool> AddReview(IReview review);
        public Task<bool> UpdateReview(IReview review);
        public Task<bool> DeleteReview(int id);
    }
}
