using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.GraphQL.Buyers
{
    public record AddBuyerInput(string firstName, string lastName, string email, string telephoneNumber, string location, 
        string type, string password);
}
