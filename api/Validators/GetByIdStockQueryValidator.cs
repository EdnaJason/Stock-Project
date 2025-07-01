using FinShark.Service.Stocks.Query.GetById;
using FluentValidation;

namespace FinShark.api.Validators
{
    public class GetByIdStockQueryValidator : AbstractValidator<GetByIdStockQuery>
    {
        public GetByIdStockQueryValidator()
        {
            //Console.WriteLine(Id);
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required")
                .GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }
}
