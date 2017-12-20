using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest.Web.Engineer.Logic
{
    public abstract class LogicBase<TContext> where TContext : IDisposable, new()
    {
        protected readonly TContext DbContext;

        protected LogicBase()
        {
            DbContext = new TContext();
        }

        protected LogicBase(TContext context)
        {
            DbContext = context;
        }
    }
}
