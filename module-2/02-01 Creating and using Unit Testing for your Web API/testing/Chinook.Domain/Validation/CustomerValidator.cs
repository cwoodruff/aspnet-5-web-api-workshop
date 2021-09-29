using Chinook.Domain.ApiModels;
using FluentValidation;

namespace Chinook.Domain.Validation
{
    public class CustomerValidator : AbstractValidator<CustomerApiModel>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.FirstName).NotNull();
            RuleFor(c => c.LastName).NotNull();
            RuleFor(c => c.Email).EmailAddress();
            RuleFor(c => c.Phone).Matches(@"\(?\d{3}\)?[-\.]? *\d{3}[-\.]? *[-\.]?\d{4}");
            RuleFor(c => c.Fax).Matches(@"\(?\d{3}\)?[-\.]? *\d{3}[-\.]? *[-\.]?\d{4}");
            RuleFor(c => c.FirstName).MaximumLength(40);
            RuleFor(c => c.LastName).MaximumLength(20);
            RuleFor(c => c.Company).MaximumLength(80);
            RuleFor(c => c.Address).MaximumLength(70);
            RuleFor(c => c.City).MaximumLength(40);
            RuleFor(c => c.State).MaximumLength(40);
            RuleFor(c => c.Country).MaximumLength(40);
            RuleFor(c => c.PostalCode).Matches(@"^[0-9]{5}(?:-[0-9]{4})?$");
        }
    }
}