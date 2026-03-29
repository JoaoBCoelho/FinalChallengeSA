using FinalChallengeSA.Application.Commands.Customers.CreateCustomer;
using FinalChallengeSA.Application.Commands.Customers.DeleteCustomer;
using FinalChallengeSA.Application.Commands.Customers.UpdateCustomer;
using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Queries.Customers.CountCustomers;
using FinalChallengeSA.Application.Queries.Customers.GetAllCustomers;
using FinalChallengeSA.Application.Queries.Customers.GetCustomerById;
using FinalChallengeSA.Application.Queries.Customers.GetCustomersByName;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinalChallengeSA.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>Cria um novo customer</summary>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CustomerRequest request)
        {
            var result = await _mediator.Send(new CreateCustomerCommand(request));
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>Atualiza um customer existente</summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] CustomerRequest request)
        {
            var result = await _mediator.Send(new UpdateCustomerCommand(id, request));
            return Ok(result);
        }

        /// <summary>Remove um customer</summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteCustomerCommand(id));
            return NoContent();
        }

        /// <summary>Lista todos os customers</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCustomersQuery());
            return Ok(result);
        }

        /// <summary>Busca customer por ID</summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery(id));
            return Ok(result);
        }

        /// <summary>Busca customers por nome</summary>
        [HttpGet("getbyname")]
        [ProducesResponseType(typeof(IEnumerable<CustomerResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var result = await _mediator.Send(new GetCustomersByNameQuery(name));
            return Ok(result);
        }

        /// <summary>Retorna a contagem total de customers</summary>
        [HttpGet("count")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var result = await _mediator.Send(new CountCustomersQuery());
            return Ok(result);
        }
    }
}
