﻿using UniqueDraw.Domain.Entities.UniqueDraw;
using UniqueDraw.Domain.Models.Response;
using UniqueDraw.Domain.Ports.Persistence;
using UniqueDraw.Domain.Ports.Helpers;
using UniqueDraw.Domain.Exceptions;
using UniqueDraw.Domain.Models.Create;
using UniqueDraw.Domain.Attributes;
using UniqueDraw.Domain.Ports;
using UniqueDraw.Domain.Extensions;

namespace UniqueDraw.Domain.Services;

[DomainService]
public class UserService(IUnitOfWork unitOfWork, IEncryptionService encryptionService,  
    IRepository<User> repository, IMappingService mapper)
{
    public async Task<UserResponseDTO> CreateUserAsync(UserCreateDTO request)
    {
        if (await repository.ExistsAsync(u => u.Email == request.Email))
            throw new BusinessRuleViolationException("El usuario ya existe con el correo electrónico proporcionado.");

        var user = mapper.Map<User>(request);

        user.EncryptProperties(encryptionService);

        await repository.AddAsync(user);
        await unitOfWork.CommitAsync();

        user.DecryptProperties(encryptionService);

        return mapper.Map<UserResponseDTO>(user);
    }

    public async Task<ICollection<UserResponseDTO>> GetUsersByClientIdAsync(Guid clientId)
    {
        var users = await repository.FindAsync(u => u.ClientId == clientId);
        return mapper.Map<UserResponseDTO>(users);
    }

    public async Task<UserResponseDTO> GetUserByIdAsync(Guid userId)
    {
        var user = await repository.GetByIdAsync(userId)
            ?? throw new NotFoundException("Usuario no encontrado.", userId);

        return mapper.Map<UserResponseDTO>(user);
    }
}
