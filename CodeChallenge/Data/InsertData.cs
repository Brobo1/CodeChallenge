using CodeChallenge.Models;

namespace CodeChallenge.Data;

public class InsertData {
	private readonly CodeChallengeDbContext _context;

	public InsertData(CodeChallengeDbContext context) {
		_context = context;
	}

	public void InsertAll() {
		ResetDatabase();
		InsertCategory();
		InsertCustomers();
		InsertOrder();
		InsertOrderProduct();
		InsertProduct();
		InsertRating();
		InsertWishlist();
		InsertWishlistProduct();

		_context.SaveChanges();
	}

	public void ResetDatabase() {
		_context.Database.EnsureDeleted();
		_context.Database.EnsureCreated();
	}

	private void InsertCategory() {
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

		InsertNode(new Category { Name = "Electronics" }, new Category { Name = "Mobile" });
		InsertNode(new Category { Name = "Electronics" }, new Category { Name = "Computers" });
		InsertNode(new Category { Name = "Computers" },   new Category { Name = "Laptop" });
		InsertNode(new Category { Name = "Computers" },   new Category { Name = "Components" });
		InsertNode(new Category { Name = "Components" },  new Category { Name = "RAM" });
		InsertNode(new Category { Name = "Components" },  new Category { Name = "CPU" });
		InsertNode(new Category { Name = "Components" },  new Category { Name = "HDD" });
		InsertNode(new Category { Name = "Components" },  new Category { Name = "PSU" });

		_context.SaveChanges();
	}

	private void InsertProduct() {
		int GetCategoryId(string name) {
			var category = _context.Categories.FirstOrDefault(c => c.Name == name);
			if (category == null) {
				throw new Exception("Category not found");
			}
			return category.Id;
		}

		var products = new List<Product> {
			new() {
				Name        = "Flaptop",
				Description = "very stronk laptopf",
				Price       = 999999,
				CategoryId  = GetCategoryId("Laptop")
			},
			new() {
				Name        = "Superram",
				Description = "very stronk ram",
				Price       = 999999,
				CategoryId  = GetCategoryId("RAM")
			},
		};
		_context.Products.AddRange(products);
	}

	private void InsertCustomers() {
		var customers = new List<Customer> {
			new() { Name = "John Doe", Email       = "john.doe@example.com" },
			new() { Name = "Jane Doe", Email       = "jane.doe@example.com" },
			new() { Name = "Alice Smith", Email    = "alice.smith@example.com" },
			new() { Name = "Bob Johnson", Email    = "bob. johnson@example.com" },
			new() { Name = "Charlie Brown", Email  = "charlie.brown@example.com" },
			new() { Name = "David Williams", Email = "david.williams@example.com" }
		};

		_context.Customers.AddRange(customers);
	}

	private void InsertOrder()           { }
	private void InsertOrderProduct()    { }
	private void InsertRating()          { }
	private void InsertWishlist()        { }
	private void InsertWishlistProduct() { }
}