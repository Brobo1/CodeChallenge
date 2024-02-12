using CodeChallenge.Models;

using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Data;

public class CodeChallengeDbContext : DbContext {
	public CodeChallengeDbContext(DbContextOptions<CodeChallengeDbContext> options)
		: base(options) { }

	public DbSet<Customer>     Customers     { get; set; }
	public DbSet<Order>        Orders        { get; set; }
	public DbSet<OrderProduct> OrderProducts { get; set; }
	public DbSet<Product>      Products      { get; set; }
	public DbSet<Rating>       Ratings       { get; set; }
}