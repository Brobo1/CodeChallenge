namespace CodeChallenge.Models;

public class Order {
	public int      Id         { get; set; }
	public int      CustomerId { get; set; }
	public DateTime Date       { get; set; }

	public Customer?                  Customer      { get; set; }
	public ICollection<OrderProduct>? OrderProducts { get; set; }
}