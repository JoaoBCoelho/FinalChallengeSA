using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Exceptions;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using FluentValidation;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Orders.CreateOrder
{
    public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IValidator<CreateOrderCommand> _validator;

        public CreateOrderCommandHandler(IOrderRepository orderRepository,
            IProductRepository productRepository,
            ICustomerRepository customerRepository,
            IValidator<CreateOrderCommand> validator)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _validator = validator;
        }

        public async Task<OrderResponse> Handle(
            CreateOrderCommand command,
            CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var request = command.Request;
            await ValidateCustomer(request.CustomerId);

            var products = await GetProductsAsync(request.ProductIds);
            var totalAmount = products.Sum(p => p.Price);

            var order = new Order(request.CustomerId, products);

            await _orderRepository.AddAsync(order, cancellationToken);

            return CreateResponse(products, order);
        }

        private async Task ValidateCustomer(Guid customerId)
        {
            var _ = await _customerRepository.GetByIdAsync(customerId) ?? throw new NotFoundException($"Cliente com id {customerId} não encontrado.");
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
