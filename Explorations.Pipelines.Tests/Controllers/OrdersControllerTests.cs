using Explorations.Pipelines.Api.Controllers;
using Explorations.Pipelines.Api.Database;
using Explorations.Pipelines.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Explorations.Pipelines.Tests.Controllers;

public class OrdersControllerTests
{
    [Fact]
    public async Task Get_ReturnsOrders()
    {
        var orderIds = new [] { 3, 5 };

        var orders = new[]
        {
            new Order { Id = 3 },
            new Order { Id = 4 },
            new Order { Id = 5 },
        };
        
        var dbContext = new Mock<OrdersDbContext>(new DbContextOptions<OrdersDbContext>());
        dbContext.Setup(c => c.Orders).ReturnsDbSet(orders);
        
        var controller = new OrdersController(dbContext.Object);

        var result = await controller.Get(orderIds, CancellationToken.None);

        var objectResult = result as ObjectResult;

        var orderDto = objectResult?.Value as OrderDto[];
        
        Assert.NotNull(orderDto);
        Assert.Equal(2, orderDto.Length);
        Assert.True(orderIds.SequenceEqual(orderDto.Select(o => o.Id).ToArray()));
    }
}