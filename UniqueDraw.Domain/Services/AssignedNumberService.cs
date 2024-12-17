using UniqueDraw.Domain.Entities.UniqueDraw;
using UniqueDraw.Domain.Ports.Persistence;
using UniqueDraw.Domain.Models.Response;
using UniqueDraw.Domain.Models.Request;
using UniqueDraw.Domain.Ports.Helpers;
using UniqueDraw.Domain.Attributes;

namespace UniqueDraw.Domain.Services;
[DomainService]
public class AssignedNumberService(IUnitOfWork unitOfWork,
    IRepository<AssignedNumber> repository,
    IMappingService mapper)
{
    public async Task<AssignedNumberResponseDTO> AssignNumberAsync(AssignedNumberRequestDTO requestDto)
    {
        var existingNumbers = await repository.FindAsync(an => an.ClientId == requestDto.ClientId && an.RaffleId == requestDto.RaffleId);

        var assignedNumber = mapper.Map<AssignedNumber>(requestDto);

        mapper.Map<AssignedNumberRequestDTO>(requestDto);

        assignedNumber.AssignUniqueNumber(existingNumbers);

        await repository.AddAsync(assignedNumber);
        await unitOfWork.CommitAsync();

        return mapper.Map<AssignedNumber, AssignedNumberResponseDTO>(assignedNumber);
    }

    public async Task<ICollection<AssignedNumberResponseDTO>> GetAssignedNumbersByClientAsync(Guid clientId)
    {
        var assignedNumbers = await repository.FindAsync(an => an.ClientId == clientId);
        return mapper.Map<AssignedNumberResponseDTO>(assignedNumbers);
    }

    public async Task<ICollection<AssignedNumberResponseDTO>> GetAssignedNumbersByRaffleAsync(Guid raffleId)
    {
        var assignedNumbers = await repository.FindAsync(an => an.RaffleId == raffleId);
        return mapper.Map<AssignedNumberResponseDTO>(assignedNumbers);
    }
}
