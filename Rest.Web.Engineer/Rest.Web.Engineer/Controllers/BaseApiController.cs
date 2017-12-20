using Rest.Web.Engineer.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Rest.Web.Engineer.Controllers
{
    public abstract class BaseApiController<TLogic, TContext> : ApiController where TContext : IDisposable, new() where TLogic : LogicBase<TContext>
    {
        protected TLogic Logic;

        public BaseApiController()
        {
            Logic = Activator.CreateInstance<TLogic>();
        }

        public BaseApiController(TLogic logic)
        {
            Logic = logic;
        }

    }
}
