using Chinook.Domain.ApiModels;
using FluentValidation;

namespace Chinook.Domain.Validation
{
    public class InvoiceLineValidator : AbstractValidator<InvoiceLineApiModel>
    {
        public InvoiceLineValidator()
        {
            RuleFor(il => il.InvoiceId).NotNull();
            RuleFor(il => il.TrackId).NotNull();
            RuleFor(il => il.Quantity).NotNull();
            RuleFor(il => il.Quantity).GreaterThan(0);
            RuleFor(il => il.UnitPrice).NotNull();
            RuleFor(il => il.UnitPrice).GreaterThan(0);
            RuleFor(il => il.UnitPrice).LessThanOrEqualTo((decimal)9.99);
        }
    }
}