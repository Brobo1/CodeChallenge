using CodeChallenge.Models;

using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Data;

public interface IApplicationDbContext {
	DbSet<Customer> Customers { get; set; }
	Task<int>       SaveChangesAsync(CancellationToken cancellationToken);
}