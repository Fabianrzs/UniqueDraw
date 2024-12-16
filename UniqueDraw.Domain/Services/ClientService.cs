using UniqueDraw.Domain.Entities.UniqueDraw;
using UniqueDraw.Domain.Models.Response;
using UniqueDraw.Domain.Ports;
using UniqueDraw.Domain.Ports.Persistence;
using UniqueDraw.Domain.Ports.Security;
using UniqueDraw.Domain.Ports.Helpers;
using UniqueDraw.Domain.Exceptions;
using UniqueDraw.Domain.Models.Create;
using UniqueDraw.Domain.Extensions;

namespace UniqueDraw.Domain.Services;

public class ClientService(
    IUnitOfWork unitOfWork, IRepository<Client> repository,
    IEncryptionService encryptionService, IPasswordHasher passwordHasher,
    ITokenService tokenService, IMappingService mapper)
{
    public async Task<ClientResponseDTO> CreateClientAsync(ClientCreateDTO request)
    {
        if(await repository.ExistsAsync(c => c.UserName == request.UserName))
            throw new BusinessRuleViolationException("El cliente ya existe.");

        var client = mapper.Map<Client>(request);
        client.EncryptProperties(encryptionService);
        client.Password = passwordHasher.HashPassword(request.Password!);
        client.ApiKey = tokenService.GenerateToken(request.Name!, client.Id);   

        await repository.AddAsync(client);
        await unitOfWork.CommitAsync();

        return mapper.Map<ClientResponseDTO>(client);
    }

    public async Task<string> RegenerateApiKeyAsync(Guid clientId)
    {
        var client = await repository.GetByIdAsync(clientId)
            ?? throw new NotFoundException("Cliente no encontrado.", clientId);

        client.ApiKey = tokenService.GenerateToken(encryptionService.Decrypt(client.Name!), client.Id);
        await unitOfWork.CommitAsync();

        return client.ApiKey;
    }

    public async Task<string> AuthenticateClientAsync(ClientLoginRequestDTO request)
    {
        var encryptedUserName = encryptionService.Encrypt(request.UserName);

        var client = (await repository.FindAsync(c => c.UserName == encryptedUserName)).FirstOrDefault()
            ?? throw new UnauthorizedException("Usuario o contraseña incorrecta.");

        if (!passwordHasher.VerifyPassword(client.Password!, request.Password))
            throw new UnauthorizedException("Usuario o contraseña incorrecta.");

        return tokenService.GenerateToken(request.UserName, client.Id);
    }

    public async Task<ClientResponseDTO> GetClientByIdAsync(Guid clientId)
    {
        var client = await repository.GetByIdAsync(clientId)
            ?? throw new NotFoundException("Cliente no encontrado.", clientId);

        client.DecryptProperties(encryptionService);
        return mapper.Map<ClientResponseDTO>(client);
    }
}
