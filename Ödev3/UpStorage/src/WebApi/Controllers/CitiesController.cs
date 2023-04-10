using Application.Feutures.Cities.Commands.Add;
using Application.Feutures.Cities.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{

    public class CitiesController :ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> AddAsync(CityAddCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("GetAll")]
        public async Task<ActionResult> GetAllAsync(CityGetAllQuery query)
        {
            return Ok(await Mediator.Send(query));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await Mediator.Send(new CityGetAllQuery(id, null)));
        }
    }
}
