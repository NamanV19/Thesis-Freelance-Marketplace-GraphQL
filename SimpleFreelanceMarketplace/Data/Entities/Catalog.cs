using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.Data.Entities
{
    public class Catalog
    {
        public Guid Id { get; set; }
        public string TypeOfWork { get; set; } // => [Part time, Full time]
        public string TitleOfJob { get; set; }
        public string JobDescription { get; set; }
        public string JobCategory { get; set; } // => [Mobile development, Graphic design, Web development, Game development
        public string ScopeOfWork { get; set; } // => [Small, Medium, Large]
        public string EstimatedTime { get; set; } // => [<3 months, 3-6 months, >6 months]
        public decimal Budget { get; set; }
        public string Status { get; set; } // => [None, Under Progress, Ended]
        public DateTime dateCreated { get; set; } = DateTime.Now;
        public Guid BuyerId { get; set; }
    }
}
