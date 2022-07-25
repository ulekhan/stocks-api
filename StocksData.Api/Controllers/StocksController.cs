using MediatR;
using Microsoft.AspNetCore.Mvc;
using StocksData.Api.Handlers;

namespace StocksData.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/stocks")]
public class StocksController : Controller
{
    private readonly IMediator _mediator;

    public StocksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("compare/{baseSymbol}/vs/{symbol}")]
    public async Task<IActionResult> CompareAsync(string baseSymbol, string symbol, DateTime fromDate, DateTime toDate)
    {
        var query = new CompareStockQuery(baseSymbol, symbol, fromDate, toDate);
        var result = await _mediator.Send(query);
        return new JsonResult(result);
    }
}