using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Exceptions;
using FinalChallengeSA.Application.Interfaces;
using FluentValidation;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Customers.UpdateCustomer
{
    public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerResponse>
    {
        private readonly ICustomerRepository _repository;
        private readonly IValidator<UpdateCustomerCommand> _validator;

        public UpdateCustomerCommandHandler(ICustomerRepository repository, IValidator<UpdateCustomerCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<CustomerResponse> Handle(
            UpdateCustomerCommand command,
            CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var customer = await _repository.GetByIdAsync(command.Id, cancellationToken) ?? throw new NotFoundException($"Cliente com id '{command.Id}' não encontrado.");
            customer.Update(command.Request.Name, command.Request.Email);

            await _repository.UpdateAsync(customer, cancellationToken);

            return new CustomerResponse(customer.Id, customer.Name, customer.Email);
        }
    }
}
