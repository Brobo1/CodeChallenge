using CodeChallenge.Data;
using CodeChallenge.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase {
	private readonly CodeChallengeDbContext _context;

	public CustomerController(CodeChallengeDbContext context) {
		_context = context;
	}
	
	[HttpGet]
	public async Task<IActionResult> GetCustomers() {
		var customers = await _context.Customers.ToListAsync();
		return Ok(customers);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetCustomer(int id) {
		var customer = await _context.Customers.FindAsync(id);
		if (customer == null) {
			return NotFound();
		}
		return Ok(customer);
	}
	
	[HttpPost]
	public async Task<IActionResult> CreateUser(CustomerObject customer) {
		Customer newCustomer = new() {
			Name  = customer.Name,
			Email = customer.Email
		};
		_context.Customers.Add(newCustomer);
		await _context.SaveChangesAsync();
		return CreatedAtRoute(new { id = newCustomer.Id }, newCustomer);
	}
	
	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateUser(int id, CustomerObject customer) {
		var customerToUpdate = await _context.Customers.FindAsync(id);
		if (customerToUpdate == null) {
			return NotFound();
		}
		customerToUpdate.Name  = customer.Name;
		customerToUpdate.Email = customer.Email;
		await _context.SaveChangesAsync();
		return Ok(customerToUpdate);
	}
	
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteUser(int id) {
		var customer = await _context.Customers.FindAsync(id);
		if (customer == null) {
			return NotFound();
		}
		_context.Customers.Remove(customer);
		await _context.SaveChangesAsync();
		return Ok();
	}
	
}

public class CustomerObject {
	public string Name  { get; set; }
	public string Email { get; set; }
}