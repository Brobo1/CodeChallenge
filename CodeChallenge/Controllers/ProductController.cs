using CodeChallenge.Data;
using CodeChallenge.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase {
	private readonly CodeChallengeDbContext _context;

	public ProductController(CodeChallengeDbContext context) {
		_context = context;
	}

	[HttpGet]
	public async Task<IActionResult> GetProducts() {
		var products = await _context.Products.ToListAsync();
		return Ok(products);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetProduct(int id) {
		var product = await _context.Products.FindAsync(id);
		if (product == null) {
			return NotFound();
		}
		return Ok(product);
	}

	[HttpPost]
	public async Task<IActionResult> CreateProduct(ProductObject product) {
		Product newProduct = new() {
			Name  = product.Name,
			Price = product.Price,
		};
		_context.Products.Add(newProduct);
		await _context.SaveChangesAsync();
		return CreatedAtRoute(new { id = newProduct.Id }, newProduct);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateProduct(int id, ProductObject product) {
		var productToUpdate = await _context.Products.FindAsync(id);
		if (productToUpdate == null) {
			return NotFound();
		}
		productToUpdate.Name  = product.Name;
		productToUpdate.Price = product.Price;
		await _context.SaveChangesAsync();
		return Ok(productToUpdate);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteProduct(int id) {
		var product = await _context.Products.FindAsync(id);
		if (product == null) {
			return NotFound();
		}
		_context.Products.Remove(product);
		await _context.SaveChangesAsync();
		return Ok();
	}
}

public class ProductObject {
	public string Name  { get; set; }
	public int    Price { get; set; }
}