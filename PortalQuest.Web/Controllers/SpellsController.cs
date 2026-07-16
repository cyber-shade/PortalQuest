using MediatR;
using Microsoft.AspNetCore.Mvc;
using PortalQuest.Application.DTOs.Core.Spells;
using PortalQuest.Application.Features.Core.Spell.Query;

namespace PortalQuest.Web.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class SpellsController(IMediator mediator) : ControllerBase
	{
		[HttpGet("{id:guid}")]
		public async Task<IActionResult> Get(Guid id)
		{
			return Ok();
		}
		[HttpGet("")]
		public async Task<IActionResult> GetAll([FromQuery] GetAllSpellsRequest request, CancellationToken cancellationToken)
		{
			var result = await mediator.Send(
				request,
				cancellationToken
			);
			return Ok(result);
		}
	}
}
