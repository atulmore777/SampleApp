using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJK.BL.ViewModels.ResponseViewModel
{
    public class AssignPermissionResponseViewModel
    {
        public int roleid { get; set; }
        public List<PermissionResponseViewModel> lstPermission { get; set; }
    }
}
