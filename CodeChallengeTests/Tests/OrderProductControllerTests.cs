using Xunit;

using CodeChallenge.Data;
using CodeChallenge.Controllers;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using System.Collections.Generic;

using CodeChallenge.Models;

public class OrderProductControllerTests {
	private readonly DbContextOptions<CodeChallengeDbContext> _options;

	public OrderProductControllerTests() {
		_options = new DbContextOptionsBuilder<CodeChallengeDbContext>()
				   .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
				   .Options;
	}

	[Fact]
	public async Task GetOrderProducts_ReturnsOrderProducts() {
		using (var context = new CodeChallengeDbContext(_options)) {
			var orderProduct1 = new OrderProduct { OrderId = 1, ProductId = 1 };
			var orderProduct2 = new OrderProduct { OrderId = 2, ProductId = 2 };

			context.OrderProducts.Add(orderProduct1);
			context.OrderProducts.Add(orderProduct2);
			await context.SaveChangesAsync();

			var controller = new OrderProductController(context);

			var result = await controller.GetOrderProducts() as OkObjectResult;

			Assert.NotNull(result);
			var orderProducts = result.Value as List<OrderProduct>;

			Assert.Equal(2, orderProducts.Count);
			Assert.Contains(orderProduct1, orderProducts);
			Assert.Contains(orderProduct2, orderProducts);
		}
	}

	[Fact]
	public async Task CreateOrderProduct_IncreasesCount() {
		using (var context = new CodeChallengeDbContext(_options)) {
			var controller = new OrderProductController(context);

			var orderProductToAdd = new OrderProductObject { OrderId = 1, ProductId = 1 };

			await controller.CreateOrderProduct(orderProductToAdd);

			var result = await controller.GetOrderProducts() as OkObjectResult;

			Assert.NotNull(result);
			var orderProducts = result.Value as List<OrderProduct>;

			Assert.Single(orderProducts);
		}
	}

	[Fact]
	public async Task DeleteOrderProduct_DecreasesCount() {
		using (var context = new CodeChallengeDbContext(_options)) {
			var orderProduct = new OrderProduct { OrderId = 5, ProductId = 5 };

			context.OrderProducts.Add(orderProduct);

			await context.SaveChangesAsync();

			var controller = new OrderProductController(context);

			await controller.DeleteOrderProduct(orderProduct.Id);

			var result = await controller.GetOrderProducts() as OkObjectResult;

			Assert.NotNull(result);
			var orderProducts = result.Value as List<OrderProduct>;

			Assert.Empty(orderProducts);
		}
	}

	[Fact]
	public async Task UpdateOrderProduct_ChangesProperties() {
		using (var context = new CodeChallengeDbContext(_options)) {
			var orderProduct = new OrderProduct { OrderId = 7, ProductId = 7 };

			context.OrderProducts.Add(orderProduct);

			await context.SaveChangesAsync();

			var controller = new OrderProductController(context);

			var orderProductToUpdate = new OrderProductObject { OrderId = 8, ProductId = 8 };

			await controller.UpdateOrderProduct(orderProduct.Id, orderProductToUpdate);

			var result = await controller.GetOrderProduct(orderProduct.Id) as OkObjectResult;

			Assert.NotNull(result);
			var updatedOrderProduct = result.Value as OrderProduct;

			Assert.Equal(orderProductToUpdate.OrderId,   updatedOrderProduct.OrderId);
			Assert.Equal(orderProductToUpdate.ProductId, updatedOrderProduct.ProductId);
		}
	}

	[Fact]
	public async Task GetOrderProduct_ReturnsCorrectOrderProduct() {
		using (var context = new CodeChallengeDbContext(_options)) {
			var orderProduct = new OrderProduct { OrderId = 1, ProductId = 1 };

			context.OrderProducts.Add(orderProduct);

			await context.SaveChangesAsync();

			var controller = new OrderProductController(context);

			var result = await controller.GetOrderProduct(orderProduct.Id) as OkObjectResult;

			Assert.NotNull(result);
			var retrievedOrderProduct = result.Value as OrderProduct;

			Assert.Equal(orderProduct.OrderId,   retrievedOrderProduct.OrderId);
			Assert.Equal(orderProduct.ProductId, retrievedOrderProduct.ProductId);
		}
	}
}