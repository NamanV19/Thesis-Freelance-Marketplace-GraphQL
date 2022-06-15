using HotChocolate;
using Microsoft.EntityFrameworkCore;
using OrderService.Data.Context;
using OrderService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.GraphQL.Repositories
{
    public class PaymentRepository : IAsyncDisposable
    {
        private readonly DatabaseContext context;

        public PaymentRepository(IDbContextFactory<DatabaseContext> contextFactory)
        {
            context = contextFactory.CreateDbContext();
        }

        public Payment GetPayment(Guid orderId)
        {
            return context.Payments.FirstOrDefault(p => p.OrderId == orderId);
        }

        public ValueTask DisposeAsync()
        {
            return context.DisposeAsync();
        }
    }
}
