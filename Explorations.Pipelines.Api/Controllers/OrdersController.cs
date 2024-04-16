using Explorations.Pipelines.Api.Database;
using Explorations.Pipelines.Api.Models;
using Explorations.Pipelines.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Explorations.Pipelines.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrdersDbContext _ordersDbContext;

    public OrdersController(OrdersDbContext ordersDbContext)
    {
        _ordersDbContext = ordersDbContext;
    }

    [HttpPost]
    public async Task<IActionResult>  Create()
    {
        var order = new Order();

        await _ordersDbContext.Orders.AddAsync(order);

        await _ordersDbContext.SaveChangesAsync();

        var dto = ToDto(order);

        return Ok(dto);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var order = await _ordersDbContext.Orders.FindAsync([id], cancellationToken);

        if (order == null)
        {
            return NotFound();
        }

        var dto = ToDto(order);

        return Ok(dto);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] IEnumerable<int>? ids, CancellationToken cancellationToken)
    {
        var orders = await _ordersDbContext.Orders
            .Where(o => ids.EmptyIfNull().Contains(o.Id))
            .ToArrayAsync(cancellationToken);

        var dtos = orders.Select(ToDto).ToArray();
        
        return Ok(dtos);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var order = await _ordersDbContext.Orders.FindAsync([id], cancellationToken);

        if (order == null)
        {
            return NotFound();
        }

        _ordersDbContext.Orders.Remove(order);

        await _ordersDbContext.SaveChangesAsync(CancellationToken.None);

        return NoContent();
    }

    private static OrderDto ToDto(Order order) => new () { Id = order.Id };
}