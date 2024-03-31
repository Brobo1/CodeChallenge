using CodeChallenge.Data;

using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ADataController : ControllerBase {
	private readonly InsertData _insertData;

	public ADataController(CodeChallengeDbContext context) {
		_insertData = new InsertData(context);
	}

	[HttpPost("seed")]
	public IActionResult SeedCustomers() {
		_insertData.InsertAll();
		return Ok("Data inserted");
	}
}