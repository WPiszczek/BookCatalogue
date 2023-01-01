using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiszczekSzpotek.BookCatalogue.Core.Enums
{
    public enum AuthorStatus
    {
        [Description("Active")]
        Active = 0,
        [Description("Dead")]
        Dead = 1,
        [Description("Retired")]
        Retired = 2
    }
}
