using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Bijankur.BL.ViewModels.RequestViewModel
{
    public class RoleCreateRequestViewModel
    {
        [Required(ErrorMessage = "101")]
        public string rolename { get; set; }

        [Required(ErrorMessage = "101")]
        public string roledescription { get; set; }

        [IgnoreDataMember]
        public string createdby { get; set; }
    }
}
