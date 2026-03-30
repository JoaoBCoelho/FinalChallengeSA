using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Domain.Entities;
using FluentValidation;
using MediatR;

namespace FinalChallengeSA.Application.Commands.Customers.CreateCustomer
{
    public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerResponse>
    {
        private readonly ICustomerRepository _repository;
        private readonly IValidator<CreateCustomerCommand> _validator;

        public CreateCustomerCommandHandler(ICustomerRepository repository, IValidator<CreateCustomerCommand> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<CustomerResponse> Handle(
            CreateCustomerCommand command,
            CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var request = command.Request;

            var customer = new Customer(request.Name, request.Email);

            await _repository.AddAsync(customer, cancellationToken);

            return new CustomerResponse(customer.Id, customer.Name, customer.Email);
        }
    }
}
