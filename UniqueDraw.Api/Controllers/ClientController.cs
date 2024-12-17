using Microsoft.AspNetCore.Mvc;
using UniqueDraw.Domain.Services;
using UniqueDraw.Domain.Models.Create;
using UniqueDraw.Domain.Models.Response;
using UniqueDraw.Domain.Models.Request;

namespace UniqueDraw.API.Controllers
{
    /// <summary>
    /// Controlador para manejar operaciones relacionadas con los clientes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController(ClientService clientService) : ControllerBase
    {
        /// <summary>
        /// Crea un nuevo cliente.
        /// </summary>
        /// <param name="request">Datos del cliente a crear.</param>
        /// <returns>El cliente creado con su información.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ClientResponseDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateClient([FromBody] ClientCreateDTO request)
        {
            var client = await clientService.CreateClientAsync(request);
            return CreatedAtAction(nameof(GetClientById), new { clientId = client.Id }, client);
        }

        /// <summary>
        /// Regenera la clave API de un cliente.
        /// </summary>
        /// <param name="clientId">Identificador único del cliente.</param>
        /// <returns>La nueva clave API generada.</returns>
        [HttpPut("{clientId:guid}/regenerate-api-key")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RegenerateApiKey(Guid clientId)
        {
            var apiKey = await clientService.RegenerateApiKeyAsync(clientId);
            return Ok(apiKey);
        }

        /// <summary>
        /// Autentica a un cliente usando su nombre de usuario y contraseña.
        /// </summary>
        /// <param name="request">Credenciales del cliente.</param>
        /// <returns>Un token de autenticación si las credenciales son correctas.</returns>
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AuthenticateClient([FromBody] ClientLoginRequestDTO request)
        {
            var token = await clientService.AuthenticateClientAsync(request);
            return Ok(token);
        }

        /// <summary>
        /// Autentica a un cliente usando su Api key.
        /// </summary>
        /// <param name="request">Credenciales del cliente.</param>
        /// <returns>Un token de autenticación si las credenciales son correctas.</returns>
        [HttpPost("{apiKey}/authenticate")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AuthenticateClient(string apiKey)
        {
            var token = await clientService.AuthenticateClientAsync(apiKey);
            return Ok(token);
        }

        /// <summary>
        /// Obtiene un cliente por su identificador único.
        /// </summary>
        /// <param name="clientId">Identificador único del cliente.</param>
        /// <returns>Los datos del cliente.</returns>
        [HttpGet("{clientId:guid}")]
        [ProducesResponseType(typeof(ClientResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetClientById(Guid clientId)
        {
            var client = await clientService.GetClientByIdAsync(clientId);
            return Ok(client);
        }
    }
}
