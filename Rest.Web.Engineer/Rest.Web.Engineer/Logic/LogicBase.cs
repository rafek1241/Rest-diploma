using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Rest.Web.Engineer.Models;

namespace Rest.Web.Engineer.Logic
{
    public abstract class LogicBase
    {
        protected readonly Entities DbContext;

        protected HttpRequestMessage Request => (HttpRequestMessage)HttpContext.Current.Items["MS_HttpRequestMessage"];

        protected LogicBase()
        {
            DbContext = new Entities();
        }

        protected LogicBase(Entities context)
        {
            DbContext = context;
        }
    }
}
