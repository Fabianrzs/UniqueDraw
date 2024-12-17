using UniqueDraw.Domain.Entities.UniqueDraw;
using UniqueDraw.Domain.Models.Request;
using UniqueDraw.Domain.Models.Response;
using AutoMapper;

namespace UniqueDraw.Infrastructure.Profiles;

public class AssignedNumberProfile : Profile
{
    public AssignedNumberProfile()
    {
        CreateMap<AssignedNumberRequestDTO, AssignedNumber>();
        CreateMap<AssignedNumber, AssignedNumberResponseDTO>();
    }
}
