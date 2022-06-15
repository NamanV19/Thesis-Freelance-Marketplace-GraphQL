using CatalogService.Data.Context;
using CatalogService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenDonut;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CatalogService.GraphQL.DataLoaders
{
    public class CatalogsByBuyerDataLoader : BatchDataLoader<Guid, List<Catalog>>
    {
        private readonly IDbContextFactory<DatabaseContext> _dbContextFactory;

        public CatalogsByBuyerDataLoader(
            IBatchScheduler batchScheduler,
            IDbContextFactory<DatabaseContext> dbContextFactory,
            DataLoaderOptions options)
            : base(batchScheduler, options)
        {
            _dbContextFactory = dbContextFactory ??
               throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<Guid, List<Catalog>>> LoadBatchAsync(
            IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken)
        {
            await using DatabaseContext dbContext = _dbContextFactory.CreateDbContext();

            // var catalogs = await dbContext.Catalogs.Where(c => keys.Contains(c.BuyerId)).ToListAsync();

            var catalogs = dbContext.Catalogs.Where(c => keys.Contains(c.BuyerId)).ToList();

            var result = catalogs.GroupBy(c => c.BuyerId).Select(c => new
            {
                key = c.Key,
                catalogs = c.ToList()
            }).ToDictionary(c => c.key, c => c.catalogs);
            return result;
        }
    }
}
