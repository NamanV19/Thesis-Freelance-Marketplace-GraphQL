using HotChocolate;
using HotChocolate.Data;
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

namespace ProfileService.GraphQL
{
    public class Query
    {
        // Via repository
        [UseFiltering]
        [UseSorting]
        public IQueryable<Buyer> GetBuyers([Service] BuyerRepository buyerRepository)
        {
            return buyerRepository.GetBuyers();
        }

        /* [UseDbContext(typeof(DatabaseContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Buyer> GetBuyers([ScopedService] DatabaseContext context)
        {
            return context.Buyers;
        } */

        [UseDbContext(typeof(DatabaseContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Freelancer> GetFreelancers([ScopedService] DatabaseContext context)
        {
            return context.Freelancers;
        }

        [UseDbContext(typeof(DatabaseContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Skill> GetSkills([ScopedService] DatabaseContext context)
        {
            return context.Skills;
        }

        public IQueryable<FreelancerSkill> GetFreelancerSkills([Service] FreelancerSkillRepository freelancerSkillRepository)
        {
            return freelancerSkillRepository.GetFreelancerSkills();
        }

        [UseFiltering]
        [UseSorting]
        public Freelancer GetFreelancerByOrder([Service] FreelancerRepository freelancerRepository, Guid freelancerId)
        {
            return freelancerRepository.GetFreelancer(freelancerId);
        }

        // With FreelancerByOrderDataLoader
        /* [UseFiltering]
        [UseSorting]
        public Task<Freelancer> GetFreelancerByOrder(FreelancerByOrderDataLoader dataLoader, Guid freelancerId)
        {
            Task<Freelancer> freelancer = dataLoader.LoadAsync(freelancerId, CancellationToken.None);
            return freelancer;
        } */
    }
}
