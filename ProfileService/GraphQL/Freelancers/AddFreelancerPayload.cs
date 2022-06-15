using ProfileService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.GraphQL.Freelancers
{
    public record AddFreelancerPayload(Freelancer freelancer);
}
