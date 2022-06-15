using HotChocolate.Types;
using ProfileService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.GraphQL.Buyers
{
    public class BuyerType : ObjectType<Buyer>
    {
        protected override void Configure(IObjectTypeDescriptor<Buyer> descriptor)
        {
            descriptor.Description("Represents buyer information");
        }
    }
}
