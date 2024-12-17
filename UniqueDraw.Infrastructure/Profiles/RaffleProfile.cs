using AutoMapper;
using UniqueDraw.Domain.Entities.UniqueDraw;
using UniqueDraw.Domain.Models.Create;
using UniqueDraw.Domain.Models.Response;

namespace UniqueDraw.Infrastructure.Profiles;

public class RaffleProfile : Profile
{
    public RaffleProfile()
    {
        CreateMap<RaffleCreateDTO, Raffle>();
        CreateMap<Raffle, RaffleResponseDTO>();
    }
}
