using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Models;

public class Category {
	public int Id { get; init; }

	[StringLength(200)]
	public string Name { get; set; } = string.Empty;

	public ICollection<Product>? Products      { get; set; }
	public ICollection<Category> SubCategories { get; set; }
}