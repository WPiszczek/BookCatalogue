using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiszczekSzpotek.BookCatalogue.Core.Exceptions
{
    public class InvalidImageDirectoryException : Exception
    {
        public InvalidImageDirectoryException() { }
        public InvalidImageDirectoryException(string message) : base(message) { }
    }
}
