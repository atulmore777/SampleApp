using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJK.DAL.Models
{
    public class Orders
    {
        public long OrderId { get; set; }
        public string Name { get; set; }
        public long CustomerId { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipArea { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public string ShipVia { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
    }

}
