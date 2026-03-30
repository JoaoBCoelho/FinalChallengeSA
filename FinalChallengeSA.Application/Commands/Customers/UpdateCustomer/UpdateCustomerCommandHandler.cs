using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Exceptions;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Customers.UpdateCustomer
{
    public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerResponse>
    {
        private readonly ICustomerRepository _repository;

        public UpdateCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerResponse> Handle(
            UpdateCustomerCommand command,
            CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync(command.Id, cancellationToken) ?? throw new NotFoundException($"Cliente com id '{command.Id}' não encontrado.");
            customer.Update(command.Request.Name, command.Request.Email);

            await _repository.UpdateAsync(customer, cancellationToken);

            return new CustomerResponse(customer.Id, customer.Name, customer.Email);
        }
    }
}
