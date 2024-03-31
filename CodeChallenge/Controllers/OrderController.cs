using CodeChallenge.Data;
using CodeChallenge.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase {
	private readonly CodeChallengeDbContext _context;

	public OrderController(CodeChallengeDbContext context) {
		_context = context;
	}

	[HttpGet]
	public async Task<IActionResult> GetOrders() {
		var orders = await _context.Orders.ToListAsync();
		return Ok(orders);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetOrder(int id) {
		var order = await _context.Orders.FindAsync(id);
		if (order == null) {
			return NotFound();
		}
		return Ok(order);
	}

	[HttpPost]
	public async Task<IActionResult> CreateOrder(OrderObject order) {
		Order newOrder = new() {
			CustomerId = order.CustomerId,
			Date       = order.Date,
		};
		_context.Orders.Add(newOrder);
		await _context.SaveChangesAsync();
		return CreatedAtRoute(new { id = newOrder.Id }, newOrder);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateOrder(int id, OrderObject order) {
		var orderToUpdate = await _context.Orders.FindAsync(id);
		if (orderToUpdate == null) {
			return NotFound();
		}
		orderToUpdate.CustomerId  = order.CustomerId;
		orderToUpdate.Date = order.Date;
		await _context.SaveChangesAsync();
		return Ok(orderToUpdate);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteOrder(int id) {
		var order = await _context.Orders.FindAsync(id);
		if (order == null) {
			return NotFound();
		}
		_context.Orders.Remove(order);
		await _context.SaveChangesAsync();
		return Ok();
	}
}

public class OrderObject {
	public int      CustomerId { get; set; }
	public DateTime Date       { get; set; }

}