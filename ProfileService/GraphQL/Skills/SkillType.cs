using HotChocolate;
using HotChocolate.Types;
using ProfileService.Data.Context;
using ProfileService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.GraphQL.Skills
{
    public class SkillType : ObjectType<Skill>
    {
        protected override void Configure(IObjectTypeDescriptor<Skill> descriptor)
        {
            descriptor.Description("Represents skill name");

            descriptor
                .Field(s => s.FreelancerSkills)
                .Description("A list of this skill's Id with the corresponding Id of freelancer");
        }
    }
}
