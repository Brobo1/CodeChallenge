using Xunit;

using CodeChallenge.Data;
using CodeChallenge.Controllers;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using System.Collections.Generic;

using CodeChallenge.Models;

public class WishlistControllerTests {
	private readonly DbContextOptions<CodeChallengeDbContext> _options;

	public WishlistControllerTests() {
		_options = new DbContextOptionsBuilder<CodeChallengeDbContext>()
				   .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
				   .Options;
	}

	[Fact]
	public async Task GetWishlists_ReturnsWishlists() {
		using (var context = new CodeChallengeDbContext(_options)) {
			var wishlist1 = new Wishlist { CustomerId = 1 };
			var wishlist2 = new Wishlist { CustomerId = 2 };

			context.Wishlists.Add(wishlist1);
			context.Wishlists.Add(wishlist2);

			await context.SaveChangesAsync();

			var controller = new WishlistController(context);

			var result = await controller.GetWishlists() as OkObjectResult;

			Assert.NotNull(result);
			var wishlists = result.Value as List<Wishlist>;

			Assert.Equal(2, wishlists.Count);
			Assert.Contains(wishlist1, wishlists);
			Assert.Contains(wishlist2, wishlists);
		}
	}

	[Fact]
	public async Task CreateWishlist_IncreasesCount() {
		using (var context = new CodeChallengeDbContext(_options)) {
			var controller = new WishlistController(context);

			var wishlistToAdd = new WishlistObject { CustomerId = 3 };

			await controller.CreateWishlist(wishlistToAdd);

			var result = await controller.GetWishlists() as OkObjectResult;

			Assert.NotNull(result);
			var wishlists = result.Value as List<Wishlist>;

			Assert.Single(wishlists);
		}
	}

	[Fact]
	public async Task DeleteWishlist_DecreasesCount() {
		using (var context = new CodeChallengeDbContext(_options)) {
			var wishlist = new Wishlist { CustomerId = 4 };

			context.Wishlists.Add(wishlist);

			await context.SaveChangesAsync();

			var controller = new WishlistController(context);

			await controller.DeleteWishlist(wishlist.Id);

			var result = await controller.GetWishlists() as OkObjectResult;

			Assert.NotNull(result);
			var wishlists = result.Value as List<Wishlist>;

			Assert.Empty(wishlists);
		}
	}

	[Fact]
	public async Task UpdateWishlist_ChangesProperties() {
		using (var context = new CodeChallengeDbContext(_options)) {
			var wishlist = new Wishlist { CustomerId = 5 };

			context.Wishlists.Add(wishlist);

			await context.SaveChangesAsync();

			var controller = new WishlistController(context);

			var wishlistToUpdate = new WishlistObject { CustomerId = 6 };

			await controller.UpdateWishlist(wishlist.Id, wishlistToUpdate);

			var result = await controller.GetWishlist(wishlist.Id) as OkObjectResult;

			Assert.NotNull(result);
			var updatedWishlist = result.Value as Wishlist;

			Assert.Equal(wishlistToUpdate.CustomerId, updatedWishlist.CustomerId);
		}
	}

	[Fact]
	public async Task GetWishlist_ReturnsCorrectWishlist() {
		using (var context = new CodeChallengeDbContext(_options)) {
			var wishlist = new Wishlist { CustomerId = 7 };

			context.Wishlists.Add(wishlist);

			await context.SaveChangesAsync();

			var controller = new WishlistController(context);

			var result = await controller.GetWishlist(wishlist.Id) as OkObjectResult;

			Assert.NotNull(result);
			var retrievedWishlist = result.Value as Wishlist;

			Assert.Equal(wishlist.CustomerId, retrievedWishlist.CustomerId);
		}
	}
}