using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bijankur.DAL.Models
{
    public class ErrorMessage
    {
        public int ErrorMessageId { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public string ErrorLanguage { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
    }
}
