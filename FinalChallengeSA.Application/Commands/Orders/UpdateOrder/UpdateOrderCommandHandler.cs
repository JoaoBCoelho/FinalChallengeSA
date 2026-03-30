using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Exceptions;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using FluentValidation;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Orders.UpdateOrder
{
    public sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IValidator<UpdateOrderCommand> _validator;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository,
            IProductRepository productRepository,
            IValidator<UpdateOrderCommand> validator)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _validator = validator;
        }

        public async Task<OrderResponse> Handle(
            UpdateOrderCommand command,
            CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var order = await _orderRepository.GetByIdAsync(command.Id, cancellationToken) ?? throw new NotFoundException($"Pedido com id '{command.Id}' não encontrado.");

            var products = await GetProductsAsync(command.Request.ProductIds);

            order.Update(command.Request.CustomerId, products);

            await _orderRepository.UpdateAsync(order, cancellationToken);

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
