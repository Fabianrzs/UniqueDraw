using Microsoft.AspNetCore.Mvc;
using UniqueDraw.Domain.Services;
using UniqueDraw.Domain.Models.Response;
using UniqueDraw.Domain.Models.Create;

namespace UniqueDraw.API.Controllers
{
    /// <summary>
    /// Controlador para manejar operaciones relacionadas con sorteos (Raffles).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RafflesController : ControllerBase
    {
        private readonly RaffleService _raffleService;

        public RafflesController(RaffleService raffleService)
        {
            _raffleService = raffleService;
        }

        /// <summary>
        /// Crea un nuevo sorteo.
        /// </summary>
        /// <param name="raffleDto">Datos del sorteo a crear.</param>
        /// <returns>El sorteo creado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(RaffleResponseDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRaffle([FromBody] RaffleCreateDTO raffleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _raffleService.CreateRaffleAsync(raffleDto);
            return CreatedAtAction(nameof(GetActiveRafflesByClient), new { clientId = result.ClientId }, result);
        }

        /// <summary>
        /// Obtiene los sorteos activos de un cliente específico.
        /// </summary>
        /// <param name="clientId">Identificador único del cliente.</param>
        /// <returns>Lista de sorteos activos.</returns>
        [HttpGet("{clientId:guid}")]
        [ProducesResponseType(typeof(ICollection<RaffleResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetActiveRafflesByClient(Guid clientId)
        {
            var result = await _raffleService.GetActiveRafflesByClientIdAsync(clientId);

            if (result == null || result.Count == 0)
            {
                return NotFound($"No se encontraron sorteos activos para el cliente con ID {clientId}.");
            }

            return Ok(result);
        }
    }
}
