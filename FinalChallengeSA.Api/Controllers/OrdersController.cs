using FinalChallengeSA.Application.Commands.Orders.CreateOrder;
using FinalChallengeSA.Application.Commands.Orders.DeleteOrder;
using FinalChallengeSA.Application.Commands.Orders.UpdateOrder;
using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Queries.Orders.CountOrders;
using FinalChallengeSA.Application.Queries.Orders.GetAllOrders;
using FinalChallengeSA.Application.Queries.Orders.GetOrderById;
using FinalChallengeSA.Application.Queries.Orders.GetOrdersByCustomerIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinalChallengeSA.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>Cria um novo pedido</summary>
        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] OrderRequest request)
        {
            var result = await _mediator.Send(new CreateOrderCommand(request));
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>Atualiza um pedido existente</summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] OrderRequest request)
        {
            var result = await _mediator.Send(new UpdateOrderCommand(id, request));
            return Ok(result);
        }

        /// <summary>Remove um pedido</summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteOrderCommand(id));
            return NoContent();
        }

        /// <summary>Lista todos os pedidos</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllOrdersQuery());
            return Ok(result);
        }

        /// <summary>Busca pedido por ID</summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery(id));
            return Ok(result);
        }

        /// <summary>Busca pedidos por CustomerId</summary>
        [HttpGet("by-customer/{customerId:guid}")]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> FindByCustomerId(Guid customerId)
        {
            var result = await _mediator.Send(new GetOrdersByCustomerIdQuery(customerId));
            return Ok(result);
        }

        /// <summary>Retorna a contagem total de pedidos</summary>
        [HttpGet("count")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var result = await _mediator.Send(new CountOrdersQuery());
            return Ok(result);
        }
    }
}
