using Xunit;

using FakeItEasy;

using Microsoft.EntityFrameworkCore;

using CodeChallenge.Data;
using CodeChallenge.Controllers;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using System.Linq;
using System.Collections.Generic;

using CodeChallenge.Models;

public class CustomerControllerTests {
	private readonly DbContextOptions<CodeChallengeDbContext> _options;

	public CustomerControllerTests() {
		_options = new DbContextOptionsBuilder<CodeChallengeDbContext>()
				   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				   .Options;
	}

	[Fact]
	public async Task GetCustomers_ReturnCustomers() {
		// Use a clean instance of context for each test
		using (var context = new CodeChallengeDbContext(_options)) {
			context.Customers.Add(new Customer { Name = "Test 1", Email = "123@123.com" });
			context.Customers.Add(new Customer { Name = "Test 2", Email = "132@132.com" });
			await context.SaveChangesAsync();

			var controller = new CustomerController(context);

			// Act
			var result = await controller.GetCustomers() as OkObjectResult;

			// Assert
			Assert.NotNull(result);
			var customers = result.Value as List<Customer>;
			Assert.Collection(
				customers,
				customer => Assert.Equal("Test 1", customer.Name),
				customer => Assert.Equal("Test 2", customer.Name)
				);
		}
	}

	[Fact]
	public async Task CreateCustomer_GetRows() {
		// Use a clean instance of context for each test
		using (var context = new CodeChallengeDbContext(_options)) {
			context.Customers.Add(new Customer { Name = "Test 1", Email = "123@123.com" });
			context.Customers.Add(new Customer { Name = "Test 2", Email = "132@132.com" });

			await context.SaveChangesAsync();

			var controller = new CustomerController(context);

			await controller.CreateUser(new() { Name = "Test 3", Email = "are@are.com" });
			
			// Act
			var result = await controller.GetCustomers() as OkObjectResult;

			// Assert
			Assert.NotNull(result);
			var customers = result.Value as List<Customer>;
			Assert.Equal(3, customers.Count);
		}
	}	
	
	[Fact]
	public async Task UpdateCustomer_GetRow() {
		// Use a clean instance of context for each test
		using (var context = new CodeChallengeDbContext(_options)) {
			context.Customers.Add(new Customer { Name = "Test 1", Email = "123@123.com" });
			context.Customers.Add(new Customer { Name = "Test 2", Email = "132@132.com" });

			await context.SaveChangesAsync();

			var controller = new CustomerController(context);

			await controller.CreateUser(new() { Name = "Test 3", Email = "are@are.com" });
			
			// Act
			var result = await controller.GetCustomers() as OkObjectResult;

			// Assert
			Assert.NotNull(result);
			var customers = result.Value as List<Customer>;
			Assert.Equal(3, customers.Count);
		}
	}
	
	[Fact]
	public async Task DeleteCustomer_GetRows() {
		// Use a clean instance of context for each test
		using (var context = new CodeChallengeDbContext(_options)) {
			context.Customers.Add(new Customer { Name = "Test 1", Email = "123@123.com" });
			context.Customers.Add(new Customer { Name = "Test 2", Email = "132@132.com" });

			await context.SaveChangesAsync();

			var controller = new CustomerController(context);

			await controller.DeleteUser(1);
			
			// Act
			var result = await controller.GetCustomers() as OkObjectResult;

			// Assert
			Assert.NotNull(result);
			var customers = result.Value as List<Customer>;
			Assert.Equal(1, customers.Count);
		}
	}

	[Fact]
	public async Task GetCustomers_ReturnCustomer() {
		// Use a clean instance of context for each test
		using (var context = new CodeChallengeDbContext(_options)) {
			context.Customers.Add(new Customer { Name = "Test 1", Email = "123@123.com" });
			context.Customers.Add(new Customer { Name = "Test 2", Email = "132@132.com" });
			await context.SaveChangesAsync();

			var controller = new CustomerController(context);

			// Act
			var result = await controller.GetCustomer(1) as OkObjectResult;

			// Assert
			Assert.NotNull(result);
			var customer = result.Value as Customer;
			Assert.Equal("Test 1", customer.Name);
		}
	}
}