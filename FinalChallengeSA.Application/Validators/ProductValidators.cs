using FinalChallengeSA.Application.Commands.Products.CreateProduct;
using FinalChallengeSA.Application.Commands.Products.UpdateProduct;
using FluentValidation;

namespace FinalChallengeSA.Application.Validators
{
    public sealed class CreateProductRequestValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Request).NotNull();
            RuleFor(x => x.Request.Name).NotEmpty().MaximumLength(200).WithMessage("Name não pode ser vazio e deve conter até 200 caracteres.");
            RuleFor(x => x.Request.Description).NotEmpty().MaximumLength(500).WithMessage("Description não pode ser vazio e deve conter até 200 caracteres.");
            RuleFor(x => x.Request.Price).GreaterThan(0).WithMessage("Price deve ser maior que zero.");
        }
    }

    public sealed class UpdateProductRequestValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(x => x.Request).NotNull();
            RuleFor(x => x.Request.Name).NotEmpty().MaximumLength(200).WithMessage("Name não pode ser vazio e deve conter até 200 caracteres.");
            RuleFor(x => x.Request.Description).NotEmpty().MaximumLength(500).WithMessage("Description não pode ser vazio e deve conter até 200 caracteres.");
            RuleFor(x => x.Request.Price).GreaterThan(0).WithMessage("Price deve ser maior que zero.");
        }
    }
}
