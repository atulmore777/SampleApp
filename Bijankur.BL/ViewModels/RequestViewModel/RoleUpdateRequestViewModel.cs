using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using static BJK.BL.Common.Validation;

namespace BJK.BL.ViewModels.RequestViewModel
{
    public class RoleUpdateRequestViewModel
    {
        [Required(ErrorMessage = "128")]
        [RoleIdValidate(ErrorMessage = "129")]
        public int roleid { get; set; }

        [Required(ErrorMessage = "126")]
        [RoleNameForUpdateValidation(ErrorMessage = "130")]
        public string rolename { get; set; }

        [Required(ErrorMessage = "127")]
        public string roledescription { get; set; }

        [IgnoreDataMember]
        public string updatedby { get; set; }
    }
}
