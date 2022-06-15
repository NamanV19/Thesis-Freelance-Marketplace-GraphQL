using Microsoft.EntityFrameworkCore;
using ProfileService.Data.Context;
using ProfileService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.GraphQL.Repositories
{
    public class SkillRepository : IAsyncDisposable
    {
        private readonly DatabaseContext context;

        public SkillRepository(IDbContextFactory<DatabaseContext> contextFactory)
        {
            context = contextFactory.CreateDbContext();
        }

        public Skill GetSkill(Guid skillId)
        {
            return context.Skills.FirstOrDefault(s => s.Id == skillId);
        }

        public ValueTask DisposeAsync()
        {
            return context.DisposeAsync();
        }
    }
}
