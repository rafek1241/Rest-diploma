﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest.Web.Engineer.Models
{
    public class Menu
    {
        public long MenuId { get; set; }
        public string Title { get; set; }
        public string Href { get; set; }
        public string Icon { get; set; }
    }
}
