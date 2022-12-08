using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiszczekSzpotek.BookCatalogue.SQLiteDatabase.Exceptions
{
    public class ContextIsNullException : Exception
    {
        public ContextIsNullException() { }
        public ContextIsNullException(string message) : base(message) { }
    }
}
