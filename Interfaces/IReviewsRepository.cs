namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IReviewsRepository
    {
        public Task<IEnumerable<IReview>> GetReviews(int? bookId, int? rating);
        public Task<IReview> GetReviewById(int id);
        public Task<bool> AddReview(IReview review);
        public Task<bool> UpdateReview(IReview review);
        public Task<bool> DeleteReview(int id);
    }
}
