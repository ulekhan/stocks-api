using System;
using FluentAssertions;
using StocksData.Api.Handlers;
using StocksData.Api.Validators;
using Xunit;

namespace StocksData.Api.Tests.Validators;

public class CompareStockQueryValidatorTests
{
    [Fact]
    public void CompareStockQueryValidator_ShouldFindError_WhenBaseSymbolIsEmpty()
    {
        var validator = new CompareStockQueryValidator();
        var result = validator.Validate(new CompareStockQuery("", "AAPL", DateTime.Today.AddDays(-2), DateTime.Today));

        result.Errors.Should().Contain(p => p.PropertyName == nameof(CompareStockQuery.BaseSymbol));
    }
    
    [Fact]
    public void CompareStockQueryValidator_ShouldFindError_WhenSymbolIsEmpty()
    {
        var validator = new CompareStockQueryValidator();
        var result = validator.Validate(new CompareStockQuery("AAPL", "", DateTime.Today.AddDays(-2), DateTime.Today));

        result.Errors.Should().Contain(p => p.PropertyName == nameof(CompareStockQuery.Symbol));
    }
    
    [Fact]
    public void CompareStockQueryValidator_ShouldFindError_WhenFromDateIsGreaterThanToDate()
    {
        var validator = new CompareStockQueryValidator();
        var result = validator.Validate(new CompareStockQuery("AAPL", "SPX", DateTime.Today.AddDays(2), DateTime.Today));

        result.Errors.Should().Contain(p => p.PropertyName == nameof(CompareStockQuery.FromDate));
    }
}