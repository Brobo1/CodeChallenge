namespace CodeChallenge.Models;

public class Product {
	public int                       Id            { get; set; }
	public string                    Name          { get; set; }
	public int                       Price         { get; set; }
	public ICollection<Rating>       Ratings       { get; set; }
	public ICollection<OrderProduct> OrderProducts { get; set; }
}