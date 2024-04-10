using Explorations.Pipelines.Api.Models;
using Explorations.Pipelines.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Explorations.Pipelines.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    [HttpGet("{id:int}")]
    public IActionResult Get(int id) => Ok(new OrderDto { Id = id });
    
    [HttpGet]
    public IActionResult Get([FromQuery] IEnumerable<int>? ids)
    {
        var orders = ids.EmptyIfNull()
            .Select(id => new OrderDto { Id = id })
            .ToArray();

        return Ok(orders);
    }  

}