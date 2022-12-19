using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiszczekSzpotek.BookCatalogue.Core.Exceptions
{
    public class ObjectIdAlreadyExistsException : Exception
    {
        public ObjectIdAlreadyExistsException() { }
        public ObjectIdAlreadyExistsException(string message) : base(message) { }
    }
}
