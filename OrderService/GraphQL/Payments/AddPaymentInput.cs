using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.GraphQL.Payments
{
    public record AddPaymentInput(string paymentMethod, DateTime transactionDate, decimal price, Guid orderId);
}
