using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Customers.CreateCustomer
{
    public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerResponse>
    {
        private readonly IGenericRepository<Customer> _repository;

        public CreateCustomerCommandHandler(IGenericRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<CustomerResponse> Handle(
            CreateCustomerCommand command,
            CancellationToken cancellationToken)
        {
            var request = command.Request;

            // Aqui entrariam validações de negócio (duplicidade de email, etc.)

            var customer = new Customer(
                Id: Guid.NewGuid(),
                Name: request.Name,
                Email: request.Email);

            await _repository.AddAsync(customer, cancellationToken);

            return new CustomerResponse(customer.Id, customer.Name, customer.Email);
        }
    }
}
