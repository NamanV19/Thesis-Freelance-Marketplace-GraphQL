using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.GraphQL.Reviews
{
    public record AddReviewInput(int stars, string comment, Guid orderId);
}
