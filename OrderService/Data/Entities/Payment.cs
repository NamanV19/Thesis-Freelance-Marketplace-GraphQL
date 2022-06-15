using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Data.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public decimal Price { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
