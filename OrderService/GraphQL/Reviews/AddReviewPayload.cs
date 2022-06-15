using OrderService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.GraphQL.Reviews
{
    public record AddReviewPayload(Review review);
}
