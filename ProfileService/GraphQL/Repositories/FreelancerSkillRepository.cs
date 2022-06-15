using Microsoft.EntityFrameworkCore;
using ProfileService.Data;
using ProfileService.Data.Context;
using ProfileService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.GraphQL.Repositories
{
    public class FreelancerSkillRepository : IAsyncDisposable
    {
        private readonly DatabaseContext context;

        public FreelancerSkillRepository(IDbContextFactory<DatabaseContext> contextFactory)
        {
            context = contextFactory.CreateDbContext();
        }

        public IQueryable<FreelancerSkill> GetFreelancerSkills()
        {
            return context.FreelancerSkills;
        }

        public ValueTask DisposeAsync()
        {
            return context.DisposeAsync();
        }
    }
}
