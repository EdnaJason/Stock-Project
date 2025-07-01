using FinShark.Service.Stocks.Command.Create;
using FluentValidation;

namespace FinShark.api.Validators
{
    public class CreateStockCommandValidator : AbstractValidator<CreateStockCommand>
    {
        public CreateStockCommandValidator()
        {
            RuleFor(x => x.Symbol)
                .NotEmpty().WithMessage("Symbol is required")
                .Length(1,10).WithMessage("Symbol must be between 1 and 10 characters long");
            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("Company Name is required")
                .MaximumLength(100).WithMessage("Company Name must not exceed 100 characters");
            RuleFor(x => x.Purchase)
                .GreaterThan(0).WithMessage("Purchase must be greater than 0")
                .LessThanOrEqualTo(1000000).WithMessage("Purchase must not exceed 1,000,000");
            RuleFor(x => x.LastDiv)
                .GreaterThanOrEqualTo(0).WithMessage("Last Dividend must be greater than or equal to 0")
                .LessThanOrEqualTo(1000000).WithMessage("Last Dividend must not exceed 1,000,000");
            RuleFor(x => x.Industry)
                .NotEmpty().WithMessage("Industry is required")
                .MaximumLength(50).WithMessage("Industry must not exceed 50 characters");
            RuleFor(x => x.MarketCap)
                .GreaterThan(0).WithMessage("Market Cap must be gre ater than 0!")
                .InclusiveBetween(1000000,1000000000).WithMessage("Market Cap must be between 1,000,000 and 1,000,000,000");
        }
    }
}
