using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Exceptions;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Orders.CreateOrder
{
    public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderResponse>
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Product> _productRepository;

        public CreateOrderCommandHandler(IGenericRepository<Order> orderRepository, IGenericRepository<Product> productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<OrderResponse> Handle(
            CreateOrderCommand command,
            CancellationToken cancellationToken)
        {
            var request = command.Request;

            var products = await GetProductsAsync(request.ProductIds);
            var totalAmount = products.Sum(p => p.Price);

            var order = new Order(
                Guid.NewGuid(),
                request.CustomerId,
                products,
                totalAmount);

            await _orderRepository.AddAsync(order, cancellationToken);

            return CreateResponse(products, order);
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
