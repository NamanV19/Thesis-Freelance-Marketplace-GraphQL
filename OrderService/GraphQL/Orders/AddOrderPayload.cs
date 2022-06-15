using OrderService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.GraphQL.Orders
{
    public record AddOrderPayload(Order order);
}
