using Microsoft.EntityFrameworkCore;
using OrderService.Data.Context;
using OrderService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.GraphQL.Repositories
{
    public class ReviewRepository : IAsyncDisposable
    {
        private readonly DatabaseContext context;

        public ReviewRepository(IDbContextFactory<DatabaseContext> contextFactory)
        {
            context = contextFactory.CreateDbContext();
        }

        public Review GetReview(Guid orderId)
        {
            return context.Reviews.FirstOrDefault(p => p.OrderId == orderId);
        }

        public ValueTask DisposeAsync()
        {
            return context.DisposeAsync();
        }
    }
}
