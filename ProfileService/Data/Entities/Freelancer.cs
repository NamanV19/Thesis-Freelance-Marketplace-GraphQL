using ProfileService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Data
{
    public class Freelancer : User
    {
        public string CV { get; set; }
        public string LinkToPortfolio { get; set; }
        public string EducationalInstitution { get; set; }
        public string YearsOfExperience { get; set; }
        public virtual IEnumerable<FreelancerSkill> FreelancerSkills { get; set; }
    }
}
