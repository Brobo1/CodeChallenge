using CodeChallenge.Data;
using CodeChallenge.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RatingController : ControllerBase {
	private readonly CodeChallengeDbContext _context;

	public RatingController(CodeChallengeDbContext context) {
		_context = context;
	}

	[HttpGet]
	public async Task<IActionResult> GetRatings() {
		var ratings = await _context.Ratings.ToListAsync();
		return Ok(ratings);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetRating(int id) {
		var rating = await _context.Ratings.FindAsync(id);
		if (rating == null) {
			return NotFound();
		}
		return Ok(rating);
	}

	[HttpPost]
	public async Task<IActionResult> CreateRating(RatingObject rating) {
		Rating newRating = new() {
			CustomerId  = rating.CustomerId,
			ProductId  = rating.ProductId,
			Stars  = rating.Stars,
		};
		_context.Ratings.Add(newRating);
		await _context.SaveChangesAsync();
		return CreatedAtRoute(new { id = newRating.Id }, newRating);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateRating(int id, RatingObject rating) {
		var ratingToUpdate = await _context.Ratings.FindAsync(id);
		if (ratingToUpdate == null) {
			return NotFound();
		}
		ratingToUpdate.CustomerId  = rating.CustomerId;
		ratingToUpdate.ProductId = rating.ProductId;
		ratingToUpdate.Stars = rating.Stars;
		await _context.SaveChangesAsync();
		return Ok(ratingToUpdate);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteRating(int id) {
		var rating = await _context.Ratings.FindAsync(id);
		if (rating == null) {
			return NotFound();
		}
		_context.Ratings.Remove(rating);
		await _context.SaveChangesAsync();
		return Ok();
	}
}

public class RatingObject {
	public int CustomerId { get; set; }
	public int ProductId  { get; set; }
	public int Stars      { get; set; }
}