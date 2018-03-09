using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJK.DAL.Models
{
    public class RoleMenu
    {
        public int RoleMenuId { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
    }
}
