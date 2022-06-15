using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.GraphQL.Freelancers
{
    public record AddFreelancerInput(string firstName, string lastName, string email, string telephoneNumber, string location,
        string type, string password, string CV, string linkToPortfolio, string educationalInstitution, string yearsOfExperience);
}
