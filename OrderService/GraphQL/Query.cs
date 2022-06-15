using HotChocolate;
using HotChocolate.Data;
using OrderService.Data.Context;
using OrderService.Data.Entities;
using OrderService.GraphQL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.GraphQL
{
    public class Query
    {
        // Via repository
        [UseFiltering]
        [UseSorting]
        public IQueryable<Order> GetOrders([Service] OrderRepository orderRepository)
        {
            return orderRepository.GetOrders();
        }

        [UseDbContext(typeof(DatabaseContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Payment> GetPayments([ScopedService] DatabaseContext context)
        {
            return context.Payments;
        }

        [UseDbContext(typeof(DatabaseContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Review> GetReviews([ScopedService] DatabaseContext context)
        {
            return context.Reviews;
        }
    }
}
