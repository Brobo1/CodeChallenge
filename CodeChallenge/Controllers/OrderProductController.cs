using CodeChallenge.Data;
using CodeChallenge.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderProductController : ControllerBase {
	private readonly CodeChallengeDbContext _context;

	public OrderProductController(CodeChallengeDbContext context) {
		_context = context;
	}

	[HttpGet]
	public async Task<IActionResult> GetOrderProducts() {
		var products = await _context.OrderProducts.ToListAsync();
		return Ok(products);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetOrderProduct(int id) {
		var product = await _context.OrderProducts.FindAsync(id);
		if (product == null) {
			return NotFound();
		}
		return Ok(product);
	}

	[HttpPost]
	public async Task<IActionResult> CreateOrderProduct(OrderProductObject orderProduct) {
		OrderProduct newOrderProduct = new() {
			OrderId   = orderProduct.OrderId,
			ProductId = orderProduct.ProductId,
		};
		_context.OrderProducts.Add(newOrderProduct);
		await _context.SaveChangesAsync();
		return CreatedAtRoute(new { id = newOrderProduct.Id }, newOrderProduct);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateOrderProduct(int id, OrderProductObject orderProduct) {
		var orderProductToUpdate = await _context.OrderProducts.FindAsync(id);
		if (orderProductToUpdate == null) {
			return NotFound();
		}
		orderProductToUpdate.OrderId  = orderProduct.OrderId;
		orderProductToUpdate.ProductId = orderProduct.ProductId;
		await _context.SaveChangesAsync();
		return Ok(orderProductToUpdate);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteOrderProduct(int id) {
		var orderProduct = await _context.OrderProducts.FindAsync(id);
		if (orderProduct == null) {
			return NotFound();
		}
		_context.OrderProducts.Remove(orderProduct);
		await _context.SaveChangesAsync();
		return Ok();
	}
}

public class OrderProductObject {
	public int ProductId { get; set; }
	public int OrderId   { get; set; }
}