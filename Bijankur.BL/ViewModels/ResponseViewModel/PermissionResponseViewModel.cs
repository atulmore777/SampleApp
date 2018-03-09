using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJK.BL.ViewModels.ResponseViewModel
{
    public class PermissionResponseViewModel
    {
        public string PermissionCode { get; set; }
        public string PermissionName { get; set; }
        public string Module { get; set; }
        public int RoleId { get; set; }
        public string Result { get; set; }
    }
}
