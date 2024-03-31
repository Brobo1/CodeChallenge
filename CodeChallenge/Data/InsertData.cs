using CodeChallenge.Models;

using Microsoft.EntityFrameworkCore;

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
		_context.Categories.ExecuteDelete();
		_context.Customers.ExecuteDelete();
		_context.Products.ExecuteDelete();
		_context.Database.ExecuteSqlRaw("DELETE FROM sqlite_sequence WHERE name='OrderProducts';"    +
										"DELETE FROM sqlite_sequence WHERE name='WishlistProducts';" +
										"DELETE FROM sqlite_sequence WHERE name='Ratings';"          +
										"DELETE FROM sqlite_sequence WHERE name='Orders';"           +
										"DELETE FROM sqlite_sequence WHERE name='Wishlists';"        +
										"DELETE FROM sqlite_sequence WHERE name='Products';"         +
										"DELETE FROM sqlite_sequence WHERE name='Categories';"       +
										"DELETE FROM sqlite_sequence WHERE name='Customers';");
		// _context.Database.EnsureDeleted();
		// _context.Database.EnsureCreated();
	}

	private void InsertCategory() {
		void InsertNode(Category parentCategory, Category newCategory = null!) {
			if (newCategory == null) {
				
			}
			_context.Categories.Add(parentCategory);
			_context.SaveChanges();

			var parentId = _context.Categories.FirstOrDefault(c => c.Name == parentCategory.Name)?.Id;
		}

		InsertNode(new Category { Name = "Computer" });
		InsertNode(new Category { Name = "Computer" }, new Category { Name = "Laptop" });
		InsertNode(new Category { Name = "Video" },    new Category { Name = "Video" });
		InsertNode(new Category { Name = "Audio" },    new Category { Name = "Audio" });
		InsertNode(new Category { Name = "Audio" },    new Category { Name = "Audio" });

		/*InsertNode(new Category { Name = "Store" },      new Category { Name = "Phones" });
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
		InsertNode(new Category { Name = "Wearables" },  new Category { Name = "Smartwatch" });*/

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
				Name        = "PremiumPro Laptop",
				Description = "Top-notch laptop with advanced features, built for professionals.",
				Price       = 2599,
				Image       = "UrlToImage1",
				Brand       = "TechBrand",
			},
		};

		/*
		var products = new List<Product> {
			new() {
				Name        = "PremiumPro Laptop",
				Description = "Top-notch laptop with advanced features, built for professionals.",
				Price       = 2599,
				Image       = "UrlToImage1",
				Brand       = "TechBrand",
				CategoryId  = GetCategoryId("Laptop")
			},
			new() {
				Name        = "CompactBook Laptop",
				Description = "Sleek design and lightweight laptop, ideal for students and frequent travelers.",
				Price       = 749,
				Image       = "UrlToImage3",
				Brand       = "SlimBrand",
				CategoryId  = GetCategoryId("Laptop")
			},
			new() {
				Name        = "BasicPlus Laptop",
				Description = "Affordable laptop offering decent performance for everyday tasks.",
				Price       = 499,
				Image       = "UrlToImage4",
				Brand       = "ValueBrand",
				CategoryId  = GetCategoryId("Laptop")
			},
			new() {
				Name        = "HomeStation Laptop",
				Description = "Reliable and durable, excellent for home use and long-term tasks.",
				Price       = 829,
				Image       = "UrlToImage5",
				Brand       = "HomeBrand",
				CategoryId  = GetCategoryId("Laptop")
			},
			new() {
				Name        = "GamerX Laptop",
				Description = "High-performance laptop suitable for gaming and graphic-intensive tasks.",
				Price       = 1999,
				Image       = "UrlToImage2",
				Brand       = "GameBrand",
				CategoryId  = GetCategoryId("Gaming Laptop")
			},
			new() {
				Name        = "OfficeMax Desktop",
				Description = "Desktop designed for productivity, ideal for home or office use.",
				Price       = 2099,
				Image       = "UrlToImage2",
				Brand       = "OfficeBrand",
				CategoryId  = GetCategoryId("Desktop")
			},
			new() {
				Name        = "CreativePro Desktop",
				Description = "High performance desktop suitable for graphic designers and video editors.",
				Price       = 3999,
				Image       = "UrlToImage3",
				Brand       = "CreativeBrand",
				CategoryId  = GetCategoryId("Desktop")
			},
			new() {
				Name        = "ValueStation Desktop",
				Description = "Budget-friendly desktop for general daily use.",
				Price       = 799,
				Image       = "UrlToImage4",
				Brand       = "ValueBrand",
				CategoryId  = GetCategoryId("Desktop")
			},
			new() {
				Name        = "MiniHome Desktop",
				Description = "Compact and energy-efficient desktop ideal for basic tasks and multimedia.",
				Price       = 1299,
				Image       = "UrlToImage5",
				Brand       = "MiniBrand",
				CategoryId  = GetCategoryId("Desktop")
			},
			new() {
				Name        = "GamerZ Desktop",
				Description = "Powerful desktop optimized for gaming with high-spec graphics card.",
				Price       = 2999,
				Image       = "UrlToImage1",
				Brand       = "GameBrand",
				CategoryId  = GetCategoryId("Gaming Desktop")
			},
			new() {
				Name        = "PowerTower",
				Description = "High-performance tower with advanced capabilities for power users and professionals",
				Price       = 2999,
				Image       = "UrlToImage6",
				Brand       = "PowerBrand",
				CategoryId  = GetCategoryId("Tower")
			},
			new() {
				Name        = "HomeTower",
				Description = "Cost-efficient and reliable tower for everyday home usage",
				Price       = 1099,
				Image       = "UrlToImage7",
				Brand       = "HomeBrand",
				CategoryId  = GetCategoryId("Tower")
			},
			new() {
				Name        = "OfficeTower",
				Description = "Durable tower designed to handle big loads of work efficiently",
				Price       = 2099,
				Image       = "UrlToImage8",
				Brand       = "OfficeBrand",
				CategoryId  = GetCategoryId("Tower")
			},
			new() {
				Name        = "GamerTower",
				Description = "Powerful and fast tower optimized for latency-free gaming",
				Price       = 3499,
				Image       = "UrlToImage9",
				Brand       = "GameBrand",
				CategoryId  = GetCategoryId("Tower")
			},
			new() {
				Name        = "DesignerTower",
				Description = "High-end tower designed for graphic designers and creative professionals",
				Price       = 2999,
				Image       = "UrlToImage10",
				Brand       = "CreativeBrand",
				CategoryId  = GetCategoryId("Tower")
			},
			new() {
				Name        = "RamSpeed 8GB DDR4",
				Description = "8GB DDR4 RAM with 3200MHz speed for optimal performance",
				Price       = 79,
				Image       = "UrlToRAMImage1",
				Brand       = "RamSpeed",
				CategoryId  = GetCategoryId("RAM")
			},
			new() {
				Name        = "RamFast 16GB DDR4",
				Description = "16GB DDR4 RAM with 3600MHz speed, ideal for intensive tasks",
				Price       = 129,
				Image       = "UrlToRAMImage2",
				Brand       = "RamFast",
				CategoryId  = GetCategoryId("RAM")
			},
			new() {
				Name        = "RamPro 32GB DDR4",
				Description = "32GB DDR4 RAM with 3600MHz speed, designed for professional use",
				Price       = 229,
				Image       = "UrlToRAMImage3",
				Brand       = "RamPro",
				CategoryId  = GetCategoryId("RAM")
			},
			new() {
				Name        = "RamValue 8GB DDR3",
				Description = "Budget-friendly 8GB DDR3 RAM, suitable for everyday tasks",
				Price       = 49,
				Image       = "UrlToRAMImage4",
				Brand       = "RamValue",
				CategoryId  = GetCategoryId("RAM")
			},
			new() {
				Name        = "RamBasic 4GB DDR3",
				Description = "Cost-efficient 4GB DDR3 RAM, ideal for basic usage",
				Price       = 29,
				Image       = "UrlToRAMImage5",
				Brand       = "RamBasic",
				CategoryId  = GetCategoryId("RAM")
			}
		};
		*/
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