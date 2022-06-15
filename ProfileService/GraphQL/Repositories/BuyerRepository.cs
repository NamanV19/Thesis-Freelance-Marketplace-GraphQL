using Microsoft.EntityFrameworkCore;
using ProfileService.Data;
using ProfileService.Data.Context;
using ProfileService.GraphQL.Buyers;
using ProfileService.GraphQL.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.GraphQL.Repositories
{
    public class BuyerRepository : IAsyncDisposable
    {
        private readonly DatabaseContext context;

        public BuyerRepository(IDbContextFactory<DatabaseContext> contextFactory)
        {
            context = contextFactory.CreateDbContext();
        }

        public string SimpleLogin(AddLoginInput input)
        {
            var validate = false;

            if (input.type == "Buyer")
            {
                validate = context.Buyers.Any(buyer => buyer.Email == input.email && buyer.Password == input.password);
            }
            else if (input.type == "Freelancer")
            {
                validate = context.Freelancers.Any(freelancer => freelancer.Email == input.email && freelancer.Password == input.password);
            }

            if (validate == true)
            {
                return "Successful login";
            }
            else
            {
                return "This user does not exist";
            }
        }

        public async Task<AddBuyerPayload> PutBuyerAsync(Guid id, AddBuyerInput input)
        {
            var buyerEntity = await context.Buyers.FindAsync(id);
            if (buyerEntity == null) { throw new Exception($"Incorrect id: {id} specified for Buyer. "); }
            else
            {
                buyerEntity.FirstName = input.firstName;
                buyerEntity.LastName = input.lastName;
                buyerEntity.Email = input.email;
                buyerEntity.TelephoneNumber = input.telephoneNumber;
                buyerEntity.Location = input.location;
                buyerEntity.Type = input.type;
                buyerEntity.Password = input.password;

                context.Entry(buyerEntity).State = EntityState.Modified;
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Cannot update Buyer's information");
            }

            return new AddBuyerPayload(buyerEntity);
        }

        public IQueryable<Buyer> GetBuyers()
        {
            return context.Buyers;
        } 

        public ValueTask DisposeAsync()
        {
            return context.DisposeAsync();
        }
    }
}
