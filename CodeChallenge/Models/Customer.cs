namespace CodeChallenge.Models;

public class Customer {
	public int                 Id       { get; set; }
	public string              Name     { get; set; }
	public string              Email    { get; set; }
	public ICollection<Order>?  Orders   { get; set; }
	public ICollection<Rating>? Ratings  { get; set; }
	public Wishlist?            Wishlist { get; set; }

}