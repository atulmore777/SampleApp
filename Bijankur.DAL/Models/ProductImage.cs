using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bijankur.DAL.Models
{
    public class ProductImage
    {
        public long ProductImageId { get; set; }
        public long ProductId { get; set; }
        public string Type { get; set; }
        public byte[] Image { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
    }
}
