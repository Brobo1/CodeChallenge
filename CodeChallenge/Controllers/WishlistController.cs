using CodeChallenge.Data;
using CodeChallenge.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WishlistController : ControllerBase {
	private readonly CodeChallengeDbContext _context;

	public WishlistController(CodeChallengeDbContext context) {
		_context = context;
	}

	[HttpGet]
	public async Task<IActionResult> GetWishlists() {
		var wishlist = await _context.Wishlists.ToListAsync();
		return Ok(wishlist);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetWishlist(int id) {
		var wishlist = await _context.Wishlists.FindAsync(id);
		if (wishlist == null) {
			return NotFound();
		}
		return Ok(wishlist);
	}

	[HttpPost]
	public async Task<IActionResult> CreateWishlist(WishlistObject wishlist) {
		Wishlist newWishlist = new() {
			CustomerId = wishlist.CustomerId,
		};
		_context.Wishlists.Add(newWishlist);
		await _context.SaveChangesAsync();
		return CreatedAtRoute(new { id = newWishlist.Id }, newWishlist);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateWishlist(int id, WishlistObject wishlist) {
		var wishlistToUpdate = await _context.Wishlists.FindAsync(id);
		if (wishlistToUpdate == null) {
			return NotFound();
		}
		wishlistToUpdate.CustomerId = wishlist.CustomerId;
		await _context.SaveChangesAsync();
		return Ok(wishlistToUpdate);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteWishlist(int id) {
		var wishlist = await _context.Wishlists.FindAsync(id);
		if (wishlist == null) {
			return NotFound();
		}
		_context.Wishlists.Remove(wishlist);
		await _context.SaveChangesAsync();
		return Ok();
	}
}

public class WishlistObject {
	public int CustomerId { get; set; }
}