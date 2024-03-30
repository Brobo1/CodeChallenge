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
		void PopulateSubcategories(Category parent, List<Category> categories) {
			parent.SubCategories = categories
								   .Where(c => c.Lft > parent.Lft &&
											   c.Rgt < parent.Rgt &&
											   !categories.Any(
												   p => p.Lft    > parent.Lft &&
														p.Rgt    < parent.Rgt
														&& p.Lft < c.Lft &&
														p.Rgt > c
															.Rgt))
								   .ToList();

			foreach (var subCategory in parent.SubCategories) {
				PopulateSubcategories(subCategory, categories);
			}
		}

		var categories = await _context.Categories.OrderBy(c => c.Lft).ToListAsync();

		// We need to find the root category (assuming it has Lft = 1)
		var rootCategory = categories.FirstOrDefault(c => c.Lft == 1);
		if (rootCategory == null) {
			return NotFound("No root category found.");
		}

		PopulateSubcategories(rootCategory, categories);

		return Ok(rootCategory);
	}

	[HttpGet("{categoryName}")]
	public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string categoryName) 
	{
		// Get the specified category
		var category = await _context.Categories.AsNoTracking()
									 .FirstOrDefaultAsync(c => c.Name == categoryName);

		if (category == null)
		{
			return NotFound("Category not found");
		}

		// Get all categories that fall within the specified category
		var subCategories = await _context.Categories.AsNoTracking()
										  .Where(c => c.Lft >= category.Lft && c.Rgt <= category.Rgt)
										  .ToListAsync();
    
		// Get all products linked to these categories
		var products = new List<Product>();
		foreach (var subCategory in subCategories)
		{
			var categoryProducts = await _context.Products.AsNoTracking()
												 .Where(p => p.CategoryId == subCategory.Id).ToListAsync();
			products.AddRange(categoryProducts);
		}

		return Ok(products);
	}

	
	
	[HttpGet("id/{id}")]
	public async Task<IActionResult> GetCategory(int id) {
		var category = await _context.Categories.FindAsync(id);
		if (category == null) {
			return NotFound();
		}
		return Ok(category);
	}
	// [HttpGet("name/{name}")]
	// public async Task<IActionResult> GetCustomer(string name) {
	// 	var category = await _context.Categories.SingleOrDefaultAsync(c => c.Name == name || c.Email == name);
	// 	if (category == null) {
	// 		return NotFound();
	// 	}
	// 	return Ok(category);
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