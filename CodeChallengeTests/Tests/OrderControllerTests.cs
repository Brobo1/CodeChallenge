using CodeChallenge.Data;
using CodeChallenge.Models;
using CodeChallenge.Controllers;

using Xunit;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

public class OrderControllerTests {
	private readonly DbContextOptions<CodeChallengeDbContext> _options;

	public OrderControllerTests() {
		_options = new DbContextOptionsBuilder<CodeChallengeDbContext>()
				   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				   .Options;
	}

	[Fact]
	public async Task GetOrders_ReturnsOrders() {
		using (var context = new CodeChallengeDbContext(_options)) {
			context.Orders.Add(new Order { CustomerId = 1, Date = DateTime.Now });
			context.Orders.Add(new Order { CustomerId = 2, Date = DateTime.Now });
			await context.SaveChangesAsync();

			var controller = new OrderController(context);

			var result = await controller.GetOrders() as OkObjectResult;

			Assert.NotNull(result);
			var orders = result.Value as List<Order>;
			Assert.Collection(
				orders,
				order => Assert.Equal(1, order.CustomerId),
				order => Assert.Equal(2, order.CustomerId)
				);
		}
	}

	[Fact]
	public async Task CreateOrder_GetRows() {
		using (var context = new CodeChallengeDbContext(_options)) {
			context.Orders.Add(new Order { CustomerId = 1, Date = DateTime.Now });
			context.Orders.Add(new Order { CustomerId = 2, Date = DateTime.Now });
			await context.SaveChangesAsync();

			var controller = new OrderController(context);

			await controller.CreateOrder(new OrderObject { CustomerId = 3, Date = DateTime.Now });

			var result = await controller.GetOrders() as OkObjectResult;

			Assert.NotNull(result);
			var orders = result.Value as List<Order>;
			Assert.Equal(3, orders.Count);
		}
	}

	[Fact]
	public async Task UpdateOrder_GetRow() {
		using (var context = new CodeChallengeDbContext(_options)) {
			context.Orders.Add(new Order { CustomerId = 1, Date = DateTime.Now });
			context.Orders.Add(new Order { CustomerId = 2, Date = DateTime.Now });

			await context.SaveChangesAsync();

			var controller = new OrderController(context);

			var updatedOrder = new OrderObject { CustomerId = 1, Date = DateTime.Now.AddYears(2) };

			await controller.UpdateOrder(1, updatedOrder);

			var result = await controller.GetOrder(updatedOrder.CustomerId) as OkObjectResult;

			Assert.NotNull(result);
			var order = result.Value as Order;
			Assert.Equal(updatedOrder.CustomerId, order.CustomerId);
			Assert.Equal(updatedOrder.Date,       order.Date);
		}
	}

	[Fact]
	public async Task DeleteOrder_GetRows() {
		using (var context = new CodeChallengeDbContext(_options)) {
			context.Orders.Add(new Order { CustomerId = 1, Date = DateTime.Now });
			context.Orders.Add(new Order { CustomerId = 2, Date = DateTime.Now });
			await context.SaveChangesAsync();

			var controller = new OrderController(context);

			await controller.DeleteOrder(1);

			var result = await controller.GetOrders() as OkObjectResult;

			Assert.NotNull(result);
			var orders = result.Value as List<Order>;
			Assert.Single(orders);
		}
	}

	[Fact]
	public async Task GetOrder_ReturnOrder() {
		using (var context = new CodeChallengeDbContext(_options)) {
			var orderToAdd  = new Order { CustomerId = 1, Date = DateTime.Now };
			var orderToAdd2 = new Order { CustomerId = 2, Date = DateTime.Now };

			context.Orders.Add(orderToAdd);
			context.Orders.Add(orderToAdd2);
			await context.SaveChangesAsync();

			var controller = new OrderController(context);

			var result = await controller.GetOrder(orderToAdd.Id) as OkObjectResult;

			Assert.NotNull(result);
			var order = result.Value as Order;
			Assert.Equal(orderToAdd.CustomerId, order.CustomerId);
			Assert.Equal(orderToAdd.Date,       order.Date);
		}
	}
}