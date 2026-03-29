using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Customers.CreateCustomer
{
    public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerResponse>
    {
        private readonly ICustomerRepository _repository;

        public CreateCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerResponse> Handle(
            CreateCustomerCommand command,
            CancellationToken cancellationToken)
        {
            var request = command.Request;

            var customer = new Customer(request.Name, request.Email);

            await _repository.AddAsync(customer, cancellationToken);

            return new CustomerResponse(customer.Id, customer.Name, customer.Email);
        }
    }
}
