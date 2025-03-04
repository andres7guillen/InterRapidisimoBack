using AutoMapper;
using CSharpFunctionalExtensions;
using InterRapidisimoDomain.DTOs;
using InterRapidisimoDomain.Repositories;
using MediatR;

namespace InterRapidisimoApplication.Queries
{
    public class GetClassmatesQuery : IRequest<Result<List<ClassmateDto>>>
    {
        public Guid StudentId { get; }
        public GetClassmatesQuery(Guid studentId)
        {
            StudentId = studentId;
        }

        public class GetClassmatesQueryHandler : IRequestHandler<GetClassmatesQuery, Result<List<ClassmateDto>>>
        {
            private readonly IStudentRepository _studentRepository;
            private readonly IMapper _mapper;

            public GetClassmatesQueryHandler(IStudentRepository studentRepository, IMapper mapper)
            {
                _studentRepository = studentRepository;
                _mapper = mapper;
            }

            public async Task<Result<List<ClassmateDto>>> Handle(GetClassmatesQuery request, CancellationToken cancellationToken)
            {
                var student = await _studentRepository.GetByIdWithSubjectsAsync(request.StudentId);
                if (student.HasNoValue)
                    return Result.Failure<List<ClassmateDto>>("Student noy found.");

                var subjectIds = student.Value.StudentSubjects.Select(ss => ss.SubjectId).ToList();

                var classmates = await _studentRepository.GetClassmatesAsync(subjectIds, request.StudentId);
                if (classmates.IsFailure)
                    return Result.Failure<List<ClassmateDto>>(classmates.Error);
                var classmatesDto = _mapper.Map<List<ClassmateDto>>(classmates);

                return Result.Success(classmatesDto);
            }
        }

    }
}
