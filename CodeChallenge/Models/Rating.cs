namespace CodeChallenge.Models;

public class Rating {
	public int Id         { get; set; }
	public int CustomerId { get; set; }
	public int ProductId  { get; set; }
	public int Stars      { get; set; }

	public Customer? Customer { get; set; }
	public Product?  Product  { get; set; }
}