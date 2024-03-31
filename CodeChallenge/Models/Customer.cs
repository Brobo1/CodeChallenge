using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Models;

public class Customer {
	public int Id { get; set; }

	[StringLength(200)]
	public string Name { get; set; } = string.Empty;

	[StringLength(200)]
	public string Email { get; set; } = string.Empty;

	public ICollection<Order>?  Orders   { get; set; }
	public ICollection<Rating>? Ratings  { get; set; }
	public Wishlist?            Wishlist { get; set; }
}