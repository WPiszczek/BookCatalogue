namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IReviewsDAO
    {
        public IEnumerable<IReview> GetAllBookReviews(int bookId);
        public IReview GetReview(int id);
        public void UpdateReview(IReview author);
        public void DeleteReview(IReview author);
    }
}
