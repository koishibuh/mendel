using MediatR;
using Mendel.Core.Common;
using Mendel.Core.Services.FinalOutpost;
using Microsoft.AspNetCore.Mvc;

namespace Mendel.Core.Features.Review.Controllers;

/*══════════════════【 CONTROLLER 】══════════════════*/
[Route("api/review")]
public class ImportGrowingCreaturesController : ApiControllerBase
{
	[HttpPost]
	public async Task<ActionResult> ImportGrowingCreatures
	([FromBody] ImportGrowingCreaturesQuery query)
	{
		await Mediator.Send(query);
		 return Ok();
	}
}

/*═══════════════════【 HANDLER 】═══════════════════*/
public record ImportGrowingCreatureHandler(
IFinalOutpostService FinalOutpostService
) : IRequestHandler<ImportGrowingCreaturesQuery>
{
	public async Task Handle(ImportGrowingCreaturesQuery query, CancellationToken cancel)
	{
		var result = await FinalOutpostService.GetGrowingCreatures(query.ScientistName);
		// add to database
		// create vm for ui
	}
}


/*════════════════════【 QUERY 】════════════════════*/
public record ImportGrowingCreaturesQuery(
string ScientistName
) : IRequest;