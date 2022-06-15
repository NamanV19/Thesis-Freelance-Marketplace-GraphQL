using HotChocolate;
using HotChocolate.Types;
using ProfileService.Data;
using ProfileService.Data.Context;
using ProfileService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.GraphQL.Freelancers
{
    public class FreelancerType : ObjectType<Freelancer>
    {
        protected override void Configure(IObjectTypeDescriptor<Freelancer> descriptor)
        {
            descriptor.Description("Represents freelancer information");

            descriptor
                .Field(f => f.FreelancerSkills)
                .Description("A list of this freelancer's Id with the corresponding Id of skill");
        }
    }
}
