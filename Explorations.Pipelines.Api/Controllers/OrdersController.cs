using Explorations.Pipelines.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Explorations.Pipelines.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    [HttpGet("{id:int}")]
    public IActionResult Get(int id) => Ok(new OrderDto { Id = id });
}