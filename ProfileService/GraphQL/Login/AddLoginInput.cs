using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.GraphQL.Login
{
    public record AddLoginInput(string email, string password, string type);
}
