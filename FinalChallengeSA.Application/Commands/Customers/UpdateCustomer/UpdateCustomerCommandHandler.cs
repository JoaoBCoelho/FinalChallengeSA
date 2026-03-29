using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Exceptions;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Customers.UpdateCustomer
{
    public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerResponse>
    {
        private readonly IGenericRepository<Customer> _repository;

        public UpdateCustomerCommandHandler(IGenericRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<CustomerResponse> Handle(
            UpdateCustomerCommand command,
            CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(command.Id, cancellationToken) ?? throw new NotFoundException($"Customer '{command.Id}' not found.");
            var updated = existing with
            {
                Name = command.Request.Name,
                Email = command.Request.Email
            };

            await _repository.UpdateAsync(updated, cancellationToken);

            return new CustomerResponse(updated.Id, updated.Name, updated.Email);
        }
    }
}
