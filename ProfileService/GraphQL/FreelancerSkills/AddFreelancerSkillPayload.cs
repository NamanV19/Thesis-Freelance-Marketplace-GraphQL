using ProfileService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.GraphQL.FreelancerSkills
{
    public record AddFreelancerSkillPayload(FreelancerSkill freelancerSkill);
}
