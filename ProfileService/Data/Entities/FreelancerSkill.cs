using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Data.Entities
{
    public class FreelancerSkill
    {
        public Guid Id { get; set; }
        public Guid FreelancerId { get; set; }
        public Guid SkillId { get; set; }
        public virtual Freelancer Freelancer { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
