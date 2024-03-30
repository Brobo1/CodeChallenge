namespace CodeChallenge.Models;

public class Category {
	public int                   Id       { get; set; }
	public string                Name     { get; set; } = string.Empty;
	public int                   Lft      { get; set; }
	public int                   Rgt      { get; set; }
	public ICollection<Product>? Products { get; set; }
	public ICollection<Category> Children { get; set; } = new List<Category>();

}