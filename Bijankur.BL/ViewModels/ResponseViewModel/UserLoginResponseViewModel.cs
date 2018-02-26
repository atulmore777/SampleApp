using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bijankur.BL.ViewModels.ResponseViewModel
{
    public class UserLoginResponseViewModel
    {
        public long userid { get; set; }
        public string email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string token { get; set; }

    }
}
