using GreenDonut;
using Microsoft.EntityFrameworkCore;
using ProfileService.Data;
using ProfileService.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProfileService.GraphQL.DataLoaders
{
    public class FreelancerByOrderDataLoader : BatchDataLoader<Guid, Freelancer>
    {
        private readonly IDbContextFactory<DatabaseContext> _dbContextFactory;

        public FreelancerByOrderDataLoader(
            IBatchScheduler batchScheduler,
            IDbContextFactory<DatabaseContext> dbContextFactory, DataLoaderOptions options)
            : base(batchScheduler, options)
        {
            _dbContextFactory = dbContextFactory ??
                throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<Guid, Freelancer>> LoadBatchAsync(
            IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken)
        {
            await using DatabaseContext dbContext =
                _dbContextFactory.CreateDbContext();

            /* var freelancers = await dbContext.Freelancers.Where(f => keys.Contains(f.Id)).ToDictionaryAsync(f => f.Id, cancellationToken);
               return freelancers; */

            var freelancers = dbContext.Freelancers.Where(f => keys.Contains(f.Id)).ToList();
            return freelancers.ToDictionary(f => f.Id);
        }
    }
}
