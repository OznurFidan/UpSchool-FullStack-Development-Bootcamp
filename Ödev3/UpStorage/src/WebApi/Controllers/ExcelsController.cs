using Application.Feutures.Exel.Comments.ReadCities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ExcelsController : ApiControllerBase
    {
        [HttpPost ("ReadCities")]
        public async Task<ActionResult> ReadCitiesAsync(ExcelReadCitiesCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
