using Xunit;

using CodeChallenge.Data;
using CodeChallenge.Controllers;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using System.Collections.Generic;

using CodeChallenge.Models;

public class RatingControllerTests {
	private readonly DbContextOptions<CodeChallengeDbContext> _options;

	public RatingControllerTests() {
		_options = new DbContextOptionsBuilder<CodeChallengeDbContext>()
				   .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
				   .Options;
	}

	[Fact]
	public async Task GetRatings_ReturnsRatings() {
		using (var context = new CodeChallengeDbContext(_options)) {
			var rating1 = new Rating { CustomerId = 1, ProductId = 1, Stars = 5 };
			var rating2 = new Rating { CustomerId = 2, ProductId = 2, Stars = 4 };

			context.Ratings.Add(rating1);
			context.Ratings.Add(rating2);

			await context.SaveChangesAsync();

			var controller = new RatingController(context);

			var result = await controller.GetRatings() as OkObjectResult;

			Assert.NotNull(result);
			var ratings = result.Value as List<Rating>;

			Assert.Equal(2, ratings.Count);
			Assert.Contains(rating1, ratings);
			Assert.Contains(rating2, ratings);
		}
	}

	[Fact]
	public async Task CreateRating_IncreasesCount() {
		using (var context = new CodeChallengeDbContext(_options)) {
			var controller = new RatingController(context);

			var ratingToAdd = new RatingObject { CustomerId = 3, ProductId = 3, Stars = 3 };

			await controller.CreateRating(ratingToAdd);

			var result = await controller.GetRatings() as OkObjectResult;

			Assert.NotNull(result);
			var ratings = result.Value as List<Rating>;

			Assert.Single(ratings);
		}
	}

	[Fact]
	public async Task DeleteRating_DecreasesCount() {
		using (var context = new CodeChallengeDbContext(_options)) {
			var rating = new Rating { CustomerId = 4, ProductId = 4, Stars = 5 };

			context.Ratings.Add(rating);

			await context.SaveChangesAsync();

			var controller = new RatingController(context);

			await controller.DeleteRating(rating.Id);

			var result = await controller.GetRatings() as OkObjectResult;

			Assert.NotNull(result);
			var ratings = result.Value as List<Rating>;

			Assert.Empty(ratings);
		}
	}

	[Fact]
	public async Task UpdateRating_ChangesProperties() {
		using (var context = new CodeChallengeDbContext(_options)) {
			var rating = new Rating { CustomerId = 5, ProductId = 5, Stars = 5 };

			context.Ratings.Add(rating);

			await context.SaveChangesAsync();

			var controller = new RatingController(context);

			var ratingToUpdate = new RatingObject { CustomerId = 6, ProductId = 6, Stars = 4 };

			await controller.UpdateRating(rating.Id, ratingToUpdate);

			var result = await controller.GetRating(rating.Id) as OkObjectResult;

			Assert.NotNull(result);
			var updatedRating = result.Value as Rating;

			Assert.Equal(ratingToUpdate.CustomerId, updatedRating.CustomerId);
			Assert.Equal(ratingToUpdate.ProductId,  updatedRating.ProductId);
			Assert.Equal(ratingToUpdate.Stars,      updatedRating.Stars);
		}
	}

	[Fact]
	public async Task GetRating_ReturnsCorrectRating() {
		using (var context = new CodeChallengeDbContext(_options)) {
			var rating = new Rating { CustomerId = 7, ProductId = 7, Stars = 5 };

			context.Ratings.Add(rating);

			await context.SaveChangesAsync();

			var controller = new RatingController(context);

			var result = await controller.GetRating(rating.Id) as OkObjectResult;

			Assert.NotNull(result);
			var retrievedRating = result.Value as Rating;

			Assert.Equal(rating.CustomerId, retrievedRating.CustomerId);
			Assert.Equal(rating.ProductId,  retrievedRating.ProductId);
			Assert.Equal(rating.Stars,      retrievedRating.Stars);
		}
	}
}