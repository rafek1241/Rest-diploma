using Rest.Web.Engineer.Logic;
using Rest.Web.Engineer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Rest.Web.Engineer.Controllers
{
    public class MenuController : BaseApiController<MenuLogic, Entities>
    {
        [HttpGet]
        public List<Menu> GetList()
        {
            return Logic.GetList();
        }
    }
}
