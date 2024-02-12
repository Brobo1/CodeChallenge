using CodeChallenge.Data;
using CodeChallenge.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WishlistProductController : ControllerBase {
	private readonly CodeChallengeDbContext _context;

	public WishlistProductController(CodeChallengeDbContext context) {
		_context = context;
	}

	[HttpGet]
	public async Task<IActionResult> GetWishlistProducts() {
		var wishlistProduct = await _context.WishlistProducts.ToListAsync();
		return Ok(wishlistProduct);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetWishlistProduct(int id) {
		var wishlistProduct = await _context.WishlistProducts.FindAsync(id);
		if (wishlistProduct == null) {
			return NotFound();
		}
		return Ok(wishlistProduct);
	}

	[HttpPost]
	public async Task<IActionResult> CreateWishlistProduct(WishlistProductObject wishlistProduct) {
		WishlistProduct newWishlistProduct = new() {
			WishlistId = wishlistProduct.WishlistId,
			ProductId  = wishlistProduct.ProductId
		};
		_context.WishlistProducts.Add(newWishlistProduct);
		await _context.SaveChangesAsync();
		return CreatedAtRoute(new { id = newWishlistProduct.Id }, newWishlistProduct);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateWishlistProduct(int id, WishlistProductObject wishlistProduct) {
		var wishlistProductToUpdate = await _context.WishlistProducts.FindAsync(id);
		if (wishlistProductToUpdate == null) {
			return NotFound();
		}
		wishlistProductToUpdate.ProductId  = wishlistProduct.ProductId;
		wishlistProductToUpdate.WishlistId = wishlistProduct.WishlistId;
		await _context.SaveChangesAsync();
		return Ok(wishlistProductToUpdate);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteWishlistProduct(int id) {
		var wishlistProduct = await _context.WishlistProducts.FindAsync(id);
		if (wishlistProduct == null) {
			return NotFound();
		}
		_context.WishlistProducts.Remove(wishlistProduct);
		await _context.SaveChangesAsync();
		return Ok();
	}
}

public class WishlistProductObject {
	public int WishlistId { get; set; }
	public int ProductId  { get; set; }
}