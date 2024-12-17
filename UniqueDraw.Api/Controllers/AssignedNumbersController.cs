using Microsoft.AspNetCore.Mvc;
using UniqueDraw.Domain.Services;
using UniqueDraw.Domain.Models.Request;
using UniqueDraw.Domain.Models.Response;

namespace UniqueDraw.API.Controllers;

/// <summary>
/// Controlador para manejar operaciones relacionadas con los números asignados.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AssignedNumbersController(AssignedNumberService assignedNumberService) : ControllerBase
{
    /// <summary>
    /// Asigna un número único a un cliente en un sorteo.
    /// </summary>
    /// <param name="requestDto">Datos de la solicitud para asignar un número.</param>
    /// <returns>El número asignado con su información.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(AssignedNumberResponseDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AssignNumber([FromBody] AssignedNumberRequestDTO requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await assignedNumberService.AssignNumberAsync(requestDto);
        return CreatedAtAction(nameof(GetAssignedNumbersByClient), new { clientId = result.ClientId }, result);
    }

    /// <summary>
    /// Obtiene los números asignados a un cliente específico.
    /// </summary>
    /// <param name="clientId">Identificador único del cliente.</param>
    /// <returns>Lista de números asignados.</returns>
    [HttpGet("{clientId:guid}")]
    [ProducesResponseType(typeof(ICollection<AssignedNumberResponseDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAssignedNumbersByClient(Guid clientId)
    {
        var result = await assignedNumberService.GetAssignedNumbersByClientAsync(clientId);

        if (result == null || result.Count == 0)
        {
            return NotFound($"No se encontraron números asignados para el cliente con ID {clientId}.");
        }

        return Ok(result);
    }
}
