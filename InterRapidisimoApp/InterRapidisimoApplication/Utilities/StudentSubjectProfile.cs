using AutoMapper;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Entities;

namespace InterRapidisimoApplication.Utilities;

public class StudentSubjectProfile : Profile
{
    public StudentSubjectProfile()
    {
        CreateMap<StudentSubject, StudentSubjectDto>();
    }
}
