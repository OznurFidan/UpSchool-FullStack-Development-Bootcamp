using Application.Feutures.Adresses.Commands.Add;
using Application.Feutures.Adresses.Queries.GetAll;
using Application.Feutures.Adresses.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ApiControllerBase
    {

        [HttpPost]
        public async Task<ActionResult> AddAsync(AddressAddCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("GetAll")]
        public async Task<ActionResult> GetAllAsync(AddressGetAllQuery query)
        {
            return Ok(await Mediator.Send(query));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await Mediator.Send(new AddressGetByIdQuery(id, null)));
        }
    }
}
