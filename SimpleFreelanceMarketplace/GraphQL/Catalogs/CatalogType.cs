using CatalogService.Data.Entities;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.GraphQL.Catalogs
{
    public class CatalogType : ObjectType<Catalog>
    {
        protected override void Configure(IObjectTypeDescriptor<Catalog> descriptor)
        {
            descriptor.Description("Represents job posted by buyer");
        }

    }
}
