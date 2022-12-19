namespace PiszczekSzpotek.BookCatalogue.API.Exceptions
{
    public class InvalidAssemblyException : Exception
    {
        public InvalidAssemblyException() { }
        public InvalidAssemblyException(string message): base(message) { }
    }
}
