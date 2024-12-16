using UniqueDraw.Domain.Entities.UniqueDraw;
using UniqueDraw.Domain.Ports.Persistence;
using UniqueDraw.Domain.Models.Response;
using UniqueDraw.Domain.Ports.Helpers;
using UniqueDraw.Domain.Models.Create;
using UniqueDraw.Domain.Attributes;

namespace UniqueDraw.Domain.Services;

[DomainService]
public class RaffleService(IUnitOfWork unitOfWork,
    IRepository<Raffle> repository,
    IMappingService mapper)
{
    public async Task<RaffleResponseDTO> CreateRaffleAsync(RaffleCreateDTO raffleDto)
    {
        var raffle = mapper.Map<Raffle>(raffleDto);
        await repository.AddAsync(raffle);
        await unitOfWork.CommitAsync();
        return mapper.Map<Raffle, RaffleResponseDTO>(raffle);
    }

    public async Task<ICollection<RaffleResponseDTO>> GetActiveRafflesByClientIdAsync(Guid clientId)
    {
        var now = DateTime.UtcNow;
        var raffles = await repository.FindAsync(r => r.ClientId == clientId && r.StartDate <= now && r.EndDate >= now);
        return mapper.Map<RaffleResponseDTO>(raffles);
    }
}
