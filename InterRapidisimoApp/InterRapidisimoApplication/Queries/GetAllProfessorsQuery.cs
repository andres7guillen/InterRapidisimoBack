using AutoMapper;
using CSharpFunctionalExtensions;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Repositories;
using MediatR;

namespace InterRapidisimoApplication.Queries
{
    public class GetAllProfessorsQuery : IRequest<Result<List<ProfessorDto>>>
    {
        public sealed class GetAllProfessorsQueryHandler : IRequestHandler<GetAllProfessorsQuery, Result<List<ProfessorDto>>>
        {
            private readonly IProfessorRepository _repository;
            private readonly IMapper _mapper;

            public GetAllProfessorsQueryHandler(IProfessorRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Result<List<ProfessorDto>>> Handle(GetAllProfessorsQuery request, CancellationToken cancellationToken)
            {
                var professors = await _repository.GetAll();
                var result = _mapper.Map<List<ProfessorDto>>(professors.Value);
                return Result.Success(result);
            }
        }
    }
}
