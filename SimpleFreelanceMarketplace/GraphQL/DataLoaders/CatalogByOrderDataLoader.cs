using CatalogService.Data.Context;
using CatalogService.Data.Entities;
using GreenDonut;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CatalogService.GraphQL.DataLoaders
{
    public class CatalogByOrderDataLoader : BatchDataLoader<Guid, Catalog>
    {
        private readonly IDbContextFactory<DatabaseContext> _dbContextFactory;

        public CatalogByOrderDataLoader(
            IBatchScheduler batchScheduler,
            IDbContextFactory<DatabaseContext> dbContextFactory, DataLoaderOptions options)
            : base(batchScheduler, options)
        {
            _dbContextFactory = dbContextFactory ??
                throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<Guid, Catalog>> LoadBatchAsync(
            IReadOnlyList<Guid> keys,
            CancellationToken cancellationToken)
        {
            await using DatabaseContext dbContext =
                _dbContextFactory.CreateDbContext();

            /* var catalogs = await dbContext.Catalogs.Where(c => keys.Contains(c.Id)).ToDictionaryAsync(c => c.Id, cancellationToken);
               return catalogs; */

            var catalogs = dbContext.Catalogs.Where(c => keys.Contains(c.Id)).ToList();
            return catalogs.ToDictionary(c => c.Id);
        }
    }
}
