using CatalogService.Data.Context;
using CatalogService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CatalogService.GraphQL.Repositories
{
    public class CatalogRepository : IAsyncDisposable
    {
        private readonly DatabaseContext context;
        // private readonly IDbContextFactory<DatabaseContext> context;

        public CatalogRepository(IDbContextFactory<DatabaseContext> contextFactory)
        {
            context = contextFactory.CreateDbContext();
            // context = contextFactory;
        }

        public IQueryable<Catalog> GetCatalogs()
        {
            return context.Catalogs;
        }

        public IQueryable<Catalog> GetCatalogsByBuyer(Guid buyerId)
        {
            return context.Catalogs.Where(catalog => catalog.BuyerId == buyerId);
        }

        public async Task<List<Catalog>> GetCatalogsByBuyerAsync(IReadOnlyList<Guid> keys)
        {
            var catalogs = await context.Catalogs.Where(c => keys.Select(k => k).Contains(c.BuyerId)).ToListAsync(CancellationToken.None);

            return catalogs;
        }

        // With Scoped
        /* public async Task<List<Catalog>> GetCatalogsByBuyerAsync(IReadOnlyList<Guid> keys)
        // public async Task<IQueryable<Catalog>> GetCatalogsByBuyerAsync(Guid buyerId)
        {
            await using (DatabaseContext _context = context.CreateDbContext())
            {
                var catalogs = await _context.Catalogs.Where(c => keys.Select(k => k).ToList().Contains(c.BuyerId)).ToListAsync(CancellationToken.None);

                return catalogs;
                // return  _context.Catalogs.Where(catalog => catalog.BuyerId == buyerId);
            }
        } */

        public Catalog GetCatalogByOrder(Guid catalogId)
        {
            return context.Catalogs.FirstOrDefault(c => c.Id == catalogId);
        }

        public ValueTask DisposeAsync()
        {
            return context.DisposeAsync();
        }
    }
}
