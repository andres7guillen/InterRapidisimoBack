using AutoMapper;
using CSharpFunctionalExtensions;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterRapidisimoApplication.Queries;

public class GetProfessorsBySubjectQuery : IRequest<Result<List<ProfessorDto>>>
{
    public Guid SubjectId { get; }
    public GetProfessorsBySubjectQuery(Guid subjectId)
    {
        SubjectId = subjectId;
    }

    public class GetProfessorsBySubjectHandler : IRequestHandler<GetProfessorsBySubjectQuery, Result<List<ProfessorDto>>>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public GetProfessorsBySubjectHandler(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<ProfessorDto>>> Handle(GetProfessorsBySubjectQuery request, CancellationToken cancellationToken)
        {
            var subjectResult = await _subjectRepository.GetById(request.SubjectId);
            if (subjectResult.HasNoValue)
                return Result.Failure<List<ProfessorDto>>("Subject not found");

            var subject = subjectResult.Value;
            var professors = subject.ProfessorSubjects.Select(ps => ps.Professor).ToList();

            // AutoMapper convierte List<Professor> a List<ProfessorDto>
            var professorDtos = _mapper.Map<List<ProfessorDto>>(professors);

            return Result.Success(professorDtos);
        }
    }


}
