using FinalChallengeSA.Application.Commands.Orders.CreateOrder;
using FinalChallengeSA.Application.Commands.Orders.UpdateOrder;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalChallengeSA.Application.Validators
{
    public sealed class CreateOrderRequestValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.Request).NotNull();
            RuleFor(x => x.Request.ProductIds).NotNull().Must(ids => ids is not null && ids.Count > 0).WithMessage("ProductIds não pode ser nulo ou vazio.");
        }
    }

    public sealed class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderRequestValidator()
        {
            RuleFor(x => x.Request).NotNull();
            RuleFor(x => x.Request.ProductIds).NotNull().Must(ids => ids is not null && ids.Count > 0).WithMessage("ProductIds não pode ser nulo ou vazio.");
        }
    }
}
