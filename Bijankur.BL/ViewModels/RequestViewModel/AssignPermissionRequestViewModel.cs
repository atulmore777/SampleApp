using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using static BJK.BL.Common.Validation;

namespace BJK.BL.ViewModels.RequestViewModel
{
    public class AssignPermissionRequestViewModel
    {
        [Required(ErrorMessage = "128")]
        [RoleIdValidate(ErrorMessage = "129")]
        public int roleid { get; set; }        
        public List<PermissionRequestViewmodel> lstPermission { get; set; }

        [IgnoreDataMember]
        public string createdby { get; set; }

        [IgnoreDataMember]
        public string updatedby { get; set; }

    }
    public class PermissionRequestViewmodel
    {
        [Required(ErrorMessage = "128")]
        [RoleIdValidate(ErrorMessage = "129")]
        public int roleid { get; set; }

        [Required(ErrorMessage = "128")]
        [PermissionIdValidate(ErrorMessage = "134")]
        public int permissionid { get; set; }
    }
}
