using FluentValidation;
using StocksData.Api.Handlers;

namespace StocksData.Api.Validators;

public class CompareStockQueryValidator : AbstractValidator<CompareStockQuery>
{
    public CompareStockQueryValidator()
    {
        RuleFor(p => p.ToDate).GreaterThan(p => p.FromDate);
        RuleFor(p => p.FromDate).LessThan(p => p.ToDate);
        RuleFor(p => p.Symbol).NotEmpty().Length(3, 5);
        RuleFor(p => p.BaseSymbol).NotEmpty().Length(3, 5);;
    }
}