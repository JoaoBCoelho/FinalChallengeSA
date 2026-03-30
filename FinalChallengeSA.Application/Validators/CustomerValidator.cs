using FinalChallengeSA.Application.Commands.Customers.CreateCustomer;
using FinalChallengeSA.Application.Commands.Customers.UpdateCustomer;
using FluentValidation;

namespace FinalChallengeSA.Application.Validators
{
    public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerRequestValidator()
        {
            RuleFor(x => x.Request).NotNull();
            RuleFor(x => x.Request.Name).NotEmpty().MaximumLength(200).WithMessage("Name não pode ser vazio e deve conter até 200 caracteres.");
            RuleFor(x => x.Request.Email).NotEmpty().MaximumLength(200).EmailAddress().WithMessage("O endereço de e-mail deve ser válido e conter até 200 caracteres.");
        }
    }

    public class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerRequestValidator()
        {
            RuleFor(x => x.Request).NotNull();
            RuleFor(x => x.Request.Name).NotEmpty().MaximumLength(200).WithMessage("Name não pode ser vazio e deve conter até 200 caracteres.");
            RuleFor(x => x.Request.Email).NotEmpty().MaximumLength(200).EmailAddress().WithMessage("O endereço de e-mail deve ser válido e conter até 200 caracteres.");
        }
    }

}
