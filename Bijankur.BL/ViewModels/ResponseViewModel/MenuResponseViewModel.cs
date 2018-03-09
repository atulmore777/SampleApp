using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJK.BL.ViewModels.ResponseViewModel
{
    public class MenuResponseViewModel
    {
        public int menuid { get; set; }
        public string icon { get; set; }
        public string menucode { get; set; }
        public string menuname { get; set; }
        public int parentmenuid { get; set; }
        public int sequence { get; set; }
        public int roleId { get; set; }
        public string result { get; set; }
    }
}
