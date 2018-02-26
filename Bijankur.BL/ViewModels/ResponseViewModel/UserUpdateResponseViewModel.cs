using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bijankur.BL.ViewModels.ResponseViewModel
{
    public class UserUpdateResponseViewModel
    {
        public long userid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string contactnumber { get; set; }
        public string birthdate { get; set; }
        public string address { get; set; }
        public string status { get; set; }
        public string usertype { get; set; }
    }
}
