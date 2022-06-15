using CatalogService.Data.Context;
using CatalogService.Data.Entities;
using CatalogService.GraphQL.Catalogs;
using HotChocolate;
using HotChocolate.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(DatabaseContext))]
        public async Task<AddCatalogPayload> AddCatalogAsync(AddCatalogInput input, [ScopedService] DatabaseContext context)
        {
            var catalog = new Catalog
            {
                TypeOfWork = input.typeOfWork,
                TitleOfJob = input.titleOfJob,
                JobDescription = input.jobDescription,
                JobCategory = input.jobCategory,
                ScopeOfWork = input.scopeOfWork,
                EstimatedTime = input.estimatedTime,
                Budget = input.budget,
                Status = input.status,
                dateCreated = input.dateCreated,
                BuyerId = input.BuyerId
            };

            context.Catalogs.Add(catalog);
            await context.SaveChangesAsync();

            return new AddCatalogPayload(catalog);
        }

        [UseDbContext(typeof(DatabaseContext))]
        public async Task<AddCatalogPayload> PutCatalogAsync(Guid id, AddCatalogInput input, [ScopedService] DatabaseContext context)
        {
            var catalogEntity = await context.Catalogs.FindAsync(id);
            if (catalogEntity == null) { throw new Exception($"Incorrect id: {id} specified for Catalog. "); }
            else
            {
                catalogEntity.TypeOfWork = input.typeOfWork;
                catalogEntity.TitleOfJob = input.titleOfJob;
                catalogEntity.JobDescription = input.jobDescription;
                catalogEntity.JobCategory = input.jobCategory;
                catalogEntity.ScopeOfWork = input.scopeOfWork;
                catalogEntity.EstimatedTime = input.estimatedTime;
                catalogEntity.Budget = input.budget;
                catalogEntity.Status = input.status;
                catalogEntity.dateCreated = input.dateCreated;
                catalogEntity.BuyerId = input.BuyerId;

                context.Entry(catalogEntity).State = EntityState.Modified;
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Cannot update Catalog's information");
            }

            return new AddCatalogPayload(catalogEntity);
        }

        [UseDbContext(typeof(DatabaseContext))]
        public async Task<AddCatalogPayload> DeleteCatalog(Guid id, [ScopedService] DatabaseContext context)
        {
            var catalogEntity = await context.Catalogs.FindAsync(id);
            if (catalogEntity == null) { throw new Exception($"Incorrect id: {id} specified for Catalog. "); }
            context.Catalogs.Remove(catalogEntity);

            try { await context.SaveChangesAsync(); }
            catch (Exception exception)
            {
                throw new Exception($"Exception deleting Buyer: {id} Exception: {exception.Message} {exception.InnerException?.Message}");
            }

            return new AddCatalogPayload(catalogEntity);
        }
    }
}
