using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.GraphQL.Catalogs
{
    public record AddCatalogInput(string typeOfWork, string titleOfJob, string jobDescription, string jobCategory, 
        string scopeOfWork, string estimatedTime, decimal budget, string status, DateTime dateCreated, Guid BuyerId);
}
