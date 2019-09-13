using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InclusCommunication.Services.Implementations
{
    public abstract class AbstractRepository
    {
        protected readonly Postgres Db;

        protected AbstractRepository(Postgres db)
        {
            Db = db;
        }
    }
}
