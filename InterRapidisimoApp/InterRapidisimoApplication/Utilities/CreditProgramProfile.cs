using AutoMapper;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Entities;

namespace InterRapidisimoApplication.Utilities;

public class CreditProgramProfile : Profile
{
    public CreditProgramProfile()
    {
        CreateMap<CreditProgram, CreditProgramDto>();
    }
}
