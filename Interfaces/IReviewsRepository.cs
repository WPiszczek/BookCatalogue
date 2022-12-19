namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IReviewsRepository
    {
        public Task<IEnumerable<IReview>> GetReviewsByBook(int bookId); 
        public Task<IEnumerable<IReview>> GetReviewsByRating(int rating);
        public Task<IReview> GetReviewById(int id);
        public void AddReview(IReview review);
        public void UpdateReview(IReview review);
        public void DeleteReview(int id);
    }
}
