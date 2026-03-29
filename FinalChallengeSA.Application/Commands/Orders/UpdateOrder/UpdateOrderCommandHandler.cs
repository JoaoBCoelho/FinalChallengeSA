using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Exceptions;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Orders.UpdateOrder
{
    public sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderResponse>
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Product> _productRepository;

        public UpdateOrderCommandHandler(IGenericRepository<Order> orderRepository, IGenericRepository<Product> productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<OrderResponse> Handle(
            UpdateOrderCommand command,
            CancellationToken cancellationToken)
        {
            var existing = await _orderRepository.GetByIdAsync(command.Id, cancellationToken) ?? throw new NotFoundException($"Order '{command.Id}' not found.");

            var products = await GetProductsAsync(command.Request.ProductIds);
            var totalAmount = products.Sum(p => p.Price);

            var updated = new Order(
                existing.Id,
                command.Request.CustomerId,
                products,
                totalAmount);

            await _orderRepository.UpdateAsync(updated, cancellationToken);

            return CreateResponse(products, updated);
        }

        private static OrderResponse CreateResponse(IReadOnlyList<Product> products, Order order)
        {
            var productsResponse = products.Select(p => new OrderProductResponse(p.Id, p.Name, p.Description, p.Price)).ToList();
            return new OrderResponse(order.Id, order.CustomerId, productsResponse, order.TotalAmount);
        }

        private async Task<IReadOnlyList<Product>> GetProductsAsync(IReadOnlyList<Guid> productIds)
        {
            var product = new List<Product>();
            foreach (var id in productIds)
            {
                var productEntity = await _productRepository.GetByIdAsync(id)
                    ?? throw new NotFoundException($"Produto com id {id} não encontrado.");
                product.Add(productEntity);
            }
            return product;
        }
    }
}
