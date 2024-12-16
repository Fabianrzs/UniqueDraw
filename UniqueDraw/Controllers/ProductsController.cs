using Microsoft.AspNetCore.Mvc;
using UniqueDraw.Domain.Services;
using UniqueDraw.Domain.Models.Response;
using UniqueDraw.Domain.Models.Create;

namespace UniqueDraw.API.Controllers
{
    /// <summary>
    /// Controlador para manejar operaciones relacionadas con productos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="productDto">Datos del producto a crear.</param>
        /// <returns>Producto creado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ProductResponseDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _productService.CreateProductAsync(productDto);
            return CreatedAtAction(nameof(GetProductsByClient), new { clientId = result.ClientId }, result);
        }

        /// <summary>
        /// Obtiene todos los productos de un cliente específico.
        /// </summary>
        /// <param name="clientId">Identificador único del cliente.</param>
        /// <returns>Lista de productos del cliente.</returns>
        [HttpGet("{clientId:guid}")]
        [ProducesResponseType(typeof(ICollection<ProductResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductsByClient(Guid clientId)
        {
            var result = await _productService.GetProductsByClientIdAsync(clientId);

            if (result == null || result.Count == 0)
            {
                return NotFound($"No se encontraron productos para el cliente con ID {clientId}.");
            }

            return Ok(result);
        }
    }
}
