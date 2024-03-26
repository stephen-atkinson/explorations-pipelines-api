using Explorations.Pipelines.Api.Controllers;
using Explorations.Pipelines.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Explorations.Pipelines.Tests.Controllers;

public class OrdersControllerTests
{
    [Fact]
    public void Get_ReturnsOrder()
    {
        const int orderId = 5;
        var controller = new OrdersController();

        var result = controller.Get(orderId);

        var objectResult = result as ObjectResult;

        var orderDto = objectResult?.Value as OrderDto;
        
        Assert.NotNull(orderDto);
        Assert.Equal(orderId, orderDto.Id);
    }
}