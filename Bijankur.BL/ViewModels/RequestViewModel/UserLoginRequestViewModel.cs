using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BJK.BL.ViewModels.RequestViewModel
{
    public class UserLoginRequestViewModel
    {
        [Required(ErrorMessage = "113")]
        public string email { get; set; }

        [Required(ErrorMessage = "114")]
        public string password { get; set; }
    }
}
