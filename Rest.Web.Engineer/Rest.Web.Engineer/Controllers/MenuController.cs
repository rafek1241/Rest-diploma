using System.Collections.Generic;
using Rest.Web.Engineer.Logic;
using Rest.Web.Engineer.Models;

namespace Rest.Web.Engineer.Controllers
{
    public class MenuController : BaseApiController<MenuLogic>
    {
        public List<Menu> Get()
        {
            return Logic.GetList();
        }
    }
}