using Xunit;

using CodeChallenge.Data;
using CodeChallenge.Controllers;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using System.Collections.Generic;

using CodeChallenge.Models;

public class WishlistProductControllerTests {
	private readonly DbContextOptions<CodeChallengeDbContext> _options;

	public WishlistProductControllerTests() {
		_options = new DbContextOptionsBuilder<CodeChallengeDbContext>()
				   .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
				   .Options;
	}

	[Fact]
	public async Task GetWishlistProducts_ReturnsWishlistProducts() {
		using (var context = new CodeChallengeDbContext(_options)) {
			var wishlistProduct1 = new WishlistProduct { WishlistId = 1, ProductId = 1 };
			var wishlistProduct2 = new WishlistProduct { WishlistId = 2, ProductId = 2 };

			context.WishlistProducts.Add(wishlistProduct1);
			context.WishlistProducts.Add(wishlistProduct2);

			await context.SaveChangesAsync();

			var controller = new WishlistProductController(context);

			var result = await controller.GetWishlistProducts() as OkObjectResult;

			Assert.NotNull(result);
			var wishlistProducts = result.Value as List<WishlistProduct>;

			Assert.Equal(2, wishlistProducts.Count);
			Assert.Contains(wishlistProduct1, wishlistProducts);
			Assert.Contains(wishlistProduct2, wishlistProducts);
		}
	}

	[Fact]
	public async Task CreateWishlistProduct_IncreasesCount() {
		using (var context = new CodeChallengeDbContext(_options)) {
			var controller = new WishlistProductController(context);

			var wishlistProductToAdd = new WishlistProductObject { WishlistId = 3, ProductId = 3 };

			await controller.CreateWishlistProduct(wishlistProductToAdd);

			var result = await controller.GetWishlistProducts() as OkObjectResult;

			Assert.NotNull(result);
			var wishlistProducts = result.Value as List<WishlistProduct>;

			Assert.Single(wishlistProducts);
		}
	}

	[Fact]
	public async Task DeleteWishlistProduct_DecreasesCount() {
		using (var context = new CodeChallengeDbContext(_options)) {
			var wishlistProduct = new WishlistProduct { WishlistId = 4, ProductId = 4 };

			context.WishlistProducts.Add(wishlistProduct);

			await context.SaveChangesAsync();

			var controller = new WishlistProductController(context);

			await controller.DeleteWishlistProduct(wishlistProduct.Id);

			var result = await controller.GetWishlistProducts() as OkObjectResult;

			Assert.NotNull(result);
			var wishlistProducts = result.Value as List<WishlistProduct>;

			Assert.Empty(wishlistProducts);
		}
	}

	[Fact]
	public async Task UpdateWishlistProduct_ChangesProperties() {
		using (var context = new CodeChallengeDbContext(_options)) {
			var wishlistProduct = new WishlistProduct { WishlistId = 5, ProductId = 5 };

			context.WishlistProducts.Add(wishlistProduct);

			await context.SaveChangesAsync();

			var controller = new WishlistProductController(context);

			var wishlistProductToUpdate = new WishlistProductObject { WishlistId = 6, ProductId = 6 };

			await controller.UpdateWishlistProduct(wishlistProduct.Id, wishlistProductToUpdate);

			var result = await controller.GetWishlistProduct(wishlistProduct.Id) as OkObjectResult;

			Assert.NotNull(result);
			var updatedWishlistProduct = result.Value as WishlistProduct;

			Assert.Equal(wishlistProductToUpdate.WishlistId, updatedWishlistProduct.WishlistId);
			Assert.Equal(wishlistProductToUpdate.ProductId,  updatedWishlistProduct.ProductId);
		}
	}

	[Fact]
	public async Task GetWishlistProduct_ReturnsCorrectWishlistProduct() {
		using (var context = new CodeChallengeDbContext(_options)) {
			var wishlistProduct = new WishlistProduct { WishlistId = 7, ProductId = 7 };

			context.WishlistProducts.Add(wishlistProduct);

			await context.SaveChangesAsync();

			var controller = new WishlistProductController(context);

			var result = await controller.GetWishlistProduct(wishlistProduct.Id) as OkObjectResult;

			Assert.NotNull(result);
			var retrievedWishlistProduct = result.Value as WishlistProduct;

			Assert.Equal(wishlistProduct.WishlistId, retrievedWishlistProduct.WishlistId);
			Assert.Equal(wishlistProduct.ProductId,  retrievedWishlistProduct.ProductId);
		}
	}
}