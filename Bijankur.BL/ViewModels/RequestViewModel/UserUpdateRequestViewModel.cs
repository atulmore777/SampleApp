using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static BJK.BL.Common.Validation;

namespace BJK.BL.ViewModels.RequestViewModel
{
    public class UserUpdateRequestViewModel
    {
        [Required(ErrorMessage = "119")]
        [UserIdValidate(ErrorMessage = "118")]
        public long userid { get; set; }

        [Required(ErrorMessage = "101")]
        public string firstname { get; set; }

        [Required(ErrorMessage = "102")]
        public string lastname { get; set; }

        [Required(ErrorMessage = "103")]
        [EmailAddress(ErrorMessage = "107")]
        public string email { get; set; }

        [Required(ErrorMessage = "111")]
        public string password { get; set; }

        [Required(ErrorMessage = "104")]
        public string contactnumber { get; set; }

        [Required(ErrorMessage = "105")]
        [BirthDateValidate]
        public string birthdate { get; set; }

        public string address { get; set; }

        [UserStatusValidate(ErrorMessage = "108")]
        public string status { get; set; }

        [UserTypeValidate(ErrorMessage = "109")]
        public string usertype { get; set; }
    }
}
