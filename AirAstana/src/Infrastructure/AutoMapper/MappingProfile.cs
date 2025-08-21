using Application.Models.DTO;
using AutoMapper;
using Domain.Models;

namespace Infrastructure.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateFlightDto, Flight>();
        CreateMap<Flight, CreateFlightDto>();
    }
}