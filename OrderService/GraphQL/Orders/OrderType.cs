using HotChocolate;
using HotChocolate.Types;
using OrderService.Data.Context;
using OrderService.Data.Entities;
using OrderService.GraphQL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.GraphQL.Orders
{
    public class OrderType : ObjectType<Order>
    {
        protected override void Configure(IObjectTypeDescriptor<Order> descriptor)
        {
            descriptor.Description("Contains information about a job posting and the corresponding freelancer working on it");
            descriptor
                .Field(o => o.Payment)
                .Description("This is the payment details of this order");
            descriptor
                .Field(o => o.Review)
                .Description("This is the review of this order");
        }
    }
}
