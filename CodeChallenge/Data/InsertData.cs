using CodeChallenge.Models;

namespace CodeChallenge.Data;

public class InsertData {
	private readonly CodeChallengeDbContext _context;

	public InsertData(CodeChallengeDbContext context) {
		_context = context;
	}

	public void InsertAll() {
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

	private void InsertCategory() {
		void InsertNode(Category parent, Category newCategory) {
			var siblings = parent.Children;
			siblings = siblings.OrderBy(c => c.Rgt).ToList();
			var lastRightValue = siblings.LastOrDefault()?.Rgt ?? parent.Lft;

			newCategory.Lft = lastRightValue  + 1;
			newCategory.Rgt = newCategory.Lft + 1;

			foreach (var sibling in siblings.Where(s => s.Rgt > lastRightValue)) {
				sibling.Lft += 2;
				sibling.Rgt += 2;
			}

			parent.Children.Add(newCategory);
			_context.Update(parent);
			_context.SaveChanges();
		}
		
		var parentCategory = new Category { Name = "Electronics" };
		_context.Categories.Add(parentCategory);
		_context.SaveChanges();

		
		InsertNode(parentCategory, new Category{Name = "Laptop"});
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
	private void InsertProduct()         { }
	private void InsertRating()          { }
	private void InsertWishlist()        { }
	private void InsertWishlistProduct() { }
}