using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DBContextFactory
    {
        public static EntityContext GetDBContext()
        {
            ThreadLocal<EntityContext> localCtx = new ThreadLocal<EntityContext>(() => new EntityContext());
            return localCtx.Value;
        }
    }
}
