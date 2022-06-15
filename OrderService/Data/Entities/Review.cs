using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Data.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
