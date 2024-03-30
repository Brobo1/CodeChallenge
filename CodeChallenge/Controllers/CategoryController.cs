using CodeChallenge.Data;
using CodeChallenge.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase {
	private readonly CodeChallengeDbContext _context;

	public CategoryController(CodeChallengeDbContext context) {
		_context = context;
	}

	// [HttpGet]
	// public async Task<IActionResult> GetCategories() {
	// 	var customers = await _context.Categories.ToListAsync();
	// 	return Ok(customers);
	// }
	
	[HttpGet]
	public async Task<IActionResult> GetCategories() {
		var customers = await _context.Categories.ToListAsync();
		return Ok(customers);
	}

	[HttpGet("id/{id}")]
	public async Task<IActionResult> GetCategory(int id) {
		var customer = await _context.Categories.FindAsync(id);
		if (customer == null) {
			return NotFound();
		}
		return Ok(customer);
	}
	// [HttpGet("name/{name}")]
	// public async Task<IActionResult> GetCustomer(string name) {
	// 	var customer = await _context.Categories.SingleOrDefaultAsync(c => c.Name == name || c.Email == name);
	// 	if (customer == null) {
	// 		return NotFound();
	// 	}
	// 	return Ok(customer);
	// }

	[HttpPost]
	public async Task<IActionResult> CreateCategory(CategoryObject category) {
		Category newCategory = new() {
			Name = category.Name,
		};
		_context.Categories.Add(newCategory);
		await _context.SaveChangesAsync(CancellationToken.None);
		return CreatedAtRoute(new { id = newCategory.Id }, newCategory);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateCategory(int id, CategoryObject category) {
		var categoryToUpdate = await _context.Categories.FindAsync(id);
		if (categoryToUpdate == null) {
			return NotFound();
		}
		categoryToUpdate.Name = category.Name;
		await _context.SaveChangesAsync(CancellationToken.None);
		return Ok(categoryToUpdate);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteCategory(int id) {
		var category = await _context.Categories.FindAsync(id);
		if (category == null) {
			return NotFound();
		}
		_context.Categories.Remove(category);
		await _context.SaveChangesAsync(CancellationToken.None);
		return Ok();
	}
}

public class CategoryObject {
	public string Name { get; set; } = string.Empty;
}