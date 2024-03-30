using CodeChallenge.Data;

using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DataController : ControllerBase {
	private readonly InsertData _insertData;

	public DataController(CodeChallengeDbContext context) {
		_insertData = new InsertData(context);
	}

	[HttpPost("seed")]
	public IActionResult SeedCustomers() {
		_insertData.InsertAll();
		return Ok("Data inserted");
	}
}