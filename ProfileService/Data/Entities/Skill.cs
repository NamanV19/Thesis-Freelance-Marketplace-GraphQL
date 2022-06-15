using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Data.Entities
{
    public class Skill
    {
        public Guid Id { get; set; }
        public string SkillName { get; set; }
        public virtual IEnumerable<FreelancerSkill> FreelancerSkills {get; set;}
    }
}
