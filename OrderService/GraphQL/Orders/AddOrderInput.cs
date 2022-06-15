using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.GraphQL.Orders
{
    public record AddOrderInput(Guid catalogId, Guid freelancerId, DateTime startDate, DateTime endDate);
}
