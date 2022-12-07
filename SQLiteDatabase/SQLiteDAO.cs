using PiszczekSzpotek.BookCatalogue.Interfaces;
using PiszczekSzpotek.BookCatalogue.SQLiteDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiszczekSzpotek.BookCatalogue.SQLiteDatabase
{
    public class SQLiteDAO : IBooksDAO
    {
        SQLiteDatabaseContext _context;

        public SQLiteDAO(SQLiteDatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<IBook> GetAllBooks()
        {
            return _context.Books.ToList<Book>();
        }
        // TODO
        public IBook GetBook(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(IBook book)
        {
            throw new NotImplementedException();
        }


        public void DeleteBook(IBook book)
        {
            throw new NotImplementedException();
        }

    }
}
