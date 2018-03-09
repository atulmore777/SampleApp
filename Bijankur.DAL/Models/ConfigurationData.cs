using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJK.DAL.Models
{
    public class ConfigurationData
    {
        public int ConfigId { get; set; }
        public string GroupKey { get; set; }
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
        public string Description { get; set; }
        public bool IsMobileAccess { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
    }
}
