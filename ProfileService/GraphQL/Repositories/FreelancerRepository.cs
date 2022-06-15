using Microsoft.EntityFrameworkCore;
using ProfileService.Data;
using ProfileService.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProfileService.GraphQL.Repositories
{
    public class FreelancerRepository : IAsyncDisposable
    {
        private readonly DatabaseContext context;

        public FreelancerRepository(IDbContextFactory<DatabaseContext> contextFactory)
        {
            context = contextFactory.CreateDbContext();
        }

        public Freelancer GetFreelancer(Guid freelancerId)
        {
            return context.Freelancers.FirstOrDefault(f => f.Id == freelancerId);
        }

        public async Task<List<Freelancer>> GetFreelancersByIdsAsync(IReadOnlyList<Guid> keys)
        {
            var catalogs = await context.Freelancers.Where(f => keys.Select(k => k).ToList().Contains(f.Id)).ToListAsync(CancellationToken.None);

            return catalogs;
        }

        public ValueTask DisposeAsync()
        {
            return context.DisposeAsync();
        }
    }
}
