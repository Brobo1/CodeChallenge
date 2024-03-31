/*
using System.Collections.Generic;

//Nested set Model

#region Model

public class Category {
	public         int                    Id            { get; set; }
	public         string                 Name          { get; set; } = string.Empty;
	public         int                    Lft           { get; set; }
	public         int                    Rgt           { get; set; }
	public         ICollection<Product>?  Products      { get; set; }
	public virtual ICollection<Category>? SubCategories { get; set; }
}

#endregion

//Nested set endpoints

#region Endpoints

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
public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string categoryName) {
	// Get the specified category
	var category = await _context.Categories.AsNoTracking()
								 .FirstOrDefaultAsync(c => c.Name == categoryName);

	if (category == null) {
		return NotFound("Category not found");
	}

	// Get all categories that fall within the specified category
	var subCategories = await _context.Categories.AsNoTracking()
									  .Where(c => c.Lft >= category.Lft && c.Rgt <= category.Rgt)
									  .ToListAsync();

	// Get all products linked to these categories
	var products = new List<Product>();
	foreach (var subCategory in subCategories) {
		var categoryProducts = await _context.Products.AsNoTracking()
											 .Where(p => p.CategoryId == subCategory.Id).ToListAsync();
		products.AddRange(categoryProducts);
	}

	return Ok(products);
}

#endregion

//Nested set insert data

#region Insert Data

void InsertNode(Category parentCategory, Category newCategory) {
	var parent = _context.Categories
						 .FirstOrDefault(c => c.Name == parentCategory.Name);
	if (parent == null) {
		parentCategory.Lft = 1;
		parentCategory.Rgt = 2;

		_context.Categories.Add(parentCategory);
		_context.SaveChanges();
		parent = parentCategory;
	}

	newCategory.Lft = parent.Rgt;
	newCategory.Rgt = newCategory.Lft + 1;

	foreach (var node in _context.Categories.Where(n => n.Rgt >= newCategory.Lft)) {
		node.Rgt += 2;
	}

	foreach (var node in _context.Categories.Where(n => n.Lft > newCategory.Lft)) {
		node.Lft += 2;
	}

	_context.Categories.Add(newCategory);

	parent.Rgt = newCategory.Rgt + 1;
	_context.SaveChanges();
}

InsertNode(new Category { Name = "Store" },      new Category { Name = "Computer" });
InsertNode(new Category { Name = "Store" },      new Category { Name = "Video" });
InsertNode(new Category { Name = "Store" },      new Category { Name = "Audio" });
InsertNode(new Category { Name = "Store" },      new Category { Name = "Phones" });
InsertNode(new Category { Name = "Store" },      new Category { Name = "Wearables" });
InsertNode(new Category { Name = "Computer" },   new Category { Name = "Laptop" });
InsertNode(new Category { Name = "Laptop" },     new Category { Name = "Gaming Laptop" });
InsertNode(new Category { Name = "Computer" },   new Category { Name = "Desktop" });
InsertNode(new Category { Name = "Desktop" },    new Category { Name = "Gaming Desktop" });
InsertNode(new Category { Name = "Desktop" },    new Category { Name = "Workstation" });
InsertNode(new Category { Name = "Desktop" },    new Category { Name = "Components" });
InsertNode(new Category { Name = "Components" }, new Category { Name = "Tower" });
InsertNode(new Category { Name = "Components" }, new Category { Name = "Mommyboard" });
InsertNode(new Category { Name = "Components" }, new Category { Name = "RAM" });
InsertNode(new Category { Name = "Components" }, new Category { Name = "CPU" });
InsertNode(new Category { Name = "Components" }, new Category { Name = "GPU" });
InsertNode(new Category { Name = "Components" }, new Category { Name = "PSU" });
InsertNode(new Category { Name = "Video" },      new Category { Name = "Screens" });
InsertNode(new Category { Name = "Video" },      new Category { Name = "TVs" });
InsertNode(new Category { Name = "Video" },      new Category { Name = "Projector" });
InsertNode(new Category { Name = "Audio" },      new Category { Name = "Speakers" });
InsertNode(new Category { Name = "Wearables" },  new Category { Name = "Smartwatch" });

_context.SaveChanges();

#endregion

*/

