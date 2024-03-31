using CodeChallenge.Models;

using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Data;

public class CodeChallengeDbContext : DbContext, IApplicationDbContext {
	public CodeChallengeDbContext(DbContextOptions<CodeChallengeDbContext> options)
		: base(options) { }

	public DbSet<Customer>        Customers        { get; set; }
	public DbSet<Order>           Orders           { get; set; }
	public DbSet<Category>        Categories       { get; set; }
	public DbSet<OrderProduct>    OrderProducts    { get; set; }
	public DbSet<Product>         Products         { get; set; }
	public DbSet<Rating>          Ratings          { get; set; }
	public DbSet<Wishlist>        Wishlists        { get; set; }
	public DbSet<WishlistProduct> WishlistProducts { get; set; }
}