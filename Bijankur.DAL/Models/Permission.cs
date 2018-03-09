using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJK.DAL.Models
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string PermissionCode { get; set; }
        public string PermissionName { get; set; }
        public string Module { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
    }
}
