using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Data.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CatalogId { get; set; }
        public Guid FreelancerId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual Review Review { get; set; }
    }
}
