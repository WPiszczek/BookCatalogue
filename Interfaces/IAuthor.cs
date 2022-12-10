namespace PiszczekSzpotek.BookCatalogue.Interfaces
{
    public interface IAuthor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        //public IEnumerable<IBook> Books { get; set; }
    }
}
