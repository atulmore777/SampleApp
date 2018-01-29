using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Bijankur.BL.Common.Validation;

namespace Bijankur.BL.ViewModels.RequestViewModel
{
    public class UserRequestViewModel
    {
        [Required(ErrorMessage = "2013$$first name")]
        public string firstname { get; set; }

        [Required(ErrorMessage = "2013$$last name")]
        public string lastname { get; set; }

        [Required(ErrorMessage = "2013$$email")]
        [EmailAddress(ErrorMessage = "2012$$email")]
        public string email { get; set; }

        [Required(ErrorMessage = "2013$$contact number")]
        public string contactnumber { get; set; }

        [Required(ErrorMessage = "2013$$birth date")]
        [BirthDateValidate]
        public DateTime birthdate { get; set; }

        public string address { get; set; }

        public string status { get; set; }

        public string usertype { get; set; }
    }
}
