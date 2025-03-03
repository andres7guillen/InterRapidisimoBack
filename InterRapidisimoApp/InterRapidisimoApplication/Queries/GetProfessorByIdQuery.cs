using AutoMapper;
using CSharpFunctionalExtensions;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Repositories;
using MediatR;

namespace InterRapidisimoApplication.Queries;

public class GetProfessorByIdQuery : IRequest<Result<ProfessorDto>>
{
    public Guid ProfessorId { get; }

    public GetProfessorByIdQuery(Guid professorId)
    {
        ProfessorId = professorId;
    }

    public class GetProfessorByIdQueryHandler : IRequestHandler<GetProfessorByIdQuery, Result<ProfessorDto>>
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly IMapper _mapper;

        public GetProfessorByIdQueryHandler(IProfessorRepository professorRepository, IMapper mapper)
        {
            _professorRepository = professorRepository;
            _mapper = mapper;
        }

        public async Task<Result<ProfessorDto>> Handle(GetProfessorByIdQuery request, CancellationToken cancellationToken)
        {
            var professor = await _professorRepository.GetById(request.ProfessorId);
            if (professor.HasNoValue)
                return Result.Failure<ProfessorDto>("Professor not found.");

            return Result.Success(_mapper.Map<ProfessorDto>(professor.Value));
        }
    }

}
