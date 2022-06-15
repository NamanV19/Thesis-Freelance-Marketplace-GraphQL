using CatalogService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.GraphQL.Catalogs
{
    public record AddCatalogPayload(Catalog catalog);
}
