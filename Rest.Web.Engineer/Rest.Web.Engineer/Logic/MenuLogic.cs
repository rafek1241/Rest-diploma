using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Rest.Web.Engineer.Models;

namespace Rest.Web.Engineer.Logic
{
    public class MenuLogic : LogicBase
    {

        public List<Menu> GetList()
        {
            return DbContext.Menus.ToList();
        }
    }
}