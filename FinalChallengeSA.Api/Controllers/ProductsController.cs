using FinalChallengeSA.Application.Commands.Products.CreateProduct;
using FinalChallengeSA.Application.Commands.Products.DeleteProduct;
using FinalChallengeSA.Application.Commands.Products.UpdateProduct;
using FinalChallengeSA.Application.DTOs;
using FinalChallengeSA.Application.Queries.Products.CountProducts;
using FinalChallengeSA.Application.Queries.Products.GetAllProducts;
using FinalChallengeSA.Application.Queries.Products.GetProductById;
using FinalChallengeSA.Application.Queries.Products.GetProductsByName;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinalChallengeSA.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>Cria um novo produto</summary>
        [HttpPost]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ProductRequest request)
        {
            var result = await _mediator.Send(new CreateProductCommand(request));
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>Atualiza um produto existente</summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductRequest request)
        {
            var result = await _mediator.Send(new UpdateProductCommand(id, request));
            return Ok(result);
        }

        /// <summary>Remove um produto</summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }

        /// <summary>Lista todos os produtos</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            return Ok(result);
        }

        /// <summary>Busca produto por ID</summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(result);
        }

        /// <summary>Busca produtos por nome</summary>
        [HttpGet("getbyname")]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var result = await _mediator.Send(new GetProductsByNameQuery(name));
            return Ok(result);
        }

        /// <summary>Retorna a contagem total de produtos</summary>
        [HttpGet("count")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var result = await _mediator.Send(new CountProductsQuery());
            return Ok(result);
        }
    }
}
