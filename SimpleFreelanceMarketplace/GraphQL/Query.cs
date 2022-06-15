using CatalogService.Data.Context;
using CatalogService.Data.Entities;
using CatalogService.GraphQL.DataLoaders;
using CatalogService.GraphQL.Repositories;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CatalogService.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(DatabaseContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Catalog> GetCatalogs([ScopedService] DatabaseContext context)
        {
            return context.Catalogs;
        }

        // Via repository 
        [UseFiltering]
        [UseSorting]
        public IQueryable<Catalog> GetCatalogsByBuyer([Service] CatalogRepository catalogRepository, Guid buyerId)
        {
            return catalogRepository.GetCatalogsByBuyer(buyerId);
        }

        [UseFiltering]
        [UseSorting]
        public Catalog GetCatalogByOrder([Service] CatalogRepository catalogRepository, Guid catalogId)
        {
            return catalogRepository.GetCatalogByOrder(catalogId);
        }

        // With CatalogsByBuyerDataLoader
        /* [UseFiltering]
        [UseSorting]
        public Task<List<Catalog>> GetCatalogsByBuyer(CatalogsByBuyerDataLoader dataLoader, Guid buyerId)
        {
            return dataLoader.LoadAsync(buyerId, CancellationToken.None);
        } */

        // With CatalogsByBuyerDataLoader - Async
        /* [UseFiltering]
        [UseSorting]
        public async Task<List<Catalog>> GetCatalogsByBuyer(CatalogsByBuyerDataLoader dataLoader, Guid buyerId)
            => await dataLoader.LoadAsync(buyerId, CancellationToken.None); */


        // With CatalogByOrderDataLoader
        /* [UseFiltering]
        [UseSorting]
        public Task<Catalog> GetCatalogByOrder(CatalogByOrderDataLoader dataLoader, Guid catalogId)
        {
            Task<Catalog> catalog = dataLoader.LoadAsync(catalogId, CancellationToken.None);
            return catalog;
        } */
    }
}
