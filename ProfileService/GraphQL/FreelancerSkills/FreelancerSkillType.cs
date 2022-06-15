using HotChocolate;
using HotChocolate.Types;
using ProfileService.Data;
using ProfileService.Data.Context;
using ProfileService.Data.Entities;
using ProfileService.GraphQL.DataLoaders;
using ProfileService.GraphQL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProfileService.GraphQL.FreelancerSkills
{
    public class FreelancerSkillType : ObjectType<FreelancerSkill>
    {
        protected override void Configure(IObjectTypeDescriptor<FreelancerSkill> descriptor)
        {
            descriptor.Description("Contains FreelancerId and SkillId");

            descriptor
                .Field(f => f.Freelancer)
                .Description("Represents the freelancer's information");

            descriptor
                .Field(f => f.Skill)
                .Description("Represents the skill of the freelancer");
        }
    }
} 

