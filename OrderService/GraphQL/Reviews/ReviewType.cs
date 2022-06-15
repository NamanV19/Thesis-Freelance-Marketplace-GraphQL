using HotChocolate;
using HotChocolate.Types;
using OrderService.Data.Context;
using OrderService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.GraphQL.Reviews
{
    public class ReviewType : ObjectType<Review>
    {
        protected override void Configure(IObjectTypeDescriptor<Review> descriptor)
        {
            descriptor.Description("Contains information about the review of an order");
            descriptor
                .Field(p => p.Order)
                .Description("This is the order to which the review belongs");
        }
    }
}
