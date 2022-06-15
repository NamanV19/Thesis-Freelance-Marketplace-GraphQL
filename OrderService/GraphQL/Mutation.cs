using HotChocolate;
using HotChocolate.Data;
using Microsoft.EntityFrameworkCore;
using OrderService.Data.Context;
using OrderService.Data.Entities;
using OrderService.GraphQL.Orders;
using OrderService.GraphQL.Payments;
using OrderService.GraphQL.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(DatabaseContext))]
        public async Task<AddOrderPayload> AddOrderAsync(AddOrderInput input, [ScopedService] DatabaseContext context)
        {
            var order = new Order
            {
                CatalogId = input.catalogId,
                FreelancerId = input.freelancerId
            };

            context.Orders.Add(order);
            await context.SaveChangesAsync();

            return new AddOrderPayload(order);
        }

        [UseDbContext(typeof(DatabaseContext))]
        public async Task<AddOrderPayload> PutOrderAsync(Guid id, AddOrderInput input, [ScopedService] DatabaseContext context)
        {
            var orderEntity = await context.Orders.FindAsync(id);
            if (orderEntity == null) { throw new Exception($"Incorrect id: {id} specified for Order. "); }
            else
            {
                orderEntity.CatalogId = input.catalogId;
                orderEntity.FreelancerId = input.freelancerId;

                context.Entry(orderEntity).State = EntityState.Modified;
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Cannot update Order information");
            }

            return new AddOrderPayload(orderEntity);
        }

        [UseDbContext(typeof(DatabaseContext))]
        public async Task<AddOrderPayload> DeleteOrder(Guid id, [ScopedService] DatabaseContext context)
        {
            var orderEntity = await context.Orders.FindAsync(id);
            if (orderEntity == null) { throw new Exception($"Incorrect id: {id} specified for Order. "); }
            context.Orders.Remove(orderEntity);

            try { await context.SaveChangesAsync(); }
            catch (Exception exception)
            {
                throw new Exception($"Exception deleting Order: {id} Exception: {exception.Message} {exception.InnerException?.Message}");
            }

            return new AddOrderPayload(orderEntity);
        }

        [UseDbContext(typeof(DatabaseContext))]
        public async Task<AddPaymentPayload> AddPaymentAsync(AddPaymentInput input, [ScopedService] DatabaseContext context)
        {
            var payment = new Payment
            {
                PaymentMethod = input.paymentMethod,
                TransactionDate = input.transactionDate,
                Price = input.price,
                OrderId = input.orderId
            };

            context.Payments.Add(payment);
            await context.SaveChangesAsync();

            return new AddPaymentPayload(payment);
        }

        [UseDbContext(typeof(DatabaseContext))]
        public async Task<AddPaymentPayload> PutPaymentAsync(Guid id, AddPaymentInput input, [ScopedService] DatabaseContext context)
        {
            var paymentEntity = await context.Payments.FindAsync(id);
            if (paymentEntity == null) { throw new Exception($"Incorrect id: {id} specified for Payment. "); }
            else
            {
                paymentEntity.PaymentMethod = input.paymentMethod;
                paymentEntity.TransactionDate = input.transactionDate;
                paymentEntity.Price = input.price;
                paymentEntity.OrderId = input.orderId;

                context.Entry(paymentEntity).State = EntityState.Modified;
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Cannot update Payment information");
            }

            return new AddPaymentPayload(paymentEntity);
        }

        [UseDbContext(typeof(DatabaseContext))]
        public async Task<AddPaymentPayload> DeletePayment(Guid id, [ScopedService] DatabaseContext context)
        {
            var paymentEntity = await context.Payments.FindAsync(id);
            if (paymentEntity == null) { throw new Exception($"Incorrect id: {id} specified for Payment. "); }
            context.Payments.Remove(paymentEntity);

            try { await context.SaveChangesAsync(); }
            catch (Exception exception)
            {
                throw new Exception($"Exception deleting Payment: {id} Exception: {exception.Message} {exception.InnerException?.Message}");
            }

            return new AddPaymentPayload(paymentEntity);
        }

        [UseDbContext(typeof(DatabaseContext))]
        public async Task<AddReviewPayload> AddReviewAsync(AddReviewInput input, [ScopedService] DatabaseContext context)
        {
            var review = new Review
            {
                Stars = input.stars,
                Comment = input.comment,
                OrderId = input.orderId
            };

            context.Reviews.Add(review);
            await context.SaveChangesAsync();

            return new AddReviewPayload(review);
        }

        [UseDbContext(typeof(DatabaseContext))]
        public async Task<AddReviewPayload> PutReviewAsync(Guid id, AddReviewInput input, [ScopedService] DatabaseContext context)
        {
            var reviewEntity = await context.Reviews.FindAsync(id);
            if (reviewEntity == null) { throw new Exception($"Incorrect id: {id} specified for Review. "); }
            else
            {
                reviewEntity.Stars = input.stars;
                reviewEntity.Comment = input.comment;
                reviewEntity.OrderId = input.orderId;

                context.Entry(reviewEntity).State = EntityState.Modified;
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Cannot update Review information");
            }

            return new AddReviewPayload(reviewEntity);
        }

        [UseDbContext(typeof(DatabaseContext))]
        public async Task<AddReviewPayload> DeleteReview(Guid id, [ScopedService] DatabaseContext context)
        {
            var reviewEntity = await context.Reviews.FindAsync(id);
            if (reviewEntity == null) { throw new Exception($"Incorrect id: {id} specified for Review. "); }
            context.Reviews.Remove(reviewEntity);

            try { await context.SaveChangesAsync(); }
            catch (Exception exception)
            {
                throw new Exception($"Exception deleting Review: {id} Exception: {exception.Message} {exception.InnerException?.Message}");
            }

            return new AddReviewPayload(reviewEntity);
        }
    }
}
