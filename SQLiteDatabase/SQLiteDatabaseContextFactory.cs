using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiszczekSzpotek.BookCatalogue.SQLiteDatabase
{
    public class SQLiteDatabaseContextFactory : IDbContextFactory<SQLiteDatabaseContext>
    {

        public SQLiteDatabaseContext CreateDbContext()
        {
            return new SQLiteDatabaseContext();
        }
    }
}
