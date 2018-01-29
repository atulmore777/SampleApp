using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bijankur.DAL.Models
{
    public class ApplicationInfo
    {
        public int ApplicationInfoId { get; set; }
        public string AppName { get; set; }
        public string AppToken { get; set; }
        public int RoleId { get; set; }
        public DateTime? CreatedOn { get; set; }       
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
    }
}
