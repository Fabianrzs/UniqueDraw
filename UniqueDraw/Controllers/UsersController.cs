﻿using Microsoft.AspNetCore.Mvc;
using UniqueDraw.Domain.Services;
using UniqueDraw.Domain.Models.Response;
using UniqueDraw.Domain.Models.Create;
using UniqueDraw.Domain.Exceptions;

namespace UniqueDraw.API.Controllers
{
    /// <summary>
    /// Controlador para manejar operaciones relacionadas con usuarios.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        /// <param name="userDto">Datos del usuario a crear.</param>
        /// <returns>El usuario creado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _userService.CreateUserAsync(userDto);
                return CreatedAtAction(nameof(GetUserById), new { userId = result.Id }, result);
            }
            catch (BusinessRuleViolationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los usuarios de un cliente específico.
        /// </summary>
        /// <param name="clientId">Identificador único del cliente.</param>
        /// <returns>Lista de usuarios asociados al cliente.</returns>
        [HttpGet("client/{clientId:guid}")]
        [ProducesResponseType(typeof(ICollection<UserResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUsersByClient(Guid clientId)
        {
            var result = await _userService.GetUsersByClientIdAsync(clientId);

            if (result == null || result.Count == 0)
            {
                return NotFound($"No se encontraron usuarios para el cliente con ID {clientId}.");
            }

            return Ok(result);
        }

        /// <summary>
        /// Obtiene un usuario por su identificador único.
        /// </summary>
        /// <param name="userId">Identificador único del usuario.</param>
        /// <returns>Información del usuario encontrado.</returns>
        [HttpGet("{userId:guid}")]
        [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            try
            {
                var result = await _userService.GetUserByIdAsync(userId);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
