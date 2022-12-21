namespace PiszczekSzpotek.BookCatalogue.Core.Exceptions
{
    public class InvalidBookCategoryException : Exception
    {
        public InvalidBookCategoryException() { }
        public InvalidBookCategoryException(string message) : base(message) { }
    }
}
