using FinalChallengeSA.Application.Exceptions;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Customers.DeleteCustomer
{
    public sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _repository;

        public DeleteCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(
            DeleteCustomerCommand command,
            CancellationToken cancellationToken)
        {
            var _ = await _repository.GetByIdAsync(command.Id, cancellationToken) ?? throw new NotFoundException($"Customer '{command.Id}' not found.");
            await _repository.DeleteAsync(command.Id, cancellationToken);
        }
    }
}
