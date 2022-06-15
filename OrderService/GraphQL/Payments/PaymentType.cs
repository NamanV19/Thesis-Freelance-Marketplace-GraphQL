using HotChocolate;
using HotChocolate.Types;
using OrderService.Data.Context;
using OrderService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.GraphQL.Payments
{
    public class PaymentType : ObjectType<Payment>
    {
        protected override void Configure(IObjectTypeDescriptor<Payment> descriptor)
        {
            descriptor.Description("Contains information about the payment details of an order");
            descriptor
                .Field(p => p.Order)
                .Description("This is the order to which the payment belongs");
        }
    }

}
