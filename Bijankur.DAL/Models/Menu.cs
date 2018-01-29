using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bijankur.DAL.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string Icon { get; set; }
        public string MenuCode { get; set; }
        public string MenuName { get; set; }
        public int ParentMenuId { get; set; }
        public int Sequence { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }

    }
}
