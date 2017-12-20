using Rest.Web.Engineer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest.Web.Engineer.Logic
{
    public class MenuLogic : LogicBase<Entities>
    {
        public List<Menu> GetList()
        {
            return DbContext.Menus.ToList();
        }
    }
}
