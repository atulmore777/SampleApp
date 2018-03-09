using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJK.BL.ViewModels.ResponseViewModel
{
    public class RoleResponseViewModel
    {
        public int roleid { get; set; }
        public string rolename { get; set; }
        public string roledescription { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
