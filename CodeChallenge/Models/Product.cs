using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Models;

public class Product {
	public int Id { get; set; }

	[StringLength(200)]
	public string Name { get; set; } = string.Empty;

	[StringLength(200)]
	public string Description { get; set; } = string.Empty;

	[StringLength(200)]
	public string Brand { get; set; } = string.Empty;

	[StringLength(200)]
	public string Image { get; set; } = string.Empty;

	public int Price      { get; set; }
	public int? CategoryId { get; set; }

	public ICollection<Rating>?          Ratings          { get; set; }
	public ICollection<OrderProduct>?    OrderProducts    { get; set; }
	public ICollection<WishlistProduct>? WishlistProducts { get; set; }
	public Category?                      Category         { get; set; }
}