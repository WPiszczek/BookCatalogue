namespace PiszczekSzpotek.BookCatalogue.API.Exceptions
{
    public class AssemblyNameNotSetException : Exception
    {
        public AssemblyNameNotSetException() { }
        public AssemblyNameNotSetException(string message) : base(message) { }
    }
}
