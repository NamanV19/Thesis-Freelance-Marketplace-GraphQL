using HotChocolate.Data;
using Microsoft.EntityFrameworkCore;
using OrderService.Data.Context;
using OrderService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.GraphQL.Repositories
{
    public class OrderRepository : IAsyncDisposable
    {
        private readonly DatabaseContext context;

        public OrderRepository(IDbContextFactory<DatabaseContext> contextFactory)
        {
            context = contextFactory.CreateDbContext();
        }

        public IQueryable<Order> GetOrders()
        {
            return context.Orders;
        }

        public ValueTask DisposeAsync()
        {
            return context.DisposeAsync();
        }
    }
}
